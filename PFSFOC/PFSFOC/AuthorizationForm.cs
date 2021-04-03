using System;
using System.Windows.Forms;
using PFSFOC.Controller;

namespace PFSFOC
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            AVDB = new AdminViewDB(ConnectionString.ConnectoinSTR);
            InitializeComponent();

            textBox2.PasswordChar = '*';
        }

        #region ГЛОБАЛЬНЫЕ ПЕРЕМЕННЫЕ
        /// <summary>
        /// Представление БД для оператора
        /// </summary>
        AdminViewDB AVDB;
        #endregion

        /// <summary>
        /// Попытка входа в форму администратора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (AVDB.CheckAdmin(textBox1.Text, textBox2.Text))
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Неправильный логин или пароль.\nОтказано в доступе",
                    "ОШИБКА",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
