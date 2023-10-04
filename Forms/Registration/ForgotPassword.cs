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
    public partial class ForgotPassword : Form
    {
        private static bool isResetKeySaved = false;
        private string ResetKey;
        public ForgotPassword()
        {
            InitializeComponent();
            PaintForm(this);
            this.guna2Button1.Enabled = false;
        }
        public void PaintForm(ForgotPassword frm)
        {
            frm.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            frm.guna2GroupBox1.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            frm.guna2GroupBox1.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);

            Configurations.ControlsConfigurations.InitializeLabel(frm.label1);
            Configurations.ControlsConfigurations.InitializeLabel(frm.label2);
            Configurations.ControlsConfigurations.InitializeLabel(frm.label3);
            Configurations.ControlsConfigurations.InitializeLabel(frm.label4);
            Configurations.ControlsConfigurations.InitializeLabel(frm.label5);
            Configurations.ControlsConfigurations.InitializeLabel(frm.label6);
            Configurations.ControlsConfigurations.InitializeLabel(frm.NewAuthReskey, Configurations.InitializedVariables.LOGOCOLOR);
            Configurations.ControlsConfigurations.InitializeLabel(frm.MessageBar, "#db261a");

            Configurations.ControlsConfigurations.InitializeTextBox(frm.resetKeyp1, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.resetKeyp2, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.resetKeyp3, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.resetKeyp4, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.username, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.password, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(frm.confirmpassword, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(frm.ResetBtn, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(frm.guna2Button1, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(frm.guna2Button2, null, null, -1, -1, true);

        }
        private bool saveresetkey()
        {

            string resetkey = Configurations.SecurityEncryption.AuthResetKeyGenerator(this.password.Text);
            SaveFileDialog saveresetkey = new SaveFileDialog();
            saveresetkey.InitialDirectory = @"C:/";
            saveresetkey.FileName = "SYSTEMAUTHENTICATIONRESETKEY.txt";
            saveresetkey.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveresetkey.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveresetkey.FileName;
                using (StreamWriter writer = new StreamWriter(filePath))
                    writer.WriteLine($"{this.username.Text} users credential reset key is:{resetkey}");
                isResetKeySaved = true;
                return true;
            }
            else
                this.MessageBar.Text = "ResetKey unsecure: you have to save the reset key file!";
            return false;
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            string resetKey = resetKeyp1.Text +"-"+ resetKeyp2.Text +"-" + resetKeyp3.Text +"-" + resetKeyp4.Text;
            if (resetKey == Configurations.SecurityEncryption.AuthResetKeyGenerator
                (Configurations.SecurityEncryption.DecryptString(Configurations.ProjectConfigurations.
                ReadValueOfKeyConfig(Configurations.ConfigurationKeys.AuthPasswordKey, Configurations.ConfigurationKeys.ConfigurationfileName)))){
                if(this.password.Text == this.confirmpassword.Text)
                {
                    while (!isResetKeySaved) { saveresetkey();}
                    Configurations.ProjectConfigurations.RegisterAuthentication(this.username.Text, this.password.Text);
                    this.Dispose();
                }
                else
                {
                    this.MessageBar.Text = "Confirmation error: password confirmation didn't matched";
                    this.confirmpassword.Text = "";
                }
            }
            else
            {
                this.MessageBar.Text = "Reset key error: reset key wasn't recognized!";
                this.resetKeyp1.Text = "";
                this.resetKeyp2.Text = "";
                this.resetKeyp3.Text = "";
                this.resetKeyp4.Text = "";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (this.resetKeyp1.Text != null && this.resetKeyp1.Text != "" && this.resetKeyp2.Text != null && this.resetKeyp2.Text != "" && this.resetKeyp3.Text != null && this.resetKeyp3.Text != "" && this.resetKeyp4.Text != null && this.resetKeyp4.Text != "")
                saveresetkey();
            else
                this.MessageBar.Text = "Reset key error: Please Enter reset key!";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string resetkey;
            OpenFileDialog openresetkey = new OpenFileDialog();
            openresetkey.InitialDirectory = @"C:/";
            openresetkey.FileName = "SYSTEMAUTHENTICATIONRESETKEY.txt";
            openresetkey.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openresetkey.ShowDialog() == DialogResult.OK)
            {
                int indexO = 0;
                string filePath = openresetkey.FileName;
                using (StreamReader reader = new StreamReader(filePath))
                    resetkey = reader.ReadLine();
                indexO = resetkey.IndexOf(':');
                resetkey = resetkey.Substring(indexO+1, resetkey.Length - indexO-1);
                this.resetKeyp1.Text = resetkey.Substring(0, 3);
                this.resetKeyp2.Text = resetkey.Substring(4, 3);
                this.resetKeyp3.Text = resetkey.Substring(8, 3);
                this.resetKeyp4.Text = resetkey.Substring(12, 3);
                isResetKeySaved = false;
            }
        }

        private void confirmpassword_TextChanged(object sender, EventArgs e)
        {
            if(this.password.Text == this.confirmpassword.Text)
            {
                ResetKey = Configurations.SecurityEncryption.AuthResetKeyGenerator(this.confirmpassword.Text);
                this.NewAuthReskey.Text = ResetKey;
                this.guna2Button1.Enabled = true;
            }
        }
    }
}
