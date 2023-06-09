using System.ComponentModel;

namespace SQL
{
    partial class Conect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.BTTest_Connection = new System.Windows.Forms.Button();
            this.CBSwitcher = new System.Windows.Forms.CheckBox();
            this.BTDrop_Table = new System.Windows.Forms.Button();
            this.BTCreate = new System.Windows.Forms.Button();
            this.CBFilter = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Conect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "jwkrush.com.ua";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(32, 92);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(171, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "kkte_nau";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(32, 146);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(171, 22);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "kkte_nau";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(588, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(685, 570);
            this.dataGridView1.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(32, 270);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(171, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "create conection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BTTest_Connection
            // 
            this.BTTest_Connection.Location = new System.Drawing.Point(32, 349);
            this.BTTest_Connection.Name = "BTTest_Connection";
            this.BTTest_Connection.Size = new System.Drawing.Size(171, 23);
            this.BTTest_Connection.TabIndex = 10;
            this.BTTest_Connection.Text = "Test connection";
            this.BTTest_Connection.UseVisualStyleBackColor = true;
            this.BTTest_Connection.Click += new System.EventHandler(this.BTTest_Connection_Click);
            // 
            // CBSwitcher
            // 
            this.CBSwitcher.Appearance = System.Windows.Forms.Appearance.Button;
            this.CBSwitcher.AutoSize = true;
            this.CBSwitcher.Enabled = false;
            this.CBSwitcher.Location = new System.Drawing.Point(32, 438);
            this.CBSwitcher.Name = "CBSwitcher";
            this.CBSwitcher.Size = new System.Drawing.Size(71, 27);
            this.CBSwitcher.TabIndex = 16;
            this.CBSwitcher.Text = "Switcher";
            this.CBSwitcher.UseVisualStyleBackColor = true;
            this.CBSwitcher.CheckedChanged += new System.EventHandler(this.CBSwitcher_CheckedChanged);
            // 
            // BTDrop_Table
            // 
            this.BTDrop_Table.Enabled = false;
            this.BTDrop_Table.Location = new System.Drawing.Point(134, 395);
            this.BTDrop_Table.Name = "BTDrop_Table";
            this.BTDrop_Table.Size = new System.Drawing.Size(95, 23);
            this.BTDrop_Table.TabIndex = 15;
            this.BTDrop_Table.Text = "Drop Table";
            this.BTDrop_Table.UseVisualStyleBackColor = true;
            this.BTDrop_Table.Click += new System.EventHandler(this.BTDrop_Table_Click);
            // 
            // BTCreate
            // 
            this.BTCreate.Enabled = false;
            this.BTCreate.Location = new System.Drawing.Point(32, 395);
            this.BTCreate.Name = "BTCreate";
            this.BTCreate.Size = new System.Drawing.Size(95, 23);
            this.BTCreate.TabIndex = 14;
            this.BTCreate.Text = "Create Table";
            this.BTCreate.UseVisualStyleBackColor = true;
            this.BTCreate.Click += new System.EventHandler(this.BTCreate_Click);
            // 
            // CBFilter
            // 
            this.CBFilter.AutoSize = true;
            this.CBFilter.Enabled = false;
            this.CBFilter.Location = new System.Drawing.Point(32, 484);
            this.CBFilter.Name = "CBFilter";
            this.CBFilter.Size = new System.Drawing.Size(18, 17);
            this.CBFilter.TabIndex = 13;
            this.CBFilter.UseVisualStyleBackColor = true;
            this.CBFilter.CheckedChanged += new System.EventHandler(this.CBFilter_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(134, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "add date";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(32, 198);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new System.Drawing.Size(171, 22);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "KkTe#NaU";
            // 
            // textBox5
            // 
            this.textBox5.AccessibleDescription = "";
            this.textBox5.AccessibleName = "";
            this.textBox5.Location = new System.Drawing.Point(134, 469);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(171, 22);
            this.textBox5.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(134, 497);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(171, 22);
            this.textBox6.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(134, 525);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(171, 22);
            this.textBox7.TabIndex = 19;
            // 
            // Conect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1273, 570);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.CBSwitcher);
            this.Controls.Add(this.BTDrop_Table);
            this.Controls.Add(this.BTCreate);
            this.Controls.Add(this.CBFilter);
            this.Controls.Add(this.BTTest_Connection);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Conect";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;

        private System.Windows.Forms.TextBox textBox4;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        private System.Windows.Forms.CheckBox CBSwitcher;
        private System.Windows.Forms.Button BTDrop_Table;
        private System.Windows.Forms.Button BTCreate;
        private System.Windows.Forms.CheckBox CBFilter;

        private System.Windows.Forms.Button BTTest_Connection;

        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;

        #endregion
    }
}