using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd.Forms.TabController.RegistriesForms
{
    public partial class RegTellerForm : Form
    {
        public event EventHandler RCRDUpdated;
        bool isRegd;
        string Name = null, OfficeNo = null, Phone = null, Address = null, ID = null;
        public RegTellerForm(bool isReg = false, DataGridViewRow row = null)
        {
            InitializeComponent();
            if (isReg) initializeRegistrationBTN(); else initializeModificationBTN();
            PaintForm(isReg);
            isRegd = isReg;
            string tmp;
            if (row != null)
            {
                tmp = row.Cells["ID"].Value.ToString();
                ID = tmp;
                tmp = row.Cells["Name"].Value.ToString();
                this.NameTXT.Text = tmp;
                Name = tmp;
                tmp = row.Cells["OfficeNo"].Value.ToString();
                this.OfficeNoTXT.Text = tmp;
                OfficeNo = tmp;
                tmp = row.Cells["Address"].Value.ToString();
                this.AddressTXT.Text = tmp;
                Address = tmp;
                tmp = row.Cells["Phone"].Value.ToString();
                this.PhoneTXT.Text = tmp;
                Phone = tmp;
            }
        }
        public void PaintForm(bool isReg)
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.PRDGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1);
            Configurations.ControlsConfigurations.InitializeTextBox(this.NameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label2);
            Configurations.ControlsConfigurations.InitializeTextBox(this.OfficeNoTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PhoneTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label5);
            Configurations.ControlsConfigurations.InitializeTextBox(this.AddressTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            if (isReg) Configurations.ControlsConfigurations.InitializeButton(this.AddProduct, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            else
            {
                Configurations.ControlsConfigurations.InitializeButton(this.ModifyRCRD, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
                Configurations.ControlsConfigurations.InitializeButton(this.DeleteRCRD, Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);
            }
        }

        private void NameTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.NameTXT.Text.Length != 0) Name = this.NameTXT.Text.ToString();
            else Name = null;
            resetColors();
        }

        private void OfficeNoTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.OfficeNoTXT.Text.Length != 0) OfficeNo = this.OfficeNoTXT.Text.ToString();
            else OfficeNo = null;
            resetColors();
        }

        private void PhoneTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.PhoneTXT.Text.Length != 0) Phone = this.PhoneTXT.Text.ToString();
            else Phone = null;
            resetColors();
        }

        private void AddressTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.AddressTXT.Text.Length != 0) Address = this.AddressTXT.Text.ToString();
            else Address = null;
            resetColors();
        }
        private void AddProduct_Click(Object sender, EventArgs e)
        {
            if (Name != null && OfficeNo != null && Phone != null)
            {
                try
                {

                    using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                    {
                        try
                        {
                            new SqlCommand($"INSERT INTO Tellers(Name, OfficeNo, Phone, Address) VALUES ('{Name}', '{OfficeNo}', '{Phone}', '{Address}');", con).ExecuteNonQuery();
                            this.NameTXT.Text = this.OfficeNoTXT.Text = this.PhoneTXT.Text = this.AddressTXT.Text = Name = OfficeNo = Phone = Address = ID = "";
                            this.AddProduct.HoverState.FillColor = Color.Green;
                            this.AddProduct.FillColor = Color.Green;
                            RCRDUpdated?.Invoke(this, EventArgs.Empty);
                        }
                        catch (Exception exc) { this.AddProduct.HoverState.FillColor = Color.Red; this.AddProduct.FillColor = Color.Red; }
                    }
                }
                catch (Exception exc)
                {
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
                        new SqlCommand($"delete from Tellers where ID = '{ID}';", con).ExecuteNonQuery();
                        this.NameTXT.Text = this.OfficeNoTXT.Text = this.PhoneTXT.Text = this.AddressTXT.Text = Name = OfficeNo = Phone = Address = ID = "";
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
                        string UpdateQuery = "";
                        bool first = false;
                        if (Name != null)
                        {
                            first = true;
                            UpdateQuery += $"Name = '{Name}'";
                        }
                        if (OfficeNo != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"OfficeNo = '{OfficeNo}'";
                        }
                        if (Phone != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"Phone = '{Phone}'";

                        }
                        if (Address != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"Address = '{Address}'";
                        }
                        new SqlCommand($"UPDATE Tellers set {UpdateQuery} where ID = '{ID}'", con).ExecuteNonQuery();
                        this.NameTXT.Text = this.OfficeNoTXT.Text = this.PhoneTXT.Text = this.AddressTXT.Text = Name = OfficeNo = Phone = Address = ID = "";
                        this.ModifyRCRD.HoverState.FillColor = Color.Green;
                        this.ModifyRCRD.FillColor = Color.Green;
                        RCRDUpdated?.Invoke(this, EventArgs.Empty);

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


        }
    }
}