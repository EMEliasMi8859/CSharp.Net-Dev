using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd.Forms
{
    public partial class MainGUI : Form
    {
        private Guna2Button CurrentActiveButton = null;
        private Random random;
        private int ColorIndex;
        private Form CurrentActiveForm = null;


        public MainGUI()
        {
            random = new Random();
            Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR = ReadHighlightinColor();
            Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR = Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR;
            InitializeComponent();
            this.firmNameLbl.Text = Configurations.InitializedVariables.ORGANIZATIONNAME;
            paintForm();


            /*BtnCloseChildForms.Visible = false;*/
        }
        public void paintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.guna2ShadowForm1.ShadowColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LOGOCOLOR);
            this.guna2GroupBox1.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.guna2GroupBox1.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.firmNameLbl, Configurations.InitializedVariables.LOGOCOLOR);
            Configurations.ControlsConfigurations.InitializeLabel(this.TabControlTitleLbl, null);

            Configurations.ControlsConfigurations.InitializeControls(this.CloseCnt, null, null, true);
            Configurations.ControlsConfigurations.InitializeControls(this.MaximizeCnt, null, null, true);
            Configurations.ControlsConfigurations.InitializeControls(this.MinimizeCnt, null, null, true);
            Configurations.ControlsConfigurations.InitializeButton(this.LockApplicationBtn, null, Configurations.InitializedVariables.FRMBGRCLR , 0, 0, true);
            this.LockApplicationBtn.ShadowDecoration.Enabled = false;
            Configurations.ControlsConfigurations.InitializeButton(this.StatisticsBtn, Configurations.InitializedVariables.FRMBGRCLR, null,0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.StocksBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.SalesBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.PurchasesBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.ReturnStockBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.RegistriesBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.AccountsBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.SettingsBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
            Configurations.ControlsConfigurations.InitializeButton(this.AboutBtn, Configurations.InitializedVariables.FRMBGRCLR, null, 0, 0, true);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void TitlebarPnl_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        //Methods
        private String ReadHighlightinColor()
        {
            int index = random.Next(Configurations.HighLightingColors.ColorList.Count);
            while (ColorIndex == index)
            {
                index = random.Next(Configurations.HighLightingColors.ColorList.Count);
            }
            ColorIndex = index;
            return Configurations.HighLightingColors.ColorList[index];
        }
        private void ActiveButton(Guna2Button btnsender, String title = null)
        {
            DisabledButton();
            Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR = Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR;
            CurrentActiveButton = btnsender;
            Configurations.ControlsConfigurations.InitializeButton(CurrentActiveButton, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, null, 0, 0, false);
            /*this.TitlebarPnl.BackColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR);
            firmNameLbl.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            pictureBox1.BackColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR), -.2);
            this.LockApplicationBtn.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR), -0.2);
            */Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR = ReadHighlightinColor();
        }
        private void DisabledButton()
        {
            if (CurrentActiveButton != null)
            {
                Configurations.ControlsConfigurations.InitializeButton(CurrentActiveButton, null, null, 0, 0, true);
                /*this.TitlebarPnl.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
                firmNameLbl.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LOGOCOLOR);
                pictureBox1.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
                this.LockApplicationBtn.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);*/
            }
        }
        private void OpenChildForm(Form childForm, object btnSender, string title = null)
        {
            if (CurrentActiveForm != null)
                CurrentActiveForm.Dispose();
            ActiveButton((Guna2Button)btnSender, title);
            CurrentActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.childFormPnl.Controls.Add(childForm);
            this.childFormPnl.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            TabControlTitleLbl.Text = ((title == null) ? CurrentActiveButton.Text : title);
            /*CloseChildFormCnt.Visible = true;*/
        }
        private void BtnCloseChildForms_Click(object sender, EventArgs e)
        {
            if (CurrentActiveForm != null)
                CurrentActiveForm.Close();
            Reset();
        }
        public void Reset()
        {
            DisabledButton();
            TitlebarPnl.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            pictureBox1.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            CurrentActiveButton = null;
            TabControlTitleLbl.Text = "Home";
            /*CloseChildFormCnt.Visible = false;*/
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Configurations.InitializedVariables.REMEMBERMEFLAG = false;
            this.Hide();
            AuthenticationForm authform = new AuthenticationForm();
            authform.disableThemToggle();
            authform.ShowDialog();
            if (authform.DialogResult == DialogResult.OK)
                this.Show();
            else if (authform.DialogResult == DialogResult.No)
            {
                Configurations.ProjectConfigurations.RemoveLineByKeyConfig(Configurations.ConfigurationKeys.REMEMBERME, Configurations.ConfigurationKeys.ConfigurationfileName);
                Application.Exit();
            }
        }
        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            TabController.StatisticsForm statisticsform = new TabController.StatisticsForm();
            OpenChildForm(statisticsform, sender);
        }

        private void StocksBtn_Click(object sender, EventArgs e)
        {
            TabController.StocksForm stockform = new TabController.StocksForm();
            OpenChildForm(stockform, sender);
        }
        private void SalesBtn_Click(object sender, EventArgs e)
        {
            TabController.SalesForm salesform = new TabController.SalesForm();
            OpenChildForm(salesform, sender);
        }


        private void PurchasesBtn_Click(object sender, EventArgs e)
        {
            TabController.Purchases PurchaseForm = new TabController.Purchases();
            OpenChildForm(PurchaseForm, sender);
        }

        private void ReturnStockBtn_Click(object sender, EventArgs e)
        {
            TabController.ReturnStocks returnStocksForm = new TabController.ReturnStocks();
            OpenChildForm(returnStocksForm, sender);
        }

        private void RegistriesBtn_Click(object sender, EventArgs e)
        {
            TabController.RegistriesForm registriesForm = new TabController.RegistriesForm();
            OpenChildForm(registriesForm, sender);
        }
        private void AccountsBtn_Click(object sender, EventArgs e)
        {
            TabController.AccountsForm accountsform = new TabController.AccountsForm();
            OpenChildForm(accountsform, sender);
        }
        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            TabController.SettingsForm settingsform= new TabController.SettingsForm();
            OpenChildForm(settingsform, sender);
        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            TabController.AboutForm aboutForm= new TabController.AboutForm();
            OpenChildForm(aboutForm, sender);
        }
        private void CloseCnt_Click(object sender, EventArgs e)
        {
            Console.WriteLine("remember flag" + Configurations.InitializedVariables.REMEMBERMEFLAG);
            if (Configurations.InitializedVariables.REMEMBERMEFLAG) Configurations.ProjectConfigurations.RegisterRememberMe();
            else Configurations.ProjectConfigurations.RemoveLineByKeyConfig(Configurations.ConfigurationKeys.REMEMBERME, Configurations.ConfigurationKeys.ConfigurationfileName);
            Application.Exit();
            if (TabController.RegistriesForm.CurrentActiveForm != null)
            {
                TabController.RegistriesForm.CurrentActiveForm.Close();
                TabController.RegistriesForm.CurrentActiveForm.Dispose();
            }
            this.Close();
            this.Dispose();
        }
    }
}
