namespace Kursach
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBoxRotatingVector = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLightPoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxObservation = new System.Windows.Forms.TextBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRotatingSpeed = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.LineCheckBox = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonPolyhedronColor = new System.Windows.Forms.Button();
            this.buttonLineColor = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(318, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(300, 300);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // textBoxRotatingVector
            // 
            this.textBoxRotatingVector.Location = new System.Drawing.Point(12, 344);
            this.textBoxRotatingVector.Name = "textBoxRotatingVector";
            this.textBoxRotatingVector.Size = new System.Drawing.Size(100, 20);
            this.textBoxRotatingVector.TabIndex = 2;
            this.textBoxRotatingVector.Text = "0;0;1";
            this.textBoxRotatingVector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRotatingVector_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Вектор вращения (X;Y;Z):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(199, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Источник света (X;Y;Z):";
            // 
            // textBoxLightPoint
            // 
            this.textBoxLightPoint.Location = new System.Drawing.Point(202, 344);
            this.textBoxLightPoint.Name = "textBoxLightPoint";
            this.textBoxLightPoint.Size = new System.Drawing.Size(100, 20);
            this.textBoxLightPoint.TabIndex = 4;
            this.textBoxLightPoint.Text = "150;150;150";
            this.textBoxLightPoint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLightPoint_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(384, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Точка обзора (X;Y;Z):";
            // 
            // textBoxObservation
            // 
            this.textBoxObservation.Location = new System.Drawing.Point(387, 344);
            this.textBoxObservation.Name = "textBoxObservation";
            this.textBoxObservation.Size = new System.Drawing.Size(100, 20);
            this.textBoxObservation.TabIndex = 6;
            this.textBoxObservation.Text = "150;150;150";
            this.textBoxObservation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxObservation_KeyDown);
            // 
            // AcceptButton
            // 
            this.AcceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptButton.Location = new System.Drawing.Point(12, 381);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(90, 23);
            this.AcceptButton.TabIndex = 8;
            this.AcceptButton.Text = "Применить";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(546, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Скорость:";
            // 
            // textBoxRotatingSpeed
            // 
            this.textBoxRotatingSpeed.Location = new System.Drawing.Point(549, 344);
            this.textBoxRotatingSpeed.Name = "textBoxRotatingSpeed";
            this.textBoxRotatingSpeed.Size = new System.Drawing.Size(77, 20);
            this.textBoxRotatingSpeed.TabIndex = 9;
            this.textBoxRotatingSpeed.Text = "1";
            this.textBoxRotatingSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRotatingSpeed_KeyDown);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Items.AddRange(new object[] {
            "Кривая(Фонг)",
            "Кривая(Диффузное)",
            "Сфера(Фонг)",
            "Сфера(Диффузное)",
            "Конус(Фонг)",
            "Конус(Диффузное)",
            "Цилиндр(Фонг)",
            "Цилиндр(Диффузное)",
            "Куб(Фонг)",
            "Куб(Диффузное)"});
            this.comboBox1.Location = new System.Drawing.Point(683, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // LineCheckBox
            // 
            this.LineCheckBox.AutoSize = true;
            this.LineCheckBox.Checked = true;
            this.LineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LineCheckBox.Location = new System.Drawing.Point(683, 64);
            this.LineCheckBox.Name = "LineCheckBox";
            this.LineCheckBox.Size = new System.Drawing.Size(172, 17);
            this.LineCheckBox.TabIndex = 12;
            this.LineCheckBox.Text = "Наличие секущей плоскости";
            this.LineCheckBox.UseVisualStyleBackColor = true;
            this.LineCheckBox.CheckedChanged += new System.EventHandler(this.LineCheckBox_CheckedChanged);
            // 
            // buttonPolyhedronColor
            // 
            this.buttonPolyhedronColor.Location = new System.Drawing.Point(833, 10);
            this.buttonPolyhedronColor.Name = "buttonPolyhedronColor";
            this.buttonPolyhedronColor.Size = new System.Drawing.Size(108, 23);
            this.buttonPolyhedronColor.TabIndex = 13;
            this.buttonPolyhedronColor.Text = "Выбрать цвет";
            this.buttonPolyhedronColor.UseVisualStyleBackColor = true;
            this.buttonPolyhedronColor.Click += new System.EventHandler(this.buttonPolyhedronColor_Click);
            // 
            // buttonLineColor
            // 
            this.buttonLineColor.Location = new System.Drawing.Point(861, 64);
            this.buttonLineColor.Name = "buttonLineColor";
            this.buttonLineColor.Size = new System.Drawing.Size(108, 23);
            this.buttonLineColor.TabIndex = 14;
            this.buttonLineColor.Text = "Выбрать цвет";
            this.buttonLineColor.UseVisualStyleBackColor = true;
            this.buttonLineColor.Click += new System.EventHandler(this.buttonLineColor_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(242, 510);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 15;
            this.buttonPlay.Text = "PLAY!!!";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 536);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Берегите уши";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 413);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonLineColor);
            this.Controls.Add(this.buttonPolyhedronColor);
            this.Controls.Add(this.LineCheckBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRotatingSpeed);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxObservation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLightPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRotatingVector);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Курсовая";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBoxRotatingVector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLightPoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxObservation;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRotatingSpeed;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox LineCheckBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonPolyhedronColor;
        private System.Windows.Forms.Button buttonLineColor;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label5;
    }
}

