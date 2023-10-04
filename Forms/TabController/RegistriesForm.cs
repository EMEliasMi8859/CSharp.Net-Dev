using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd.Forms.TabController
{
    
    public partial class RegistriesForm : Form
    {
        public static DataTable regGrdVDataTable = new DataTable();
        public static Form CurrentActiveForm = null;
        private static int CurrentActiveEvent = -1;
        public RegistriesForm()
        {
            InitializeComponent();
            PaintForm();
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from products;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    RegGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewPRD_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.PRDGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeButton(this.PRDBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRDTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.CusGRB, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeButton(this.CUSBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.CUSTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGridView(this.RegGrdView, null, null, null, -1, null, true, true);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.VNDGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeButton(this.VNDBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label4, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.VNDTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.TLRGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeButton(this.TLRBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label5, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TLRTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.STKHDRGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeButton(this.STHDRBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeLabel(this.label6, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.STKTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            this.RegTotal.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.SaleLBL10.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.RegStatusMessageLBL.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
        }

        private void removeCurrentActiveEvent()
        {
            RegGrdView.CellDoubleClick -= RegGrdViewPRD_CellDoubleClick;
            if (CurrentActiveEvent == 0) RegGrdView.CellDoubleClick -= RegGrdViewPRD_CellDoubleClick;
            else if (CurrentActiveEvent == 1) RegGrdView.CellDoubleClick -= RegGrdViewCUS_CellDoubleClick;
            else if (CurrentActiveEvent == 2) RegGrdView.CellDoubleClick -= RegGrdViewVND_CellDoubleClick;
            else if (CurrentActiveEvent == 3) RegGrdView.CellDoubleClick -= RegGrdViewTLR_CellDoubleClick;
            else if (CurrentActiveEvent == 4) RegGrdView.CellDoubleClick -= RegGrdViewSTK_CellDoubleClick;
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (CurrentActiveForm != null)
            {
                CurrentActiveForm.Dispose();
                CurrentActiveForm.Close();
            }
            CurrentActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.REGChildForms.Controls.Add(childForm);
            this.REGChildForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            RegistriesForms.ProductRegForm productregform = new RegistriesForms.ProductRegForm(true);
            productregform.RCRDUpdated -= guna2TextBox1_Enter;
            productregform.RCRDUpdated += guna2TextBox1_Enter;
            OpenChildForm(productregform, sender);
        }
        private void guna2TextBox1_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from products;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewPRD_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void PRDTBXFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if(this.PRDTBXFilter.Text.Length == 0)
                        cmd = new SqlCommand($"Select * from Products;", con);
                    else
                    {
                        if(int.TryParse(this.PRDTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Products where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Products where Name Like '%{PRDTBXFilter.Text.ToString()}%' or CompanyName Like '%{PRDTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void RegGrdViewPRD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RegistriesForms.ProductRegForm productregform = new RegistriesForms.ProductRegForm(false);
            OpenChildForm(productregform, sender);

            try
            {
                this.RegGrdView.Rows[e.RowIndex].Selected = true;
                DataGridViewRow tmp = RegGrdView.Rows[e.RowIndex];
                this.pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.PRODUCTTABLEIMAGES + "\\" + tmp.Cells["Image"].Value.ToString();
                RegistriesForms.ProductRegForm regprd = new RegistriesForms.ProductRegForm(false, tmp);
                regprd.RCRDUpdated -= guna2TextBox1_Enter;
                regprd.RCRDUpdated += guna2TextBox1_Enter;
                OpenChildForm(regprd, sender);
            }
            catch (Exception exc) { this.RegStatusMessageLBL.Text = "ERROR: Index out of range you selected unavailable row!"; this.RegStatusMessageLBL.ForeColor = Color.Red; }

        }

        private void CUSBTN_Click(object sender, EventArgs e)
        {
            RegistriesForms.RegCustomerForm regcustomerform = new RegistriesForms.RegCustomerForm(true);
            regcustomerform.RCRDUpdated -= CUSTBXFilter_Enter;
            regcustomerform.RCRDUpdated += CUSTBXFilter_Enter;
            OpenChildForm(regcustomerform, sender);
        }
        private void CUSTBXFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Customers;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 1;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewCUS_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void CUSTBXFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.CUSTBXFilter.Text.Length == 0)
                        cmd = new SqlCommand($"Select * from Customers;", con);
                    else
                    {
                        if (int.TryParse(this.CUSTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Customers where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Customers where Name Like '%{CUSTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void RegGrdViewCUS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RegistriesForms.RegCustomerForm regcustomerform = new RegistriesForms.RegCustomerForm(false);
            OpenChildForm(regcustomerform, sender);
            try
            {
                this.RegGrdView.Rows[e.RowIndex].Selected = true;
                DataGridViewRow tmp = RegGrdView.Rows[e.RowIndex];
                this.pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.CUSTOMERTABLEIMAGES + "\\" + tmp.Cells["Image"].Value.ToString();
                RegistriesForms.RegCustomerForm regcus = new RegistriesForms.RegCustomerForm(false, tmp);
                regcus.RCRDUpdated -= CUSTBXFilter_Enter;
                regcus.RCRDUpdated += CUSTBXFilter_Enter;
                OpenChildForm(regcus, sender);
            }
            catch (Exception exc) { this.RegStatusMessageLBL.Text = "ERROR: Index out of range you selected unavailable row!"; this.RegStatusMessageLBL.ForeColor = Color.Red; }

        }

        private void VNDBTN_Click(object sender, EventArgs e)
        {
            RegistriesForms.RegVendorForm regvendorform = new RegistriesForms.RegVendorForm(true);
            regvendorform.RCRDUpdated -= VNDTBXFilter_Enter;
            regvendorform.RCRDUpdated += VNDTBXFilter_Enter;
            OpenChildForm(regvendorform, sender);
        }
        private void VNDTBXFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Vendors;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                    dataTable.AcceptChanges();
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 2;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewVND_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void VNDTBXFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.VNDTBXFilter.Text.Length == 0)
                        cmd = new SqlCommand($"Select * from Vendors;", con);
                    else
                    {
                        if (int.TryParse(this.VNDTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Vendors where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Vendors where Name Like '%{VNDTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void RegGrdViewVND_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RegistriesForms.RegVendorForm regvendorform = new RegistriesForms.RegVendorForm(false);
            regvendorform.RCRDUpdated -= VNDTBXFilter_Enter;
            regvendorform.RCRDUpdated += VNDTBXFilter_Enter;
            OpenChildForm(regvendorform, sender);
            try
            {
                this.RegGrdView.Rows[e.RowIndex].Selected = true;
                DataGridViewRow tmp = RegGrdView.Rows[e.RowIndex];
                this.pictureBox1.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
                RegistriesForms.RegVendorForm regvnd = new RegistriesForms.RegVendorForm(false, tmp);
                regvnd.RCRDUpdated -= VNDTBXFilter_Enter;
                regvnd.RCRDUpdated += VNDTBXFilter_Enter;
                OpenChildForm(regvnd, sender);
            }
            catch (Exception exc) { this.RegStatusMessageLBL.Text = "ERROR: Index out of range you selected unavailable row!"; this.RegStatusMessageLBL.ForeColor = Color.Red; }
        }

        private void TLRBTN_Click(object sender, EventArgs e)
        {
            RegistriesForms.RegTellerForm regtellerform = new RegistriesForms.RegTellerForm(true);
            regtellerform.RCRDUpdated -= TLRTBXFilter_Enter;
            regtellerform.RCRDUpdated += TLRTBXFilter_Enter;
            OpenChildForm(regtellerform, sender);
        }
        private void TLRTBXFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Tellers;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    dataTable.AcceptChanges();
                    RegGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 3;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewTLR_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void TLRTBXFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.TLRTBXFilter.Text.Length == 0)
                        cmd = new SqlCommand($"Select * from Tellers;", con);
                    else
                    {
                        if (int.TryParse(this.TLRTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Tellers where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Tellers where Name Like '%{TLRTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void RegGrdViewTLR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RegistriesForms.RegTellerForm regtellerform = new RegistriesForms.RegTellerForm(false);
            OpenChildForm(regtellerform, sender);
            try
            {
                this.RegGrdView.Rows[e.RowIndex].Selected = true;
                DataGridViewRow tmp = RegGrdView.Rows[e.RowIndex];
                this.pictureBox1.Image = global::TawheedBasitPvtLtd.Properties.Resources.TBLogo120;
                RegistriesForms.RegTellerForm regtlr = new RegistriesForms.RegTellerForm(false, tmp);
                regtlr.RCRDUpdated -= TLRTBXFilter_Enter;
                regtlr.RCRDUpdated += TLRTBXFilter_Enter;
                OpenChildForm(regtlr, sender);
            }
            catch (Exception exc) { this.RegStatusMessageLBL.Text = "ERROR: Index out of range you selected unavailable row!"; this.RegStatusMessageLBL.ForeColor = Color.Red; }
        }
        
        private void STHDRBTN_Click(object sender, EventArgs e)
        {
            RegistriesForms.RegStockHolderForm regstockholderform = new RegistriesForms.RegStockHolderForm(true);
            regstockholderform.RCRDUpdated -= STKTBXFilter_Enter;
            regstockholderform.RCRDUpdated += STKTBXFilter_Enter;
            OpenChildForm(regstockholderform, sender);
        }
        private void STKTBXFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from StockHolders;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 4;
                    RegGrdView.CellDoubleClick += new DataGridViewCellEventHandler(RegGrdViewSTK_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void STKTBXFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.TLRTBXFilter.Text.Length == 0)
                        cmd = new SqlCommand($"Select * from StockHolders;", con);
                    else
                    {
                        if (int.TryParse(this.TLRTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from StockHolders where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from StockHolders where Name Like '%{TLRTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.Fill(regGrdVDataTable);
                    RegGrdView.DataSource = dataTable;
                }
            }
            catch (Exception exc)
            {
                this.RegStatusMessageLBL.Text = "ERROR: SQL server connection problem start the server if it is not running!";
                this.RegStatusMessageLBL.ForeColor = Color.Red;
            }

        }        
        private void RegGrdViewSTK_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RegistriesForms.RegStockHolderForm regstockholderform = new RegistriesForms.RegStockHolderForm(false);
            OpenChildForm(regstockholderform, sender);
            try
            {
                this.RegGrdView.Rows[e.RowIndex].Selected = true;
                DataGridViewRow tmp = RegGrdView.Rows[e.RowIndex];
                this.pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.STOCKHOLDERTABLEIMAGES + "\\" + tmp.Cells["Image"].Value.ToString();
                RegistriesForms.RegStockHolderForm regstk = new RegistriesForms.RegStockHolderForm(false, tmp);
                regstk.RCRDUpdated -= STKTBXFilter_Enter;
                regstk.RCRDUpdated += STKTBXFilter_Enter;
                OpenChildForm(regstk, sender);
            }
            catch (Exception exc) { this.RegStatusMessageLBL.Text = "ERROR: Index out of range you selected unavailable row!"; this.RegStatusMessageLBL.ForeColor = Color.Red; }
        }

    }
}
