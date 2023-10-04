using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace TawheedBasitPvtLtd.Forms.TabController
{
    public partial class SalesForm : Form
    {
        bool isNew = true;
        private static int CurrentActiveEvent = -1;
        int BELLNo = -1, PRDID = -1, CUSID = -1, Quantity = -1, TELLERID = -1, HAWALANo = -1, TLRTRNSID = -1, RemStocks = -1;
        decimal Price = -1,  tmptopay = -1, topay = -1, payin = -1, ExCHRateM = 1; DateTime date = DateTime.Today; string CURRENCY = "AFG", PRDCurrency = null, priceCurr = null;
        public SalesForm()
        {
            InitializeComponent();
            PaintForm();
            Configurations.ControlsConfigurations.InitializeGridView(this.SalesGrdView, null, null, null, -1, null, true, true);
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Sales;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    SalesGrdView.CellDoubleClick += new DataGridViewCellEventHandler(SalesGrdViewSales_CellDoubleClick);
                }
            }catch(Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            /*Configurations.ControlsConfigurations.InitializeControls(this.CloseSalesBTN, Configurations.InitializedVariables.FRMBGRCLR, Configurations.InitializedVariables.FRMBGRCLR, true);*/
            Configurations.ControlsConfigurations.InitializeGroupBox(this.ProductGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRDFilterTBX, null, Configurations.InitializedVariables.FRMBGRCLR, - 1, -1, true);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.CustomerGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.CUSFilterTBX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.SaleGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SaleFilterTBX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.isRegular, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.isRegular.CheckedState.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR);
            this.isRegular.CheckedState.InnerColor = Color.White;
            this.isRegular.Checked = true;
            Configurations.ControlsConfigurations.InitializeLabel(this.SaleLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.SaleLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PRDCURR, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PRDPRICE, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.SaleLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.SaleLBL4, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SaleTBX1, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SaleTBX2, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SaleTBX3, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.SaleTBX4, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHRate, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.PaymentGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL4, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PiceCurrncy, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PMTTBX1, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PMTTBX2, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.SaleCurrencyTXT.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.SaleCurrencyTXT.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.SaleCurrencyTXT.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.SaleCurrencyTXT.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            this.PaymentDate.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.PaymentDate.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.PaymentDate.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.PaymentDate.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.PMTBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.UpdatePMTBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.TellerGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TellerTBXFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL3, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERTBX1, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERTBX2, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERTBX3, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            this.SalesRemainingStock.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.SaleLBL10.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.SaleStatusMessageLBL.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.SalePrintBell, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, 3, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.TELLERSwitch, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.TELLERSwitch.CheckedState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR), .3);
            this.TELLERSwitch.CheckedState.InnerColor = Color.White;
            this.TELLERSwitch.Checked = false;
        }
        private void removeCurrentActiveEvent()
        {
            SalesGrdView.CellDoubleClick -= SalesGrdViewSales_CellDoubleClick;
            if(CurrentActiveEvent == 0) SalesGrdView.CellDoubleClick -= SalesGrdViewPRD_CellDoubleClick;
            else if(CurrentActiveEvent == 1) SalesGrdView.CellDoubleClick -= SalesGrdViewCus_CellDoubleClick;
            else if(CurrentActiveEvent == 2) SalesGrdView.CellDoubleClick -= SalesGrdViewSales_CellDoubleClick;
            else if(CurrentActiveEvent == 3) SalesGrdView.CellDoubleClick -= SalesGrdViewTeller_CellDoubleClick;
        }
        private void CUSFilterTBX_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Customers;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 1;
                    SalesGrdView.CellDoubleClick += new DataGridViewCellEventHandler(SalesGrdViewCus_CellDoubleClick);
                    CUSID = -1;
                    this.SaleTBX2.Text = "";
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void CUSFilterTBX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = null;
                    if(this.CUSFilterTBX.Text.Length == 0) cmd = new SqlCommand($"Select * from Customers;", con);
                    else
                    {
                        if (int.TryParse(this.CUSFilterTBX.Text.ToString(), out int res)) cmd = new SqlCommand($"Select * from Customers where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Customers where Name Like '%{this.CUSFilterTBX.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    if(SalesGrdView.Rows.Count == 1)
                    {
                        if (this.isRegular.Checked == true)
                        {
                            this.SalesGrdView.Rows[0].Selected = true;
                            string tmp = SalesGrdView.Rows[0].Cells["ID"].Value.ToString();
                            this.SaleTBX2.Text = tmp;
                            CUSID = int.Parse(tmp);
                        }
                    }
                    else CUSID = -1;
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void SalesGrdViewCus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.isRegular.Checked == true)
                {
                    this.SalesGrdView.Rows[e.RowIndex].Selected = true;
                    string tmp = SalesGrdView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    this.SaleTBX2.Text = tmp;
                    CUSID = int.Parse(tmp);
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }

        }
        private void PRDFilterTBX_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.PRDFilterTBX.Text.Length == 0) cmd = new SqlCommand($"Select * from Products;", con);
                    else
                    {
                        if (decimal.TryParse(this.PRDFilterTBX.Text, out decimal res)) cmd = new SqlCommand($"Select * from Products where ID like '%{res}%' or Price like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Products where Name Like '%{this.PRDFilterTBX.Text.ToString()}%' or CompanyName Like '%{this.PRDFilterTBX.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    SalesGrdView.CellDoubleClick += new DataGridViewCellEventHandler(SalesGrdViewPRD_CellDoubleClick);
                    PRDID = -1;
                    this.SaleTBX1.Text = "";
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void PRDFilterTBX_TextChanged(object sender, EventArgs e)
        {

            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (decimal.TryParse(this.PRDFilterTBX.Text, out decimal res)) cmd = new SqlCommand($"Select * from Products where ID like '%{res}%' or Price like '%{res}%';", con);
                    else cmd = new SqlCommand($"Select * from Products where Name Like '%{this.PRDFilterTBX.Text.ToString()}%' or CompanyName Like '%{this.PRDFilterTBX.Text.ToString()}%';", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    if(SalesGrdView.Rows.Count == 1)
                    {
                        this.SalesGrdView.Rows[0].Selected = true;
                        string tmp = SalesGrdView.Rows[0].Cells["ID"].Value.ToString();
                        this.SaleTBX1.Text = tmp;
                        PRDID = int.Parse(tmp);
                        tmp = SalesGrdView.Rows[0].Cells["Currency"].Value.ToString();
                        PRDCurrency = tmp;
                    }
                    else
                    {
                        PRDID = -1;
                        PRDCurrency = null;
                    }
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void SalesGrdViewPRD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SalesGrdView.Rows[e.RowIndex].Selected = true;
                string tmp = SalesGrdView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                this.SaleTBX1.Text = tmp;
                PRDID = int.Parse(tmp);
                tmp = SalesGrdView.Rows[e.RowIndex].Cells["Currency"].Value.ToString();
                PRDCurrency = tmp;
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }


        }
        private void SaleFilterTBX_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.isRegular.Checked == true)
                    {
                        if (this.SaleFilterTBX.Text.Length == 0) cmd = new SqlCommand($"SELECT * FROM Sales", con);
                        else if (int.TryParse(this.SaleFilterTBX.Text, out int res)) cmd = new SqlCommand($"Select * from Sales where BellNo like '%{res}%';", con);
                        else cmd = new SqlCommand($"SELECT * FROM Sales", con);
                    }
                    else
                    {
                        if (this.SaleFilterTBX.Text.Length == 0) cmd = new SqlCommand($"SELECT * FROM NRegCusSales", con);
                        else if (int.TryParse(this.SaleFilterTBX.Text, out int res)) cmd = new SqlCommand($"Select * from NRegCusSales where BellNo like '%{res}%';", con);
                        else cmd = new SqlCommand($"SELECT * FROM NRegCusSales", con);
                    }
                    
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable; 
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 2;
                    SalesGrdView.CellDoubleClick += new DataGridViewCellEventHandler(SalesGrdViewSales_CellDoubleClick);
                    BELLNo = -1;
                    this.TELLERTBX3.Text = "";
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void SaleFilterTBX_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (this.isRegular.Checked == true) cmd = new SqlCommand($"Select * from Sales;", con);
                    else cmd = new SqlCommand($"Select * from NRegCusSales;", con);
                    if (this.SaleFilterTBX.Text.Length == 0)
                    {
                        isNew = true;
                        resetCells();
                    }
                    else
                    {
                        if (int.TryParse(this.SaleFilterTBX.Text, out int res))
                        {
                            if (this.isRegular.Checked == true) cmd = new SqlCommand($"Select * from Sales where BellNo like '%{res}%';", con);
                            else cmd = new SqlCommand($"Select * from NRegCusSales where BellNo like '%{res}%';", con);
                        }
                        else
                        {
                            this.SaleStatusMessageLBL.Text = "ERROR: Bell no is out of range or you entered an invalid bell no!";
                            this.SaleStatusMessageLBL.ForeColor = Color.Red;
                        }
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
/*
                    if (SalesGrdView.Rows.Count == 1 && this.SaleFilterTBX.Text.Length != 0)
                    {
                        int RowIndex = 0;
                        try
                        {
                            isNew = false;
                            this.SalesGrdView.Rows[RowIndex].Selected = true;
                            string tmp = SalesGrdView.Rows[RowIndex].Cells["BellNo"].Value.ToString();
                            this.TELLERTBX3.Text = tmp;
                            BELLNo = int.Parse(tmp);
                            if (this.isRegular.Checked == true)
                            {
                                tmp = SalesGrdView.Rows[RowIndex].Cells["CusID"].Value.ToString();
                                this.SaleTBX2.Text = tmp;
                                CUSID = int.Parse(tmp);
                            }
                            else CUSID = -1;
                            tmp = SalesGrdView.Rows[RowIndex].Cells["PrdID"].Value.ToString();
                            this.SaleTBX1.Text = tmp;
                            PRDID = int.Parse(tmp);
                            tmp = SalesGrdView.Rows[RowIndex].Cells["Quantity"].Value.ToString();
                            this.SaleTBX3.Text = tmp;
                            Quantity = int.Parse(tmp);
                            tmp = SalesGrdView.Rows[RowIndex].Cells["Price"].Value.ToString();
                            this.SaleTBX4.Text = tmp;
                            Price = decimal.Parse(tmp);
                            tmp = SalesGrdView.Rows[RowIndex].Cells["Currency"].Value.ToString();
                            this.PiceCurrncy.Text = tmp;
                            decimal tmp1 = 0, tmp2 = 0, tmp3 = 0;
                            int returnQuantity = 0;
                            object obj;
                            try
                            {
                                if (this.isRegular.Checked == true)
                                    cmd = new SqlCommand($"select sum (Payin / Echangerate) as res from SalesPayments where BellNo = '{BELLNo}';", con);
                                else cmd = new SqlCommand($"select sum (Payin / Echangerate) as res from NRegCusSalesPayments where BellNo = '{BELLNo}';", con);
                                obj = cmd.ExecuteScalar();
                                try { tmp1 = obj != DBNull.Value ? decimal.Parse(obj.ToString()) : 0; } catch (Exception) { }
                                if (this.isRegular.Checked == true)
                                    cmd = new SqlCommand($"select Quantity from ReturnStocks where BellNo = '{BELLNo}';", con);
                                else cmd = new SqlCommand($"select Quantity from NRegCusReturnStocks where BellNo = '{BELLNo}';", con);
                                obj = cmd.ExecuteScalar();
                                try { returnQuantity = obj != DBNull.Value ? int.Parse(obj.ToString()) : 0; } catch (Exception) { }
                                
                                if (this.isRegular.Checked == true)
                                    cmd = new SqlCommand($"select sum(Payin / Echangerate) as payin, sum(Payout / Echangerate) as payout from ReturnStocksPayments where BellNo = '{BELLNo}';", con);
                                else cmd = new SqlCommand($"select sum(Payin / Echangerate) as payin, sum(Payout / Echangerate) as payout from NRegCusReturnStocksPayments where BellNo = '{BELLNo}';", con);
                                SqlDataReader reader1 = cmd.ExecuteReader();
                                while (reader1.Read())
                                {
                                    try
                                    {
                                        tmp2 = reader1.IsDBNull(reader1.GetOrdinal("payin")) ? 0 : reader1.GetDecimal(reader1.GetOrdinal("payin"));
                                    }
                                    catch (Exception) { }
                                    try
                                    {
                                        tmp3 = reader1.IsDBNull(reader1.GetOrdinal("payout")) ? 0 : reader1.GetDecimal(reader1.GetOrdinal("payout"));
                                    }
                                    catch (Exception) { }
                                }
                                reader1.Close();
                                topay = ((Quantity - returnQuantity) * Price) - tmp1 + tmp2 - tmp3;
                                tmptopay = topay;
                                PMTTBX1_TextChanged(this, EventArgs.Empty);
                            }
                            catch (Exception exc)
                            {
                                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                this.SaleStatusMessageLBL.ForeColor = Color.Red;
                            }
                        }
                        catch (Exception exc)
                        {
                            this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                            this.SaleStatusMessageLBL.ForeColor = Color.Red;
                        }

                    }
                    else resetCells();*/
                }
            }catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }

        }
        private void SalesGrdViewSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                isNew = false;
                this.SalesGrdView.Rows[e.RowIndex].Selected = true;
                string tmp = SalesGrdView.Rows[e.RowIndex].Cells["BellNo"].Value.ToString();
                this.TELLERTBX3.Text = tmp;
                BELLNo = int.Parse(tmp);
                if (this.isRegular.Checked == true)
                {
                    tmp = SalesGrdView.Rows[e.RowIndex].Cells["CusID"].Value.ToString();
                    this.SaleTBX2.Text = tmp;
                    CUSID = int.Parse(tmp);
                } else CUSID = -1;
                tmp = SalesGrdView.Rows[e.RowIndex].Cells["PrdID"].Value.ToString();
                this.SaleTBX1.Text = tmp;
                PRDID = int.Parse(tmp);
                tmp = SalesGrdView.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                this.SaleTBX3.Text = tmp;
                Quantity = int.Parse(tmp);
                tmp = SalesGrdView.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                this.SaleTBX4.Text = tmp;
                Price = decimal.Parse(tmp);
                this.SaleFilterTBX.Text = "";
                tmp = SalesGrdView.Rows[e.RowIndex].Cells["Currency"].Value.ToString();
                this.PiceCurrncy.Text = tmp;
                this.SaleFilterTBX.Text = "";
                decimal tmp1 = 0, tmp2 = 1;
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (this.isRegular.Checked == true)
                        cmd = new SqlCommand($"select sum (Payin / Echangerate) as res from SalesPayments where BellNo = '{BELLNo}';", con);
                    else cmd = new SqlCommand($"select sum (Payin / Echangerate) as res from NRegCusSalesPayments where BellNo = '{BELLNo}';", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tmp1 = reader.IsDBNull(reader.GetOrdinal("res")) ? 0 : reader.GetDecimal(reader.GetOrdinal("res"));
                    }
                    reader.Close();
                    if (this.isRegular.Checked == true)
                        cmd = new SqlCommand($"select Echangerate as res from Sales where BellNo = '{BELLNo}';", con);
                    else cmd = new SqlCommand($"select Echangerate as res from NRegCusSales where BellNo = '{BELLNo}';", con);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        tmp2 = reader1.IsDBNull(reader1.GetOrdinal("res")) ? 1 : reader1.GetDecimal(reader1.GetOrdinal("res"));
                    }
                    reader1.Close();
                    topay = (Quantity * Price) - tmp1;
                    tmptopay = topay;
                    PMTTBX1_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }

        }
        private void TellerTBXFilter_Enter(object sender, EventArgs e)
    {
            this.TELLERSwitch.Checked = true;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {

                    SqlCommand cmd;
                    if(TellerTBXFilter.Text.Length == 0) cmd = new SqlCommand($"Select * from Tellers;", con);
                    else
                    {
                        if (int.TryParse(this.TellerTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Tellers where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Tellers where Name like '%{TellerTBXFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 3;
                    SalesGrdView.CellDoubleClick += new DataGridViewCellEventHandler(SalesGrdViewTeller_CellDoubleClick);
                    TELLERID = -1;
                    this.TELLERTBX1.Text = "";
                }
            }catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
}
        private void TellerTBXFilter_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (int.TryParse(this.TellerTBXFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Tellers where ID like '%{res}%';", con);
                    else cmd = new SqlCommand($"Select * from Tellers where Name like '%{TellerTBXFilter.Text}%';", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    SalesGrdView.DataSource = null;
                    SalesGrdView.DataSource = dataTable;
                    if (SalesGrdView.Rows.Count == 1)
                    {
                        this.SalesGrdView.Rows[0].Selected = true;
                        string tmp = SalesGrdView.Rows[0].Cells["ID"].Value.ToString();
                        this.TELLERTBX1.Text = tmp;
                        TELLERID = int.Parse(tmp);
                    }
                    else TELLERID = -1;
                }
            }catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void SaleTBX3_TextChanged(object sender, EventArgs e)
        {
            if (SaleTBX3.Text.Length != 0)
            {
                if (int.TryParse(SaleTBX3.Text.ToString(), out int res))
                {
                    if(RemStocks != -1)
                    {
                        if (res < 0) SaleTBX3.Text = "0";
                        if (res > RemStocks) SaleTBX3.Text = RemStocks.ToString();
                    }
                    Quantity = res;
                }
            }else
                Quantity = -1;
            if (Quantity == -1)
                this.SalesRemainingStock.Text = RemStocks == -1 ? "0" : RemStocks.ToString();
            else
                this.SalesRemainingStock.Text = isNew ? RemStocks == -1 ? "0" : (RemStocks - Quantity).ToString() : RemStocks == -1 ? "0" : RemStocks.ToString();
            PMTTBX1_TextChanged(this, EventArgs.Empty);
        }
        private void PMTTBX2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal tmp=0, tmp2=1;
                if (Quantity == -1) Quantity = 0;
                if (Price == -1) Price = 0;
                if (payin == -1) payin = 0;
                if (ExCHRateM == -1) ExCHRateM = 1;
                topay = isNew ? (Quantity * Price ) - payin :(Quantity * Price * ExCHRateM) - payin;
                this.PMTTBX2.Text = topay.ToString();
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void PMTTBX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void SaleTBX1_TextChanged(object sender, EventArgs e)
        {
            if(this.SaleTBX1.Text.Length > 0)
            {
                if (int.TryParse(this.SaleTBX1.Text, out int res))
                {
                    try
                    {
                        using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                        {
                            string tmp = "";
                            decimal tmp2 = 0;
                            int a = 0, b = 0, c = 0, d = 0, f = 0;
                            SqlCommand cmd = new SqlCommand($"select Currency as res, Price as prs from Products where ID = '{res}';", con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                tmp = reader.IsDBNull(reader.GetOrdinal("res")) ? "AFG" : reader.GetString(reader.GetOrdinal("res"));
                                tmp2 = reader.IsDBNull(reader.GetOrdinal("prs")) ? 0 : reader.GetDecimal(reader.GetOrdinal("prs"));
                            }
                            reader.Close();
                            object tp = new SqlCommand($"select sum(Quantity) as purch from purchases where PrdID = {res};", con).ExecuteScalar();
                            a = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                            tp = new SqlCommand($"select sum(Quantity) as sale from sales where PrdID = {res};", con).ExecuteScalar();
                            b = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                            tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from ReturnStocks rs join sales s on rs.BellNo = s.BellNo where s.PrdID = {res};", con).ExecuteScalar();
                            c = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                            tp = new SqlCommand($"select sum(Quantity) as nregsale from NRegCusSales where PrdID = {res};", con).ExecuteScalar();
                            d = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                            tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from NRegCusReturnStocks rs join NRegCusSales s on rs.BellNo = s.BellNo where s.PrdID = {res};", con).ExecuteScalar();
                            f = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                            RemStocks = a - b - d + c + f;
                            this.SalesRemainingStock.Text = RemStocks.ToString();
                            PRDCurrency = tmp;
                            this.PRDCURR.Text = tmp;
                            this.PRDPRICE.Text = tmp2.ToString();
                        }
                    }
                    catch (Exception exc)
                    {
                        this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                        this.SaleStatusMessageLBL.ForeColor = Color.Red;
                    }
                    PRDID = res;
                }
                else PRDID = -1;
            }
        }
        private void SaleTBX2_TextChanged(object sender, EventArgs e)
        {

            if (this.SaleTBX2.Text.Length > 0)
            {
                if (int.TryParse(this.SaleTBX2.Text, out int res))
                    CUSID = res;
                else CUSID = -1;
            }
        }

        private void isRegular_CheckedChanged(object sender, EventArgs e)
        {
            if(this.isRegular.Checked == true)
            {
                this.SaleTBX2.KeyPress -= PMTTBX2_KeyPress;
                this.CUSFilterTBX.KeyPress -= PMTTBX2_KeyPress;
            }
            else
            {
                this.CUSFilterTBX.KeyPress += PMTTBX2_KeyPress;
                this.SaleTBX2.KeyPress += PMTTBX2_KeyPress;
            }
            SaleFilterTBX_TextChanged(this, EventArgs.Empty);
        }

        private void SaleTBX4_TextChanged(object sender, EventArgs e)
        {

            if (SaleTBX4.Text.Length != 0)
            {
                if (decimal.TryParse(SaleTBX4.Text.ToString(), out decimal res))
                {
                    Price = res;
                }
            }else
            {
               Price = -1;
            }
            PMTTBX1_TextChanged(this, EventArgs.Empty);
        }

        private void SaleCurrencyTXT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isNew) this.PiceCurrncy.Text = SaleCurrencyTXT.Text;
            CURRENCY = this.SaleCurrencyTXT.Text;
        }
        private void SalesGrdViewTeller_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SalesGrdView.Rows[e.RowIndex].Selected = true;
                string tmp = SalesGrdView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                this.TELLERTBX1.Text = tmp;
                TELLERID = int.Parse(tmp);
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void PMTTBX1_TextChanged(object sender, EventArgs e)
        {
            payin = 0;
            if (Quantity == -1) Quantity = 0;
            if (Price == -1) Price = 0;
            if (PMTTBX1.Text.Length != 0)
            {
                if (decimal.TryParse(PMTTBX1.Text.ToString(), out decimal res))
                {
                    if (res < 0) this.PMTTBX1.Text = "";
                    if (topay != -1 && res > topay * ExCHRateM)
                    {
                        payin = res = (topay * ExCHRateM);
                        tmptopay = res / ExCHRateM;
                        this.PMTTBX1.Text = res.ToString();
                    }
                    else if (topay == -1 && res > Quantity * Price) { res = Quantity * Price; this.PMTTBX1.Text = res.ToString(); }
                    payin = res;
                }
            }
            if(Quantity != -1 && Price != -1 && topay == -1) tmptopay = isNew ? ((Quantity * Price) - payin) : ((Quantity * Price * ExCHRateM) - payin);
            else if (topay == -1) tmptopay = isNew ? ((Quantity * Price) - payin): ((Quantity * Price * ExCHRateM) - payin);
            else tmptopay = isNew ? (topay - payin) : ((topay * ExCHRateM) - payin);
            this.PMTTBX2.Text = tmptopay.ToString();
            if (this.PMTTBX1.Text.Length == 0) { payin = -1; }
        }    
        private void SaleCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaleCurrencyTXT.Text.Length == 0)
                CURRENCY = "AFG";
            else
                CURRENCY = this.SaleCurrencyTXT.Text.ToString();
        }
        private void PaymentDate_ValueChanged(object sender, EventArgs e)
        {
            date = this.PaymentDate.MaxDate;
        }
        private void TELLERTBX1_TextChanged(object sender, EventArgs e)
        {
            this.TELLERSwitch.Checked = true;
            if(this.TELLERTBX1.Text.Length == 0)
                TELLERID = -1;
            else
            {
                if(int.TryParse(this.TELLERTBX1.Text.ToString(), out int res))
                    TELLERID = res;
                else
                {
                    TELLERID = -1;
                    this.SaleStatusMessageLBL.Text = "ERROR: Invalid Teller ID Detected try a valid teller ID!";
                    this.SaleStatusMessageLBL.ForeColor = Color.Red;
                }
            }
        }
        private void TELLERTBX2_TextChanged(object sender, EventArgs e)
        {
            if (this.TELLERTBX2.Text.Length == 0)
                HAWALANo = -1;
            else
            {
                if (int.TryParse(this.TELLERTBX2.Text.ToString(), out int res))
                    HAWALANo = res;
                else
                {
                    HAWALANo = -1;
                    this.SaleStatusMessageLBL.Text = "ERROR: Invalid Hawala number Detected try a valid hawala number!";
                    this.SaleStatusMessageLBL.ForeColor = Color.Red;
                }
            }
        }
        private void TELLERTBX3_TextChanged(object sender, EventArgs e)
        {
            if (this.TELLERTBX3.Text.Length == 0)
                BELLNo = -1;
            else
            {
                if (int.TryParse(this.TELLERTBX3.Text.ToString(), out int res))
                    BELLNo = res;
                else
                {
                    BELLNo = -1;
                    this.SaleStatusMessageLBL.Text = "ERROR: Invalid Bell number Detected try a valid Bell number!";
                    this.SaleStatusMessageLBL.ForeColor = Color.Red;
                }
            }
        }
        private void EXCHRate_TextChanged(object sender, EventArgs e)
        {
            if (this.EXCHRate.Text.Length == 0)
                ExCHRateM = 1;
            else
            {
                if (decimal.TryParse(this.EXCHRate.Text.ToString(), out decimal res))
                {
                    if (res < 0) this.EXCHRate.Text = "0";
                    ExCHRateM = res;
                }
                else
                {
                    ExCHRateM = 1;
                }
            }
            PMTTBX1_TextChanged(this, EventArgs.Empty);
        }
        private void PMTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDCurrency == CURRENCY || ExCHRateM != -1)
                    {
                        if (PRDID != -1 && Quantity != -1 && Price != -1)
                        {
                            SqlCommand cmd;
                            string QueryStringSale, QueryStringPayment, QueryStringTellerTrans;
                            decimal topay = 0;
                            if (payin == -1) payin = 0;
                            topay = (Quantity * Price * ExCHRateM) - payin;

                            if (BELLNo != -1)
                            {
                                this.SaleStatusMessageLBL.Text = "Warning: Bell No has no effec when adding new record it is considered null!";
                                this.SaleStatusMessageLBL.ForeColor = Color.Yellow;
                            }
                            if (this.TELLERSwitch.Checked == true)
                            {
                                if (this.isRegular.Checked == true)
                                {
                                    new SqlCommand($"INSERT INTO SALES (CusID, PrdID, Quantity, Price, Echangerate, Currency, Date) VALUES ('{CUSID}','{PRDID}','{Quantity}','{Price}', '{ExCHRateM}', '{CURRENCY}', '{date}');", con).ExecuteNonQuery();
                                    object tmp = new SqlCommand("select max(BellNo) from SALES", con).ExecuteScalar();
                                    BELLNo = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    ExCHRateM = 1;
                                    new SqlCommand($"INSERT INTO TellersTransactions (TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERID}', '{CUSID}', '{BELLNo}','{HAWALANo}','{payin}', '0' , '{ExCHRateM}','{CURRENCY}' , '{date}');", con).ExecuteNonQuery();
                                    tmp = new SqlCommand("select max(TnsNo) from TellersTransactions", con).ExecuteScalar();
                                    TLRTRNSID = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    new SqlCommand($"INSERT INTO SalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', '{TLRTRNSID}' ,'{date}');", con).ExecuteNonQuery();
                                }
                                else
                                {
                                    new SqlCommand($"INSERT INTO NRegCusSales (PrdID, Quantity, Price, Echangerate, Currency, Date) VALUES ('{PRDID}','{Quantity}','{Price}', '{ExCHRateM}', '{CURRENCY}', '{date}');", con).ExecuteNonQuery();
                                    object tmp = new SqlCommand("select max(BellNo) from NRegCusSales", con).ExecuteScalar();
                                    BELLNo = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    ExCHRateM = 1;
                                    new SqlCommand($"INSERT INTO TellersTransactions (TID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERID}', '{BELLNo}','{HAWALANo}','{payin}', '0' , '{ExCHRateM}', '{CURRENCY}' , '{date}');", con).ExecuteNonQuery();
                                    tmp = new SqlCommand("select max(TnsNo) from TellersTransactions", con).ExecuteScalar();
                                    TLRTRNSID = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    new SqlCommand($"INSERT INTO NRegCusSalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', '{TLRTRNSID}', '{date}');", con).ExecuteNonQuery();

                                }
                            }
                            else
                            {

                                if (this.isRegular.Checked == true)
                                {
                                    new SqlCommand ($"INSERT INTO SALES (CusID, PrdID, Quantity, Price, Echangerate, Currency, Date) VALUES ('{CUSID}','{PRDID}','{Quantity}','{Price}', '{ExCHRateM}', '{CURRENCY}', '{date}');", con).ExecuteNonQuery();
                                    object tmp = new SqlCommand("select max(BellNo) from sales", con).ExecuteScalar();
                                    BELLNo = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    ExCHRateM = 1;
                                    new SqlCommand($"INSERT INTO SalesPayments (BellNo, Payin ,Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', null ,'{date}');", con).ExecuteNonQuery();
                                }
                                else
                                {
                                    new SqlCommand($"INSERT INTO NRegCusSales (PrdID, Quantity, Price, Echangerate, Currency, Date) VALUES ('{PRDID}','{Quantity}','{Price}', '{ExCHRateM}', '{CURRENCY}', '{date}');", con).ExecuteNonQuery();
                                    object tmp = new SqlCommand("select max(BellNo) from NRegCusSales", con).ExecuteScalar();
                                    BELLNo = tmp != DBNull.Value ? int.Parse(tmp.ToString()) : 1;
                                    ExCHRateM = 1;
                                    new SqlCommand($"INSERT INTO NRegCusSalesPayments (BellNo, Payin ,Echangerate, Currency , TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}',  null ,'{date}');", con).ExecuteNonQuery();
                                }
                            }
                            SaleFilterTBX_Enter(this, EventArgs.Empty);
                            resetCells();
                        }
                        else
                        {
                            this.SaleStatusMessageLBL.Text = "ERROR: You have to Fill All nececcary fields!";
                            this.SaleStatusMessageLBL.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.SaleStatusMessageLBL.Text = $"ERROR: Exchange Rate have to be filled like one {CURRENCY} equals to {PRDCurrency}!";
                        this.SaleStatusMessageLBL.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void UpdatePMTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDCurrency == CURRENCY || ExCHRateM != -1)
                    {
                        if (PRDID != -1 && Quantity != -1 && Price != -1)
                        {
                            SqlCommand cmd;
                            string QueryStringPayment, QueryStringTellerTrans;
                            decimal topay = 0;
                            if (payin == -1) payin = 0;
                            topay = (Quantity * Price * ExCHRateM) - payin;

                            if (BELLNo != -1 && payin != 0 )
                            {
                                if (this.TELLERSwitch.Checked == true)
                                {
                                    object tmp = new SqlCommand("select max(TnsNo) from TellersTransactions", con).ExecuteScalar();
                                    if (tmp != DBNull.Value) TLRTRNSID = int.Parse(tmp.ToString()) + 1;
                                    else TLRTRNSID = 1;
                                    if (this.isRegular.Checked == true)
                                    {
                                        QueryStringPayment = $"INSERT INTO SalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', '{TLRTRNSID}' ,'{date}');";
                                        QueryStringTellerTrans = $"INSERT INTO TellersTransactions (TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERID}', '{CUSID}', '{BELLNo}','{HAWALANo}','{payin}', '0' , '{ExCHRateM}','{CURRENCY}' , '{date}');";
                                        new SqlCommand(QueryStringTellerTrans, con).ExecuteNonQuery();
                                        new SqlCommand(QueryStringPayment, con).ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        QueryStringPayment = $"INSERT INTO NRegCusSalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', '{TLRTRNSID}', '{date}');";
                                        QueryStringTellerTrans = $"INSERT INTO TellersTransactions (TID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERID}', '{BELLNo}','{HAWALANo}','{payin}', '0' , '{ExCHRateM}', '{CURRENCY}' , '{date}');";
                                        new SqlCommand(QueryStringTellerTrans, con).ExecuteNonQuery();
                                        new SqlCommand(QueryStringPayment, con).ExecuteNonQuery();
                                    }
                                }
                                else
                                {

                                    if (this.isRegular.Checked == true)
                                    {
                                        QueryStringPayment = $"INSERT INTO SalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', null ,'{date}');";
                                        new SqlCommand(QueryStringPayment, con).ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        QueryStringPayment = $"INSERT INTO NRegCusSalesPayments (BellNo, Payin, Echangerate, Currency, TellerTransID, Date) VALUES ('{BELLNo}', '{payin}', '{ExCHRateM}', '{CURRENCY}', null , '{date}');";
                                        new SqlCommand(QueryStringPayment, con).ExecuteNonQuery();
                                    }
                                }
                                SaleFilterTBX_Enter(this, EventArgs.Empty);
                                resetCells();
                            }
                            else
                            {
                                this.SaleStatusMessageLBL.Text = "ERROR: bell number empty or not payment is done!";
                                this.SaleStatusMessageLBL.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            this.SaleStatusMessageLBL.Text = "ERROR: You have to Product id quantity and sale price per one product!";
                            this.SaleStatusMessageLBL.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.SaleStatusMessageLBL.Text = $"ERROR: Exchange Rate have to be filled like one {PRDCurrency} equals to {CURRENCY}!";
                        this.SaleStatusMessageLBL.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.SaleStatusMessageLBL.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.SaleStatusMessageLBL.ForeColor = Color.Red;
            }
        }
        private void resetCells()
        {
            this.PRDFilterTBX.Text = this.CUSFilterTBX.Text = this.SaleTBX1.Text = this.SaleTBX2.Text = this.SaleTBX3.Text = this.SaleTBX4.Text = this.SaleCurrencyTXT.Text = this.PMTTBX1.Text = this.PMTTBX2.Text = this.EXCHRate.Text = this.TellerTBXFilter.Text = this.TELLERTBX1.Text = this.TELLERTBX2.Text = this.TELLERTBX3.Text = "";
            Price = topay = payin = BELLNo = PRDID = CUSID = Quantity = TELLERID = HAWALANo = TLRTRNSID = RemStocks = -1;
            ExCHRateM = 1; date = DateTime.Today; CURRENCY = "AFG"; PRDCurrency = null;
        }
    }
}