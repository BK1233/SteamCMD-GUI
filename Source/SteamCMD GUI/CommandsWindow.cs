using System;
using System.Windows.Forms;
using System.IO;

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
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.txtCommands.Location = new System.Drawing.Point(12, 27);
            this.txtCommands.Multiline = true;
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(260, 100);
            this.txtCommands.TabIndex = 0;
            if (File.Exists("commands.txt")) this.txtCommands.Text = File.ReadAllText("commands.txt");

            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(125, 15);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Additional Commands:";

            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Location = new System.Drawing.Point(12, 133);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 165);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtCommands);
            this.Name = "CommandsWindow";
            this.Text = "Commands";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtCommands;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSave;

        private void btnSave_Click(object sender, EventArgs e) {
            System.IO.File.WriteAllText("commands.txt", this.txtCommands.Text);
            this.Close();
        }
    }
}
