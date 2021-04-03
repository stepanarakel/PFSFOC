
namespace PFSFOC
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_search = new System.Windows.Forms.Button();
            this.comboBox_foctype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_focmark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_foclenght = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_envtype = new System.Windows.Forms.ComboBox();
            this.textBox_montageTemp = new System.Windows.Forms.TextBox();
            this.textBox_striplenght = new System.Windows.Forms.TextBox();
            this.checkBox_humidity = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button_openAdminPanel = new System.Windows.Forms.Button();
            this.comboBox_fcount = new System.Windows.Forms.ComboBox();
            this.comboBox_focradius = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_operationTemp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_minCost = new System.Windows.Forms.TextBox();
            this.textBox_maxCost = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox_frost_resistance = new System.Windows.Forms.CheckBox();
            this.checkBox_UV_protection = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_foc_protection = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_foc_life = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(708, 660);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(6, 626);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(230, 37);
            this.button_search.TabIndex = 1;
            this.button_search.Text = "НАЙТИ";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // comboBox_foctype
            // 
            this.comboBox_foctype.FormattingEnabled = true;
            this.comboBox_foctype.ItemHeight = 13;
            this.comboBox_foctype.Location = new System.Drawing.Point(6, 40);
            this.comboBox_foctype.Name = "comboBox_foctype";
            this.comboBox_foctype.Size = new System.Drawing.Size(230, 21);
            this.comboBox_foctype.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ТИП ОК";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "МАРКИРОВКА ОК";
            // 
            // textBox_focmark
            // 
            this.textBox_focmark.Location = new System.Drawing.Point(6, 80);
            this.textBox_focmark.Name = "textBox_focmark";
            this.textBox_focmark.Size = new System.Drawing.Size(230, 20);
            this.textBox_focmark.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ДЛИНА ОК (м)";
            // 
            // textBox_foclenght
            // 
            this.textBox_foclenght.Location = new System.Drawing.Point(6, 119);
            this.textBox_foclenght.Name = "textBox_foclenght";
            this.textBox_foclenght.Size = new System.Drawing.Size(230, 20);
            this.textBox_foclenght.TabIndex = 7;
            this.textBox_foclenght.Text = "1020";
            this.textBox_foclenght.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_foclenght_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "МИН. РАДИУС ИЗГИБА (мм)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "КОЛ. ВОЛОКН";
            // 
            // comboBox_envtype
            // 
            this.comboBox_envtype.FormattingEnabled = true;
            this.comboBox_envtype.Location = new System.Drawing.Point(6, 426);
            this.comboBox_envtype.Name = "comboBox_envtype";
            this.comboBox_envtype.Size = new System.Drawing.Size(230, 21);
            this.comboBox_envtype.TabIndex = 12;
            this.comboBox_envtype.SelectedValueChanged += new System.EventHandler(this.comboBox_envtype_SelectedValueChanged);
            // 
            // textBox_montageTemp
            // 
            this.textBox_montageTemp.Location = new System.Drawing.Point(6, 298);
            this.textBox_montageTemp.Name = "textBox_montageTemp";
            this.textBox_montageTemp.Size = new System.Drawing.Size(230, 20);
            this.textBox_montageTemp.TabIndex = 13;
            this.textBox_montageTemp.Text = "0";
            this.textBox_montageTemp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_middletemp_KeyPress);
            // 
            // textBox_striplenght
            // 
            this.textBox_striplenght.Location = new System.Drawing.Point(6, 384);
            this.textBox_striplenght.Name = "textBox_striplenght";
            this.textBox_striplenght.Size = new System.Drawing.Size(230, 20);
            this.textBox_striplenght.TabIndex = 14;
            this.textBox_striplenght.Text = "1000";
            this.textBox_striplenght.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_foclenght_KeyPress);
            // 
            // checkBox_humidity
            // 
            this.checkBox_humidity.AutoSize = true;
            this.checkBox_humidity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBox_humidity.Location = new System.Drawing.Point(6, 468);
            this.checkBox_humidity.Name = "checkBox_humidity";
            this.checkBox_humidity.Size = new System.Drawing.Size(128, 17);
            this.checkBox_humidity.TabIndex = 15;
            this.checkBox_humidity.Text = "ВЛАГОСТОЙКОСТЬ";
            this.checkBox_humidity.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 410);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "ТИП СРЕДЫ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "t МОНТАЖА (C°)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 368);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "ДЛИНА ТРАССЫ (m)";
            // 
            // button_openAdminPanel
            // 
            this.button_openAdminPanel.Location = new System.Drawing.Point(6, 18);
            this.button_openAdminPanel.Name = "button_openAdminPanel";
            this.button_openAdminPanel.Size = new System.Drawing.Size(163, 42);
            this.button_openAdminPanel.TabIndex = 19;
            this.button_openAdminPanel.Text = "ОТКРЫТЬ АДМИН ПАНЕЛЬ";
            this.button_openAdminPanel.UseVisualStyleBackColor = true;
            this.button_openAdminPanel.Click += new System.EventHandler(this.button_openAdminPanel_Click);
            // 
            // comboBox_fcount
            // 
            this.comboBox_fcount.FormattingEnabled = true;
            this.comboBox_fcount.Location = new System.Drawing.Point(6, 197);
            this.comboBox_fcount.Name = "comboBox_fcount";
            this.comboBox_fcount.Size = new System.Drawing.Size(230, 21);
            this.comboBox_fcount.TabIndex = 20;
            // 
            // comboBox_focradius
            // 
            this.comboBox_focradius.FormattingEnabled = true;
            this.comboBox_focradius.Location = new System.Drawing.Point(6, 159);
            this.comboBox_focradius.Name = "comboBox_focradius";
            this.comboBox_focradius.Size = new System.Drawing.Size(230, 21);
            this.comboBox_focradius.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 34);
            this.button1.TabIndex = 22;
            this.button1.Text = "ПОДРОБНЕЕ ОБ ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 325);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "t ЭКСПЛУАТАЦИИ (C°)";
            // 
            // textBox_operationTemp
            // 
            this.textBox_operationTemp.Location = new System.Drawing.Point(6, 341);
            this.textBox_operationTemp.Name = "textBox_operationTemp";
            this.textBox_operationTemp.Size = new System.Drawing.Size(230, 20);
            this.textBox_operationTemp.TabIndex = 24;
            this.textBox_operationTemp.Text = "0";
            this.textBox_operationTemp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_middletemp_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "ЦЕНА ОТ (₽)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(105, 231);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "ЦЕНА ДО (₽)";
            // 
            // textBox_minCost
            // 
            this.textBox_minCost.Location = new System.Drawing.Point(6, 247);
            this.textBox_minCost.Name = "textBox_minCost";
            this.textBox_minCost.Size = new System.Drawing.Size(96, 20);
            this.textBox_minCost.TabIndex = 27;
            this.textBox_minCost.Text = "0";
            this.textBox_minCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_foclenght_KeyPress);
            // 
            // textBox_maxCost
            // 
            this.textBox_maxCost.Location = new System.Drawing.Point(108, 247);
            this.textBox_maxCost.Name = "textBox_maxCost";
            this.textBox_maxCost.Size = new System.Drawing.Size(96, 20);
            this.textBox_maxCost.TabIndex = 28;
            this.textBox_maxCost.Text = "1000";
            this.textBox_maxCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_foclenght_KeyPress);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 35);
            this.button2.TabIndex = 29;
            this.button2.Text = "ВЫВЕСТИ ВСЕ ОК";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox_frost_resistance
            // 
            this.checkBox_frost_resistance.AutoSize = true;
            this.checkBox_frost_resistance.Location = new System.Drawing.Point(6, 491);
            this.checkBox_frost_resistance.Name = "checkBox_frost_resistance";
            this.checkBox_frost_resistance.Size = new System.Drawing.Size(139, 17);
            this.checkBox_frost_resistance.TabIndex = 30;
            this.checkBox_frost_resistance.Text = "МОРОЗОСТОЙКОСТЬ";
            this.checkBox_frost_resistance.UseVisualStyleBackColor = true;
            // 
            // checkBox_UV_protection
            // 
            this.checkBox_UV_protection.AutoSize = true;
            this.checkBox_UV_protection.Location = new System.Drawing.Point(6, 514);
            this.checkBox_UV_protection.Name = "checkBox_UV_protection";
            this.checkBox_UV_protection.Size = new System.Drawing.Size(112, 17);
            this.checkBox_UV_protection.TabIndex = 31;
            this.checkBox_UV_protection.Text = "ЗАЩИТА ОТ УФ";
            this.checkBox_UV_protection.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox_foc_life);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.comboBox_foc_protection);
            this.groupBox1.Controls.Add(this.comboBox_foctype);
            this.groupBox1.Controls.Add(this.checkBox_UV_protection);
            this.groupBox1.Controls.Add(this.button_search);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox_frost_resistance);
            this.groupBox1.Controls.Add(this.textBox_focmark);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_operationTemp);
            this.groupBox1.Controls.Add(this.checkBox_humidity);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox_maxCost);
            this.groupBox1.Controls.Add(this.comboBox_envtype);
            this.groupBox1.Controls.Add(this.textBox_minCost);
            this.groupBox1.Controls.Add(this.textBox_foclenght);
            this.groupBox1.Controls.Add(this.textBox_striplenght);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboBox_focradius);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox_fcount);
            this.groupBox1.Controls.Add(this.textBox_montageTemp);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 679);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ПАРАМЕТРЫ ПОИСКА";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 534);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "ЗАЩИТА";
            // 
            // comboBox_foc_protection
            // 
            this.comboBox_foc_protection.FormattingEnabled = true;
            this.comboBox_foc_protection.Location = new System.Drawing.Point(6, 550);
            this.comboBox_foc_protection.Name = "comboBox_foc_protection";
            this.comboBox_foc_protection.Size = new System.Drawing.Size(230, 21);
            this.comboBox_foc_protection.TabIndex = 33;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_openAdminPanel);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(957, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 679);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "НАВИГАЦИЯ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(243, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(714, 679);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ТАБЛИЦА РЕЗУЛЬТАТОВ";
            // 
            // textBox_foc_life
            // 
            this.textBox_foc_life.Location = new System.Drawing.Point(6, 590);
            this.textBox_foc_life.Name = "textBox_foc_life";
            this.textBox_foc_life.Size = new System.Drawing.Size(230, 20);
            this.textBox_foc_life.TabIndex = 35;
            this.textBox_foc_life.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_foclenght_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 574);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "СРОК ЭКСПЛУАТАЦИИ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 679);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа для выбора ОК";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.ComboBox comboBox_foctype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_focmark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_foclenght;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_envtype;
        private System.Windows.Forms.TextBox textBox_montageTemp;
        private System.Windows.Forms.TextBox textBox_striplenght;
        private System.Windows.Forms.CheckBox checkBox_humidity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_openAdminPanel;
        private System.Windows.Forms.ComboBox comboBox_fcount;
        private System.Windows.Forms.ComboBox comboBox_focradius;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_operationTemp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_minCost;
        private System.Windows.Forms.TextBox textBox_maxCost;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox_frost_resistance;
        private System.Windows.Forms.CheckBox checkBox_UV_protection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_foc_protection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_foc_life;
    }
}

