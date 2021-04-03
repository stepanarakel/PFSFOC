using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PFSFOC.Controller;

namespace PFSFOC
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            AVDB = new AdminViewDB(ConnectionString.ConnectoinSTR);
            AVDB.LoadTables();

            InitializeComponent();

            comboBoxFocType.DataSource = AVDB.FocTypeTable;
            comboBoxFocType.DisplayMember = "type_name";
            comboBoxFocType.ValueMember = "ID";

            comboBoxFocStandard.DataSource = AVDB.StandartTable;
            comboBoxFocStandard.DisplayMember = "standard_name";
            comboBoxFocStandard.ValueMember = "ID";

            comboBoxManufacturer.DataSource = AVDB.ManufacturerTable;
            comboBoxManufacturer.DisplayMember = "manufacturer_name";
            comboBoxManufacturer.ValueMember = "ID";

            comboBoxLIMin.DataSource = AVDB.TLITable;
            comboBoxLIMin.DisplayMember = "min_temperature";
            comboBoxLIMin.ValueMember = "ID";

            comboBoxLIMax.DataSource = AVDB.TLITable;
            comboBoxLIMax.DisplayMember = "max_temperature";
            comboBoxLIMax.ValueMember = "ID";

            comboBoxOMin.DataSource = AVDB.OTTable;
            comboBoxOMin.DisplayMember = "min_temperature";
            comboBoxOMin.ValueMember = "ID";

            comboBoxOMax.DataSource = AVDB.OTTable;
            comboBoxOMax.DisplayMember = "max_temperature";
            comboBoxOMax.ValueMember = "ID";

            tablesList.Columns.Add("tablesText");
            tablesList.Columns.Add("tableValue");
            for (int i = 0; i < 6; i++)
            {
                object[] tmp = new object[] { tablesText[i], tablesValue[i] };
                tablesList.Rows.Add(tmp);
            }

            comboBoxTables.DataSource = tablesList;
            comboBoxTables.DisplayMember = "tablesText";
            comboBoxTables.ValueMember = "tableValue";

            comboBoxEnv.DataSource = AVDB.EnvironmentTable;
            comboBoxEnv.DisplayMember = "environment_name";
            comboBoxEnv.ValueMember = "ID";
        }



        #region ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
        /// <summary>
        /// Для работы администратора
        /// </summary>
        AdminViewDB AVDB;
        /// <summary>
        /// Регулярное выражения для правильного считывания чисел с плавающей точкой
        /// </summary>
        Regex dotNumber = new Regex(@"^\d*([.,]\d+)?$");
        /// <summary>
        /// Регулярное выражения для правильного считывания чисел
        /// </summary>
        Regex number = new Regex(@"(^\d+$)||(^-\d+$)");
        /// <summary>
        /// Идентификатор ОК
        /// </summary>
        int ID = -1;
        /// <summary>
        /// Табица с названия ми таблиц БД
        /// </summary>
        DataTable tablesList = new DataTable();
        /// <summary>
        /// Массив названий таблиц
        /// </summary>
        object[] tablesText = new object[] { "Среда", "Тип ОК", "Производитель", "Стандарт", "Монтаж и прокладка", "Эксплуатация" };
        /// <summary>
        /// Массив идентификаторов таблиц
        /// </summary>
        object[] tablesValue = new object[] { 1, 2, 3, 4, 5, 6 };
        /// <summary>
        /// Фалг отображения данных в таблице (true - столбцы одинаковые, false - столбцы = длинне данных)
        /// </summary>
        bool dataFocFlag = false;
        #endregion

        #region ADMINS
        /// <summary>
        /// Добавление нового админимтатора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewAdmin_Click(object sender, EventArgs e)
        {
            if (textBoxNewLogin.Text == "" || textBoxNewPassword.Text == "")
            {
                MessageBox.Show(
                        "Не указан ЛОГИН или ПАРОЛЬ\n" +
                        "Придумайте ЛОГИН и ПАРОЛЬ.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
            {
                if (!AVDB.CheckAdmin(textBoxNewLogin.Text, textBoxNewPassword.Text))
                {
                    AVDB.AddAdmin(textBoxNewLogin.Text, textBoxNewPassword.Text);
                    if (AVDB.CheckAdmin(textBoxNewLogin.Text, textBoxNewPassword.Text))
                    {
                        MessageBox.Show(
                            "Запись о новом администраторе успешно создана.",
                            "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        textBoxNewLogin.Clear();
                        textBoxNewPassword.Clear();
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Запись с указанным ЛОГИНОМ и ПАРОЛЕМ уже есть в базе!\n" +
                        "Придумайте другой ЛОГИН и ПАРОЛЬ.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Загрузка всех администраторов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadAdmins_Click(object sender, EventArgs e)
        {
            AVDB.LoadAdmins();
            dataGridViewAdmin.DataSource = new object();
            dataGridViewAdmin.DataSource = AVDB.AdminTable;
            dataGridViewAdmin.Columns[0].ReadOnly = true;
        }
        /// <summary>
        /// Изменение записи администратора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditAdmin_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdmin.SelectedRows.Count > 0 && dataGridViewAdmin.SelectedRows.Count == 1)
            {
                int ID = int.Parse(dataGridViewAdmin.Rows[dataGridViewAdmin.CurrentRow.Index].Cells["ID"].Value.ToString());
                string Login = dataGridViewAdmin.Rows[dataGridViewAdmin.CurrentRow.Index].Cells["admin_name"].Value.ToString();
                string Password = dataGridViewAdmin.Rows[dataGridViewAdmin.CurrentRow.Index].Cells["admin_password"].Value.ToString();
                AVDB.UpDateAdmin(ID, Login, Password);
                if (AVDB.CheckAdmin(Login, Password))
                {
                    MessageBox.Show(
                        "Запись об администраторе успешно изменена.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Запись о новом администраторе не была изменена.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                AVDB.LoadAdmins();
                dataGridViewAdmin.DataSource = new object();
                dataGridViewAdmin.DataSource = AVDB.AdminTable;
                dataGridViewAdmin.Columns[0].ReadOnly = true;
            }
        }
        /// <summary>
        /// Удаление администратора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteAdmin_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdmin.Rows.Count > 2)
            {
                if (dataGridViewAdmin.SelectedRows.Count > 0 && dataGridViewAdmin.SelectedRows.Count == 1)
                {
                    int ID = int.Parse(dataGridViewAdmin.Rows[dataGridViewAdmin.CurrentRow.Index].Cells["ID"].Value.ToString());
                    AVDB.DeleteAdmin(ID);
                    if (!AVDB.CheckAdmin(ID))
                    {
                        MessageBox.Show(
                            "Запись об администраторе успешно удалена.",
                            "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Запись о новом администраторе не была удалена.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    AVDB.LoadAdmins();
                    dataGridViewAdmin.DataSource = new object();
                    dataGridViewAdmin.DataSource = AVDB.AdminTable;
                    dataGridViewAdmin.Columns[0].ReadOnly = true;
                }
            }
            else
            {
                MessageBox.Show(
                    "Должен быть минимум 1 администратор!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Инструкция по редактированию администраторов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdminInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Для изменения записи об администраторе, измените поле 'admin_name' и/или 'admin_password', выбирете, отредактированную строку и нажмите 'Изменить'.\n\n" +
                "Для удаления записи об администраторе, выбирете строку и нажмите 'Удалить'.",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        #region FOC
        /// <summary>
        /// Изменение отображения данных в таблице ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (!dataFocFlag)
            {
                button5.Text = "Отобразить данные полностью";
                dataFocFlag = true;
                dataGridViewFOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                button5.Text = "Одинаковые столбцы";
                dataFocFlag = false;
                dataGridViewFOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        /// <summary>
        /// Загрузка всех кабелей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadFoc_Click(object sender, EventArgs e)
        {
            AVDB.LoadFoc();
            dataGridViewFOC.DataSource = new object();
            dataGridViewFOC.DataSource = AVDB.FOCTable;
            dataGridViewFOC.Columns[0].ReadOnly = true;
            buttonEditFoc.Enabled = true;
        }
        /// <summary>
        /// Добавление ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFoc_Click(object sender, EventArgs e)
        {
            groupBoxFoc.Enabled = true;
            buttonAdd.Enabled = true;
            buttonEdit.Enabled = false;
            dataGridViewFOC.Enabled = false;
        }
        /// <summary>
        /// Редактирование ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditFoc_Click(object sender, EventArgs e)
        {
            groupBoxFoc.Enabled = true;
            buttonEdit.Enabled = true;
            buttonAdd.Enabled = false;
            dataGridViewFOC.Enabled = true;
        }

        /// <summary>
        /// Инструкция по работе с кабелями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFocInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Для добавления записи об ОК, нажмите 'Добавить' и заполните все необходимые поля.\n\n" +
                "Для изменения записи об ОК, выбирете строку и нажмите 'Редактировать' и измените нужные параметры.\n\n" +
                "Кнопка 'Обновить данные' подгрузит актуальные данные для создания или изменения записи об ОК.",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        /// <summary>
        /// Обновление дополнительных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AVDB.LoadTables();
            comboBoxFocType.DataSource = AVDB.FocTypeTable;
            comboBoxFocType.DisplayMember = "type_name";
            comboBoxFocType.ValueMember = "ID";

            comboBoxFocStandard.DataSource = AVDB.StandartTable;
            comboBoxFocStandard.DisplayMember = "standard_name";
            comboBoxFocStandard.ValueMember = "ID";

            comboBoxManufacturer.DataSource = AVDB.ManufacturerTable;
            comboBoxManufacturer.DisplayMember = "manufacturer_name";
            comboBoxManufacturer.ValueMember = "ID";

            comboBoxLIMin.DataSource = AVDB.TLITable;
            comboBoxLIMin.DisplayMember = "min_temperature";
            comboBoxLIMin.ValueMember = "ID";

            comboBoxLIMax.DataSource = AVDB.TLITable;
            comboBoxLIMax.DisplayMember = "max_temperature";
            comboBoxLIMax.ValueMember = "ID";

            comboBoxOMin.DataSource = AVDB.OTTable;
            comboBoxOMin.DisplayMember = "min_temperature";
            comboBoxOMin.ValueMember = "ID";

            comboBoxOMax.DataSource = AVDB.OTTable;
            comboBoxOMax.DisplayMember = "max_temperature";
            comboBoxOMax.ValueMember = "ID";
        }
        /// <summary>
        /// Добавление ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxDRY.Text != "" &&
                textBoxFiberCount.Text != "" &&
                textBoxFocDiameter.Text != "" &&
                textBoxFocMark.Text != "" &&
                textBoxFocMinRadius.Text != "" &&
                textBoxFocOuterShellMaterial.Text != "" &&
                textBoxFocPE.Text != "" &&
                textBoxFocPrice.Text != "" &&
                textBoxFocPurpose.Text != "" &&
                textBoxFocLife.Text != "")
            {
                if (dotNumber.IsMatch(textBoxFocDiameter.Text) && dotNumber.IsMatch(textBoxFocPrice.Text) && dotNumber.IsMatch(textBoxDRY.Text))
                {
                    int idTemperatureLI = -1, idTemperatureO = -1;
                    if ((int)comboBoxLIMin.SelectedValue == (int)comboBoxLIMax.SelectedValue)
                        idTemperatureLI = (int)comboBoxLIMin.SelectedValue;

                    if ((int)comboBoxOMin.SelectedValue == (int)comboBoxOMax.SelectedValue)
                        idTemperatureO = (int)comboBoxOMin.SelectedValue;

                    if (idTemperatureLI != -1 && idTemperatureO != -1)
                    {
                        AVDB.CheckFOC((int)comboBoxFocType.SelectedValue,
                        textBoxFocMark.Text,
                        (int)comboBoxManufacturer.SelectedValue,
                        textBoxFocPurpose.Text,
                        (int)comboBoxFocStandard.SelectedValue,
                        double.Parse(textBoxFocDiameter.Text),
                        textBoxFocOuterShellMaterial.Text,
                        textBoxFocProtection.Text,
                        checkBox1.Checked,
                        checkBox2.Checked,
                        checkBox3.Checked,
                        int.Parse(textBoxFiberCount.Text),
                        textBoxFocPE.Text,
                        idTemperatureLI,
                        idTemperatureO,
                        double.Parse(textBoxDRY.Text),
                        int.Parse(textBoxFocMinRadius.Text),
                        double.Parse(textBoxFocPrice.Text),
                            int.Parse(textBoxFocLife.Text));
                        if (AVDB.FOCTable.Rows.Count > 0)
                        {
                            MessageBox.Show(
                                "Кабель с указанными параметрами уже существует!",
                                "Попытка добавить существующий кабель",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        else
                        {
                            AVDB.AddFOC((int)comboBoxFocType.SelectedValue,
                            textBoxFocMark.Text,
                            (int)comboBoxManufacturer.SelectedValue,
                            textBoxFocPurpose.Text,
                            (int)comboBoxFocStandard.SelectedValue,
                            double.Parse(textBoxFocDiameter.Text),
                            textBoxFocOuterShellMaterial.Text,
                            textBoxFocProtection.Text,
                            checkBox1.Checked,
                            checkBox2.Checked,
                            checkBox3.Checked,
                            int.Parse(textBoxFiberCount.Text),
                            textBoxFocPE.Text,
                            idTemperatureLI,
                            idTemperatureO,
                            double.Parse(textBoxDRY.Text),
                            int.Parse(textBoxFocMinRadius.Text),
                            double.Parse(textBoxFocPrice.Text),
                            int.Parse(textBoxFocLife.Text));
                            MessageBox.Show(
                                "Новый кабель успешно задан!",
                                "Добавление кабеля",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                buttonLoadFoc_Click(sender, e);
                        }                       
                    }
                    else
                    {
                        if (idTemperatureLI != -1)
                        {
                            MessageBox.Show(
                                "К сожалению не подходящая запись о температуре монтажа и прокладки не нашлось." +
                                "Пожалуйста добаьте новую запись в соответсвующую таблицу в разделе 'Таблицы'",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        if (idTemperatureO != -1)
                        {
                            MessageBox.Show(
                                "К сожалению не подходящая запись о температуре эксплуатации не нашлось." +
                                "Пожалуйста добаьте новую запись в соответсвующую таблицу в разделе 'Таблицы'",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "В поле Диаметр, ДРУ или Цена указанны не корректные данные!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Все поля обязательны для заполнения!\n\n" +
                    "Пожалуйста заполните все поля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Выбор ОК для редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewFOC_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewFOC.Rows.Count > 1)
            {
                dataGridViewFOC.Enabled = false;

                ID = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID"].Value.ToString());

                comboBoxFocType.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_foc_type"].Value.ToString());
                comboBoxFocStandard.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_standard"].Value.ToString());
                comboBoxManufacturer.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_manufacturer"].Value.ToString());
                comboBoxLIMin.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_T_of_the_L_and_I"].Value.ToString());
                comboBoxLIMax.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_T_of_the_L_and_I"].Value.ToString());
                comboBoxOMin.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_O_T"].Value.ToString());
                comboBoxOMax.SelectedValue = int.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["ID_O_T"].Value.ToString());

                textBoxDRY.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_permissible_tensile_force"].Value.ToString();
                textBoxFiberCount.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["fibers_count"].Value.ToString();
                textBoxFocDiameter.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_diameter"].Value.ToString();
                textBoxFocMark.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_mark"].Value.ToString();
                textBoxFocMinRadius.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_min_bending_radius"].Value.ToString();
                textBoxFocOuterShellMaterial.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_outer_shell_material"].Value.ToString();
                textBoxFocPE.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_power_element"].Value.ToString();
                textBoxFocPrice.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_price/meter"].Value.ToString();
                textBoxFocProtection.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_protection"].Value.ToString();
                textBoxFocPurpose.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_purpose"].Value.ToString();
                textBoxFocLife.Text = dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_life"].Value.ToString();

                checkBox1.Checked = bool.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_protection_from_water"].Value.ToString());
                checkBox2.Checked = bool.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_protection_from_cold"].Value.ToString());
                checkBox3.Checked = bool.Parse(dataGridViewFOC.Rows[dataGridViewFOC.CurrentRow.Index].Cells["foc_UV_protection"].Value.ToString());

                dataGridViewFOC.Enabled = true;
            }
        }
        /// <summary>
        /// Изменение ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (textBoxDRY.Text != "" &&
                textBoxFiberCount.Text != "" &&
                textBoxFocDiameter.Text != "" &&
                textBoxFocMark.Text != "" &&
                textBoxFocMinRadius.Text != "" &&
                textBoxFocOuterShellMaterial.Text != "" &&
                textBoxFocPE.Text != "" &&
                textBoxFocPrice.Text != "" &&
                textBoxFocPurpose.Text != "" &&
                textBoxFocLife.Text != "")
            {
                if (dotNumber.IsMatch(textBoxFocDiameter.Text) && dotNumber.IsMatch(textBoxFocPrice.Text) && dotNumber.IsMatch(textBoxDRY.Text))
                {
                    int idTemperatureLI = -1, idTemperatureO = -1;
                    if ((int)comboBoxLIMin.SelectedValue == (int)comboBoxLIMax.SelectedValue)
                        idTemperatureLI = (int)comboBoxLIMin.SelectedValue;

                    if ((int)comboBoxOMin.SelectedValue == (int)comboBoxOMax.SelectedValue)
                        idTemperatureO = (int)comboBoxOMin.SelectedValue;

                    if (idTemperatureLI != -1 && idTemperatureO != -1)
                    {
                        AVDB.CheckFOC((int)comboBoxFocType.SelectedValue,
                        textBoxFocMark.Text,
                        (int)comboBoxManufacturer.SelectedValue,
                        textBoxFocPurpose.Text,
                        (int)comboBoxFocStandard.SelectedValue,
                        double.Parse(textBoxFocDiameter.Text),
                        textBoxFocOuterShellMaterial.Text,
                        textBoxFocProtection.Text,
                        checkBox1.Checked,
                        checkBox2.Checked,
                        checkBox3.Checked,
                        int.Parse(textBoxFiberCount.Text),
                        textBoxFocPE.Text,
                        idTemperatureLI,
                        idTemperatureO,
                        double.Parse(textBoxDRY.Text),
                        int.Parse(textBoxFocMinRadius.Text),
                        double.Parse(textBoxFocPrice.Text),
                            int.Parse(textBoxFocLife.Text));
                        if (AVDB.FOCTable.Rows.Count > 0)
                        {
                            MessageBox.Show(
                                "Параметры кабеля не были изменены!",
                                "Попытка обновить кабель",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        else
                        {
                            AVDB.EditFOC(ID,
                            (int)comboBoxFocType.SelectedValue,
                            textBoxFocMark.Text,
                            (int)comboBoxManufacturer.SelectedValue,
                            textBoxFocPurpose.Text,
                            (int)comboBoxFocStandard.SelectedValue,
                            double.Parse(textBoxFocDiameter.Text),
                            textBoxFocOuterShellMaterial.Text,
                            textBoxFocProtection.Text,
                            checkBox1.Checked,
                            checkBox2.Checked,
                            checkBox3.Checked,
                            int.Parse(textBoxFiberCount.Text),
                            textBoxFocPE.Text,
                            idTemperatureLI,
                            idTemperatureO,
                            double.Parse(textBoxDRY.Text),
                            int.Parse(textBoxFocMinRadius.Text),
                            double.Parse(textBoxFocPrice.Text),
                            int.Parse(textBoxFocLife.Text));

                            AVDB.LoadFoc();
                            dataGridViewFOC.DataSource = new object();
                            dataGridViewFOC.DataSource = AVDB.FOCTable;
                            dataGridViewFOC.Columns[0].ReadOnly = true;

                            MessageBox.Show(
                                    "Данные кабеля успешно обновлены!",
                                    "Обновление кабеля",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            buttonLoadFoc_Click(sender, e);
                        }                        
                    }
                    else
                    {
                        if (idTemperatureLI != -1)
                        {
                            MessageBox.Show(
                                "К сожалению не подходящая запись о температуре монтажа и прокладки не нашлось." +
                                "Пожалуйста добаьте новую запись в соответсвующую таблицу в разделе 'Таблицы'",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        if (idTemperatureO != -1)
                        {
                            MessageBox.Show(
                                "К сожалению не подходящая запись о температуре эксплуатации не нашлось." +
                                "Пожалуйста добаьте новую запись в соответсвующую таблицу в разделе 'Таблицы'",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "В поле Диаметр, ДРУ или Цена указанны не корректные данные!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Все поля обязательны для заполнения!\n\n" +
                    "Пожалуйста заполните все поля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void textBoxFocDiameter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44/*46*/) // цифры и клавиша BackSpace и ','
                e.Handled = true;
        }


        private void textBoxFocMinRadius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) // цифры и клавиша BackSpace
                e.Handled = true;
        }
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Если вы не нашли подходящие параметры в полях: " +
                "Тип ОК, Производитель, Стандарт, t Монтажа и Прокладки, t Эксплуатации, " +
                "то вам следует добавить интересующие вас данные в соответсвующие таблицы в разделе 'Таблицы'.",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region ТАБЛИЦЫ
        /// <summary>
        /// Загрузка таблиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadTable_Click(object sender, EventArgs e)
        {
            AVDB.LoadTables();
            dataGridViewTable.DataSource = new object();
            switch (int.Parse(comboBoxTables.SelectedValue.ToString()))
            {
                case 1:
                    dataGridViewTable.DataSource = AVDB.EnvironmentTable;
                    break;
                case 2:
                    dataGridViewTable.DataSource = AVDB.FocTypeTable;
                    break;
                case 3:
                    dataGridViewTable.DataSource = AVDB.ManufacturerTable;
                    break;
                case 4:
                    dataGridViewTable.DataSource = AVDB.StandartTable;
                    break;
                case 5:
                    dataGridViewTable.DataSource = AVDB.TLITable;
                    break;
                case 6:
                    dataGridViewTable.DataSource = AVDB.OTTable;
                    break;
            }
            dataGridViewTable.Columns[0].ReadOnly = true;
            dataGridViewTable.Refresh();
        }
        /// <summary>
        /// Изменение данных в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditData_Click(object sender, EventArgs e)
        {
            if (dataGridViewTable.Rows.Count > 2)
            {
                if (dataGridViewTable.SelectedRows.Count == 1)
                {
                    switch (int.Parse(comboBoxTables.SelectedValue.ToString()))
                    {
                        case 1:
                            //30
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" && dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["environment_name"].Value.ToString().Length <= 30)
                            {
                                AVDB.UpdateEnvironment(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()), dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["environment_name"].Value.ToString());
                                MessageBox.Show(
                                        "Данные успешно изменены.",
                                        "Сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Длинна названия среды не должна превишать 30 символов.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                        case 2:
                            //30
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" && dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["type_name"].Value.ToString().Length <= 30)
                            {
                                AVDB.UpdateFocType(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()), dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["type_name"].Value.ToString());
                                MessageBox.Show(
                                        "Данные успешно изменены.",
                                        "Сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Длинна названия типа ОК не должна превишать 30 символов.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                        case 3:
                            //50
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" && dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["manufacturer_name"].Value.ToString().Length <= 50)
                            {
                                AVDB.UpdateManufacturer(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()), dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["manufacturer_name"].Value.ToString());
                                MessageBox.Show(
                                        "Данные успешно изменены.",
                                        "Сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Длинна названия производителя не должна превишать 50 символов.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                        case 4:
                            //50
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" && dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["standard_name"].Value.ToString().Length <= 50)
                            {
                                AVDB.UpdateStandard(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()), dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["standard_name"].Value.ToString());
                                MessageBox.Show(
                                        "Данные успешно изменены.",
                                        "Сообщение",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Длинна названия производителя не должна превишать 50 символов.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                        case 5:
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" &&
                                number.IsMatch(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()) &&
                                number.IsMatch(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()))
                            {
                                AVDB.UpdateLI(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()),
                                    int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()),
                                    int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()));
                                MessageBox.Show(
                                    "Данные успешно изменены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Температура указывается числами.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                        case 6:
                            if (dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString() != "" &&
                                number.IsMatch(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()) &&
                                number.IsMatch(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()))
                            {
                                AVDB.UpdateO(int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["ID"].Value.ToString()),
                                    int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()),
                                    int.Parse(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells["min_temperature"].Value.ToString()));
                                MessageBox.Show(
                                    "Данные успешно изменены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Температура указывается числами.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(
                         "Нужно выбрать только 1 записть.",
                         "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Извините, нет записей.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private void buttonTablesInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Чтобы изменить данные в таблице выбирите нужную таблицу, нажмине 'Загрузить таблицу', измените запись, выбирите изменённую строку и нажмите 'Изменить данные'.\n\n" +
                "Чтобы добавить запись в таблицу выбирите нужную таблицу, заполните параметры и нажмите 'Добавить'." +
                "Чтобы получить добавленные или изменённые данные нажмите кнопку \"Загрузить таблицу\".",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void comboBoxTables_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                switch (int.Parse(comboBoxTables.SelectedValue.ToString()))
                {
                    case 1:
                        textBoxParam2.Enabled = false;
                        break;
                    case 2:
                        textBoxParam2.Enabled = false;
                        break;
                    case 3:
                        textBoxParam2.Enabled = false;
                        break;
                    case 4:
                        textBoxParam2.Enabled = false;
                        break;
                    case 5:
                        textBoxParam2.Enabled = true;
                        break;
                    case 6:
                        textBoxParam2.Enabled = true;
                        break;
                }
            }
        }
        /// <summary>
        /// Добавление данных в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd1_Click(object sender, EventArgs e)
        {
            switch (int.Parse(comboBoxTables.SelectedValue.ToString()))
            {
                case 1:
                    //30
                    if (textBoxParam1.Text.Length > 0 && textBoxParam1.Text.Length <= 30)
                    {
                        AVDB.CheckEnvironment(textBoxParam1.Text);
                        if (AVDB.CheckTable.Rows.Count == 0)
                        {
                            AVDB.AddEnvironment(textBoxParam1.Text);
                            MessageBox.Show(
                                    "Данные успешно добавлены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанная сдера уже существует!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }                       
                    }
                    else
                    {
                        MessageBox.Show(
                            "Длинна названия среды не должна превишать 30 символов.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    //30
                    if (textBoxParam1.Text.Length > 0 && textBoxParam1.Text.Length <= 30)
                    {
                        AVDB.CheckFocType(textBoxParam1.Text);
                        if (AVDB.CheckTable.Rows.Count == 0)
                        {
                            AVDB.AddFocType(textBoxParam1.Text);
                            MessageBox.Show(
                                    "Данные успешно добавлены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанный тип ОК уже существует!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }                        
                    }
                    else
                    {
                        MessageBox.Show(
                            "Длинна названия типа ОК не должна превишать 30 символов.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
                case 3:
                    //50
                    if (textBoxParam1.Text.Length > 0 && textBoxParam1.Text.Length <= 50)
                    {
                        AVDB.CheckManufacturer(textBoxParam1.Text);
                        if (AVDB.CheckTable.Rows.Count == 0)
                        {
                            AVDB.AddManufacturer(textBoxParam1.Text);
                            MessageBox.Show(
                                    "Данные успешно добавлены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанный производитель уже существует!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }                        
                    }
                    else
                    {
                        MessageBox.Show(
                            "Длинна названия производителя не должна превишать 50 символов.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
                case 4:
                    //50
                    if (textBoxParam1.Text.Length > 0 && textBoxParam1.Text.Length <= 50)
                    {
                        AVDB.CheckStandard(textBoxParam1.Text);
                        if (AVDB.CheckTable.Rows.Count == 0)
                        {
                            AVDB.AddStandard(textBoxParam1.Text);
                            MessageBox.Show(
                                    "Данные успешно добавлены.",
                                    "Сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанный стандарт уже существует",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show(
                            "Длинна названия стандарта не должна превишать 50 символов.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
                case 5:
                    if (textBoxParam1.Text.Length > 0 && textBoxParam2.Text.Length > 0 &&
                        number.IsMatch(textBoxParam1.Text) &&
                        number.IsMatch(textBoxParam2.Text))
                    {
                        AVDB.CheckLI(int.Parse(textBoxParam1.Text), int.Parse(textBoxParam2.Text));
                        if (AVDB.TLITable.Rows.Count == 0)
                        {
                            AVDB.AddLI(int.Parse(textBoxParam1.Text), int.Parse(textBoxParam2.Text));
                            MessageBox.Show(
                                "Данные успешно добавлены.",
                                "Сообщение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанная температура уже существует!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Температура указывается числами.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
                case 6:
                    if (textBoxParam1.Text.Length > 0 && textBoxParam2.Text.Length > 0 &&
                        number.IsMatch(textBoxParam1.Text) &&
                        number.IsMatch(textBoxParam2.Text))
                    {
                        AVDB.CheckO(int.Parse(textBoxParam1.Text), int.Parse(textBoxParam2.Text));
                        if (AVDB.OTTable.Rows.Count == 0)
                        {
                            AVDB.AddO(int.Parse(textBoxParam1.Text), int.Parse(textBoxParam2.Text));
                            MessageBox.Show(
                                "Данные успешно добавлены.",
                                "Сообщение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                            "Указанная температура уже существует!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }                        
                    }
                    else
                    {
                        MessageBox.Show(
                            "Температура указывается числами.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    break;
            }
        }
        #endregion

        #region Среда и ОК      
        /// <summary>
        /// Загрузка ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadFoc1_Click(object sender, EventArgs e)
        {
            AVDB.LoadShortFoc();
            dataGridViewFoc1.DataSource = AVDB.ShortFOCTable;
        }
        /// <summary>
        /// Загрузка связей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AVDB.LoadFocforEntity();
            dataGridViewFocEnv.DataSource = AVDB.FocInEnvTable;
        }
        /// <summary>
        /// Создание связи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridViewFoc1.Rows.Count > 1)
            {
                if (dataGridViewFoc1.SelectedRows.Count == 1)
                {
                    AVDB.AddEntity(int.Parse(comboBoxEnv.SelectedValue.ToString()), int.Parse(dataGridViewFoc1.Rows[dataGridViewFoc1.CurrentRow.Index].Cells["ID"].Value.ToString()));
                    MessageBox.Show(
                        "Связь успешно создана.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Выбирете только одну запись!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Удаление связи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteEntity_Click(object sender, EventArgs e)
        {
            if (dataGridViewFocEnv.Rows.Count > 1)
            {
                if (dataGridViewFocEnv.SelectedRows.Count == 1)
                {
                    AVDB.DeleteEntity(int.Parse(dataGridViewFocEnv.Rows[dataGridViewFocEnv.CurrentRow.Index].Cells["ID"].Value.ToString()));
                    MessageBox.Show(
                        "Связь успешно удалена.",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Выбирете только одну запись!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Чтобы создать связь выбирите ОК, в левой таблице, и нажмите 'Добавить связь'.\n\n" +
                "Чтобы удалить связь выбирите связь, в правой таблице, и нажмите 'Удалить связь'.",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion
    }
}