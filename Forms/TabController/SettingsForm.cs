using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Guna.UI2.WinForms;
using System.Security.Principal;
using System.Diagnostics;

namespace TawheedBasitPvtLtd.Forms.TabController
{
    public partial class SettingsForm : Form
    {
        DataGridViewRow selectedrow = null;
        public SettingsForm()
        {
            InitializeComponent();
            PaintForm();
            List<datag> dt = new List<datag>();
            dt.Add(new datag { id = 1, name = "Example1", age = 21 });
            dt.Add(new datag { id = 2, name = "Example2", age = 22 });
            dt.Add(new datag { id = 3, name = "Example3", age = 18 });
            dt.Add(new datag { id = 4, name = "Example4", age = 35 });
            this.DGWSample.DataSource = dt;
            Configurations.ControlsConfigurations.InitializeGridView(this.DGWSample, null, null, null, -1, null, false, true);


        }
        private void DGWSample_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DGWSample.Rows[e.RowIndex].Selected = true;
                selectedrow = DGWSample.Rows[e.RowIndex];
            }
            catch(Exception exc)
            {
            }
            this.Status.Text = $"ID: {selectedrow.Cells[0].Value.ToString()} name {selectedrow.Cells[1].Value.ToString()} age {selectedrow.Cells[2].Value.ToString()}";
        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeTabController(this.ACCTabCNT, null, null, null, false, true);

            //General
            this.OrganizationName.Text = Configurations.InitializedVariables.ORGANIZATIONNAME;
            Configurations.ControlsConfigurations.InitializeLabel(this.OrganizationName, Configurations.InitializedVariables.LOGOCOLOR);

            Configurations.ControlsConfigurations.InitializeTextBox(this.FirmNameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.AboutFirmTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(this.FirmDetailsBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            

            Configurations.ControlsConfigurations.InitializeTextBox(this.SQLSERVERName, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SQLDBName, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SQLUsername, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SQLPassword, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(this.REGDBDetailsBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            Configurations.ControlsConfigurations.InitializeButton(this.ConnectToExitingDBBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.CREATENEWDBBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.AttachDB, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1);
            Configurations.ControlsConfigurations.InitializeLabel(this.StatusMessage);

            //Layouts
            Configurations.ControlsConfigurations.InitializeLabel(this.label2, Configurations.InitializedVariables.LOGOCOLOR);
            Configurations.ControlsConfigurations.InitializeButton(this.RESETBTN, null, Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.Button, null, Configurations.InitializedVariables.FRMBGRCLR);

                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNFILL, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNFOR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNBRDCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNRDS, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNTKN, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.BTNSHDSPRD, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeButton(this.BTNSAVE, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TextBox, null, Configurations.InitializedVariables.FRMBGRCLR);

                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBFILL, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBFOR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBBRDCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBPLACEHLD, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBBRDRDS, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBBRDTHK, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeTextBox(this.TXBSHDSPRD, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
                Configurations.ControlsConfigurations.InitializeButton(this.TXBSAVE, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            //grid

            Configurations.ControlsConfigurations.InitializeTextBox(this.GRIDFILL, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.GRIDFOR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.GRIDHDRHGHT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.GRIDBRDCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.GRIDHDRHGHT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(this.GRIDSAVE, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);


            Configurations.ControlsConfigurations.InitializeTextBox(this.LBLCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.FRMCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.GRPBXCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.CNTCLR, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeButton(this.MixSave, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
        }
        private void FirmDetailsBTN_Click(object sender, EventArgs e)
        {
            if(this.FirmNameTXT.Text != null && this.FirmNameTXT.Text != "")
            {
                string msg = "SUCCESS: Organization name and organization identification text has registered successfully!";
                if(this.AboutFirmTXT.Text == null || this.AboutFirmTXT.Text == "") msg = "SUCCESS: Organization name has registered successfully!";
                Configurations.ProjectConfigurations.RegisterFirmDetails(this.FirmNameTXT.Text, this.AboutFirmTXT.Text);
                Configurations.InitializedVariables.InitializeFirmInformation();
                this.StatusMessage.Text = msg;
                this.StatusMessage.ForeColor = Color.Green;
                this.FirmNameTXT.Text = null;
                this.AboutFirmTXT.Text = null;
            }
            else
            {
                this.StatusMessage.Text = "ERROR: invalid organization or firm name!";
                this.StatusMessage.ForeColor = Color.Red;
                this.FirmNameTXT.Text = null;
            }
            
        }

        private void REGDBDetailsBTN_Click(object sender, EventArgs e)
        {
            if (this.SQLSERVERName.Text != null && this.SQLSERVERName.Text != "")
            {
                bool authmtp = false;
                string tmpmsg = "SUCCESS: SQL Server, user, database names and password  were registered successfully!";
                if (this.SQLUsername.Text == null || this.SQLUsername.Text == "" || this.SQLPassword.Text == null || this.SQLPassword.Text == "")
                {
                    tmpmsg = "SUCCESS: SQL Server and database names were registered successfully!";
                    this.SQLUsername.Text = null;
                    this.SQLPassword.Text = null;
                    authmtp = true;
                }
                if (this.SQLDBName.Text == null || this.SQLDBName.Text == "")
                {
                    if(!authmtp) tmpmsg = "SUCCESS: SQL Server name user name and password were registered successfully!";
                    else tmpmsg = "SUCCESS: SQL Server name were registered successfully!";
                    this.SQLDBName.Text = null;
                }
                Configurations.DataBaseConnection.RegisterSQLServerAuth(this.SQLSERVERName.Text, this.SQLUsername.Text, this.SQLPassword.Text, this.SQLDBName.Text);
                this.StatusMessage.Text = tmpmsg;
                this.StatusMessage.ForeColor = Color.Green;
                this.SQLSERVERName.Text = null;
                this.SQLUsername.Text = null;
                this.SQLPassword.Text = null;
                this.SQLDBName.Text = null;
                Configurations.InitializedVariables.initializeDataBaseInfo();
                                
            }
            else
            {
                this.StatusMessage.Text = "ERROR: SQL Server name is invalid!";
                this.SQLSERVERName.Text = null;
            }

        }

        private void ConnectToExitingDBBTN_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, false))
                {
                    if (Configurations.DataBaseConnection.CheckDataBaseSystem(con, true) == true)
                    {
                        this.StatusMessage.Text = $"SUCCESS: Every things seems good The database contains {Configurations.DataBaseConnection.NumberTablesInDB(con)} Tables and {Configurations.DataBaseConnection.NumberOfColumnsDB(con)} columns!";
                        this.StatusMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.StatusMessage.Text = $"ERROR: No database named {Configurations.InitializedVariables.DataBaseName} were found or Tables schema is incorrect!";
                        this.StatusMessage.ForeColor = Color.Red;
                    }
                }
            }catch(Exception exc)
            {
                this.StatusMessage.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.StatusMessage.ForeColor = Color.Red;
            }
        }

        private void CREATENEWDBBTN_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, false))
                {
                    if (con != null)
                    {
                        string path = null;
                        SaveFileDialog SaveSQLDB = new SaveFileDialog();
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.SQLServerDATADefaulDirectory)) Configurations.ProjectConfigurations.createDirectory(Configurations.ConfigurationKeys.SQLServerDATADefaulDirectory);
                        SaveSQLDB.InitialDirectory = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.SQLServerDATADefaulDirectory;
                        SaveSQLDB.FileName = $"{Configurations.InitializedVariables.DataBaseName.ToUpper()}";
                        SaveSQLDB.Filter = "Master DB Files(*.mdf)|*.mdf|Log DB Files(*.ldf)|*.ldf|All files(*.*)|*.*";
                        SaveSQLDB.Title = "Where do you want to store your database!";
                        if (SaveSQLDB.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                path = System.IO.Path.GetDirectoryName(SaveSQLDB.FileName);
                                if (Configurations.DataBaseConnection.createDatabse(con, path) && Configurations.DataBaseConnection.CreateNewTables(con))
                                {
                                    this.StatusMessage.Text = $"SUCCESS: {Configurations.InitializedVariables.DataBaseName.ToUpper()} Database with {Configurations.DataBaseConnection.NumberTablesInDB(con)} Tables and {Configurations.DataBaseConnection.NumberOfColumnsDB(con)} Columns was created successfully!";
                                    this.StatusMessage.ForeColor = Color.Green;
                                }
                                else
                                {
                                    this.StatusMessage.Text = $"ERROR: Database creation was not successfull Try diffrent name or Location!";
                                    this.StatusMessage.ForeColor = Color.Red;
                                }
                            }catch(Exception exc)
                            {
                                this.StatusMessage.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                this.StatusMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            this.StatusMessage.Text = $"ERROR: Database creation was not successfull canceled by user!";
                            this.StatusMessage.ForeColor = Color.Red;
                        }
                        con.Close();
                    }
                }
            }catch(Exception exc)
            {
                this.StatusMessage.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.StatusMessage.ForeColor = Color.Red;
            }
        }

        private void AttachDB_Click(object sender, EventArgs e)
        {
            try
            {
                WindowsIdentity idnt = WindowsIdentity.GetCurrent();
                WindowsPrincipal prncpl = new WindowsPrincipal(idnt);
                bool isAdmin = prncpl.IsInRole(WindowsBuiltInRole.Administrator);
                if (isAdmin)
                {/*
                    ProcessStartInfo stinfo = new ProcessStartInfo();
                    stinfo.FileName = "TawheedBasitPvtLtd.exe";
                    stinfo.Verb = "runas";
                    Process.Start(stinfo);*/
                    bool reopenfile = true;
                    string MDFpath = null, LDFpath = null, DBName = Configurations.InitializedVariables.DataBaseName;
                    OpenFileDialog openFile = new OpenFileDialog();
                    openFile.Multiselect = true;
                    openFile.Filter = "DataBase Files(*.mdf, *.ldf)|*.mdf; *.ldf|All files(*.*)|*.*";
                    openFile.Title = "Select Master and log data files to attach the database!";
                    while (reopenfile)
                    {
                        bool dialogres = (openFile.ShowDialog() == DialogResult.OK) ? true : false;
                        reopenfile = false;
                        if (dialogres)
                        {
                            string[] filenames = openFile.FileNames;
                            foreach (string file in filenames)
                            {
                                Console.WriteLine(Path.GetExtension(file));
                                if (string.Equals(Path.GetExtension(file), ".mdf", StringComparison.OrdinalIgnoreCase))
                                {
                                    MDFpath = file;
                                }
                                else if (string.Equals(Path.GetExtension(file), ".ldf", StringComparison.OrdinalIgnoreCase))
                                {
                                    LDFpath = file;
                                }
                            }
                            try
                            {

                                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, false))
                                {
                                    if (MDFpath != null && LDFpath != null)
                                    {
                                        new SqlCommand($"USE [master]; CREATE DATABASE {DBName} ON(FILENAME = N'{MDFpath}') LOG ON (FILENAME = N'‪{LDFpath}') FOR ATTACH; ", con).ExecuteNonQuery();
                                    }
                                    else reopenfile = true;
                                }
                            }
                            catch (Exception exc)
                            {
                                this.StatusMessage.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                this.StatusMessage.ForeColor = Color.Red;
                            }
                        }
                        else if (!dialogres) reopenfile = false;

                    }
                }
                else { 
                    StatusMessage.Text = "Warning: You have to run software as administrator!";
                    this.StatusMessage.ForeColor = Color.Yellow;
                }
            }
            catch (Exception exc)
            {
                this.StatusMessage.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.StatusMessage.ForeColor = Color.Red;
            }
        }

        //button

        string btnfill = null, btnfor = null, btnborderclr = null;
        int btnshdsprd = -1, btnborderrad = -1, btnborderthik = -1;
        private void BTNFILL_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(BTNFILL.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + BTNFILL.Text; else clr = BTNFILL.Text;
                this.Button.FillColor = ColorTranslator.FromHtml(clr);
                btnfill = clr;
            }
        }

        private void BTNFOR_TextChanged(object sender, EventArgs e)
        {

            int x = Configurations.ColorConfiguration.IsValidHexColor(BTNFOR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + BTNFOR.Text; else clr = BTNFOR.Text;
                this.Button.ForeColor= ColorTranslator.FromHtml(clr);
                btnfor = clr;
            }
        }

        private void BTNBRDCLR_TextChanged(object sender, EventArgs e)
        {

            int x = Configurations.ColorConfiguration.IsValidHexColor(BTNBRDCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + BTNBRDCLR.Text; else clr = BTNBRDCLR.Text;
                this.Button.BorderColor= ColorTranslator.FromHtml(clr);
                btnborderclr = clr;
            }
        }

        private void RESETBTN_Click(object sender, EventArgs e)
        {
            Configurations.ControlsConfigurations.regDefaultControls();
            Configurations.ColorConfiguration.RegDefaultDarkColorThem();
            Configurations.ColorConfiguration.RegDefaultLightColorThem(); 
            btnfill = btnfor = btnborderclr = null;
            btnshdsprd = btnborderrad = btnborderthik = -1;
            BTNFILL.Text = BTNFOR.Text = BTNBRDCLR.Text = BTNSHDSPRD.Text = BTNBRDCLR.Text = BTNTKN.Text = "";
            tbxfill = tbxfor = tbxborderclr = tbxplaceholder = null;
            tbxshdsprd = tbxborderrad = tbxborderthik = -1;
            TXBFILL.Text = TXBFOR.Text = TXBBRDCLR.Text = TXBSHDSPRD.Text = TXBPLACEHLD.Text = TXBBRDRDS.Text = TXBBRDTHK.Text = ""; 
            lblcolor = formcolor = groupboxcolor = controlscolor = null;
            LBLCLR.Text = FRMCLR.Text = GRPBXCLR.Text = CNTCLR.Text = ""; dgwfill = dgwfor = dgwborderclr = null; dgwheadhieght = -1;
            GRIDFILL.Text = GRIDFOR.Text = GRIDBRDCLR.Text = GRIDHDRHGHT.Text = "";
        }


        private void BTNSHDSPRD_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(BTNSHDSPRD.Text, out int result))
            {
                this.Button.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(result);
                btnshdsprd = result;
            }
           

        }
        private void BTNRDS_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BTNRDS.Text, out int result))
            {
                this.Button.BorderRadius = result;
                btnborderrad = result;
            }
        }

        private void BTNTKN_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BTNTKN.Text, out int result))
            {
                this.Button.BorderThickness = result;
                btnborderthik = result;
            }
        }

        private void BTNSAVE_Click(object sender, EventArgs e)
        {
            if (btnfill != null && btnfill != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNBGRCLRKey, btnfill, Configurations.InitializedVariables.CurrentThemedFile);
            if (btnfor != null && btnfor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNFGRCLRKey, btnfor, Configurations.InitializedVariables.CurrentThemedFile);
            if (btnborderclr != null && btnborderclr != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNBRDCLRKey, btnborderclr, Configurations.InitializedVariables.CurrentThemedFile);
            if (btnshdsprd != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNSHDSPRKey, btnshdsprd.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            if (btnborderrad != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNBRDRDSKey, btnborderrad.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            if (btnborderthik != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.BTNBRDTKNKey, btnborderthik.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            btnfill = btnfor = btnborderclr = null;
            btnshdsprd = btnborderrad = btnborderthik = -1;
            BTNFILL.Text = BTNFOR.Text = BTNBRDCLR.Text = BTNSHDSPRD.Text = BTNBRDCLR.Text = BTNTKN.Text = "";
        }
        //texbox

        string tbxfill = null, tbxfor = null, tbxborderclr = null, tbxplaceholder;
        int tbxshdsprd = -1, tbxborderrad = -1, tbxborderthik = -1;
        private void TXBFILL_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(TXBFILL.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + TXBFILL.Text; else clr = TXBFILL.Text;
                this.TextBox.FillColor = ColorTranslator.FromHtml(clr);
                tbxfill = clr;
            }

        }
        private void TXBFOR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(TXBFOR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + TXBFOR.Text; else clr = TXBFOR.Text;
                this.TextBox.ForeColor = ColorTranslator.FromHtml(clr);
                tbxfor = clr;
            }
        }

        private void TXBBRDCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(TXBBRDCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + TXBBRDCLR.Text; else clr = TXBBRDCLR.Text;
                this.TextBox.BorderColor = ColorTranslator.FromHtml(clr);
                tbxborderclr = clr;
            }
        }

        private void TXBPLACEHLD_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(TXBPLACEHLD.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + TXBPLACEHLD.Text; else clr = TXBPLACEHLD.Text;
                this.TextBox.PlaceholderForeColor = ColorTranslator.FromHtml(clr);
                tbxplaceholder = clr;
            }
        }

        private void TXBBRDRDS_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TXBBRDRDS.Text, out int result))
            {
                this.TextBox.BorderRadius = result;
                tbxborderrad = result;
            }
        }

        private void TXBBRDTHK_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TXBBRDTHK.Text, out int result))
            {
                this.TextBox.BorderThickness = result;
                tbxborderthik = result;
            }
        }

        private void TXBSHDSPRD_TextChanged(object sender, EventArgs e)
        {

            if (int.TryParse(TXBSHDSPRD.Text, out int result))
            {
                this.TextBox.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(result);
                tbxshdsprd = result;
            }
        }

        private void TXBSAVE_Click(object sender, EventArgs e)
        {
            if (tbxfill != null && tbxfill != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXBGRCLRKey, tbxfill, Configurations.InitializedVariables.CurrentThemedFile);
            if (tbxfor != null && tbxfor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXFGRCLRKey, tbxfor, Configurations.InitializedVariables.CurrentThemedFile);
            if (tbxborderclr != null && tbxborderclr != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXBRDCLRKey, tbxborderclr, Configurations.InitializedVariables.CurrentThemedFile);
            if (tbxplaceholder != null && tbxplaceholder != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXPCHCLRKey, tbxplaceholder, Configurations.InitializedVariables.CurrentThemedFile);
            if (tbxshdsprd != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXSHDSPRKey, tbxshdsprd.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            if (tbxborderrad != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXBRDRDSKey, tbxborderrad.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            if (tbxborderthik != -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.TBXBRDTKNKey, tbxborderthik.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);

            tbxfill = tbxfor = tbxborderclr = tbxplaceholder = null;
            tbxshdsprd = tbxborderrad = tbxborderthik = -1;
            TXBFILL.Text = TXBFOR.Text = TXBBRDCLR.Text = TXBSHDSPRD.Text = TXBPLACEHLD.Text = TXBBRDRDS.Text = TXBBRDTHK.Text = "";
        }

        string dgwfill = null, dgwfor = null, dgwborderclr = null;
        int dgwheadhieght = -1;



        //        GRIDHDRHGHT


        private void GRIDFILL_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(GRIDFILL.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + GRIDFILL.Text; else clr = GRIDFILL.Text;
                this.DGWSample.BackgroundColor = ColorTranslator.FromHtml(clr);
                dgwfill = clr;
            }
        }

        private void GRIDFOR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(GRIDFOR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + GRIDFOR.Text; else clr = GRIDFOR.Text;
                this.DGWSample.ForeColor = ColorTranslator.FromHtml(clr);
                dgwfill = clr;
            }
        }

        private void GRIDBRDCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(GRIDBRDCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + GRIDBRDCLR.Text; else clr = GRIDBRDCLR.Text;
                this.DGWSample.GridColor = ColorTranslator.FromHtml(clr);
                dgwborderclr = clr;
            }
        }

        private void GRIDHDRHGHT_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(GRIDHDRHGHT.Text, out int result))
            {
                if(result >= 4)
                {
                    this.DGWSample.ColumnHeadersHeight = result;
                    dgwheadhieght = result;
                }
                else
                {
                    this.StatusMessage.Text = "ERROR: Header row must have height greater than or equal to four";
                }
            }
        }

        private void GRIDSAVE_Click(object sender, EventArgs e)
        {
            if (dgwfill != null && dgwfill != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.DGWBGRCLRKey, dgwfill.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (dgwfor != null && dgwfor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.DGWFGRCLRKey, dgwfor.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (dgwborderclr != null && dgwborderclr!= "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.DGWBRDCLRKey, dgwborderclr.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (dgwheadhieght!= -1) Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.DGWHDRHGTKey, dgwheadhieght.ToString(), Configurations.ConfigurationKeys.ControlConfigFileName);
            dgwfill = dgwfor = dgwborderclr = null; dgwheadhieght = -1;
            GRIDFILL.Text = GRIDFOR.Text = GRIDBRDCLR.Text = GRIDHDRHGHT.Text = "";
        }


        string lblcolor = null, formcolor = null, groupboxcolor = null, controlscolor = null;
        private void LBLCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(LBLCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + LBLCLR.Text; else clr = LBLCLR.Text;
                lblcolor = clr;
            }
        }

        private void FRMCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(FRMCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + FRMCLR.Text; else clr = FRMCLR.Text;
                this.BackColor = ColorTranslator.FromHtml(clr);
                formcolor = clr;
            }
        }

        private void GRPBXCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(GRPBXCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + GRPBXCLR.Text; else clr = GRPBXCLR.Text;
                groupboxcolor = clr;
            }
        }

        private void CNTCLR_TextChanged(object sender, EventArgs e)
        {
            int x = Configurations.ColorConfiguration.IsValidHexColor(CNTCLR.Text);
            string clr;
            if (x != -1)
            {
                if (x == 0) clr = "#" + CNTCLR.Text; else clr = CNTCLR.Text;
                controlscolor = clr;
            }
        }

        private void MixSave_Click(object sender, EventArgs e)
        {
            if (lblcolor != null && lblcolor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.LBLFGRCLRKey, lblcolor.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (formcolor != null && formcolor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.FRMBGRCLRKey, formcolor.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (groupboxcolor != null && groupboxcolor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.GRPBGRCLRKey, groupboxcolor.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            if (controlscolor != null && controlscolor != "") Configurations.ProjectConfigurations.ReplaceKeyValueConfig(Configurations.ConfigurationKeys.CNTBGRCLRKey, controlscolor.ToString(), Configurations.InitializedVariables.CurrentThemedFile);
            lblcolor = formcolor = groupboxcolor = controlscolor = null;
            LBLCLR.Text = FRMCLR.Text = GRPBXCLR.Text = CNTCLR.Text = "";
        }

    }
}
