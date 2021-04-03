using System;
using System.Data;
using System.Windows.Forms;
using PFSFOC.Controller;

namespace PFSFOC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            OVDB = new OperatorViewDB(ConnectionString.ConnectoinSTR);
            OVDB.SearchUpDate();

            InitializeComponent();

            comboBox_foctype.DataSource = OVDB.FocTypeTable;
            comboBox_foctype.DisplayMember = "type_name";

            comboBox_focradius.DataSource = OVDB.MinFocRadiusTable;
            comboBox_focradius.DisplayMember = "foc_min_bending_radius";

            comboBox_fcount.DataSource = OVDB.FiberCountTable;
            comboBox_fcount.DisplayMember = "fibers_count";

            comboBox_envtype.DataSource = OVDB.EnvironmentTable;
            comboBox_envtype.DisplayMember = "environment_name";

            comboBox_foc_protection.DataSource = OVDB.FocProtection;
            comboBox_foc_protection.DisplayMember = "foc_protection";
        }

        #region ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
        /// <summary>
        /// Представление БД для оператора
        /// </summary>
        OperatorViewDB OVDB;
        #endregion

        /// <summary>
        /// Поиск ОК по парамметрам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_search_Click(object sender, EventArgs e)
        {
            // Проверки
            if (textBox_striplenght.Text != "" && textBox_foclenght.Text != "")
            {
                if (comboBox_envtype.Text == "Грунт" || comboBox_envtype.Text == "Коллектор")
                {
                    if ((float.Parse(textBox_striplenght.Text) + float.Parse(textBox_striplenght.Text) / 100 * 2) > float.Parse(textBox_foclenght.Text))
                    {
                        DialogResult result = MessageBox.Show(
                            "Извините, для полкладки ОК в Грунт или Коллектор, рекомендуется на 1 КМ трассы использовать 1,02 КМ ОК.\n" +
                            "Изменить длинну кабеля для соответсвия этой рекомендации?",
                            "Сообщение",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.Yes)
                        {
                            textBox_foclenght.Text = ((int)(float.Parse(textBox_striplenght.Text) + float.Parse(textBox_striplenght.Text) / 100 * 2)).ToString();
                        }
                    }
                }
                else if (comboBox_envtype.Text == "Канализация")
                {
                    if ((float.Parse(textBox_striplenght.Text) + float.Parse(textBox_striplenght.Text) / 100 * 5.7) > float.Parse(textBox_foclenght.Text))
                    {
                        DialogResult result = MessageBox.Show(
                            "Извините, для полкладки ОК в Канализация, рекомендуется на 1 КМ трассы использовать 1,057 КМ ОК.\n" +
                            "Изменить длинну кабеля для соответсвия этой рекомендации?",
                            "Сообщение",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.Yes)
                        {
                            textBox_foclenght.Text = ((int)(float.Parse(textBox_striplenght.Text) + float.Parse(textBox_striplenght.Text) / 100 * 5.7)).ToString();
                        }
                    }
                }

            }

            string sqlRequest = "";
            //Столбцы
            // Проверка на подсчёт цены
            if (textBox_foclenght.Text != "")
                sqlRequest = "select МАРКИРОВКА, [ДИАМЕТР ОК (mm)], [СИЛОВОЙ ЭЛЕМЕНТ], [ВЛАГОЗАЩИТА], МОРОЗОСТОЙКОСТЬ, [ЗАЩИТА ОТ УФ], [ДРУ (kH)], ПРОИЗВОДИТЕЛЬ, СТАНДАРТ, [СРОК СЛУЖБЫ], [ЦЕНА ЗА " + textBox_foclenght.Text + " m (руб)] ";
            else
                sqlRequest = "select МАРКИРОВКА, [ДИАМЕТР ОК (mm)], [СИЛОВОЙ ЭЛЕМЕНТ], [ВЛАГОЗАЩИТА], МОРОЗОСТОЙКОСТЬ, [ЗАЩИТА ОТ УФ], [ДРУ (kH)], ПРОИЗВОДИТЕЛЬ, СТАНДАРТ, [СРОК СЛУЖБЫ] ";
            sqlRequest += "from (select distinct Fiber_optic_cable.ID, Fiber_optic_cable.foc_mark as МАРКИРОВКА, " +
                "Fiber_optic_cable.foc_diameter as [ДИАМЕТР ОК (mm)], " +
                "Fiber_optic_cable.foc_power_element as [СИЛОВОЙ ЭЛЕМЕНТ], " +
                "Fiber_optic_cable.foc_protection_from_water as ВЛАГОЗАЩИТА, " +
                "Fiber_optic_cable.foc_protection_from_cold as МОРОЗОСТОЙКОСТЬ, " +
                "Fiber_optic_cable.foc_UV_protection as [ЗАЩИТА ОТ УФ], " +
                "Fiber_optic_cable.foc_permissible_tensile_force as [ДРУ (kH)], " +
                "Manufacturer.manufacturer_name as ПРОИЗВОДИТЕЛЬ, " +
                "Standard.standard_name as СТАНДАРТ, " +
                "Fiber_optic_cable.foc_life as [СРОК СЛУЖБЫ]";
            // Проверка на длинну кабеля в соответсвии с выбранным типом кабеля
            if (textBox_foclenght.Text != "")
            {
                sqlRequest += ", Fiber_optic_cable.[foc_price/meter] * " + textBox_foclenght.Text + " as [ЦЕНА ЗА " + textBox_foclenght.Text + " m (руб)] ";
                if (int.Parse(textBox_foclenght.Text) > 1000 && comboBox_foctype.Text == "Многомодовый")
                    MessageBox.Show("Для указанной длинны ОК, рекомендуется использовать Одномодовый тип ОК.", "Сообщение");
            }
            //Таблицы
            sqlRequest += "from Fiber_optic_cable, Manufacturer, Standard, Foc_type, Environment, Foc_in_Env, The_temperature_of_the_laying_and_installation, Operating_temperature " +
                //Условия
                "where Manufacturer.ID = Fiber_optic_cable.ID_manufacturer and " +
                "Standard.ID = Fiber_optic_cable.ID_standard and " +
                "(Foc_type.type_name = '" + comboBox_foctype.Text + "' and Foc_type.ID = Fiber_optic_cable.ID_foc_type) and " +
                "Fiber_optic_cable.foc_min_bending_radius <= " + comboBox_focradius.Text + " and " +
                "Fiber_optic_cable.fibers_count = " + comboBox_fcount.Text + " and " +                
                "(Environment.environment_name = '" + comboBox_envtype.Text + "' and Environment.ID = Foc_in_Env.ID_environment and Fiber_optic_cable.ID = Foc_in_Env.ID_foc) ";
            // Проверка на ввод макри ОК            
            if (textBox_focmark.Text != "")
                sqlRequest += " and Fiber_optic_cable.foc_mark = '" + textBox_focmark.Text + "' ";
            // Проверка на выбор защиты            
            if (comboBox_foc_protection.Text != "")
                sqlRequest += " and Fiber_optic_cable.foc_protection = '" + comboBox_foc_protection.Text + "' ";
            // Проверка на ввод срока эксплуатации            
            if (textBox_foc_life.Text != "")
                sqlRequest += " and Fiber_optic_cable.foc_life >= " + textBox_foc_life.Text + " ";
            // Проверка на ввод температуры при которой может работать OК
            if (textBox_montageTemp.Text != "")
            {
                sqlRequest += " and The_temperature_of_the_laying_and_installation.ID = Fiber_optic_cable.ID_T_of_the_L_and_I and " +
                    "The_temperature_of_the_laying_and_installation.min_temperature <= " + textBox_montageTemp.Text + " and " +
                    "The_temperature_of_the_laying_and_installation.max_temperature >= " + textBox_montageTemp.Text + " ";
            }
            // Проверка на ввод температуры при которой может работать OК
            if (textBox_operationTemp.Text != "")
            {
                sqlRequest += " and Operating_temperature.ID = Fiber_optic_cable.ID_O_T and " +
                    "Operating_temperature.min_temperature <= " + textBox_operationTemp.Text + " and " +
                    "Operating_temperature.max_temperature >= " + textBox_operationTemp.Text + " ";
            }
            // Проверка на минимальную цену
            if (textBox_minCost.Text != "")
            {
                sqlRequest += " and Fiber_optic_cable.[foc_price/meter] >= " + textBox_minCost.Text + " ";
            }
            // Проверка на максимальную цену
            if (textBox_maxCost.Text != "")
            {
                sqlRequest += " and Fiber_optic_cable.[foc_price/meter] <= " + textBox_maxCost.Text + " ";
            }
            // Проверка на стойкость ОК к воде
            if (checkBox_humidity.Checked)
                sqlRequest += " and Fiber_optic_cable.foc_protection_from_water = TRUE ";
            // Проверка на морозостойкость
            if (checkBox_frost_resistance.Checked)
                sqlRequest += " and Fiber_optic_cable.foc_protection_from_cold = TRUE ";
            // Проверка на защиту от УФ
            if (checkBox_UV_protection.Checked)
                sqlRequest += " and Fiber_optic_cable.foc_UV_protection = TRUE ";
            //Сортировка
            sqlRequest += "order by Fiber_optic_cable.ID)";

            // Запрос к БД по ОК
            OVDB.SimpeSearch(sqlRequest);
            // Проверка на количество найденных записей
            if (OVDB.FOCTable.Rows.Count != 0)
                dataGridView1.DataSource = OVDB.FOCTable;
            else
            {
                MessageBox.Show(
                    "Извините, но по указанным параметрам не удалось ничего найти.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult result = MessageBox.Show(
                    "Подобрать альтернативы, не учитывая ряд параметров?\n" +
                    "Не будут учитываться: маркировка, температура, минимальная и " +
                    "максимальная стоимость, влагостойкость, морозостойкость, " +
                    "защита от УФ, срок эксплуатации.\n" +
                    "Внимание! Возможно уменьшение срока эксплуатации!", 
                    "Сообщение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    textBox_focmark.Text = "";
                    textBox_montageTemp.Text = "";
                    textBox_operationTemp.Text = "";
                    textBox_minCost.Text = "";
                    textBox_maxCost.Text = "";
                    checkBox_humidity.Checked = false;
                    checkBox_frost_resistance.Checked = false;
                    checkBox_UV_protection.Checked = false;
                    textBox_foc_life.Text = "";
                    button_search_Click(sender, e);
                }
            }                
        }
        /// <summary>
        /// Проверка на ввод длинны кабеля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_foclenght_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Проверка на ввод температуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_middletemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox_montageTemp.Text == "")
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45) // цифры, - и клавиша BackSpace
                    e.Handled = true;
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) // цифры и клавиша BackSpace
                    e.Handled = true;
            }

        }
        /// <summary>
        /// Проверка на выбор влажной среды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_envtype_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRowView selectedEnv = (DataRowView)comboBox_envtype.SelectedValue;

            if (selectedEnv.Row.ItemArray[0].ToString().ToLower().IndexOf("болото") != -1 || selectedEnv.Row.ItemArray[0].ToString().ToLower().IndexOf("река") != -1)
                checkBox_humidity.Checked = true;
            else
                checkBox_humidity.Checked = false;
        }
        /// <summary>
        /// Вывод подробного описанния об ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count == 1)
                {
                    //Столбцы                    
                    string sqlRequest = "select distinct Fiber_optic_cable.foc_mark as МАРКИРОВКА, " +
                        "Fiber_optic_cable.foc_diameter as ДИАМЕТР, " +
                        "Fiber_optic_cable.foc_purpose as ОПИСАНИЕ, " +
                        "Fiber_optic_cable.foc_outer_shell_material as ОБОЛОЧКА, " +
                        "Fiber_optic_cable.foc_protection as ЗАЩИТА, " +
                        "Fiber_optic_cable.foc_power_element as С_Э, " +
                        "Fiber_optic_cable.foc_protection_from_water as ВЛАГОЗАЩИТА, " +
                        "Fiber_optic_cable.foc_protection_from_cold as МОРОЗОСТОЙКОСТЬ, " +
                        "Fiber_optic_cable.foc_UV_protection as З_УФ, " +
                        "Fiber_optic_cable.foc_permissible_tensile_force as ДРУ, " +
                        "Manufacturer.manufacturer_name as ПРОИЗВОДИТЕЛЬ, " +
                        "Fiber_optic_cable.fibers_count as КВ, " +
                        "Standard.standard_name as СТАНДАРТ, " +
                        "The_temperature_of_the_laying_and_installation.min_temperature as МТ1, " +
                        "The_temperature_of_the_laying_and_installation.max_temperature as МТ2, " +
                        "Operating_temperature.min_temperature as ЭТ1, " +
                        "Operating_temperature.max_temperature as ЭТ2, " +
                        "Fiber_optic_cable.[foc_price/meter] as ЦЕНА, " +
                        "Fiber_optic_cable.foc_life as [СРОК СЛУЖБЫ] " +
                        //Таблицы                      
                        "from Fiber_optic_cable, Manufacturer, Standard, Foc_type, The_temperature_of_the_laying_and_installation, Operating_temperature " +
                        //Условия
                        "where Fiber_optic_cable.foc_mark = '" + dataGridView1.SelectedRows[0].Cells[0].Value + "' and " +
                        "Manufacturer.ID = Fiber_optic_cable.ID_manufacturer and " +
                        "Standard.ID = Fiber_optic_cable.ID_standard and " +
                        "Foc_type.ID = Fiber_optic_cable.ID_foc_type and " +
                        "The_temperature_of_the_laying_and_installation.ID = Fiber_optic_cable.ID_T_of_the_L_and_I and " +
                        "Operating_temperature.ID = Fiber_optic_cable.ID_O_T ";
                    // Запрос на поиск конкретного кабеля
                    OVDB.ConcretFocSearch(sqlRequest);
                    sqlRequest = "select Environment.environment_name as СРЕДА " +
                        "from Fiber_optic_cable, Environment, Foc_in_Env " +
                        "where Environment.ID = Foc_in_Env.ID_environment and Fiber_optic_cable.ID = Foc_in_Env.ID_foc and " +
                        "Fiber_optic_cable.foc_mark = '" + dataGridView1.SelectedRows[0].Cells[0].Value + "' " +
                        "order by Environment.ID";
                    // Запрос на получение среды, где можно использовать кабель
                    OVDB.EnvFocSearch(sqlRequest);                    

                    if (OVDB.ConcretFOCTable.Rows.Count > 0)
                    {
                        // Массив с данными о конеретном ОК
                        object[] focInfo = OVDB.ConcretFOCTable.Rows[0].ItemArray;
                        string resultMessage = "Оптоволоконный кабель марки: " + focInfo[0] + "\n" +
                            "Производитель: " + focInfo[10] + "\n" +
                            focInfo[2] + "\n" +
                            "Оболочка: " + focInfo[3] + "\n" +
                            "Защита: " + focInfo[4] + "\n" +
                            "Упрочнаяющий элемент: " + focInfo[5] + "\n";

                        if ((bool)focInfo[6])
                            resultMessage += "Влагозащита: ЕСТЬ\n";
                        else
                            resultMessage += "Влагозащита: НЕТ\n";
                        if ((bool)focInfo[7])
                            resultMessage += "Морозостойкость: ЕСТЬ\n";
                        else
                            resultMessage += "Морозостойкость: НЕТ\n";
                        if ((bool)focInfo[8])
                            resultMessage += "Защита от УФ: ЕСТЬ\n";
                        else
                            resultMessage += "Защита от УФ: НЕТ\n";

                        resultMessage += "Количество волокон: " + focInfo[11] + "\n" +
                            "Допустимое растягивающие усилие (kH): " + focInfo[9] + "\n" +
                            "Диаметр кабеля (mm): " + focInfo[1] + "\n" +
                            "Стандарт: " + focInfo[12] + "\n" +
                            "Прокладка и монтаж при: от " + focInfo[13] + "C° до " + focInfo[14] + "C°\n" +
                            "Эксплуатация при: от " + focInfo[15] + "C° до " + focInfo[16] + "C°\n" +
                            "Цена за метр кабеля: " + focInfo[17] + " ₽\n";
                        double c = int.Parse(textBox_foclenght.Text) * Convert.ToDouble(focInfo[17]);
                        resultMessage += "Цена за " + textBox_foclenght.Text + " m кабеля: " + c.ToString() + " ₽\n" +
                            "Срок эксплуатации (год): " + focInfo[18] + "\n" +
                            "Среда, где можно использовать ОК:\n";
                        for (int i = 0; i < OVDB.FocInEnvTable.Rows.Count; i++)
                        {
                            if (i + 1 < OVDB.FocInEnvTable.Rows.Count)
                                resultMessage += OVDB.FocInEnvTable.Rows[i].ItemArray[0] + ", ";
                            else
                                resultMessage += OVDB.FocInEnvTable.Rows[i].ItemArray[0] + ".";
                        }

                        MessageBox.Show(resultMessage, "Информация по кабелю " + focInfo[0]);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка", "Информация по кабелю ");
                    }                    
                }
                else
                    MessageBox.Show("Выбирите только 1 ОК.\nЧтобы выбрать ОК кликните на самый левый столбец.");
            }
            else
                MessageBox.Show("Извените, пока нет ни одной записи об ОК");
        }
        /// <summary>
        /// ВЫвод всех записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlRequest = "";
            //Столбцы
            //sqlRequest = "МАРКИРОВКА, [ТИП ОК], [ДИАМЕТР ОК (mm)], [СИЛОВОЙ ЭЛЕМЕНТ], ЗАЩИТА, ВЛАГОЗАЩИТА, МОРОЗОСТОЙКОСТЬ, [ЗАЩИТА ОТ УФ], [КОЛИЧЕСТВО ВОЛОКОН], [ДРУ (kH)], ПРОИЗВОДИТЕЛЬ, СТАНДАРТ ";

            sqlRequest = "select МАРКИРОВКА, [ТИП ОК], [ДИАМЕТР ОК (mm)], [СИЛОВОЙ ЭЛЕМЕНТ], ЗАЩИТА, [КОЛИЧЕСТВО ВОЛОКОН], [ДРУ (kH)], ПРОИЗВОДИТЕЛЬ, СТАНДАРТ, [СРОК СЛУЖБЫ] ";
            sqlRequest += "from (select distinct Fiber_optic_cable.ID, " +
                "Fiber_optic_cable.foc_mark as МАРКИРОВКА, " +
                "Foc_type.type_name as [ТИП ОК], " +
                "Fiber_optic_cable.foc_diameter as [ДИАМЕТР ОК (mm)], " +
                "Fiber_optic_cable.foc_power_element as [СИЛОВОЙ ЭЛЕМЕНТ], " +
                "Fiber_optic_cable.foc_protection as ЗАЩИТА, " +
                "Fiber_optic_cable.foc_protection_from_water as ВЛАГОЗАЩИТА, " +
                "Fiber_optic_cable.foc_protection_from_cold as МОРОЗОСТОЙКОСТЬ, " +
                "Fiber_optic_cable.foc_UV_protection as [ЗАЩИТА ОТ УФ], " +
                "Fiber_optic_cable.fibers_count as [КОЛИЧЕСТВО ВОЛОКОН], " +
                "Fiber_optic_cable.foc_permissible_tensile_force as [ДРУ (kH)], " +
                "Manufacturer.manufacturer_name as ПРОИЗВОДИТЕЛЬ, " +
                "Standard.standard_name as СТАНДАРТ, " +
                "Fiber_optic_cable.foc_life as [СРОК СЛУЖБЫ]";
            // Проверка на длинну кабеля в соответсвии с выбранным типом кабеля
            if (textBox_foclenght.Text != "")
            {
                sqlRequest += ", Fiber_optic_cable.[foc_price/meter] * " + textBox_foclenght.Text + " as [ЦЕНА ЗА " + textBox_foclenght.Text + " m (руб)] ";
            }
            //Таблицы
            sqlRequest += "from Fiber_optic_cable, Manufacturer, Standard, Foc_type, The_temperature_of_the_laying_and_installation, Operating_temperature " +
                //Условия
                "where Manufacturer.ID = Fiber_optic_cable.ID_manufacturer and " +
                "Standard.ID = Fiber_optic_cable.ID_standard and " +
                "Foc_type.ID = Fiber_optic_cable.ID_foc_type ";
            //Сортировка
            sqlRequest += "order by Fiber_optic_cable.ID)";

            // Запрос к БД по ОК
            OVDB.SimpeSearch(sqlRequest);
            // Проверка на количество найденных записей
            if (OVDB.FOCTable.Rows.Count != 0)
                dataGridView1.DataSource = OVDB.FOCTable;
            else
                MessageBox.Show("Извините, но по указанным параметрам не удалось ничего найти.", "Сообщение");
        }
        /// <summary>
        /// Переход к форме авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_openAdminPanel_Click(object sender, EventArgs e)
        {
            AuthorizationForm adminForm = new AuthorizationForm();
            adminForm.Show();
        }
    }
}
