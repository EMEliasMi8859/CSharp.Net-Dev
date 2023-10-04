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

namespace TawheedBasitPvtLtd.Forms.TabController.RegistriesForms
{
    public partial class ProductRegForm : Form
    {

        public event EventHandler RCRDUpdated;
        bool isRegd;
        string prdID = null, dstpath = null, prdname = null, prdcmpname = null, currency = "AFG";
        decimal Price = -1;
        public ProductRegForm(bool isReg, DataGridViewRow row = null)
        {
            InitializeComponent();
            if (isReg) initializeRegistrationBTN();
            else initializeModificationBTN();

            PaintForm(isReg);
            isRegd = isReg;
            string tmp;
            if(row != null)
            {
                tmp = row.Cells["ID"].Value.ToString();
                prdID = tmp;
                tmp = row.Cells["Name"].Value.ToString();
                this.NameTXT.Text = tmp;
                prdname = tmp;
                tmp = row.Cells["CompanyName"].Value.ToString();
                this.CMPNameTXT.Text = tmp;
                prdcmpname = tmp;
                tmp = row.Cells["Price"].Value.ToString();
                this.PriceTXT.Text = tmp;
                Price = decimal.Parse(tmp);
                tmp = row.Cells["Currency"].Value.ToString();
                this.Currency.Text = tmp;
                currency = tmp;
                tmp = row.Cells["Image"].Value.ToString();
                dstpath = tmp;
            }
        }
        public void PaintForm(bool isReg)
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.PRDGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1); 
            Configurations.ControlsConfigurations.InitializeTextBox(this.NameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label2);
            Configurations.ControlsConfigurations.InitializeTextBox(this.CMPNameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PriceTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label5);
 
            this.Currency.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.Currency.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.Currency.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            this.Currency.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            Configurations.ControlsConfigurations.InitializeButton(this.OpenImageBTN, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            if (isReg) Configurations.ControlsConfigurations.InitializeButton(this.AddProduct, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            else
            {
                Configurations.ControlsConfigurations.InitializeButton(this.ModifyRCRD, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
                Configurations.ControlsConfigurations.InitializeButton(this.DeleteRCRD, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            }
        }

        private void NameTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.NameTXT.Text.Length != 0) prdname = this.NameTXT.Text.ToString();
            else prdname = null;
            resetColors();
        }
        private void CMPNameTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.CMPNameTXT.Text.Length != 0) prdcmpname = this.CMPNameTXT.Text.ToString();
            else prdcmpname = null;
            resetColors();
        }
        private void PriceTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.PriceTXT.Text.Length != 0)
                if (decimal.TryParse(this.PriceTXT.Text.ToString(), out decimal res)) Price = res;
                else Price = -1;
            else Price = -1;
            resetColors();
        }

        private void Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Currency.Text.Length != 0) currency = this.Currency.Text.ToString();
            else currency = null;
            resetColors();
        }

        private void OpenImageBTN_Click(object sender, EventArgs e)
        {
            resetColors();
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                {
                    if (con != null)
                    {
                        string imageName;
                        object tmp = new SqlCommand("SELECT MAX (ID) from products", con).ExecuteScalar();
                        if (tmp != DBNull.Value) imageName = (int.Parse(tmp.ToString()) + 1).ToString(); else imageName = "1";
                        string srcpath = null;
                        OpenFileDialog openFile = new OpenFileDialog();
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry)) Configurations.ProjectConfigurations.createDirectory(Configurations.ConfigurationKeys.ImagesDataDirecotry);
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.PRODUCTTABLEIMAGES)) Configurations.ProjectConfigurations.createDirectory(Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.PRODUCTTABLEIMAGES);
                        openFile.FileName = $"{imageName.ToUpper()}";
                        openFile.Filter = "Image Files(*.jpg, *.jpeg, *.png)|*.jpg; *.JPEG; *.png|Icons Files(*.ico)|*.ico|All files(*.*)|*.*";
                        openFile.Title = "Open Image File for the product!";
                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            srcpath = openFile.FileName;
                            dstpath = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.PRODUCTTABLEIMAGES +"\\" +imageName + Path.GetExtension(openFile.FileName);
                            try
                            {
                                if (File.Exists(dstpath)) File.Delete(dstpath);
                                File.Copy(srcpath, dstpath);
                            }
                            catch (Exception exc) { this.OpenImageBTN.FillColor = Color.Red; this.OpenImageBTN.HoverState.FillColor = Color.Red; dstpath = null; }
                            this.OpenImageBTN.FillColor = Color.Green;
                            this.OpenImageBTN.HoverState.FillColor = Color.Green;
                            dstpath = imageName + Path.GetExtension(openFile.FileName);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                this.OpenImageBTN.FillColor = Color.Red;
                this.OpenImageBTN.HoverState.FillColor = Color.Red;
            }
        }

        private void AddProduct_Click(Object sender, EventArgs e)
        {
            if(prdname != null && prdcmpname != null && Price != -1)
            {
                try
                {
                    using( SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                    {
                        try
                        {
                            new SqlCommand($"INSERT INTO Products (Name, CompanyName , Price , Currency , Image) VALUES ('{prdname}', '{prdcmpname}', '{Price}', '{currency}', '{dstpath}');", con).ExecuteNonQuery();
                            this.NameTXT.Text = this.CMPNameTXT.Text = this.PriceTXT.Text = "";
                            prdname = prdcmpname = dstpath = null;
                            Price = -1;
                            currency = "AFG";
                            this.AddProduct.HoverState.FillColor = Color.Green;
                            this.AddProduct.FillColor = Color.Green;
                            RCRDUpdated?.Invoke(this, EventArgs.Empty);
                        }
                        catch (Exception exc) { this.AddProduct.HoverState.FillColor = Color.Red; this.AddProduct.FillColor = Color.Red; }
                    }
                }
                catch (Exception exc) {
                    this.AddProduct.HoverState.FillColor = Color.Red;
                    this.AddProduct.FillColor = Color.Red;
                }
            }
        }
        private void DeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                {
                    try
                    {
                        int prdInstance = -1;
                        object tmp = new SqlCommand($"select * from NRegCusSales where PrdID = '{prdID}'; select * from sales where PrdID = '{prdID}';select * from Purchases where PrdID = '{prdID}';", con).ExecuteScalar();
                        if (tmp != DBNull.Value && tmp != null) prdInstance = int.Parse(tmp.ToString());
                        else prdInstance = 0;
                        if (prdInstance == 0) new SqlCommand($"delete from Products where ID = '{prdID}';", con).ExecuteNonQuery();
                        else
                        {
                            this.DeleteRCRD.FillColor = Color.Red;
                            this.DeleteRCRD.HoverState.FillColor = Color.Red;
                        }
                        this.NameTXT.Text = this.CMPNameTXT.Text = this.PriceTXT.Text = "";
                        prdname = prdcmpname = dstpath = null;
                        Price = -1;
                        currency = "AFG";
                        this.DeleteRCRD.FillColor = Color.Green;
                        this.DeleteRCRD.HoverState.FillColor = Color.Green;
                        RCRDUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception exc) { this.DeleteRCRD.HoverState.FillColor = Color.Red; this.DeleteRCRD.FillColor = Color.Red; }
                }
            }
            catch (Exception exc)
            {
                this.DeleteRCRD.HoverState.FillColor = Color.Red;
                this.DeleteRCRD.FillColor = Color.Red;
            }
        }
        private void ModifyRecord_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                {
                    try
                    {
                        decimal PRDPriceDB = -1;
                        string PRDCurrencyDB = null;
                        SqlDataReader tmp;
                        tmp = new SqlCommand($"SELECT Price, Currency FROM Products where ID = '{prdID}'", con).ExecuteReader();
                        while (tmp.Read())
                        {
                            PRDPriceDB = decimal.Parse(tmp["Price"].ToString());
                            PRDCurrencyDB = tmp["Currency"].ToString();
                        }
                        tmp.Close();

                        if (PRDCurrencyDB != null && PRDPriceDB != -1)
                        {
                            if (PRDCurrencyDB == currency && PRDPriceDB == Price) new SqlCommand($"UPDATE Products SET Name = '{prdname}', CompanyName = '{prdcmpname}', Image = '{dstpath}' where ID = '{prdID}';", con).ExecuteNonQuery();
                            else
                            {
                                if (prdname != null && prdcmpname != null && Price != -1)
                                {
                                    try
                                    {
                                        new SqlCommand($"INSERT INTO Products (Name, CompanyName , Price , Currency , Image) VALUES ('{prdname}', '{prdcmpname}', '{Price}', '{currency}', '{dstpath}');", con).ExecuteNonQuery();
                                        this.ModifyRCRD.HoverState.FillColor = Color.Green;
                                        this.ModifyRCRD.FillColor = Color.Green;
                                    }
                                    catch (Exception exc) { this.ModifyRCRD.HoverState.FillColor = Color.Red; this.ModifyRCRD.FillColor = Color.Red; }
                                }
                            }
                            this.NameTXT.Text = this.CMPNameTXT.Text = this.PriceTXT.Text = "";
                            prdname = prdcmpname = dstpath = null;
                            Price = -1;
                            currency = "AFG";
                            this.ModifyRCRD.HoverState.FillColor = Color.Green;
                            this.ModifyRCRD.FillColor = Color.Green;
                            RCRDUpdated?.Invoke(this, EventArgs.Empty);

                        }
                    }
                    catch (Exception exc) { this.ModifyRCRD.HoverState.FillColor = Color.Red; this.ModifyRCRD.FillColor = Color.Red; }
                }
            }
            catch (Exception exc)
            {
                this.ModifyRCRD.HoverState.FillColor = Color.Red;
                this.ModifyRCRD.FillColor = Color.Red;
            }
        }
        private void resetColors()
        {
            if (isRegd)
            {
                this.AddProduct.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR);
                this.AddProduct.HoverState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR), -2 * 0.3); ;
            }
            else
            {
                this.ModifyRCRD.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR);
                this.ModifyRCRD.HoverState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR), -2 * 0.3); ;
                this.DeleteRCRD.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR);
                this.DeleteRCRD.HoverState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR), -2 * 0.3); ;
            }
            this.OpenImageBTN.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR); 
            this.OpenImageBTN.HoverState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR), -2 * 0.3);
        }
    }
}
