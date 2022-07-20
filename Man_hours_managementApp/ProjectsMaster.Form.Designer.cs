namespace Man_hours_managementApp
{
    partial class ProjectsMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.メンバー = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.メンバー工数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.commonUtilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commonUtilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(110, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "マイページ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Location = new System.Drawing.Point(110, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(915, 474);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入力";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(628, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(250, 27);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.メンバー,
            this.メンバー工数});
            this.dataGridView1.Location = new System.Drawing.Point(60, 217);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(392, 188);
            this.dataGridView1.TabIndex = 8;
            // 
            // メンバー
            // 
            this.メンバー.HeaderText = "メンバー";
            this.メンバー.MinimumWidth = 6;
            this.メンバー.Name = "メンバー";
            this.メンバー.Width = 150;
            // 
            // メンバー工数
            // 
            this.メンバー工数.HeaderText = "メンバー工数";
            this.メンバー工数.MinimumWidth = 6;
            this.メンバー工数.Name = "メンバー工数";
            this.メンバー工数.Width = 150;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "総工数";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(212, 139);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(123, 27);
            this.textBox7.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "PL";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(57, 139);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(125, 27);
            this.textBox6.TabIndex = 6;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(628, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "登録日";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(628, 217);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 43);
            this.button3.TabIndex = 10;
            this.button3.Text = "クリア";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 43);
            this.button1.TabIndex = 9;
            this.button1.Text = "登録";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "プロジェクト名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "プロジェクトID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "顧客名";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(406, 54);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(196, 27);
            this.textBox5.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(212, 54);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(176, 27);
            this.textBox4.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(60, 54);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(125, 27);
            this.textBox3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(325, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(319, 45);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "プロジェクトマスタ(新規)";
            // 
            // commonUtilBindingSource
            // 
            this.commonUtilBindingSource.DataSource = typeof(Man_hours_managementApp.CommonUtil);
            // 
            // ProjectsMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 734);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "ProjectsMaster";
            this.Text = "ProjectsMaster";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commonUtilBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button2;
        private GroupBox groupBox1;
        private Label label6;
        private TextBox textBox6;
        private Label label2;
        private TextBox textBox2;
        private Button button1;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label label1;
        private TextBox textBox1;
        private Label label7;
        private TextBox textBox7;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn メンバー;
        private DataGridViewTextBoxColumn メンバー工数;
        private Button button3;
        private DateTimePicker dateTimePicker1;
        private BindingSource commonUtilBindingSource;
    }
}