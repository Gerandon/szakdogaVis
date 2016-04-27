namespace GrafikusClientServer
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.zVisible = new System.Windows.Forms.CheckBox();
            this.yVisible = new System.Windows.Forms.CheckBox();
            this.xVisible = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.saveData = new System.Windows.Forms.CheckBox();
            this.saveAct = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.withGraph = new System.Windows.Forms.CheckBox();
            this.circle = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Kapcsolódás";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "<Érték>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "<Érték>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "<Érték>";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.zVisible);
            this.groupBox1.Controls.Add(this.yVisible);
            this.groupBox1.Controls.Add(this.xVisible);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 123);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Szenzor adat";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // zVisible
            // 
            this.zVisible.AutoSize = true;
            this.zVisible.Checked = true;
            this.zVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zVisible.Location = new System.Drawing.Point(114, 91);
            this.zVisible.Name = "zVisible";
            this.zVisible.Size = new System.Drawing.Size(68, 17);
            this.zVisible.TabIndex = 12;
            this.zVisible.Text = "Z látható";
            this.zVisible.UseVisualStyleBackColor = true;
            this.zVisible.CheckedChanged += new System.EventHandler(this.zVisible_CheckedChanged);
            // 
            // yVisible
            // 
            this.yVisible.AutoSize = true;
            this.yVisible.Checked = true;
            this.yVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yVisible.Location = new System.Drawing.Point(115, 55);
            this.yVisible.Name = "yVisible";
            this.yVisible.Size = new System.Drawing.Size(68, 17);
            this.yVisible.TabIndex = 11;
            this.yVisible.Text = "Y látható";
            this.yVisible.UseVisualStyleBackColor = true;
            this.yVisible.CheckedChanged += new System.EventHandler(this.yVisible_CheckedChanged);
            // 
            // xVisible
            // 
            this.xVisible.AutoSize = true;
            this.xVisible.Checked = true;
            this.xVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xVisible.Location = new System.Drawing.Point(115, 23);
            this.xVisible.Name = "xVisible";
            this.xVisible.Size = new System.Drawing.Size(68, 17);
            this.xVisible.TabIndex = 10;
            this.xVisible.Text = "X látható";
            this.xVisible.UseVisualStyleBackColor = true;
            this.xVisible.CheckedChanged += new System.EventHandler(this.xVisible_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Z tengely:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Y tengely:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "X tengely:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Szerver IP cím";
            // 
            // saveData
            // 
            this.saveData.AutoSize = true;
            this.saveData.Location = new System.Drawing.Point(12, 259);
            this.saveData.Name = "saveData";
            this.saveData.Size = new System.Drawing.Size(103, 17);
            this.saveData.TabIndex = 10;
            this.saveData.Text = "Adatok mentése";
            this.saveData.UseVisualStyleBackColor = true;
            // 
            // saveAct
            // 
            this.saveAct.Location = new System.Drawing.Point(12, 282);
            this.saveAct.Name = "saveAct";
            this.saveAct.Size = new System.Drawing.Size(200, 23);
            this.saveAct.TabIndex = 11;
            this.saveAct.Text = "Aktuális pozíció mentése";
            this.saveAct.UseVisualStyleBackColor = true;
            this.saveAct.Click += new System.EventHandler(this.saveAct_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 308);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "<Elmentett pozíció>";
            // 
            // chart1
            // 
            chartArea4.AxisX.LabelStyle.Enabled = false;
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(218, 18);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series10.BorderWidth = 2;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Legend = "Legend1";
            series10.Name = "X tengely";
            series11.BorderWidth = 2;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Legend = "Legend1";
            series11.Name = "Y tengely";
            series12.BorderWidth = 2;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Legend = "Legend1";
            series12.Name = "Z tengely";
            this.chart1.Series.Add(series10);
            this.chart1.Series.Add(series11);
            this.chart1.Series.Add(series12);
            this.chart1.Size = new System.Drawing.Size(950, 287);
            this.chart1.TabIndex = 14;
            this.chart1.Text = "Diagram";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 177);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 17);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Gyorsulás grafikon";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 196);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(46, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "Inga";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 214);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(146, 17);
            this.radioButton3.TabIndex = 17;
            this.radioButton3.Text = "Harmonikus rezgőmozgás";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // withGraph
            // 
            this.withGraph.AutoSize = true;
            this.withGraph.Location = new System.Drawing.Point(31, 236);
            this.withGraph.Name = "withGraph";
            this.withGraph.Size = new System.Drawing.Size(80, 17);
            this.withGraph.TabIndex = 18;
            this.withGraph.Text = "Grafikonnal";
            this.withGraph.UseVisualStyleBackColor = true;
            // 
            // circle
            // 
            this.circle.AutoSize = true;
            this.circle.Location = new System.Drawing.Point(114, 236);
            this.circle.Name = "circle";
            this.circle.Size = new System.Drawing.Size(42, 17);
            this.circle.TabIndex = 19;
            this.circle.Text = "Kör";
            this.circle.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(218, 266);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 336);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.circle);
            this.Controls.Add(this.withGraph);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.saveAct);
            this.Controls.Add(this.saveData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GerInga";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox saveData;
        private System.Windows.Forms.Button saveAct;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox zVisible;
        private System.Windows.Forms.CheckBox yVisible;
        private System.Windows.Forms.CheckBox xVisible;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.CheckBox withGraph;
        private System.Windows.Forms.CheckBox circle;
        private System.Windows.Forms.Label label9;
    }
}

