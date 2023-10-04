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
    public partial class ReturnStocks : Form
    {
        bool CurrentActiveEvent = true, isNew;
        int PRDIDV = -1, CUSIDV = -1, TELLERIDV = -1, BELLNOV = -1, HAWALANOV = -1, QUANTITYV = -1, SaleQuantity = -1, RemStocks = -1;
        decimal PRICEV = -1, PAYINV = -1, PAYOUTV = -1, TOPAYV = -1, tmpTopay = -1, TotalPrice = -1 , ReturnedPrice = -1, PayedPrice = -1 , EXCHANGERV = 1;
        String PRDCURRENCYV = null, CURRENCY = null, DATE = DateTime.Today.ToString();


        public ReturnStocks()
        {
            InitializeComponent();
            PaintForm();

            Configurations.ControlsConfigurations.InitializeGridView(this.RTSGRDView, null, null, null, -1, null, true, true);
            onLoad();
        }
        private void onLoad()
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (isRegular.Checked == true) cmd = new SqlCommand($"select rs.BellNo as BellNo,s.PrdID as ProductID ,s.Quantity as Quantity, rs.Quantity as RTSQuantity, s.Price as Price,s.Currency as Currency ,rs.Date as Date from ReturnStocks rs join Sales s on rs.BellNo = s.BellNo;", con);
                    else cmd = new SqlCommand($"select rs.BellNo as BellNo,s.PrdID as ProductID ,s.Quantity as Quantity, rs.Quantity as RTSQuantity, s.Price as Price,s.Currency as Currency ,rs.Date as Date from NRegCusReturnStocks rs join NRegCusSales s on rs.BellNo = s.BellNo;",con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    RTSGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = true;
                    RTSGRDView.CellDoubleClick += new DataGridViewCellEventHandler(RTRNSTKFILTER_CellDoubleClick);
                    isNew = false;
                    this.PMTBTN.Enabled = true;
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
            /*Configurations.ControlsConfigurations.InitializeControls(this.CloseSalesBTN, Configurations.InitializedVariables.FRMBGRCLR, Configurations.InitializedVariables.FRMBGRCLR, true);*/
            Configurations.ControlsConfigurations.InitializeGroupBox(this.RTSGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.RTRNSTKFILTERX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.isRegular, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.isRegular.CheckedState.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR);
            this.isRegular.CheckedState.InnerColor = Color.White;
            this.isRegular.Checked = true;

            Configurations.ControlsConfigurations.InitializeLabel(this.LBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.LBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.LBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRDIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PRICEX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            this.PRDCURRENCYX.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.PRDCURRENCYX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.PRDCURRENCYX.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.PRDCURRENCYX.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            Configurations.ControlsConfigurations.InitializeTextBox(this.CUSIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.QUANTITYX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.PaymentGBX, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.PMTLBL4, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PAYOUTX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PAYINX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHANGEX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            
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
            Configurations.ControlsConfigurations.InitializeTextBox(this.TOPAYX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL1, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL2, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.TELLERLBL3, null);
            Configurations.ControlsConfigurations.InitializeLabel(this.label2, null);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TELLERIDX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.HAWALANOX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.BELLNOX, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            
            this.RemStock.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.SaleLBL10.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.MessageX.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            Configurations.ControlsConfigurations.InitializeToggleSwitch(this.TELLERSwitch, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            this.TELLERSwitch.CheckedState.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR);
            this.TELLERSwitch.CheckedState.InnerColor = Color.White;
            this.TELLERSwitch.Checked = true;

            Configurations.ControlsConfigurations.InitializeButton(this.SalePrintBell, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, 3, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
        }

        private void TELLERIDX_TextChanged(object sender, EventArgs e)
        {
            if (this.TELLERIDX.Text.Length == 0) TELLERIDV = -1;
            else
            {
                if(int.TryParse(this.TELLERIDX.Text, out int res))
                {
                    if (res < 0) this.TELLERIDX.Text = "";
                    TELLERIDV = res;
                }
            }
        }

        private void isRegular_CheckedChanged(object sender, EventArgs e)
        {
            onLoad();
        }

        private void HAWALANOX_TextChanged(object sender, EventArgs e)
        {
            if (this.HAWALANOX.Text.Length == 0) HAWALANOV = -1;
            else
            {
                if(int.TryParse(this.HAWALANOX.Text, out int res))
                {
                    if (res < 0) this.HAWALANOX.Text = "";
                    HAWALANOV = res;
                }
            }
        }
        private void CURRENCYX_SelectedIndexChanged(object sender, EventArgs e)
        {
            CURRENCY = this.CURRENCYX.Text;
        }
        private void QUANTITYX_TextChanged(object sender, EventArgs e)
        {
            if(this.QUANTITYX.Text.Length != 0)
            {
                if(int.TryParse(this.QUANTITYX.Text, out int res))
                {
                    if (res < 0) { this.QUANTITYX.Text = ""; }
                    if (SaleQuantity != -1 && res > SaleQuantity ) { this.QUANTITYX.Text = SaleQuantity.ToString(); }
                    QUANTITYV = res;
                }
            }else QUANTITYV = -1;
            this.RemStock.Text = (PRDIDV != -1 && RemStocks != -1) ? (((QUANTITYV!=-1)? QUANTITYV :0) + RemStocks).ToString() : "0";
            PAYOUTX_TextChanged(this, EventArgs.Empty);
        }

        private void PRDIDX_TextChanged(object sender, EventArgs e)
        {
            if(this.PRDIDX.Text.Length != 0)
            {
                if(int.TryParse(this.PRDIDX.Text, out int res)){
                    if (res < 0) this.PRDIDX.Text = "";
                    PRDIDV = res;
                }
            }
            if(PRDIDV != -1)
            {
                try
                {
                    int a, b, c, d, f;
                    using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                    {
                        object tp = new SqlCommand($"select sum(Quantity) as purch from purchases where PrdID = {PRDIDV};", con).ExecuteScalar();
                        a = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum(Quantity) as sale from sales where PrdID = {PRDIDV};", con).ExecuteScalar();
                        b = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from ReturnStocks rs join sales s on rs.BellNo = s.BellNo where s.PrdID = {PRDIDV};", con).ExecuteScalar();
                        c = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum(Quantity) as nregsale from NRegCusSales where PrdID = {PRDIDV};", con).ExecuteScalar();
                        d = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        tp = new SqlCommand($"select sum (rs.Quantity) as rtstocks from NRegCusReturnStocks rs join NRegCusSales s on rs.BellNo = s.BellNo where s.PrdID = {PRDIDV};", con).ExecuteScalar();
                        f = (tp == DBNull.Value) ? 0 : int.Parse(tp.ToString());
                        RemStocks = a - b - d + c + f;
                        this.RemStock.Text = RemStocks.ToString();
                    }
                }
                catch (Exception exc)
                {
                    this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                    this.MessageX.ForeColor = Color.Red;
                }
            }
        }

        private void removeCurrentActiveEvent()
        {
            RTSGRDView.CellDoubleClick -= RTRNSTKFILTER_CellDoubleClick;
            if (CurrentActiveEvent) RTSGRDView.CellDoubleClick -= RTRNSTKFILTER_CellDoubleClick;
            else RTSGRDView.CellDoubleClick -= TellerFilter_CellDoubleClick;
        }

        private void RTRNSTKFILTERX_Enter(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    SqlCommand cmd;
                    if (isRegular.Checked == true) cmd = new SqlCommand($"Select * from Sales;", con);
                    else cmd = new SqlCommand($"SELECT * from NRegCusSales;", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    RTSGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = true;
                    RTSGRDView.CellDoubleClick += new DataGridViewCellEventHandler(RTRNSTKFILTER_CellDoubleClick);
                    isNew = true;
                    this.PMTBTN.Enabled = true;
                }
            }catch(Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }
        private void RTRNSTKFILTERX_TextChanged(object sender, EventArgs e)
        {
            this.NewRCRDBTN.Enabled = true;
            if (this.RTRNSTKFILTERX.Text.Length == 0)
            {
                ResetCells();
                onLoad();
            }
            else
            {
                if(int.TryParse(this.RTRNSTKFILTERX.Text, out int res))
                {
                    isNew = true;
                    this.PMTBTN.Enabled = false;
                    try
                    {
                        using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                        {
                            SqlCommand cmd;
                            if (isRegular.Checked == true)
                            {   /*if (int.Parse(new SqlCommand($"SELECT * FROM ", con).ExecuteScalar().ToString())) ;*/
                                cmd = new SqlCommand($"Select * from Sales where BellNo = {res};", con);
                            }
                            else
                            {
                                cmd = new SqlCommand($"SELECT * from NRegCusSales where BellNo = {res}", con);
                            }
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);
                            RTSGRDView.DataSource = dataTable;
                            if (RTSGRDView.Rows.Count == 1)
                            {
                                int RowIndex = 0;
                                ResetCells(); PAYINX.KeyPress -= new KeyPressEventHandler(NoneTypable); PAYOUTX.KeyPress -= new KeyPressEventHandler(NoneTypable);
                                try
                                {
                                    this.NewRCRDBTN.Enabled = false;
                                    this.RTSGRDView.Rows[RowIndex].Selected = true;
                                    string tmp = RTSGRDView.Rows[RowIndex].Cells["BellNo"].Value.ToString();
                                    BELLNOV = int.Parse(tmp);
                                    tmp = RTSGRDView.Rows[RowIndex].Cells["Quantity"].Value.ToString();
                                    SaleQuantity = int.Parse(tmp);
                                    this.BELLNOX.Text = tmp;
                                    try{
                                        object tmpobj, tmpobj1 = null;
                                        SqlDataReader reader;
                                        if (isRegular.Checked == true) reader = new SqlCommand($"SELECT PrdID, CusID, Price, Currency from Sales where BellNo = {BELLNOV};", con).ExecuteReader();
                                        else reader = new SqlCommand($"SELECT PrdID, Price, Currency from NRegCusSales where BellNo = {BELLNOV};", con).ExecuteReader();
                                        while (reader.Read())
                                        {
                                            PRDIDV = reader.GetInt32(reader.GetOrdinal("PrdID"));
                                            PRICEV = reader.GetDecimal(reader.GetOrdinal("Price"));
                                            PRDCURRENCYV = reader.GetString(reader.GetOrdinal("Currency"));
                                            if (isRegular.Checked == true) CUSIDV = reader.GetInt32(reader.GetOrdinal("CusID"));
                                        }
                                        reader.Close();
                                        if (isRegular.Checked == true) tmpobj = new SqlCommand($"select sum(Payin / Echangerate) from SalesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                        else tmpobj = new SqlCommand($"select sum(Payin / Echangerate) from NRegCusSalesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                        TOPAYV = (SaleQuantity * PRICEV) - ((tmpobj == DBNull.Value) ? 0 : decimal.Parse(tmpobj.ToString()));
                                        if (TOPAYV < 0) { TOPAYV = -1 * TOPAYV; PAYINV = -1; PAYOUTV = 0; PAYINX.KeyPress += new KeyPressEventHandler(NoneTypable); }
                                        else { PAYOUTV = -1; PAYINV = 0; PAYOUTX.KeyPress += new KeyPressEventHandler(NoneTypable); }
                                        if (PAYINV == -1 && PAYOUTV == 0)
                                        {
                                            if (isRegular.Checked == true) tmpobj1 = new SqlCommand($"select sum(Payout / Echangerate) from ReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                            else tmpobj1 = new SqlCommand($"select sum(Payout / Echangerate) from NRegCusReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                        }
                                        else if (PAYOUTV == -1 && PAYINV == 0)
                                        {
                                            if (isRegular.Checked == true) tmpobj1 = new SqlCommand($"select sum(Payin / Echangerate) from ReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                            else tmpobj1 = new SqlCommand($"select sum(Payin / Echangerate) from NRegCusReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                                        }
                                        TOPAYV -= tmpobj1 == DBNull.Value ? 0 : decimal.Parse(tmpobj1.ToString());
                                        this.PRDIDX.Text = PRDIDV.ToString();
                                        this.PRICEX.Text = PRICEV.ToString();
                                        this.PRDCURRENCYX.Text = PRDCURRENCYV;
                                        if (isRegular.Checked == true) this.CUSIDX.Text = CUSIDV.ToString();
                                        PAYOUTX_TextChanged(this, EventArgs.Empty);
                                    }catch(Exception exc)
                                    {
                                        this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                        this.MessageX.ForeColor = Color.Red;
                                    }
                                }
                                catch (Exception exc)
                                {
                                    this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                                    this.MessageX.ForeColor = Color.Red;
                                }
                            }
                            else { ResetCells(); this.RTRNSTKFILTERX.Text = ""; }
                        }
                    }
                    catch (Exception exc)
                    {
                        this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}!";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
                else
                {
                    ResetCells();
                }
            }

        }
        private void RTRNSTKFILTER_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ResetCells(); PAYINX.KeyPress -= new KeyPressEventHandler(NoneTypable); PAYOUTX.KeyPress -= new KeyPressEventHandler(NoneTypable);
            try
            {
                this.RTSGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = RTSGRDView.Rows[e.RowIndex].Cells["BellNo"].Value.ToString();
                BELLNOV = int.Parse(tmp);
                this.BELLNOX.Text = tmp;
                if (!isNew)
                {
                    tmp = RTSGRDView.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                    QUANTITYV = int.Parse(tmp);
                    this.QUANTITYX.Text = tmp;
                }
                else if(isNew && isRegular.Checked == true)
                {
                    tmp = RTSGRDView.Rows[e.RowIndex].Cells["CusID"].Value.ToString();
                    CUSIDV = int.Parse(tmp);
                    this.CUSIDX.Text = tmp;
                }
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    object tmpobj;
                    SqlDataReader reader;
                    if (isRegular.Checked == true) reader = new SqlCommand($"SELECT PrdID, Price, Currency, Quantity from Sales where BellNo = {BELLNOV};", con).ExecuteReader();
                    else reader = new SqlCommand($"SELECT PrdID, Price, Currency, Quantity from NRegCusSales where BellNo = {BELLNOV};", con).ExecuteReader();
                    while (reader.Read())
                    {
                        PRDIDV = reader.GetInt32(reader.GetOrdinal("PrdID"));
                        PRICEV = reader.GetDecimal(reader.GetOrdinal("Price"));
                        PRDCURRENCYV = reader.GetString(reader.GetOrdinal("Currency"));
                        SaleQuantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                    }
                    reader.Close();
                    if (isRegular.Checked == true) tmpobj = new SqlCommand($"select sum(Payin / Echangerate) from SalesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                    else tmpobj = new SqlCommand($"select sum(Payin / Echangerate) from NRegCusSalesPayments where BellNo = '{BELLNOV}';", con).ExecuteScalar();
                    PayedPrice = ((tmpobj == DBNull.Value) ? 0 : decimal.Parse(tmpobj.ToString()));
                    Console.WriteLine("payed    " + PayedPrice);
                    TotalPrice = (SaleQuantity * PRICEV);

                    SqlDataReader tmpobj1 = null; decimal spayin = 0, spayout = 0;
                    if (isRegular.Checked == true) tmpobj1 = new SqlCommand($"select sum(Payout / Echangerate ) as po , sum(Payin / Echangerate) as pi from ReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteReader();
                    else tmpobj1 = new SqlCommand($"select sum(Payout / Echangerate ) as po , sum(Payin / Echangerate) as pi from NRegCusReturnStocksPayments where BellNo = '{BELLNOV}';", con).ExecuteReader();
                    while (tmpobj1.Read())
                    {
                        try
                        {
                            spayout = tmpobj1 != null ? tmpobj1.GetDecimal(tmpobj1.GetOrdinal("po")) : 0;
                            Console.WriteLine(spayout);

                        }
                        catch(Exception) { spayout = 0; }
                        try
                        {
                            spayin = tmpobj1 != null ? tmpobj1.GetDecimal(tmpobj1.GetOrdinal("pi")) : 0;
                            Console.WriteLine(spayin);
                        }
                        catch (Exception) { spayin = 0; }
                    }
                    tmpobj1.Close();
                    PayedPrice = PayedPrice + spayin - spayout;
                    this.PRDIDX.Text = PRDIDV.ToString();
                    this.PRICEX.Text = PRICEV.ToString();
                    this.PRDCURRENCYX.Text = PRDCURRENCYV;
                    PAYOUTX_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
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
                    RTSGRDView.DataSource = null;
                    RTSGRDView.DataSource = dataTable;
                    removeCurrentActiveEvent();
                    CurrentActiveEvent = false;
                    RTSGRDView.CellDoubleClick += new DataGridViewCellEventHandler(TellerFilter_CellDoubleClick);
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
                    RTSGRDView.DataSource = null;
                    RTSGRDView.DataSource = dataTable;
                    if (RTSGRDView.Rows.Count == 1)
                    {
                        this.RTSGRDView.Rows[0].Selected = true;
                        string tmp = RTSGRDView.Rows[0].Cells["ID"].Value.ToString();
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
        private void TellerFilter_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.RTSGRDView.Rows[e.RowIndex].Selected = true;
                string tmp = RTSGRDView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                this.TELLERIDX.Text = tmp;
                TELLERIDV = int.Parse(tmp);
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 35))}";
                this.MessageX.ForeColor = Color.Red;
            }

        }

        private void EXCHANGEX_TextChanged(object sender, EventArgs e)
        {
            if (this.EXCHANGEX.Text.Length == 0) EXCHANGERV = 1;
            else
            {
                if (decimal.TryParse(this.EXCHANGEX.Text, out decimal res))
                {
                    if(res < 0) { this.EXCHANGEX.Text = "0"; }
                    EXCHANGERV = res;
                }
            }
            PAYOUTX_TextChanged(this, EventArgs.Empty);
        }

        private void PAYINX_TextChanged(object sender, EventArgs e)
        {
            if (this.PAYINX.Text.Length == 0) PAYINV = -1;
            else
            {
                try
                {
                    if (decimal.TryParse(this.PAYINX.Text, out decimal res))
                    {
                        if (res < 0) { this.PAYINX.Text = "0"; }
                        PAYINV = res;
                    }
                }
                catch (Exception) { }
            }
            PAYOUTX_TextChanged(this, EventArgs.Empty);
        }

        private void PAYOUTX_TextChanged(object sender, EventArgs e)
        {
            PAYINX.KeyPress -= new KeyPressEventHandler(NoneTypable);
            PAYOUTX.KeyPress -= new KeyPressEventHandler(NoneTypable);
            ReturnedPrice = (QUANTITYV != -1 ? QUANTITYV : 0) * PRICEV;
            TOPAYV = ((TotalPrice - ReturnedPrice) - PayedPrice) * EXCHANGERV;
            if (this.PAYOUTX.Text.Length != 0)
            {
                if (int.TryParse(this.PAYOUTX.Text, out int res))
                {
                    if (res < 0) this.PAYOUTX.Text = "";
                    if (res > (((TOPAYV < 0) ? -TOPAYV : TOPAYV)-res)) this.PAYOUTX.Text = (TOPAYV < 0 ? -1 * TOPAYV : TOPAYV).ToString();
                    PAYOUTV = res;
                }
            }else PAYOUTV = -1;
            if(PAYINV == -1) PAYINV = 0;
            if(PAYOUTV == -1) PAYOUTV = 0;
            if (TOPAYV < 0) { PAYINV = -2; PAYINX.KeyPress += new KeyPressEventHandler(NoneTypable); tmpTopay = (-1*TOPAYV) - PAYOUTV; }
            else { PAYOUTV = -2; PAYOUTX.KeyPress += new KeyPressEventHandler(NoneTypable); tmpTopay = TOPAYV - PAYINV; }
            this.TOPAYX.Text = tmpTopay.ToString();
        }
        private void NoneTypable(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void NewRCRDBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDIDV != -1 && QUANTITYV != -1 && BELLNOV != -1 && PRICEV != -1)
                    {
                        try
                        {

                            if (isRegular.Checked == true)
                            {
                                if(isNew)
                                new SqlCommand($"INSERT INTO ReturnStocks(BellNo ,Quantity, Date) VALUES ('{BELLNOV}', '{QUANTITYV}', '{DATE}');", con).ExecuteNonQuery();

                                if (PAYINV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  ReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }
                                else if (PAYOUTV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  ReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                if(isNew)
                                new SqlCommand($"INSERT INTO NRegCusReturnStocks(BellNo ,Quantity, Date) VALUES ('{BELLNOV}', '{QUANTITYV}', '{DATE}');", con).ExecuteNonQuery();

                                if (PAYINV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  NRegCusReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }
                                else if (PAYOUTV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  NRegCusReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }

                            }
                            onLoad();
                            ResetCells();
                        }
                        catch (Exception exc)
                        {
                            this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                            this.MessageX.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.MessageX.Text = "ERROR: You have to Fill All Important fields!";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }

        private void PMTBTN_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true))
                {
                    if (PRDIDV != -1 && QUANTITYV != -1 && BELLNOV != -1 && PRICEV != -1)
                    {
                        try
                        {

                            if (isRegular.Checked == true)
                            {
                                if (PAYINV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  ReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }
                                else if (PAYOUTV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  ReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (PAYINV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  NRegCusReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', null, '{PAYOUTV}', '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }
                                else if (PAYOUTV == -2)
                                {
                                    new SqlCommand($"INSERT INTO  NRegCusReturnStocksPayments(BellNo ,Payin , Payout , Echangerate , Currency , Date) VALUES ('{BELLNOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                    if (TELLERSwitch.Checked == true) new SqlCommand($"INSERT INTO  TellersTransactions(TID, CUSID, BellNo, HawalaNo, Payin, Payout, Echangerate, Currency, Date) VALUES ('{TELLERIDV}', '{CUSIDV}','{BELLNOV}', '{HAWALANOV}', '{PAYINV}', null, '{EXCHANGERV}','{CURRENCY}', '{DATE}');", con).ExecuteNonQuery();
                                }

                            }
                            onLoad();
                            ResetCells();
                        }
                        catch (Exception exc)
                        {
                            this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                            this.MessageX.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.MessageX.Text = "ERROR: You have to Fill All Important fields!";
                        this.MessageX.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception exc)
            {
                this.MessageX.Text = $"ERROR: {exc.Message.Substring(0, Math.Min(exc.Message.Length, 100))}";
                this.MessageX.ForeColor = Color.Red;
            }
        }

        private void ResetCells()
        {
            this.PRDIDX.Text = this.PRICEX.Text = this.CUSIDX.Text = this.QUANTITYX.Text = this.PAYINX.Text = this.PAYOUTX.Text = this.EXCHANGEX.Text = this.TELLERFilter.Text = this.TELLERIDX.Text = this.HAWALANOX.Text = this.BELLNOX.Text = "";
            PRDIDV = CUSIDV = TELLERIDV = HAWALANOV = QUANTITYV = -1;
            PRICEV = PAYINV = PAYOUTV = -1; EXCHANGERV = 1;
            PRDCURRENCYV = CURRENCY = DATE = null;
        }
    }
}
 