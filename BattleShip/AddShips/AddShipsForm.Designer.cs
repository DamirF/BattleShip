namespace BattleShip.AddShips
{
    partial class AddShipsForm
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
            this.ActionButs = new System.Windows.Forms.GroupBox();
            this.Auto = new System.Windows.Forms.Button();
            this.ClearBut = new System.Windows.Forms.Button();
            this.CompleteBut = new System.Windows.Forms.Button();
            this.Field = new System.Windows.Forms.PictureBox();
            this.AddShipPB = new System.Windows.Forms.PictureBox();
            this.decksCount = new System.Windows.Forms.NumericUpDown();
            this.shipOrientation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddShip = new System.Windows.Forms.Button();
            this.DeleteShip = new System.Windows.Forms.Button();
            this.ActionButs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddShipPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decksCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ActionButs
            // 
            this.ActionButs.Controls.Add(this.Auto);
            this.ActionButs.Controls.Add(this.ClearBut);
            this.ActionButs.Controls.Add(this.CompleteBut);
            this.ActionButs.Location = new System.Drawing.Point(716, 4);
            this.ActionButs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ActionButs.Name = "ActionButs";
            this.ActionButs.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ActionButs.Size = new System.Drawing.Size(366, 137);
            this.ActionButs.TabIndex = 1;
            this.ActionButs.TabStop = false;
            // 
            // Auto
            // 
            this.Auto.Location = new System.Drawing.Point(5, 93);
            this.Auto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(356, 32);
            this.Auto.TabIndex = 2;
            this.Auto.Text = "Авторасстановка";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // ClearBut
            // 
            this.ClearBut.Location = new System.Drawing.Point(4, 56);
            this.ClearBut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearBut.Name = "ClearBut";
            this.ClearBut.Size = new System.Drawing.Size(356, 32);
            this.ClearBut.TabIndex = 1;
            this.ClearBut.Text = "Очистить";
            this.ClearBut.UseVisualStyleBackColor = true;
            this.ClearBut.Click += new System.EventHandler(this.ClearFieldBut_Click);
            // 
            // CompleteBut
            // 
            this.CompleteBut.Location = new System.Drawing.Point(5, 20);
            this.CompleteBut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CompleteBut.Name = "CompleteBut";
            this.CompleteBut.Size = new System.Drawing.Size(355, 32);
            this.CompleteBut.TabIndex = 0;
            this.CompleteBut.Text = "Сохранить";
            this.CompleteBut.UseVisualStyleBackColor = true;
            this.CompleteBut.Click += new System.EventHandler(this.CompleteBut_Click);
            // 
            // Field
            // 
            this.Field.Location = new System.Drawing.Point(10, 4);
            this.Field.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Field.MaximumSize = new System.Drawing.Size(700, 700);
            this.Field.MinimumSize = new System.Drawing.Size(700, 700);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(700, 700);
            this.Field.TabIndex = 2;
            this.Field.TabStop = false;
            this.Field.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Field_MouseDoubleClick);
            // 
            // AddShipPB
            // 
            this.AddShipPB.Location = new System.Drawing.Point(716, 206);
            this.AddShipPB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddShipPB.Name = "AddShipPB";
            this.AddShipPB.Size = new System.Drawing.Size(350, 350);
            this.AddShipPB.TabIndex = 3;
            this.AddShipPB.TabStop = false;
            this.AddShipPB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AddShipPB_MouseDoubleClick);
            // 
            // decksCount
            // 
            this.decksCount.Location = new System.Drawing.Point(843, 146);
            this.decksCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.decksCount.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.decksCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.decksCount.Name = "decksCount";
            this.decksCount.ReadOnly = true;
            this.decksCount.Size = new System.Drawing.Size(52, 23);
            this.decksCount.TabIndex = 4;
            this.decksCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // shipOrientation
            // 
            this.shipOrientation.FormattingEnabled = true;
            this.shipOrientation.Items.AddRange(new object[] {
            "Горизонтальный",
            "Вертикальный"});
            this.shipOrientation.Location = new System.Drawing.Point(901, 146);
            this.shipOrientation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shipOrientation.Name = "shipOrientation";
            this.shipOrientation.Size = new System.Drawing.Size(166, 23);
            this.shipOrientation.TabIndex = 5;
            this.shipOrientation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.shipOrientation_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(729, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество палуб";
            // 
            // AddShip
            // 
            this.AddShip.Location = new System.Drawing.Point(721, 173);
            this.AddShip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddShip.Name = "AddShip";
            this.AddShip.Size = new System.Drawing.Size(154, 22);
            this.AddShip.TabIndex = 7;
            this.AddShip.Text = "Добавить";
            this.AddShip.UseVisualStyleBackColor = true;
            this.AddShip.Click += new System.EventHandler(this.AddShip_Click);
            // 
            // DeleteShip
            // 
            this.DeleteShip.Location = new System.Drawing.Point(912, 173);
            this.DeleteShip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteShip.Name = "DeleteShip";
            this.DeleteShip.Size = new System.Drawing.Size(154, 22);
            this.DeleteShip.TabIndex = 8;
            this.DeleteShip.Text = "Удалить";
            this.DeleteShip.UseVisualStyleBackColor = true;
            this.DeleteShip.Click += new System.EventHandler(this.DeleteShip_Click);
            // 
            // AddShipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 761);
            this.Controls.Add(this.DeleteShip);
            this.Controls.Add(this.AddShip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shipOrientation);
            this.Controls.Add(this.decksCount);
            this.Controls.Add(this.AddShipPB);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.ActionButs);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddShipsForm";
            this.Text = "AddShipsForm";
            this.ActionButs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Field)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddShipPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decksCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox ActionButs;
        private System.Windows.Forms.Button ClearBut;
        private System.Windows.Forms.Button CompleteBut;
        private System.Windows.Forms.PictureBox Field;
        private System.Windows.Forms.PictureBox AddShipPB;
        private System.Windows.Forms.NumericUpDown decksCount;
        private System.Windows.Forms.ComboBox shipOrientation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddShip;
        private System.Windows.Forms.Button DeleteShip;
        private System.Windows.Forms.Button Auto;
    }
}