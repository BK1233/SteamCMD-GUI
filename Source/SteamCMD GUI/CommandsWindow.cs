using System;
using System.Windows.Forms;

namespace SteamCMD_GUI
{
    public partial class CommandsWindow : Form
    {
        public CommandsWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtCommands.Location = new System.Drawing.Point(12, 27);
            this.txtCommands.Multiline = true;
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(260, 100);
            this.txtCommands.TabIndex = 0;

            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(125, 15);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Additional Commands:";

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtCommands);
            this.Name = "CommandsWindow";
            this.Text = "Commands";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtCommands;
        private System.Windows.Forms.Label lblInfo;
    }
}
