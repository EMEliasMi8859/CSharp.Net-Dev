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
    public partial class RegCustomerForm : Form
    {
        public event EventHandler RCRDUpdated;
        bool isRegd;
        string ID = null, Name = null, FName = null, Address = null, Phone = null, dstPath = null;
        public RegCustomerForm(bool isReg = false, DataGridViewRow row = null)
        {
            InitializeComponent();
            if (isReg) initializeRegistrationBTN();
            else initializeModificationBTN();
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
                tmp = row.Cells["FatherName"].Value.ToString();
                this.FNameTXT.Text = tmp;
                FName = tmp;
                tmp = row.Cells["Address"].Value.ToString();
                this.AddressTXT.Text = tmp;
                Address = tmp;
                tmp = row.Cells["Phone"].Value.ToString();
                this.PhoneTXT.Text = tmp;
                Phone = tmp;
                tmp = row.Cells["Image"].Value.ToString();
                dstPath = tmp;
            }
        }
        public void PaintForm(bool isReg)
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeGroupBox(this.PRDGRP, null, null, null, -1, -1, true, true, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label1);
            Configurations.ControlsConfigurations.InitializeTextBox(this.NameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label2);
            Configurations.ControlsConfigurations.InitializeTextBox(this.FNameTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3);
            Configurations.ControlsConfigurations.InitializeTextBox(this.PhoneTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label4);
            Configurations.ControlsConfigurations.InitializeTextBox(this.AddressTXT, null, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label5);

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
            if (this.NameTXT.Text.Length != 0) Name = this.NameTXT.Text.ToString();
            else Name = null;
            resetColors();
        }

        private void FNameTXT_TextChanged(object sender, EventArgs e)
        {
            if (this.FNameTXT.Text.Length != 0) FName = this.FNameTXT.Text.ToString();
            else FName = null;
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
                        object tmp = new SqlCommand("SELECT MAX (ID) from Customers", con).ExecuteScalar();
                        if (tmp != DBNull.Value) imageName = (int.Parse(tmp.ToString()) + 1).ToString(); else imageName = "1";
                        string srcpath = null;
                        OpenFileDialog openFile = new OpenFileDialog();
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry)) Configurations.ProjectConfigurations.createDirectory(Configurations.ConfigurationKeys.ImagesDataDirecotry);
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.CUSTOMERTABLEIMAGES)) Configurations.ProjectConfigurations.createDirectory(Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.CUSTOMERTABLEIMAGES);
                        openFile.FileName = $"{imageName.ToUpper()}";
                        openFile.Filter = "Image Files(*.jpg, *.jpeg, *.png)|*.jpg; *.JPEG; *.png|Icons Files(*.ico)|*.ico|All files(*.*)|*.*";
                        openFile.Title = "Open Image File for the Customer!";
                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            srcpath = openFile.FileName;
                            dstPath = Directory.GetCurrentDirectory() + Configurations.ConfigurationKeys.ImagesDataDirecotry + Configurations.ConfigurationKeys.CUSTOMERTABLEIMAGES + "\\" + imageName + Path.GetExtension(openFile.FileName);
                            try
                            {
                                if (File.Exists(dstPath)) File.Delete(dstPath);
                                File.Copy(srcpath, dstPath);
                            }
                            catch (Exception exc) { this.OpenImageBTN.FillColor = Color.Red; this.OpenImageBTN.HoverState.FillColor = Color.Red; dstPath = null; }
                            this.OpenImageBTN.FillColor = Color.Green;
                            this.OpenImageBTN.HoverState.FillColor = Color.Green;
                            dstPath = imageName + Path.GetExtension(openFile.FileName);
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
            if (Name != null && FName != null && Phone != null && Address != null)
            {
                try
                {
                    using (SqlConnection con = Configurations.DataBaseConnection.ConnectToSqlServer(true, true, true, true))
                    {
                        try
                        {
                            new SqlCommand($"INSERT INTO Customers(Name , FatherName, Address, Phone, Image) VALUES ('{Name}', '{FName}','{Address}','{Phone}','{dstPath}');", con).ExecuteNonQuery();
                            this.NameTXT.Text = this.FNameTXT.Text = this.AddressTXT.Text = this.PhoneTXT.Text = dstPath = Name = FName = Address = Phone = null;
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
                        int prdInstance = -1;
                        object tmp = new SqlCommand($"select * from Sales where PrdID = '{ID}';", con).ExecuteScalar();
                        if (tmp != DBNull.Value && tmp != null) prdInstance = int.Parse(tmp.ToString());
                        else prdInstance = 0;
                        if (prdInstance == 0) new SqlCommand($"delete from Customers where ID = '{ID}';", con).ExecuteNonQuery();
                        else
                        {
                            this.DeleteRCRD.FillColor = Color.Red;
                            this.DeleteRCRD.HoverState.FillColor = Color.Red;
                        }
                        this.NameTXT.Text = this.FNameTXT.Text = this.AddressTXT.Text = this.PhoneTXT.Text = ID = Name = FName = Phone = Address = dstPath = null;
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
                        if (FName != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"FatherName = '{FName}'";
                        }
                        if (Address != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"Address = '{Address}'";

                        }
                        if (Phone != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", "; 
                            UpdateQuery += $"Phone = '{Phone}'";
                        }
                        if (dstPath != null)
                        {
                            if (!first) first = true;
                            else UpdateQuery += ", ";
                            UpdateQuery += $"Image = '{dstPath}'";
                        }
                        new SqlCommand($"UPDATE Customers set {UpdateQuery} where ID = '{ID}'", con).ExecuteNonQuery();
                        this.NameTXT.Text = this.FNameTXT.Text = this.AddressTXT.Text = this.PhoneTXT.Text = ID = Name = FName = Phone = Address = dstPath = null;
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
            this.OpenImageBTN.FillColor = ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR);
            this.OpenImageBTN.HoverState.FillColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.HighLightingColors.CurrentPIMARYHIGHLIGHTCOLOR), -2 * 0.3);
        }
    }
}
