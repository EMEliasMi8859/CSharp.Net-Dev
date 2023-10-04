using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd.Forms.Registration
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
            PaintForm(this);
        }
        public void PaintForm(Signup frm)
        {
            frm.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            frm.guna2GroupBox1.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            frm.guna2GroupBox1.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);

            Configurations.ControlsConfigurations.InitializeLabel(frm.label2, null);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.Username, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.password, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.confirmpass, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.sqlservername, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.sqlusername, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.sqlpassword, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.sqldatabsename, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(frm.SignupBTN);
            Configurations.ControlsConfigurations.InitializeLabel(this.MessageBar, "#db261a");
        }
        private void SignupBTN_Click(object sender, EventArgs e)
        {
            if(this.Username.Text != null && this.Username.Text != "")
            {
                if (this.password.Text == this.confirmpass.Text && this.password.Text != null && this.password.Text != "")
                {
                    if (this.sqlservername.Text != null && this.sqlservername.Text != "")
                    {
                        if(this.sqlusername.Text == null || this.sqlusername.Text == "" || this.sqlpassword.Text == null || this.sqlpassword.Text =="")
                        {
                            this.sqlusername.Text = null;
                            this.sqlpassword.Text = null;
                        }
                        if (this.sqldatabsename.Text == null || this.sqldatabsename.Text == "")this.sqldatabsename.Text = null;

                        Configurations.DataBaseConnection.RegisterSQLServerAuth(this.sqlservername.Text, this.sqlusername.Text, this.sqlpassword.Text, this.sqldatabsename.Text);

                    }
                    string resetkey = Configurations.SecurityEncryption.AuthResetKeyGenerator(this.password.Text);
                    SaveFileDialog saveresetkey = new SaveFileDialog();
                    saveresetkey.InitialDirectory = @"C:/";
                    saveresetkey.FileName = $"{this.Username.Text.ToUpper()}_AUTH_RESET_KEY.txt";
                    saveresetkey.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                    if (saveresetkey.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveresetkey.FileName;
                        using (StreamWriter writer = new StreamWriter(filePath))
                            writer.WriteLine($"{this.Username.Text} users credential reset key is:{resetkey}");
                        Configurations.ProjectConfigurations.RegisterAuthentication(this.Username.Text, this.password.Text);
                        Configurations.InitializedVariables.initializeDataBaseInfo();
                        this.Dispose();
                    }
                    else
                        this.MessageBar.Text = "ERROR: You have to save the reset key file!";
                }
                else
                {
                    this.MessageBar.Text = "ERROR: Invalid password or password was not confirmed!";
                    this.password.Text = "";
                    this.confirmpass.Text = "";
                }
            }
            else
            {
                this.MessageBar.Text = "ERROR: Invalid username retry!";
                this.Username.Text = "";
            }
        }
    }
}
