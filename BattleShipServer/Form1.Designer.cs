namespace BattleShipServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Clients = new System.Windows.Forms.ListBox();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Clients
            // 
            this.Clients.FormattingEnabled = true;
            this.Clients.ItemHeight = 15;
            this.Clients.Location = new System.Drawing.Point(12, 74);
            this.Clients.Name = "Clients";
            this.Clients.Size = new System.Drawing.Size(776, 364);
            this.Clients.TabIndex = 51;
            // 
            // HostTextBox
            // 
            this.HostTextBox.Location = new System.Drawing.Point(12, 45);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(100, 23);
            this.HostTextBox.TabIndex = 45;
            this.HostTextBox.Text = "127.0.0.1:8910";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Clients);
            this.Controls.Add(this.HostTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Clients;
        private System.Windows.Forms.TextBox HostTextBox;
    }
}
