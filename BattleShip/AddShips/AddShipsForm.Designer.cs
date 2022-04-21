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
            this.ClearBut = new System.Windows.Forms.Button();
            this.CompleteBut = new System.Windows.Forms.Button();
            this.Field = new System.Windows.Forms.PictureBox();
            this.AddShipPB = new System.Windows.Forms.PictureBox();
            this.decksCount = new System.Windows.Forms.NumericUpDown();
            this.shipOrientation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddShip = new System.Windows.Forms.Button();
            this.DeleteShip = new System.Windows.Forms.Button();
            this.Auto = new System.Windows.Forms.Button();
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
            this.ActionButs.Location = new System.Drawing.Point(785, 6);
            this.ActionButs.Name = "ActionButs";
            this.ActionButs.Size = new System.Drawing.Size(418, 183);
            this.ActionButs.TabIndex = 1;
            this.ActionButs.TabStop = false;
            // 
            // ClearBut
            // 
            this.ClearBut.Location = new System.Drawing.Point(5, 75);
            this.ClearBut.Name = "ClearBut";
            this.ClearBut.Size = new System.Drawing.Size(407, 43);
            this.ClearBut.TabIndex = 1;
            this.ClearBut.Text = "Очистить";
            this.ClearBut.UseVisualStyleBackColor = true;
            this.ClearBut.Click += new System.EventHandler(this.ClearFieldBut_Click);
            // 
            // CompleteBut
            // 
            this.CompleteBut.Location = new System.Drawing.Point(6, 26);
            this.CompleteBut.Name = "CompleteBut";
            this.CompleteBut.Size = new System.Drawing.Size(406, 43);
            this.CompleteBut.TabIndex = 0;
            this.CompleteBut.Text = "Сохранить";
            this.CompleteBut.UseVisualStyleBackColor = true;
            this.CompleteBut.Click += new System.EventHandler(this.CompleteBut_Click);
            // 
            // Field
            // 
            this.Field.Location = new System.Drawing.Point(12, 6);
            this.Field.MaximumSize = new System.Drawing.Size(760, 760);
            this.Field.MinimumSize = new System.Drawing.Size(760, 760);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(760, 760);
            this.Field.TabIndex = 2;
            this.Field.TabStop = false;
            this.Field.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Field_MouseDoubleClick);
            // 
            // AddShipPB
            // 
            this.AddShipPB.Location = new System.Drawing.Point(785, 275);
            this.AddShipPB.Name = "AddShipPB";
            this.AddShipPB.Size = new System.Drawing.Size(400, 400);
            this.AddShipPB.TabIndex = 3;
            this.AddShipPB.TabStop = false;
            this.AddShipPB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AddShipPB_MouseDoubleClick);
            // 
            // decksCount
            // 
            this.decksCount.Location = new System.Drawing.Point(930, 195);
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
            this.decksCount.Size = new System.Drawing.Size(60, 27);
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
            this.shipOrientation.Location = new System.Drawing.Point(996, 195);
            this.shipOrientation.Name = "shipOrientation";
            this.shipOrientation.Size = new System.Drawing.Size(189, 28);
            this.shipOrientation.TabIndex = 5;
            this.shipOrientation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.shipOrientation_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(789, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество палуб";
            // 
            // AddShip
            // 
            this.AddShip.Location = new System.Drawing.Point(791, 231);
            this.AddShip.Name = "AddShip";
            this.AddShip.Size = new System.Drawing.Size(176, 29);
            this.AddShip.TabIndex = 7;
            this.AddShip.Text = "Добавить";
            this.AddShip.UseVisualStyleBackColor = true;
            this.AddShip.Click += new System.EventHandler(this.AddShip_Click);
            // 
            // DeleteShip
            // 
            this.DeleteShip.Location = new System.Drawing.Point(1009, 231);
            this.DeleteShip.Name = "DeleteShip";
            this.DeleteShip.Size = new System.Drawing.Size(176, 29);
            this.DeleteShip.TabIndex = 8;
            this.DeleteShip.Text = "Удалить";
            this.DeleteShip.UseVisualStyleBackColor = true;
            this.DeleteShip.Click += new System.EventHandler(this.DeleteShip_Click);
            // 
            // Auto
            // 
            this.Auto.Location = new System.Drawing.Point(6, 124);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(407, 43);
            this.Auto.TabIndex = 2;
            this.Auto.Text = "Авторасстановка";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // AddShipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 780);
            this.Controls.Add(this.DeleteShip);
            this.Controls.Add(this.AddShip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shipOrientation);
            this.Controls.Add(this.decksCount);
            this.Controls.Add(this.AddShipPB);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.ActionButs);
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