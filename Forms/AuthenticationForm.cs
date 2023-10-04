using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
namespace TawheedBasitPvtLtd.Forms
{
    public partial class AuthenticationForm : Form
    {

        private Registration.Signup SignupFrom = null;
        private Registration.ForgotPassword ForgotPasswordForm = null;
        public AuthenticationForm()
        {
            InitializeComponent();
            this.firmNameLbl.Text = Configurations.InitializedVariables.ORGANIZATIONNAME;
            this.label4.Text = Configurations.InitializedVariables.ABOUTORGANIZATION;
            PaintForm();
            if (!isAuthenticated())
            {
                OpenChildForm(SignupFrom = new Registration.Signup());
            }
            else
                this.SignupBtn.Enabled = false;
                
        }
        public void PaintForm()
        {


            Console.WriteLine("initiating paint form");
            this.guna2ShadowForm1.ShadowColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMSHDCLR);
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.guna2GroupBox1.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);

            Configurations.ControlsConfigurations.InitializeButton(this.LoginBTN);
            Configurations.ControlsConfigurations.InitializeButton(this.SignupBtn);
            Configurations.ControlsConfigurations.InitializeTextBox(this.Username, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.Password, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label4, Configurations.InitializedVariables.LOGOCOLOR);
            Configurations.ControlsConfigurations.InitializeLabel(this.firmNameLbl, Configurations.InitializedVariables.LOGOCOLOR);
            Configurations.ControlsConfigurations.InitializeLabel(this.MessageBar, "#db261a");
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.RememberMe, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.guna2ToggleSwitch2, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeControls(this.AuthenticationExit, null, null, true);
            Configurations.ControlsConfigurations.InitializeControls(this.AuthenticationFormMin, null, null, true);
        }

        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {

            Configurations.ColorConfiguration.ChangeColorThem();
            PaintForm();
            if (SignupFrom != null)
                SignupFrom.PaintForm(SignupFrom);
            if (ForgotPasswordForm != null)
                ForgotPasswordForm.PaintForm(ForgotPasswordForm);
        }


        private void OpenChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.ChildFormArea.Controls.Add(childForm);
            this.ChildFormArea.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm = new Registration.ForgotPassword();
            OpenChildForm(ForgotPasswordForm);
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            if (isAuthenticated())
            {
                if (Configurations.SecurityEncryption.EncryptString(this.Username.Text) == Configurations.ProjectConfigurations.ReadValueOfKeyConfig(Configurations.ConfigurationKeys.AuthUsernameKey,
                    Configurations.ConfigurationKeys.ConfigurationfileName))
                {
                    if (Configurations.SecurityEncryption.EncryptString(this.Password.Text)
                    == Configurations.ProjectConfigurations.ReadValueOfKeyConfig(Configurations.ConfigurationKeys.AuthPasswordKey, Configurations.ConfigurationKeys.ConfigurationfileName))
                    {
                        this.DialogResult = DialogResult.OK;
                        if (this.RememberMe.Checked)Configurations.InitializedVariables.REMEMBERMEFLAG = true;
                        this.Dispose();
                    }
                    else
                    {
                        this.MessageBar.Text = "LOGIN ERROR: Incorrect password try again!";
                        this.Password.Text = "";
                    }
                }
                else
                {
                    this.MessageBar.Text = "LOGIN ERROR: No account were detected with this username!";
                    LoginTextReset();
                }


            }
            else
            {
                this.MessageBar.Text = "Well come! Please Register your self as the first administrator of the system";
                OpenChildForm(SignupFrom = new Registration.Signup());
            }

            LoginTextReset();
        }
        private void LoginTextReset()
        {
            this.Password.Text = "";
            this.Username.Text = "";
        }
        public static bool isAuthenticated()
        {

            if (Configurations.ProjectConfigurations.ReadValueOfKeyConfig(Configurations.ConfigurationKeys.AuthUsernameKey, Configurations.ConfigurationKeys.ConfigurationfileName) != null)
                if (Configurations.ProjectConfigurations.ReadValueOfKeyConfig(Configurations.ConfigurationKeys.AuthPasswordKey, Configurations.ConfigurationKeys.ConfigurationfileName) != null) return true;
            return false;
        }

        private void SignupBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(SignupFrom = new Registration.Signup());
        }
        public void disableThemToggle()
        {
            this.guna2ToggleSwitch2.Enabled = false;
        }

        private void AuthenticationExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Application.Exit();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoginBTN.PerformClick();
        }
    }
}