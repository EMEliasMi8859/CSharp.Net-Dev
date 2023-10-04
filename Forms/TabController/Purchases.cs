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
    public partial class Purchases : Form
    {
        int CurrentActiveEvent = 0;
        int PRDIDV = -1, VNDIDV = -1, QUANTITYV = -1, TELLERIDV = -1, HAWALANOV = -1, BELLNOV = -1, RemStockV = -1, TLRTRNSIDV = -1;
        decimal PAYMENTV = -1, TOPAYV = -1, EXCHANGERATEV = 1, PRICEV = -1;
        string PRDCURRECYV = null, PURCHASECURRENCY = null;
        String DATEV = DateTime.Today.ToString();
        public Purchases()
        {
            InitializeComponent();
            PaintForm();
            Configurations.ControlsConfigurations.InitializeGridView(this.PurchaseGRDView, null, null, null, -1, null, true, true);
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Purchases;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    PurchaseGRDView.CellDoubleClick += new DataGridViewCellEventHandler(PurchaseGRDView_CellDoubleClick);
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 450))}!";
                this.MessageX.ForeColor = Color.Red;
            }
        }
        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.PRDGRB, null, null, null, -1, -1, true, true,true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRDFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.VENDORGRB, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.VNDFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.PURCHASEGRB, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PURCHASEFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.PurhcaseLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PurchaseLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PurchaseLBL3, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRDIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.VNDIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.QUANTITYX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.PaymentGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL4, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PAYMENTX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            this.PMNTCURRENCYX.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.PMNTCURRENCYX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.PMNTCURRENCYX.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.PMNTCURRENCYX.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            Configurations.ControlsConfigurations.InitializeTextBox(this.TOPAYX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHANGEX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRICEX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            this.CURRENCYX.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.CURRENCYX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.CURRENCYX.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.CURRENCYX.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);
            this.DATEX.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.DATEX.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.DATEX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.DATEX.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.NewRCRDBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.PMTBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.TellerGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERFilter, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.HAWALANOX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.BELLNOX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.BELLNOX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.TELLERSwitch, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.WealthX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.WealthLBL.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.MessageX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeButton(this.PRINTBELLBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, 3, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
        }

        private void removeCurrentActiveEvent()
        {
            PurchaseGRDView.CellDoubleClick -= PurchaseGRDView_CellDoubleClick;
            if (CurrentActiveEvent == 0) PurchaseGRDView.CellDoubleClick -= PurchaseGRDView_CellDoubleClick;
            else if (CurrentActiveEvent == 1) PurchaseGRDView.CellDoubleClick -= PRDGRDView_CellDoubleClick;
            else if (CurrentActiveEvent == 2) PurchaseGRDView.CellDoubleClick -= VNDGRDView_CellDoubleClick;
            else if (CurrentActiveEvent == 3) PurchaseGRDView.CellDoubleClick -= TLRGRDView_CellDoubleClick;
        }
        private void PRDFilter_Enter(object sender, EventArgs e)
        {
            this.PRDFilter.Text = "";
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Products;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 1;
                    PurchaseGRDView.CellDoubleClick += new DataGridViewCellEventHandler(PRDGRDView_CellDoubleClick);
                    PRDIDV = -1;
                    PRDCURRECYV = null;
                    PRICEV = -1;
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void PRDFilter_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (decimal.TryParse(this.PRDFilter.Text, out decimal res)) cmd = new SqlCommand($"Select * from Products where ID like '%{res}%' or Price like '%{res}%';", con);
                    else cmd = new SqlCommand($"Select * from Products where Name Like '%{this.PRDFilter.Text.ToString()}%' or CompanyName Like '%{this.PRDFilter.Text.ToString()}%';", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    if (PurchaseGRDView.Rows.Count == 1)
                    {
                        this.PurchaseGRDView.Rows[0].Selected = true;
                        string tmp;
                        tmp = PurchaseGRDView.Rows[0].Cells["Price"].Value.ToString();
                        PRICEV = decimal.Parse(tmp);
                        tmp = PurchaseGRDView.Rows[0].Cells["Currency"].Value.ToString();
                        PRDCURRECYV = tmp;
                        tmp = PurchaseGRDView.Rows[0].Cells["ID"].Value.ToString();
                        this.PRDIDX.Text = tmp;
                    }
                    else
                    {
                        PRDIDV = -1;
                        PRDCURRECYV = null;
                    }
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void PRDGRDView_CellDoubleClick(object sender , DataGridViewCellEventArgs e)
        {
            try
            {
                this.PurchaseGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                PRDIDV = int.Parse(tmp);
                tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                PRICEV = decimal.Parse(tmp);
                tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["Currency"].Value.ToString();
                PRDCURRECYV = tmp;
                this.PRDIDX.Text = PRDIDV.ToString();
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }

        private void VNDFilter_Enter(object sender, EventArgs e)
        {
            this.VNDFilter.Text = "";
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"Select * from Vendors;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 2;
                    PurchaseGRDView.CellDoubleClick += new DataGridViewCellEventHandler(VNDGRDView_CellDoubleClick);
                    VNDIDV = -1;
                    VNDIDX_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };

        }
        private void VNDFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = null;
                    if (this.VNDFilter.Text.Length == 0) cmd = new SqlCommand($"Select * from Vendors;", con);
                    else
                    {
                        if (int.TryParse(this.VNDFilter.Text.ToString(), out int res)) cmd = new SqlCommand($"Select * from Vendors where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Vendors where Name Like '%{this.VNDFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    if (PurchaseGRDView.Rows.Count == 1)
                    {
                        this.PurchaseGRDView.Rows[0].Selected = true;
                        string tmp = PurchaseGRDView.Rows[0].Cells["ID"].Value.ToString();
                        VNDIDV = int.Parse(tmp);
                        this.VNDIDX.Text = tmp;
                    }
                    else VNDIDV = -1;
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void VNDGRDView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.PurchaseGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                VNDIDV = int.Parse(tmp);
                this.VNDIDX.Text = tmp;
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }

        private void PURCHASEFilter_Enter(object sender, EventArgs e)
        {
            PURCHASEFilter.Text = "";
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Purchases", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 0;
                    PurchaseGRDView.CellDoubleClick += new DataGridViewCellEventHandler(PurchaseGRDView_CellDoubleClick);
                    BELLNOV = -1;
                    BELLNOX_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void PURCHASEFilter_TextChanged(object sender, EventArgs e)
        {
            if(this.PURCHASEFilter.Text.Length == 0) resetCells();
            this.NewRCRDBTN.Enabled = true;
            try
            {
                SqlCommand cmd ;
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (this.PURCHASEFilter.Text.Length == 0) cmd = new SqlCommand($"Select * from Purchases;", con);
                    else
                    {
                        if (int.TryParse(this.PURCHASEFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Purchases where BellNo like '%{res}%';", con);
                        else
                        {
                            this.MessageX.Text = "ERROR: Bell no is out of range or you entered an invalid bell no!";
                            this.MessageX.ForeColor = Color.Red;
                            return;
                        }
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    if (PurchaseGRDView.Rows.Count == 1)
                    {
                        int RowIndex = 0;
                        try
                        {
                            this.NewRCRDBTN.Enabled = false;
                            this.PurchaseGRDView.Rows[RowIndex].Selected = true;
                            string tmp = PurchaseGRDView.Rows[RowIndex].Cells["BellNo"].Value.ToString();
                            BELLNOV = int.Parse(tmp);
                            this.BELLNOX.Text = tmp;
                            tmp = PurchaseGRDView.Rows[RowIndex].Cells["VndID"].Value.ToString();
                            VNDIDV = int.Parse(tmp);
                            this.VNDIDX.Text = tmp;
                            tmp = PurchaseGRDView.Rows[RowIndex].Cells["PrdID"].Value.ToString();
                            PRDIDV = int.Parse(tmp);
                            tmp = PurchaseGRDView.Rows[RowIndex].Cells["Quantity"].Value.ToString();
                            QUANTITYV = int.Parse(tmp);
                            this.QUANTITYX.Text = tmp;
                            this.PRDIDX.Text = PRDIDV.ToString();
                            try
                            {
                                object res = new SqlCommand($"select sum(Payin / Echangerate) from PurchasesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                Console.WriteLine(res.ToString());
                                TOPAYV = ((QUANTITYV!=-1?QUANTITYV : 0) * PRICEV) - ((res == DBNull.Value) ? 0 : decimal.Parse(res.ToString()));
                                this.TOPAYX.Text = TOPAYV.ToString();
                            }
                            catch (Exception exc)
                            {
                                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                this.MessageX.ForeColor = Color.Red;
                            }
                        }
                        catch (Exception exc)
                        {
                            this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                            this.MessageX.ForeColor = Color.Red;
                        }/* res = new SqlCommand($"select pch.Echangerate * prd.Price as res from Purchases as pch join Products as prd on pch.PrdID = prd.ID where pch.BellNo = '{BELLNOV}';", con).ExecuteScalar();
                        PRICEV = (res == DBNull.Value) ? 0 : decimal.Parse(res.ToString());
                     */
                    }
                    else resetCells();

                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void PurchaseGRDView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.NewRCRDBTN.Enabled = false;
                this.PurchaseGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["BellNo"].Value.ToString();
                BELLNOV = int.Parse(tmp);
                this.BELLNOX.Text = tmp;
                tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["VndID"].Value.ToString();
                VNDIDV = int.Parse(tmp);
                this.VNDIDX.Text = tmp;
                tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["PrdID"].Value.ToString();
                PRDIDV = int.Parse(tmp);
                tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                QUANTITYV = int.Parse(tmp);
                this.QUANTITYX.Text = tmp;
                this.PRDIDX.Text = PRDIDV.ToString();
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    object res = new SqlCommand($"select sum(Payin / Echangerate) from PurchasesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                    Console.WriteLine(res.ToString());
                    TOPAYV = ((QUANTITYV != -1 ? QUANTITYV : 0) * PRICEV ) - ((res == DBNull.Value) ? 0 : decimal.Parse(res.ToString())) ;
                    this.TOPAYX.Text = TOPAYV.ToString();
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }

        }

        private void TELLERFilter_Enter(object sender, EventArgs e)
        {
            this.TELLERSwitch.Checked = true;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {

                    SqlCommand cmd;
                    if (TELLERFilter.Text.Length == 0) cmd = new SqlCommand($"Select * from Tellers;", con);
                    else
                    {
                        if (int.TryParse(this.TELLERFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Tellers where ID like '%{res}%';", con);
                        else cmd = new SqlCommand($"Select * from Tellers where Name like '%{TELLERFilter.Text.ToString()}%';", con);
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = 3;
                    PurchaseGRDView.CellDoubleClick += new DataGridViewCellEventHandler(TLRGRDView_CellDoubleClick);
                    TELLERIDV = -1;
                    this.TELLERIDX.Text = "";
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void TELLERFilter_TextChanged(object sender, EventArgs e)
        {

            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (int.TryParse(this.TELLERFilter.Text, out int res)) cmd = new SqlCommand($"Select * from Tellers where ID like '%{res}%';", con);
                    else cmd = new SqlCommand($"Select * from Tellers where Name like '%{TELLERFilter.Text}%';", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    PurchaseGRDView.DataSource = null;
                    PurchaseGRDView.DataSource = dataTable;
                    if (PurchaseGRDView.Rows.Count == 1)
                    {
                        this.PurchaseGRDView.Rows[0].Selected = true;
                        string tmp = PurchaseGRDView.Rows[0].Cells["ID"].Value.ToString();
                        this.TELLERIDX.Text = tmp;
                        TELLERIDV = int.Parse(tmp);
                    }
                    else TELLERIDV = -1;
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            };
        }
        private void TLRGRDView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.PurchaseGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = PurchaseGRDView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                this.TELLERIDX.Text = tmp;
                TELLERIDV = int.Parse(tmp);
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }

        }

        private void PRDIDX_TextChanged(object sender, EventArgs e)
        {
            if (PRDIDX.Text.Length != 0)
            {
                if(int.TryParse(PRDIDX.Text, out int res))
                {
                    PRDIDV = res;
                    try
                    {
                        using(SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                        {
                            SqlDataReader reader = new SqlCommand($"select * from Products where ID = '{PRDIDV}';", con).ExecuteReader();
                            while (reader.Read())
                            {
                                PRDCURRECYV = reader.GetString(reader.GetOrdinal("Currency"));
                                PRICEV = reader.GetDecimal(reader.GetOrdinal("Price"));
                            }
                            this.CURRENCYX.Text = PRDCURRECYV;
                            this.PRICEX.Text = PRICEV.ToString();
                        }
                    }catch(Exception exc)
                    {
                        this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                this.PRICEX.Text = "";
                this.CURRENCYX.Text = "AFG";
                PRDIDV = -1; PRICEV = -1; PRDCURRECYV = "AFG";

            }
            if (PRDIDV != -1)
            {
                try
                {
                    using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                    {
                        int remStocks;
                        object tp = new SqlCommand($"select sum(Quantity) as purch from purchases where prdID = {PRDIDV};", con).ExecuteScalar();
                        remStocks = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum(Quantity) as sale from sales where PrdID = {PRDIDV};", con).ExecuteScalar();
                        remStocks -= (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum(Quantity) as sale from NRegCusSales where PrdID = {PRDIDV};", con).ExecuteScalar();
                        remStocks -= (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from ReturnStocks rs join sales s on rs.BellNo = s.BellNo where s.PrdID = {PRDIDV};", con).ExecuteScalar();
                        RemStockV = remStocks + ((tp == DBNull.Value) ? 0 : int.Parse(tp.ToString()));
                        tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from NRegCusReturnStocks rs join NRegCusSales s on rs.BellNo = s.BellNo where s.PrdID = {PRDIDV};", con).ExecuteScalar();
                        RemStockV = remStocks + ((tp == DBNull.Value) ? 0 : int.Parse(tp.ToString()));
                        this.WealthX.Text = RemStockV.ToString();
                    }
                }
                catch (Exception exc)
                {
                    this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                    this.MessageX.ForeColor = Color.Red;
                }
            }


        }

        private void TOPAYX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void CURRENCYX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void VNDIDX_TextChanged(object sender, EventArgs e)
        {
            if(this.VNDIDX.Text.Length != 0)
            {
                if(int.TryParse(this.VNDIDX.Text, out int res))
                    VNDIDV = res;
            }
            else
                VNDIDV = -1;
        }

        private void QUANTITYX_TextChanged(object sender, EventArgs e)
        {
            if (QUANTITYX.Text.Length != 0)
            {
                if (int.TryParse(QUANTITYX.Text.ToString(), out int res))
                {
                    if (res < 0) this.QUANTITYX.Text = "0";
                    QUANTITYV = res;
                    if (PRICEV == -1) PRICEV = 0;
                    if (PAYMENTV == -1) PAYMENTV = 0;
                    this.TOPAYX.Text = ((QUANTITYV * PRICEV) - PAYMENTV).ToString();
                }
            }
            else
                QUANTITYV = -1;
            if (QUANTITYV != -1 && RemStockV != -1)
                this.WealthX.Text = (RemStockV + QUANTITYV).ToString();
            else this.WealthX.Text = RemStockV != -1 ? RemStockV.ToString() : "0";
            PAYMENTX_TextChanged(this, EventArgs.Empty);
        }

        private void DATEX_ValueChanged(object sender, EventArgs e)
        {
            DATEV = this.DATEX.Value.ToString();
        }

        private void TELLERIDX_TextChanged(object sender, EventArgs e)
        {
            if (this.TELLERIDX.Text.Length != 0)
            {
                if (int.TryParse(this.TELLERIDX.Text, out int res)) TELLERIDV = res;
            }
            else TELLERIDV = -1;
        }

        private void HAWALANOX_TextChanged(object sender, EventArgs e)
        {
            if (this.HAWALANOX.Text.Length != 0)
            {
                if (int.TryParse(this.HAWALANOX.Text, out int res)) HAWALANOV= res;
            }
            else HAWALANOV = -1;
        }

        private void BELLNOX_TextChanged(object sender, EventArgs e)
        {
            if (this.BELLNOX.Text.Length != 0)
            {
                if (int.TryParse(this.BELLNOX.Text, out int res))
                    BELLNOV = res;
            }
            else
                BELLNOV = -1;
            this.BELLNOX.Text = BELLNOV != -1 ? BELLNOV.ToString() : "";
        }

        private void PAYMENTX_TextChanged(object sender, EventArgs e)
        {
            PAYMENTV = 0;
            if (QUANTITYV == -1) QUANTITYV = 0;
            if (PRICEV == -1) PRICEV = 0;
            if (PAYMENTX.Text.Length != 0)
            {
                if (decimal.TryParse(PAYMENTX.Text.ToString(), out decimal res))
                {
                    if (res < 0) this.PAYMENTX.Text = "";
                    if (TOPAYV != -1 && res > TOPAYV * EXCHANGERATEV) this.PAYMENTX.Text = (TOPAYV * EXCHANGERATEV).ToString();
                    else if (res > (QUANTITYV * PRICEV * EXCHANGERATEV)) this.PAYMENTX.Text = res.ToString();
                    PAYMENTV = res;
                }
            }
            else PAYMENTV = -1;
            if (TOPAYV != -1)
                this.TOPAYX.Text = ((TOPAYV * EXCHANGERATEV) - (PAYMENTV != -1? PAYMENTV : 0)).ToString();
            else
                this.TOPAYX.Text = ((QUANTITYV * PRICEV * EXCHANGERATEV) - (PAYMENTV != -1 ? PAYMENTV : 0)).ToString();
        }

        private void PMNTCURRENCYX_SelectedIndexChanged(object sender, EventArgs e)
        {
            PURCHASECURRENCY = this.PMNTCURRENCYX.Text.ToString();
            PRDIDX_TextChanged(this, EventArgs.Empty);
        }

        private void EXCHANGEX_TextChanged(object sender, EventArgs e)
        {
            if (this.EXCHANGEX.Text.Length != 0)
            {
                if (decimal.TryParse(this.EXCHANGEX.Text, out decimal res))
                {
                    if (res < 0) res = 0;
                    EXCHANGERATEV = res;
                }
            }
            else
                EXCHANGERATEV = 1;
            PAYMENTX_TextChanged(this, EventArgs.Empty);
        }

        private void NewRCRDBTN_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDCURRECYV == PURCHASECURRENCY || EXCHANGERATEV != -1)
                    {
                        if (PRDIDV != -1 && QUANTITYV != -1)
                        {
                            if (PAYMENTV == -1) PAYMENTV = 0;
                            new SqlCommand($"INSERT INTO Purchases ( VndID , PrdID , Quantity ,  Date ) VALUES ('{VNDIDV}','{PRDIDV}','{QUANTITYV}', '{DATEV}');", con).ExecuteNonQuery();
                            object tmp = new SqlCommand("select max(BellNo) from Purchases", con).ExecuteScalar();
                            if (tmp != DBNull.Value) BELLNOV = int.Parse(tmp.ToString());
                            else BELLNOV = 1;
                            if (this.TELLERSwitch.Checked == true)
                            {
                                new SqlCommand($"INSERT INTO TellersTransactions (TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERIDV}', null, '{BELLNOV}','{HAWALANOV}','{PAYMENTV}', '0' , '{EXCHANGERATEV}','{PURCHASECURRENCY}' , '{DATEV}');", con).ExecuteNonQuery();
                                tmp = new SqlCommand("select max(TnsNo) from TellersTransactions", con).ExecuteScalar();
                                if (tmp != DBNull.Value) TLRTRNSIDV = int.Parse(tmp.ToString());
                                else TLRTRNSIDV = 1;
                                new SqlCommand($"INSERT INTO PurchasesPayments(BellNo, Payin , Currency , Echangerate , TellerTransID, Date) VALUES ('{BELLNOV}', '{PAYMENTV}', '{PURCHASECURRENCY}' , '{EXCHANGERATEV}', '{TLRTRNSIDV}', '{DATEV}');", con).ExecuteNonQuery();
                            }
                            else
                                new SqlCommand($"INSERT INTO PurchasesPayments(BellNo, Payin , Currency , Echangerate , TellerTransID, Date) VALUES ('{BELLNOV}', '{PAYMENTV}', '{PURCHASECURRENCY}' , '{EXCHANGERATEV}', null, '{DATEV}');", con).ExecuteNonQuery();

                            PURCHASEFilter_Enter(this, EventArgs.Empty);
                            resetCells();
                        }
                        else
                        {
                            this.MessageX.Text = "ERROR: You have to Fill All Important fields!";
                            this.MessageX.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.MessageX.Text = $"ERROR: Exchange Rate have to be filled like one {PRDCURRECYV} equals to {PURCHASECURRENCY}!";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }

        private void PMTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDCURRECYV == PURCHASECURRENCY || EXCHANGERATEV != -1)
                    {
                        if (PRDIDV != -1 && QUANTITYV != -1)
                        {
                            if (BELLNOV != -1 && PAYMENTV != -1)
                            {
                                object tmp;
                                if (this.TELLERSwitch.Checked == true)
                                {
                                    new SqlCommand($"INSERT INTO TellersTransactions (TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency ,Date) VALUES ('{TELLERIDV}', null, '{BELLNOV}','{HAWALANOV}','{PAYMENTV}', '0' , '{EXCHANGERATEV}','{PURCHASECURRENCY}' , '{DATEV}');", con).ExecuteNonQuery();
                                    tmp = new SqlCommand("select max(TnsNo) from TellersTransactions", con).ExecuteScalar();
                                    if (tmp != DBNull.Value) TLRTRNSIDV = int.Parse(tmp.ToString());
                                    else TLRTRNSIDV = 1;
                                    new SqlCommand($"INSERT INTO PurchasesPayments(BellNo, Payin , Currency , Echangerate , TellerTransID, Date) VALUES ('{BELLNOV}', '{PAYMENTV}', '{PURCHASECURRENCY}' , '{EXCHANGERATEV}', '{TLRTRNSIDV}', '{DATEV}');", con).ExecuteNonQuery();
                                }
                                else
                                    new SqlCommand($"INSERT INTO PurchasesPayments(BellNo, Payin , Currency , Echangerate , TellerTransID, Date) VALUES ('{BELLNOV}', '{PAYMENTV}', '{PURCHASECURRENCY}' , '{EXCHANGERATEV}', null , '{DATEV}');", con).ExecuteNonQuery();

                                PURCHASEFilter_Enter(this, EventArgs.Empty);
                                resetCells();
                            }
                            else
                            {
                                this.MessageX.Text = "ERROR: Insert or select a bell no or specify the payment ammount!";
                                this.MessageX.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            this.MessageX.Text = "ERROR: You have to Fill All Important fields!";
                            this.MessageX.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.MessageX.Text = $"ERROR: Exchange Rate have to be filled like one {PRDCURRECYV} equals to {PURCHASECURRENCY}!";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }
        private void resetCells()
        {
            this.PRDFilter.Text = this.VNDFilter.Text = this.PURCHASEFilter.Text = this.PRDIDX.Text = this.VNDIDX.Text = this.QUANTITYX.Text = this.PAYMENTX.Text = this.PMNTCURRENCYX.Text = this.TOPAYX.Text = this.EXCHANGEX.Text = this.PRICEX.Text = this.CURRENCYX.Text = this.TELLERFilter.Text = this.TELLERIDX.Text = this.HAWALANOX.Text = this.BELLNOX.Text = "";
            PRDIDV = VNDIDV = QUANTITYV = TELLERIDV = HAWALANOV = BELLNOV = RemStockV = TLRTRNSIDV = -1;
            PAYMENTV = TOPAYV = PRICEV = -1; EXCHANGERATEV = 1;
            PRDCURRECYV = PURCHASECURRENCY = null;
        }
    }
}
