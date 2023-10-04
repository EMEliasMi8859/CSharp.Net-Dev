namespace TawheedBasitPvtLtd.Forms
{
    partial class MainGUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGUI));
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.TitlebarPnl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LockApplicationBtn = new Guna.UI2.WinForms.Guna2Button();
            this.TabControlTitleLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinimizeCnt = new Guna.UI2.WinForms.Guna2ControlBox();
            this.CloseCnt = new Guna.UI2.WinForms.Guna2ControlBox();
            this.MaximizeCnt = new Guna.UI2.WinForms.Guna2ControlBox();
            this.firmNameLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.tabcontrollerPnl = new System.Windows.Forms.Panel();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.AboutBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SettingsBtn = new Guna.UI2.WinForms.Guna2Button();
            this.AccountsBtn = new Guna.UI2.WinForms.Guna2Button();
            this.RegistriesBtn = new Guna.UI2.WinForms.Guna2Button();
            this.ReturnStockBtn = new Guna.UI2.WinForms.Guna2Button();
            this.PurchasesBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SalesBtn = new Guna.UI2.WinForms.Guna2Button();
            this.StocksBtn = new Guna.UI2.WinForms.Guna2Button();
            this.StatisticsBtn = new Guna.UI2.WinForms.Guna2Button();
            this.childFormPnl = new System.Windows.Forms.Panel();
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.TitlebarPnl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabcontrollerPnl.SuspendLayout();
            this.guna2GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.DragStartTransparencyValue = 1D;
            this.guna2DragControl1.TargetControl = this.TitlebarPnl;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // TitlebarPnl
            // 
            this.TitlebarPnl.AutoSize = true;
            this.TitlebarPnl.Controls.Add(this.panel2);
            this.TitlebarPnl.Controls.Add(this.TabControlTitleLbl);
            this.TitlebarPnl.Controls.Add(this.panel1);
            this.TitlebarPnl.Controls.Add(this.firmNameLbl);
            this.TitlebarPnl.Controls.Add(this.pictureBox1);
            this.TitlebarPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitlebarPnl.Location = new System.Drawing.Point(0, 0);
            this.TitlebarPnl.Margin = new System.Windows.Forms.Padding(0);
            this.TitlebarPnl.Name = "TitlebarPnl";
            this.TitlebarPnl.Size = new System.Drawing.Size(1000, 30);
            this.TitlebarPnl.TabIndex = 2;
            this.TitlebarPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlebarPnl_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LockApplicationBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(842, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(40, 30);
            this.panel2.TabIndex = 6;
            // 
            // LockApplicationBtn
            // 
            this.LockApplicationBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.LockApplicationBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.LockApplicationBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.LockApplicationBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.LockApplicationBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LockApplicationBtn.ForeColor = System.Drawing.Color.White;
            this.LockApplicationBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo16;
            this.LockApplicationBtn.Location = new System.Drawing.Point(3, 3);
            this.LockApplicationBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.LockApplicationBtn.Name = "LockApplicationBtn";
            this.LockApplicationBtn.Size = new System.Drawing.Size(34, 24);
            this.LockApplicationBtn.TabIndex = 5;
            this.LockApplicationBtn.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // TabControlTitleLbl
            // 
            this.TabControlTitleLbl.AutoSize = true;
            this.TabControlTitleLbl.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControlTitleLbl.Location = new System.Drawing.Point(384, 5);
            this.TabControlTitleLbl.Name = "TabControlTitleLbl";
            this.TabControlTitleLbl.Size = new System.Drawing.Size(61, 23);
            this.TabControlTitleLbl.TabIndex = 4;
            this.TabControlTitleLbl.Text = "Home";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MinimizeCnt);
            this.panel1.Controls.Add(this.CloseCnt);
            this.panel1.Controls.Add(this.MaximizeCnt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(882, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(118, 30);
            this.panel1.TabIndex = 0;
            // 
            // MinimizeCnt
            // 
            this.MinimizeCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeCnt.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimizeCnt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.MinimizeCnt.IconColor = System.Drawing.Color.White;
            this.MinimizeCnt.Location = new System.Drawing.Point(10, 3);
            this.MinimizeCnt.Name = "MinimizeCnt";
            this.MinimizeCnt.Size = new System.Drawing.Size(30, 24);
            this.MinimizeCnt.TabIndex = 0;
            // 
            // CloseCnt
            // 
            this.CloseCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseCnt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.CloseCnt.IconColor = System.Drawing.Color.White;
            this.CloseCnt.Location = new System.Drawing.Point(82, 3);
            this.CloseCnt.Name = "CloseCnt";
            this.CloseCnt.Size = new System.Drawing.Size(30, 24);
            this.CloseCnt.TabIndex = 2;
            this.CloseCnt.Click += new System.EventHandler(this.CloseCnt_Click);
            // 
            // MaximizeCnt
            // 
            this.MaximizeCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeCnt.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.MaximizeCnt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.MaximizeCnt.IconColor = System.Drawing.Color.White;
            this.MaximizeCnt.Location = new System.Drawing.Point(46, 3);
            this.MaximizeCnt.Name = "MaximizeCnt";
            this.MaximizeCnt.Size = new System.Drawing.Size(30, 24);
            this.MaximizeCnt.TabIndex = 1;
            // 
            // firmNameLbl
            // 
            this.firmNameLbl.AutoSize = true;
            this.firmNameLbl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firmNameLbl.Location = new System.Drawing.Point(42, 6);
            this.firmNameLbl.Name = "firmNameLbl";
            this.firmNameLbl.Size = new System.Drawing.Size(54, 19);
            this.firmNameLbl.TabIndex = 3;
            this.firmNameLbl.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo32;
            this.pictureBox1.Location = new System.Drawing.Point(6, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 8;
            this.guna2Elipse1.TargetControl = this;
            // 
            // tabcontrollerPnl
            // 
            this.tabcontrollerPnl.Controls.Add(this.guna2GroupBox1);
            this.tabcontrollerPnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabcontrollerPnl.Location = new System.Drawing.Point(0, 30);
            this.tabcontrollerPnl.Margin = new System.Windows.Forms.Padding(0);
            this.tabcontrollerPnl.Name = "tabcontrollerPnl";
            this.tabcontrollerPnl.Size = new System.Drawing.Size(220, 620);
            this.tabcontrollerPnl.TabIndex = 0;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GroupBox1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2GroupBox1.BorderThickness = 0;
            this.guna2GroupBox1.Controls.Add(this.AboutBtn);
            this.guna2GroupBox1.Controls.Add(this.SettingsBtn);
            this.guna2GroupBox1.Controls.Add(this.AccountsBtn);
            this.guna2GroupBox1.Controls.Add(this.RegistriesBtn);
            this.guna2GroupBox1.Controls.Add(this.ReturnStockBtn);
            this.guna2GroupBox1.Controls.Add(this.PurchasesBtn);
            this.guna2GroupBox1.Controls.Add(this.SalesBtn);
            this.guna2GroupBox1.Controls.Add(this.StocksBtn);
            this.guna2GroupBox1.Controls.Add(this.StatisticsBtn);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.Transparent;
            this.guna2GroupBox1.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.guna2GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(0, 0);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(220, 620);
            this.guna2GroupBox1.TabIndex = 2;
            this.guna2GroupBox1.TextOffset = new System.Drawing.Point(10, 0);
            this.guna2GroupBox1.UseTransparentBackground = true;
            // 
            // AboutBtn
            // 
            this.AboutBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AboutBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AboutBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AboutBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AboutBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AboutBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutBtn.ForeColor = System.Drawing.Color.White;
            this.AboutBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.AboutBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AboutBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.AboutBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.AboutBtn.Location = new System.Drawing.Point(0, 575);
            this.AboutBtn.Margin = new System.Windows.Forms.Padding(0);
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(217, 45);
            this.AboutBtn.TabIndex = 10;
            this.AboutBtn.Text = "About";
            this.AboutBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SettingsBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SettingsBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SettingsBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SettingsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.SettingsBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsBtn.ForeColor = System.Drawing.Color.White;
            this.SettingsBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.SettingsBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SettingsBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.SettingsBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.SettingsBtn.Location = new System.Drawing.Point(0, 315);
            this.SettingsBtn.Margin = new System.Windows.Forms.Padding(0);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(217, 45);
            this.SettingsBtn.TabIndex = 9;
            this.SettingsBtn.Text = "Settings";
            this.SettingsBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // AccountsBtn
            // 
            this.AccountsBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AccountsBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AccountsBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AccountsBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AccountsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.AccountsBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountsBtn.ForeColor = System.Drawing.Color.White;
            this.AccountsBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.AccountsBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AccountsBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.AccountsBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.AccountsBtn.Location = new System.Drawing.Point(0, 270);
            this.AccountsBtn.Margin = new System.Windows.Forms.Padding(0);
            this.AccountsBtn.Name = "AccountsBtn";
            this.AccountsBtn.Size = new System.Drawing.Size(217, 45);
            this.AccountsBtn.TabIndex = 8;
            this.AccountsBtn.Text = "Accounts";
            this.AccountsBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AccountsBtn.Click += new System.EventHandler(this.AccountsBtn_Click);
            // 
            // RegistriesBtn
            // 
            this.RegistriesBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.RegistriesBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.RegistriesBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegistriesBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.RegistriesBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.RegistriesBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegistriesBtn.ForeColor = System.Drawing.Color.White;
            this.RegistriesBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.RegistriesBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RegistriesBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.RegistriesBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.RegistriesBtn.Location = new System.Drawing.Point(0, 225);
            this.RegistriesBtn.Margin = new System.Windows.Forms.Padding(0);
            this.RegistriesBtn.Name = "RegistriesBtn";
            this.RegistriesBtn.Size = new System.Drawing.Size(217, 45);
            this.RegistriesBtn.TabIndex = 6;
            this.RegistriesBtn.Text = "Registries";
            this.RegistriesBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RegistriesBtn.Click += new System.EventHandler(this.RegistriesBtn_Click);
            // 
            // ReturnStockBtn
            // 
            this.ReturnStockBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ReturnStockBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ReturnStockBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ReturnStockBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ReturnStockBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReturnStockBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnStockBtn.ForeColor = System.Drawing.Color.White;
            this.ReturnStockBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.ReturnStockBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ReturnStockBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.ReturnStockBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.ReturnStockBtn.Location = new System.Drawing.Point(0, 180);
            this.ReturnStockBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ReturnStockBtn.Name = "ReturnStockBtn";
            this.ReturnStockBtn.Size = new System.Drawing.Size(217, 45);
            this.ReturnStockBtn.TabIndex = 5;
            this.ReturnStockBtn.Text = "Return Stocks";
            this.ReturnStockBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ReturnStockBtn.Click += new System.EventHandler(this.ReturnStockBtn_Click);
            // 
            // PurchasesBtn
            // 
            this.PurchasesBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PurchasesBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PurchasesBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PurchasesBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PurchasesBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.PurchasesBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchasesBtn.ForeColor = System.Drawing.Color.White;
            this.PurchasesBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.PurchasesBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.PurchasesBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.PurchasesBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.PurchasesBtn.Location = new System.Drawing.Point(0, 135);
            this.PurchasesBtn.Margin = new System.Windows.Forms.Padding(0);
            this.PurchasesBtn.Name = "PurchasesBtn";
            this.PurchasesBtn.Size = new System.Drawing.Size(217, 45);
            this.PurchasesBtn.TabIndex = 4;
            this.PurchasesBtn.Text = "Purchases";
            this.PurchasesBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.PurchasesBtn.Click += new System.EventHandler(this.PurchasesBtn_Click);
            // 
            // SalesBtn
            // 
            this.SalesBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SalesBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SalesBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SalesBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SalesBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.SalesBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalesBtn.ForeColor = System.Drawing.Color.White;
            this.SalesBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.SalesBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SalesBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.SalesBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.SalesBtn.Location = new System.Drawing.Point(0, 90);
            this.SalesBtn.Margin = new System.Windows.Forms.Padding(0);
            this.SalesBtn.Name = "SalesBtn";
            this.SalesBtn.Size = new System.Drawing.Size(217, 45);
            this.SalesBtn.TabIndex = 3;
            this.SalesBtn.Text = "Sales";
            this.SalesBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SalesBtn.Click += new System.EventHandler(this.SalesBtn_Click);
            // 
            // StocksBtn
            // 
            this.StocksBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.StocksBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.StocksBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.StocksBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.StocksBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.StocksBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StocksBtn.ForeColor = System.Drawing.Color.White;
            this.StocksBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.StocksBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StocksBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.StocksBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.StocksBtn.Location = new System.Drawing.Point(0, 45);
            this.StocksBtn.Margin = new System.Windows.Forms.Padding(0);
            this.StocksBtn.Name = "StocksBtn";
            this.StocksBtn.Size = new System.Drawing.Size(217, 45);
            this.StocksBtn.TabIndex = 2;
            this.StocksBtn.Text = "Stocks";
            this.StocksBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StocksBtn.Click += new System.EventHandler(this.StocksBtn_Click);
            // 
            // StatisticsBtn
            // 
            this.StatisticsBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.StatisticsBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.StatisticsBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.StatisticsBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.StatisticsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.StatisticsBtn.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatisticsBtn.ForeColor = System.Drawing.Color.White;
            this.StatisticsBtn.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
            this.StatisticsBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StatisticsBtn.ImageOffset = new System.Drawing.Point(3, 0);
            this.StatisticsBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.StatisticsBtn.Location = new System.Drawing.Point(0, 0);
            this.StatisticsBtn.Margin = new System.Windows.Forms.Padding(0);
            this.StatisticsBtn.Name = "StatisticsBtn";
            this.StatisticsBtn.Size = new System.Drawing.Size(217, 45);
            this.StatisticsBtn.TabIndex = 1;
            this.StatisticsBtn.Text = "Statistics";
            this.StatisticsBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StatisticsBtn.Click += new System.EventHandler(this.StatisticsBtn_Click);
            // 
            // childFormPnl
            // 
            this.childFormPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childFormPnl.ForeColor = System.Drawing.Color.DarkCyan;
            this.childFormPnl.Location = new System.Drawing.Point(220, 30);
            this.childFormPnl.Margin = new System.Windows.Forms.Padding(0);
            this.childFormPnl.Name = "childFormPnl";
            this.childFormPnl.Size = new System.Drawing.Size(780, 620);
            this.childFormPnl.TabIndex = 1;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.childFormPnl);
            this.Controls.Add(this.tabcontrollerPnl);
            this.Controls.Add(this.TitlebarPnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tawheed Basit Private limited";
            this.TitlebarPnl.ResumeLayout(false);
            this.TitlebarPnl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabcontrollerPnl.ResumeLayout(false);
            this.guna2GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Panel childFormPnl;
        private System.Windows.Forms.Panel tabcontrollerPnl;
        private System.Windows.Forms.Panel TitlebarPnl;
        private Guna.UI2.WinForms.Guna2ControlBox MinimizeCnt;
        private Guna.UI2.WinForms.Guna2ControlBox MaximizeCnt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label firmNameLbl;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2Button AboutBtn;
        private Guna.UI2.WinForms.Guna2Button SettingsBtn;
        private Guna.UI2.WinForms.Guna2Button AccountsBtn;
        private Guna.UI2.WinForms.Guna2Button RegistriesBtn;
        private Guna.UI2.WinForms.Guna2Button ReturnStockBtn;
        private Guna.UI2.WinForms.Guna2Button PurchasesBtn;
        private Guna.UI2.WinForms.Guna2Button SalesBtn;
        private Guna.UI2.WinForms.Guna2Button StocksBtn;
        private Guna.UI2.WinForms.Guna2Button StatisticsBtn;
        private System.Windows.Forms.Label TabControlTitleLbl;
        private Guna.UI2.WinForms.Guna2Button LockApplicationBtn;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2ControlBox CloseCnt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}