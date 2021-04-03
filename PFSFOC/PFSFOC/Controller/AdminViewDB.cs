using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace PFSFOC.Controller
{
    /// <summary>
    /// Класс представления ДБ для Администратора
    /// </summary>
    public class AdminViewDB : ViewDB
    {
        /// <summary>
        /// Таблица с администраторами
        /// </summary>
        public DataTable AdminTable;
        /// <summary>
        /// Таблица содержащая только ID и марку ОК
        /// </summary>
        public DataTable ShortFOCTable;
        /// <summary>
        /// Таблица для проверки существования записей перед добавлением
        /// </summary>
        public DataTable CheckTable;

        /// <summary>
        /// Конструктор для представления БД администратору
        /// </summary>
        /// <param name="ConnectionSTR">Строка подключения</param>
        public AdminViewDB(string ConnectionSTR)
        {
            this.connection = new OleDbConnection(ConnectionSTR);
            this.AdminTable = new DataTable();
            this.FocTypeTable = new DataTable();
            this.EnvironmentTable = new DataTable();
            this.FocInEnvTable = new DataTable();
            this.FOCTable = new DataTable();
            this.ManufacturerTable = new DataTable();
            this.StandartTable = new DataTable();
            this.TLITable = new DataTable();
            this.OTTable = new DataTable();
            this.ShortFOCTable = new DataTable();
            this.CheckTable = new DataTable();
        }

        #region ADMINS
        /// <summary>
        /// Метод проверки логина и пароля
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool CheckAdmin(string Login, string Password)
        {
            if (Login == "" || Password == "") return false;
            this.AdminTable = this.UpdateTable(AdminTable, "select admin_name, admin_password from Admin where admin_name = '" + Login + "' and admin_password = '" + Password + "'");
            if (AdminTable.Rows.Count >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод проверки существования администратора
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <returns></returns>
        public bool CheckAdmin(int ID)
        {
            this.AdminTable = this.UpdateTable(AdminTable, "select * from Admin where ID = " + ID);
            if (AdminTable.Rows.Count >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод добавления нового администратора
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        public void AddAdmin(string Login, string Password)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Admin(admin_name, admin_password) values(Login, Password)", connection);
            this.command.Parameters.AddWithValue("Login", Login);
            this.command.Parameters.AddWithValue("Password", Password);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод загрузки всех администраторов
        /// </summary>
        public void LoadAdmins()
        {
            this.AdminTable = this.UpdateTable(AdminTable, "select * from Admin");
        }
        /// <summary>
        /// Метод изменения данных администратора
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="Login">Идентификатор</param>
        /// <param name="Password">Пароль</param>
        public void UpDateAdmin(int ID, string Login, string Password)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Admin SET admin_name = Login, admin_password = Password WHERE ID = Id", connection);
            this.command.Parameters.AddWithValue("Login", Login);
            this.command.Parameters.AddWithValue("Password", Password);
            this.command.Parameters.AddWithValue("Id", ID);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Удаление администратора
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        public void DeleteAdmin(int ID)
        {
            this.connection.Open();
            this.command = new OleDbCommand("delete from Admin WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region FOC
        /// <summary>
        /// Метод для загрузки всех ОК
        /// </summary>
        public void LoadFoc()
        {
            this.FOCTable = this.UpdateTable(FOCTable, "select * from Fiber_optic_cable");
        }
        /// <summary>
        /// Метод проверки существования ОК
        /// </summary>
        /// <param name="ID_focType">Тип</param>
        /// <param name="focMark">Маркировка</param>
        /// <param name="ID_manufacturer">Производитель</param>
        /// <param name="focPurpose">Описание</param>
        /// <param name="ID_standard">Стандарт</param>
        /// <param name="focDiameter">Диаметр</param>
        /// <param name="focOuterShell">Материал внешней оболочки</param>
        /// <param name="focProtection">Защита</param>
        /// <param name="PW">Водостойкость</param>
        /// <param name="PC">Морозостойкость</param>
        /// <param name="PUV">Защита от УФ</param>
        /// <param name="fiberCount">Количество волокон</param>
        /// <param name="focPE">Силовой элемент</param>
        /// <param name="ID_LI">Температура монтжа и прокладки</param>
        /// <param name="ID_O">Температура экспуатации</param>
        /// <param name="PTF">ДРУ</param>
        /// <param name="focMinBR">Минимальный радиус изгиба</param>
        /// <param name="focPrice">Цена кабеля за метр</param>
        /// <param name="focLife">Срок службы кабеля</param>
        public void CheckFOC(int ID_focType, string focMark, int ID_manufacturer, string focPurpose, int ID_standard, double focDiameter, string focOuterShell, string focProtection, bool PW, bool PC, bool PUV, int fiberCount, string focPE, int ID_LI, int ID_O, double PTF, int focMinBR, double focPrice, int focLife)
        {
            string Request = "select * " +
                "from Fiber_optic_cable " +
                "where ID_foc_type = " + ID_focType+ " and " +
                "foc_mark = '" + focMark + "' and " +
                "ID_manufacturer = " + ID_manufacturer + " and " +
                "foc_purpose = '" + focPurpose + "' and " +
                "ID_standard = " + ID_standard + " and " +
                "foc_diameter = " + focDiameter.ToString("G", CultureInfo.InvariantCulture) + " and " +
                "foc_outer_shell_material = '" + focOuterShell + "' and " +
                "foc_protection = '" + focProtection + "' and " +
                "foc_protection_from_water = " + PW + " and " +
                "foc_protection_from_cold = " + PC + " and " +
                "foc_UV_protection = " + PUV + " and " +
                "fibers_count = " + fiberCount + " and " +
                "foc_power_element = '" + focPE + "' and " +
                "ID_T_of_the_L_and_I = " + ID_LI + " and " +
                "ID_O_T = " + ID_O + " and " +
                "foc_permissible_tensile_force = " + PTF.ToString("G", CultureInfo.InvariantCulture) + " and " +
                "foc_min_bending_radius = " + focMinBR + " and " +
                "[foc_price/meter] = " + focPrice.ToString("G", CultureInfo.InvariantCulture) + " and " +
                "foc_life = " + focLife.ToString();
            this.FOCTable = this.UpdateTable(FOCTable, Request);            
        }
        /// <summary>
        /// Метод добавления нового ОК в БД
        /// </summary>
        /// <param name="ID_focType">Тип</param>
        /// <param name="focMark">Маркировка</param>
        /// <param name="ID_manufacturer">Производитель</param>
        /// <param name="focPurpose">Описание</param>
        /// <param name="ID_standard">Стандарт</param>
        /// <param name="focDiameter">Диаметр</param>
        /// <param name="focOuterShell">Материал внешней оболочки</param>
        /// <param name="focProtection">Защита</param>
        /// <param name="PW">Водостойкость</param>
        /// <param name="PC">Морозостойкость</param>
        /// <param name="PUV">Защита от УФ</param>
        /// <param name="fiberCount">Количество волокон</param>
        /// <param name="focPE">Силовой элемент</param>
        /// <param name="ID_LI">Температура монтжа и прокладки</param>
        /// <param name="ID_O">Температура экспуатации</param>
        /// <param name="PTF">ДРУ</param>
        /// <param name="focMinBR">Минимальный радиус изгиба</param>
        /// <param name="focPrice">Цена кабеля за метр</param>
        /// <param name="focLife">Срок службы кабеля</param>
        public void AddFOC(int ID_focType, string focMark, int ID_manufacturer, string focPurpose, int ID_standard, double focDiameter, string focOuterShell, string focProtection, bool PW, bool PC, bool PUV, int fiberCount, string focPE, int ID_LI, int ID_O, double PTF, int focMinBR, double focPrice, int focLife)
        {
            this.connection.Open();
            string insertRequest = "INSERT INTO " +
                "Fiber_optic_cable(ID_foc_type, foc_mark, ID_manufacturer, foc_purpose, ID_standard, foc_diameter, foc_outer_shell_material, foc_protection, foc_protection_from_water, foc_protection_from_cold, foc_UV_protection, fibers_count, foc_power_element, ID_T_of_the_L_and_I, ID_O_T, foc_permissible_tensile_force, foc_min_bending_radius, [foc_price/meter], foc_life) " +
                "VALUES(" + ID_focType + ", '" + focMark + "', " + ID_manufacturer + ", '" + focPurpose + "', " + ID_standard + ", " + focDiameter.ToString("G", CultureInfo.InvariantCulture) + ", '" + focOuterShell + "', '" + focProtection + "', " + PW + ", " + PC + ", " + PUV + ", " + fiberCount + ", '" + focPE + "', " + ID_LI + ", " + ID_O + ", " + PTF.ToString("G", CultureInfo.InvariantCulture) + ", " + focMinBR + ", " + focPrice.ToString("G", CultureInfo.InvariantCulture) + ", " + focLife.ToString() + ")";
            this.command = new OleDbCommand(insertRequest, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод изменения ОК в БД
        /// </summary>
        /// /// <param name="ID">Идентификатор</param>
        /// <param name="ID_focType">Тип</param>
        /// <param name="focMark">Маркировка</param>
        /// <param name="ID_manufacturer">Производитель</param>
        /// <param name="focPurpose">Описание</param>
        /// <param name="ID_standard">Стандарт</param>
        /// <param name="focDiameter">Диаметр</param>
        /// <param name="focOuterShell">Материал внешней оболочки</param>
        /// <param name="focProtection">Защита</param>
        /// <param name="PW">Водостойкость</param>
        /// <param name="PC">Морозостойкость</param>
        /// <param name="PUV">Защита от УФ</param>
        /// <param name="fiberCount">Количество волокон</param>
        /// <param name="focPE">Силовой элемент</param>
        /// <param name="ID_LI">Температура монтжа и прокладки</param>
        /// <param name="ID_O">Температура экспуатации</param>
        /// <param name="PTF">ДРУ</param>
        /// <param name="focMinBR">Минимальный радиус изгиба</param>
        /// <param name="focPrice">Цена кабеля за метр</param>
        /// <param name="focLife">Срок службы кабеля</param>
        public void EditFOC(int ID, int ID_focType, string focMark, int ID_manufacturer, string focPurpose, int ID_standard, double focDiameter, string focOuterShell, string focProtection, bool PW, bool PC, bool PUV, int fiberCount, string focPE, int ID_LI, int ID_O, double PTF, int focMinBR, double focPrice, int focLife)
        {
            this.connection.Open();
            string updateRequest = "update Fiber_optic_cable set " +
                "ID_foc_type = " + ID_focType + ", foc_mark = '" + focMark + "', ID_manufacturer = " + ID_manufacturer + ", foc_purpose = '" + focPurpose + "', ID_standard = " + ID_standard + ", foc_diameter = " + focDiameter.ToString("G", CultureInfo.InvariantCulture) + ", foc_outer_shell_material = '" + focOuterShell + "', foc_protection = '" + focProtection + "', foc_protection_from_water = " + PW + ", foc_protection_from_cold = " + PC + ", foc_UV_protection = " + PUV + ", fibers_count = " + fiberCount + ", foc_power_element = '" + focPE + "', ID_T_of_the_L_and_I = " + ID_LI + ", ID_O_T = " + ID_O + ", foc_permissible_tensile_force = " + PTF.ToString("G", CultureInfo.InvariantCulture) + ", foc_min_bending_radius = " + focMinBR + ", [foc_price/meter] = " + focPrice.ToString("G", CultureInfo.InvariantCulture) + ", foc_life = " + focLife.ToString() + " " +
                "where ID = " + ID;
            this.command = new OleDbCommand(updateRequest, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region Tables

        /// <summary>
        /// Метод загрузки дополнительных таблиц
        /// </summary>
        public void LoadTables()
        {
            this.EnvironmentTable = this.UpdateTable(EnvironmentTable, "select * from Environment");
            this.FocTypeTable = this.UpdateTable(FocTypeTable, "select * from Foc_type");
            this.ManufacturerTable = this.UpdateTable(ManufacturerTable, "select * from Manufacturer");
            this.StandartTable = this.UpdateTable(StandartTable, "select * from Standard");
            this.TLITable = this.UpdateTable(TLITable, "select * from The_temperature_of_the_laying_and_installation");
            this.OTTable = this.UpdateTable(OTTable, "select * from Operating_temperature");
        }

        #region Environment
        /// <summary>
        /// Проверка на существование среды
        /// </summary>
        /// <param name="env_name">Название среды</param>
        public void CheckEnvironment(string env_name)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from Environment where environment_name = '" + env_name + "'");
        }
        /// <summary>
        /// Метод обновления записи в таблице Environment
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="env_name">Название среды</param>
        public void UpdateEnvironment(int ID, string env_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Environment SET environment_name = '" + env_name + "' WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод добавления среды
        /// </summary>
        /// <param name="env_name"></param>
        public void AddEnvironment(string env_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Environment(environment_name) values('" + env_name + "')", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region Manufacturer
        /// <summary>
        /// Проверка существования производителя
        /// </summary>
        /// <param name="manufacturer_name">Производитель</param>
        public void CheckManufacturer(string manufacturer_name)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from Manufacturer where manufacturer_name = '" + manufacturer_name + "'");
        }
        /// <summary>
        /// Обновление производителя
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="manufacturer_name">Производиетль</param>
        public void UpdateManufacturer(int ID, string manufacturer_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Manufacturer SET manufacturer_name = '" + manufacturer_name + "' WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод добавления производителя
        /// </summary>
        /// <param name="manufacturer_name"></param>
        public void AddManufacturer(string manufacturer_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Manufacturer(manufacturer_name) values('" + manufacturer_name + "')", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region Standard
        /// <summary>
        /// Проверка существования стандарта
        /// </summary>
        /// <param name="standard_name">Стандарт</param>
        public void CheckStandard(string standard_name)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from Standard where standard_name = '" + standard_name + "'");
        }
        /// <summary>
        /// Изменение информации о стандарте
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="standard_name">Стандарт</param>
        public void UpdateStandard(int ID, string standard_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Standard SET standard_name = '" + standard_name + "' WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод добавления стандарта
        /// </summary>
        /// <param name="standard_name"></param>
        public void AddStandard(string standard_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Standard(standard_name) values('" + standard_name + "')", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region FocType
        /// <summary>
        /// Проверка существования типа ок
        /// </summary>
        /// <param name="type_name">Тип ОК</param>
        public void CheckFocType(string type_name)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from Foc_type where type_name = '" + type_name + "'");
        }
        /// <summary>
        /// Метод обновления типов ОК
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="type_name">Тип ОК</param>
        public void UpdateFocType(int ID, string type_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Foc_type SET type_name = '" + type_name + "' WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод добавления типа ОК
        /// </summary>
        /// <param name="type_name"></param>
        public void AddFocType(string type_name)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Foc_type(type_name) values('" + type_name + "')", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region Температура прокладки и монтажа   
        /// <summary>
        /// Проверка существования записи о температуре прокладки и моонтажа
        /// </summary>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void CheckLI(int min, int max)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from The_temperature_of_the_laying_and_installation where min_temperature = " + min + " and max_temperature = " + max);
        }
        /// <summary>
        /// Метод обновления температуры прокладки и моонтажа
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void UpdateLI(int ID, int min, int max)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE The_temperature_of_the_laying_and_installation SET min_temperature = " + min + ", max_temperature = " + max + " WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Метод добавления температуры монтажа и прокладки
        /// </summary>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void AddLI(int min, int max)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO The_temperature_of_the_laying_and_installation(min_temperature, max_temperature) values(" + min + ", " + max + ")", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #region Температура эксплуатации
        /// <summary>
        /// Проверка существования записи о температуре эксплуатации
        /// </summary>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void CheckO(int min, int max)
        {
            this.CheckTable = this.UpdateTable(CheckTable, "select * from Operating_temperature where min_temperature = " + min + " and max_temperature = " + max);
        }
        /// <summary>
        /// Метод обновления температуры эксплуатации
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void UpdateO(int ID, int min, int max)
        {
            this.connection.Open();
            this.command = new OleDbCommand("UPDATE Operating_temperature SET min_temperature = " + min + ", max_temperature = " + max + " WHERE ID = " + ID, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        /// <summary>
        /// Добавление температура эксполуатации
        /// </summary>
        /// <param name="min">Мин температура</param>
        /// <param name="max">Макс температура</param>
        public void AddO(int min, int max)
        {
            this.connection.Open();
            this.command = new OleDbCommand("INSERT INTO Operating_temperature(min_temperature, max_temperature) values(" + min + ", " + max + ")", connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion

        #endregion

        #region Среда и ОК
        /// <summary>
        /// Метод загрузки связей
        /// </summary>
        public void LoadFocforEntity()
        {
            this.FocInEnvTable = this.UpdateTable(FocInEnvTable, "select * from Foc_in_Env");
        }
        /// <summary>
        /// Метод загрузки кабелей
        /// </summary>
        public void LoadShortFoc()
        {
            this.ShortFOCTable = this.UpdateTable(ShortFOCTable, "select ID, foc_mark from Fiber_optic_cable");
        }
        /// <summary>
        /// Метод загрузки среды
        /// </summary>
        public void LoadEnvironment()
        {
            this.EnvironmentTable = this.UpdateTable(EnvironmentTable, "select * from Environment");
        }
        /// <summary>
        /// Создание связи
        /// </summary>
        /// <param name="ID_env"></param>
        /// <param name="ID_foc"></param>
        public void AddEntity(int ID_env, int ID_foc)
        {
            this.connection.Open();
            string insertRequest = "INSERT INTO Foc_in_Env(ID_environment, ID_foc) values(" + ID_env + ", " + ID_foc + ")";
            this.command = new OleDbCommand(insertRequest, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        public void DeleteEntity(int ID)
        {
            this.connection.Open();
            string insertRequest = "delete from Foc_in_Env where ID = " + ID;
            this.command = new OleDbCommand(insertRequest, connection);
            this.command.ExecuteNonQuery();
            this.connection.Close();
        }
        #endregion
    }
}
