using System.Data;
using System.Data.OleDb;

namespace PFSFOC.Controller
{
    /// <summary>
    /// Класс представление БД
    /// </summary>
    public abstract class ViewDB
    {
        /// <summary>
        /// 
        /// </summary>
        protected OleDbConnection connection;
        /// <summary>
        /// 
        /// </summary>
        protected OleDbCommand command;
        /// <summary>
        /// 
        /// </summary>
        protected OleDbDataAdapter dataAdapter;
       
        
        /// <summary>
        /// таблица с средой прокладки ОК
        /// </summary>
        public DataTable EnvironmentTable;
        /// <summary>
        /// Тблица с типами ОК
        /// </summary>
        public DataTable FocTypeTable;
        /// <summary>
        /// Таблица с ОК
        /// </summary>
        public DataTable FOCTable;
        /// <summary>
        /// Таблица с связями ОК и Средой прокладки
        /// </summary>
        public DataTable FocInEnvTable;
        /// <summary>
        /// Таблица с производителями
        /// </summary>
        public DataTable ManufacturerTable;
        /// <summary>
        /// Таблица с стандартами
        /// </summary>
        public DataTable StandartTable;
        /// <summary>
        /// Таблица с температурой эксплуатации ОК
        /// </summary>
        public DataTable OTTable;
        /// <summary>
        /// Таблица с температурой прокладки и мантажа ОК
        /// </summary>
        public DataTable TLITable;

        /// <summary>
        /// Загрузка данных из бд в таблицу
        /// </summary>
        /// <param name="Table">Таблица</param>
        /// <param name="SQLRequest">SQL запрос</param>
        /// <returns></returns>
        public DataTable UpdateTable(DataTable Table, string SQLRequest)
        {
            this.connection.Open();
            this.dataAdapter = new OleDbDataAdapter(SQLRequest, connection);
            Table.Clear();
            Table.Columns.Clear();
            this.dataAdapter.Fill(Table);
            this.connection.Close();
            return Table;
        }
    }
}
