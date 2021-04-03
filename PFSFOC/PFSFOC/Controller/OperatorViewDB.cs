using System.Data;
using System.Data.OleDb;

namespace PFSFOC.Controller
{
    /// <summary>
    /// Класс представления ДБ для Оператора
    /// </summary>
    class OperatorViewDB : ViewDB
    {
        /// <summary>
        /// Таблица с минимальными радиусаами изгиба ОК
        /// </summary>
        public DataTable MinFocRadiusTable;
        /// <summary>
        /// Таблица с количеством волокон ОК
        /// </summary>
        public DataTable FiberCountTable;
        /// <summary>
        /// Таблица с единственным ОК, который выбрал польщователь
        /// </summary>
        public DataTable ConcretFOCTable;
        /// <summary>
        /// Таблица с защитой ОК
        /// </summary>
        public DataTable FocProtection;

        /// <summary>
        /// Конструктор для представления БД оператору
        /// </summary>
        /// <param name="ConnectionSTR">Строка подключения</param>
        public OperatorViewDB(string ConnectionSTR)
        {
            this.connection = new OleDbConnection(ConnectionSTR);
            this.EnvironmentTable = new DataTable();
            this.FocTypeTable = new DataTable();
            this.FOCTable = new DataTable();
            this.MinFocRadiusTable = new DataTable();
            this.FiberCountTable = new DataTable();
            this.ConcretFOCTable = new DataTable();
            this.FocInEnvTable = new DataTable();
            this.FocProtection = new DataTable();
        }

        /// <summary>
        /// Метод для обновления 
        /// </summary>
        public void SearchUpDate()
        {
            this.FocTypeTable = this.UpdateTable(FocTypeTable, "select type_name from Foc_type order by ID ");
            this.MinFocRadiusTable = this.UpdateTable(MinFocRadiusTable, "select DISTINCT foc_min_bending_radius from Fiber_optic_cable order by foc_min_bending_radius");
            this.FiberCountTable = this.UpdateTable(FiberCountTable, "select distinct fibers_count from Fiber_optic_cable order by fibers_count");
            this.EnvironmentTable = this.UpdateTable(EnvironmentTable, "select environment_name from Environment order by ID");
            this.FocProtection = this.UpdateTable(FocProtection, "select distinct foc_protection from Fiber_optic_cable");
        }
        /// <summary>
        /// Метод простого поиска
        /// </summary>
        /// <param name="SQLRequest">SQL запрос</param>
        public void SimpeSearch(string SQLRequest)
        {
            this.FOCTable = this.UpdateTable(FOCTable, SQLRequest);
        }
        /// <summary>
        /// Метод для поиска конкретного ОК
        /// </summary>
        /// <param name="SQLRequest"></param>
        public void ConcretFocSearch(string SQLRequest)
        {
            this.ConcretFOCTable = this.UpdateTable(ConcretFOCTable, SQLRequest);
        }
        /// <summary>
        /// Метод для поиска среды, где можно проложить ОК
        /// </summary>
        /// <param name="SQLRequest"></param>
        public void EnvFocSearch(string SQLRequest)
        {
            this.FocInEnvTable = this.UpdateTable(FocInEnvTable, SQLRequest);
        }

    }
}
