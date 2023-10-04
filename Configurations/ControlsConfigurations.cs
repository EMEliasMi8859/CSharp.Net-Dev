using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Effects;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;
using System.IO;
using System;

namespace TawheedBasitPvtLtd.Configurations
{

    //AddDropShadow(frm, cnt, clr, opct, lctn, sze)                      add shadow takes form control color opacity location size
    class ControlsConfigurations
    {
        public static void regDefaultControls()
        {
            File.Delete(ConfigurationKeys.ControlConfigFileName);
            ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ControlConfigFileName);
            string firmName = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ORGANIZATIONNAMEKEY, ConfigurationKeys.ControlConfigFileName);
            string moto = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ORGANIZATIONNAMEKEY, ConfigurationKeys.ControlConfigFileName);

            using (StreamWriter Swriter = new StreamWriter(ConfigurationKeys.ControlConfigFileName))
            {

                Swriter.WriteLine($"{ConfigurationKeys.LOGOCOLORKey}:#f0501e");
                Swriter.WriteLine($"{ConfigurationKeys.BTNBRDRDSKey}:8");
                Swriter.WriteLine($"{ConfigurationKeys.BTNBRDTKNKey}:1");
                Swriter.WriteLine($"{ConfigurationKeys.BTNSHDSPRKey}:5");
                Swriter.WriteLine($"{ConfigurationKeys.TBXBRDRDSKey}:8");
                Swriter.WriteLine($"{ConfigurationKeys.TBXBRDTKNKey}:1");
                Swriter.WriteLine($"{ConfigurationKeys.TBXSHDSPRKey}:5");
                Swriter.WriteLine($"{ConfigurationKeys.DGWHDRHGTKey}:35");
                if (firmName != null && firmName != "") Swriter.WriteLine($"{ConfigurationKeys.ORGANIZATIONNAMEKEY}:{firmName}");
                if (moto != null && moto != "") Swriter.WriteLine($"{ConfigurationKeys.ABOUTORGANIZATIONKEY}:{moto}");
            }
            InitializedVariables.initializeControls();
        }
        // type one for alternat, type two for header, type 3 for cell style, type 4 for row headers
        public static void SetCellStyle(DataGridViewCellStyle cell, string fillcolor, string forcolor, string type = "1 header, 2 cellstyle, 3 rowheaders", bool useHighlightingColor = false)
        {
            cell.Font = new System.Drawing.Font("Helvetica", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cell.ForeColor = ColorTranslator.FromHtml(forcolor);
            cell.SelectionBackColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(fillcolor), 0.3);
            cell.SelectionForeColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(forcolor), 0.3);
            if (type == "1")
            {
                if (useHighlightingColor) cell.BackColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(HighLightingColors.PIMARYHIGHLIGHTCOLOR), 0.2);
                else cell.BackColor = Configurations.ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(fillcolor), -0.2);
                cell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                cell.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                cell.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            }
            else if (type == "2")
            {

                cell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                cell.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                cell.BackColor = ColorTranslator.FromHtml(fillcolor);
            }
            else if (type == "3")
            {
                cell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                cell.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                cell.BackColor = ColorTranslator.FromHtml(fillcolor);

            }
            else
            {
                cell.BackColor = ColorTranslator.FromHtml(fillcolor);
            }
        }

        public static void InitializeGridView(Guna2DataGridView Contrl, string fillcolor = null, string backgroundcolor = null, string forecolor = null, int headHieght = -1, string bordercolor = null, bool istransparent = false, bool useHighlightingColor = false)
        {

            if (fillcolor == null || fillcolor == "") fillcolor = InitializedVariables.DGWBGRCLR;
            if (backgroundcolor == null || backgroundcolor == "") backgroundcolor = InitializedVariables.FRMBGRCLR;
            if (forecolor == null || forecolor == "") forecolor = InitializedVariables.DGWFGRCLR;
            if (bordercolor == null || bordercolor == "") bordercolor = InitializedVariables.DGWBRDCLR;
            if (istransparent) fillcolor = backgroundcolor;
            if (useHighlightingColor) bordercolor = HighLightingColors.PIMARYHIGHLIGHTCOLOR;
            if (headHieght == -1) headHieght = 35;

            Contrl.AllowUserToOrderColumns = true;
            Contrl.AllowUserToResizeRows = true;
            Contrl.AllowUserToAddRows = false;
            SetCellStyle(Contrl.AlternatingRowsDefaultCellStyle, fillcolor, forecolor);
            Contrl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            Contrl.BackgroundColor = ColorTranslator.FromHtml(backgroundcolor);
            Contrl.ForeColor = ColorTranslator.FromHtml(forecolor);
            Contrl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            SetCellStyle(Contrl.ColumnHeadersDefaultCellStyle, fillcolor, forecolor, "1", useHighlightingColor);
            Contrl.ColumnHeadersHeight = headHieght;
            SetCellStyle(Contrl.DefaultCellStyle, fillcolor, forecolor, "2");
            Contrl.GridColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(bordercolor), -0.3);
            Contrl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            Contrl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            Contrl.ScrollBars = ScrollBars.Both;
            SetCellStyle(Contrl.RowHeadersDefaultCellStyle, fillcolor, forecolor, "3");
        }

        public static void InitializeTabController(Guna2TabControl cnt, string tabcntbgrclr = null, string parentbgrclr = null, string brdrclr = null, bool transparnt = false, bool useHightlighColor = false)
        {
            string ParentCNTBGR = InitializedVariables.FRMBGRCLR;
            string BGRColor = InitializedVariables.BTNBGRCLR;
            string HightLightCLR;
            string BorderColor;
            float FillFactor = 0.3f;
            if (parentbgrclr != null) ParentCNTBGR = parentbgrclr;
            if (transparnt)
            {
                BGRColor = ParentCNTBGR;
            }
            if (brdrclr != null) BorderColor = brdrclr; else BorderColor = BGRColor;
            if (useHightlighColor) HightLightCLR = Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR;
            else HightLightCLR = InitializedVariables.BTNBGRCLR;


            cnt.TabButtonHoverState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BorderColor), -FillFactor);
            cnt.TabButtonHoverState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -FillFactor);
            cnt.TabButtonHoverState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNFGRCLR), FillFactor);
            cnt.TabButtonIdleState.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cnt.TabButtonHoverState.InnerColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -1.5 * FillFactor);

            cnt.TabButtonIdleState.BorderColor = ColorTranslator.FromHtml(BorderColor);
            cnt.TabButtonIdleState.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.TabButtonIdleState.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cnt.TabButtonIdleState.ForeColor = ColorTranslator.FromHtml(InitializedVariables.BTNFGRCLR);
            cnt.TabButtonIdleState.InnerColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNBGRCLR), -FillFactor);

            cnt.TabButtonSelectedState.BorderColor = ColorTranslator.FromHtml(BorderColor);
            cnt.TabButtonSelectedState.FillColor = ColorTranslator.FromHtml(HightLightCLR);
            cnt.TabButtonSelectedState.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cnt.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            cnt.TabButtonSelectedState.InnerColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(HightLightCLR), -1.5 * FillFactor);

            cnt.TabMenuBackColor = ColorTranslator.FromHtml(InitializedVariables.GRPBGRCLR);
            foreach (TabPage tbp in cnt.TabPages) tbp.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);
        }

        public static void InitializeGroupBox(Guna2GroupBox cnt, string gbxbgrclr = null, string parentbgrclr = null, string brdrclr = null, int brdrrds = -1, int brdthkns = -1, bool NoCustombrd = false, bool transparnt = false, bool useHightlighColorShad = false)
        {
            string ParentCNTBGR = InitializedVariables.FRMBGRCLR;
            string BGRColor = InitializedVariables.GRPBGRCLR;
            string HightLightShad;
            int BorderRadius = InitializedVariables.TBXBRDRDS;
            int BorderThikness = InitializedVariables.TBXBRDTKN;
            int CustomBorder = 25;
            string BorderColor = InitializedVariables.BTNBRDCLR;
            if (parentbgrclr != null) ParentCNTBGR = parentbgrclr;
            if (gbxbgrclr != null) BGRColor = gbxbgrclr;
            if (brdrclr != null) BorderColor = brdrclr;
            if (brdrrds != -1) BorderRadius = brdrrds;
            if (brdthkns != -1) BorderThikness = brdthkns;
            if (transparnt)
            {
                BGRColor = ParentCNTBGR;
                cnt.UseTransparentBackground = true;
            }
            if (NoCustombrd) CustomBorder = 0;
            else cnt.TextOffset = new System.Drawing.Point(5, -6);
            if (useHightlighColorShad) HightLightShad = HightLightShad = Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR;
            else HightLightShad = Configurations.InitializedVariables.LOGOCOLOR;
            cnt.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);
            cnt.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.ForeColor = ColorTranslator.FromHtml(InitializedVariables.LBLFGRCLR);

            cnt.BorderColor = ColorTranslator.FromHtml(BorderColor);
            cnt.BorderRadius = BorderRadius;
            cnt.BorderThickness = BorderThikness;
            cnt.CustomBorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.GRPBGRCLR), 0.3);
            cnt.CustomBorderThickness = new System.Windows.Forms.Padding(0, CustomBorder, 0, 0);

            cnt.ShadowDecoration.BorderRadius = BorderRadius;
            cnt.ShadowDecoration.Color = ColorTranslator.FromHtml(HightLightShad);
            cnt.ShadowDecoration.Depth = 23;
            cnt.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(5);
            cnt.Padding = new System.Windows.Forms.Padding(5, 30, 5, 5);
            cnt.ShadowDecoration.Enabled = true;
            cnt.UseTransparentBackground = true;
        }
        public static void InitializeTextBox(Guna2TextBox cnt, String textboxbgr = null, String parentbgrclr = null, int borderRadius = -1, int borderThikness = -1, bool trnsprnt = false)
        {
            string ParentCNTBGR = InitializedVariables.GRPBGRCLR;
            string BGRColor = InitializedVariables.TBXBGRCLR;
            int BorderRadius = InitializedVariables.TBXBRDRDS;
            int BorderThikness = InitializedVariables.TBXBRDTKN;
            float FillFactor = 0.2f;
            if (parentbgrclr != null && parentbgrclr != "")
                ParentCNTBGR = parentbgrclr;
            if (textboxbgr != null && textboxbgr != "")
                BGRColor = textboxbgr;
            if (borderRadius != -1)
                BorderRadius = borderRadius;
            if (borderThikness != -1)
                BorderThikness = borderThikness;
            if (trnsprnt)
            {
                FillFactor = 0;
                BGRColor = ParentCNTBGR;
            }

            cnt.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);
            cnt.BorderColor = ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR);
            cnt.BorderRadius = BorderRadius;
            cnt.BorderThickness = BorderThikness;
            cnt.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.PlaceholderForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXPCHCLR), -0.3);
            cnt.ForeColor = ColorTranslator.FromHtml(InitializedVariables.TBXFGRCLR);

            cnt.FocusedState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXBRDCLR), -0.3);
            cnt.FocusedState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), FillFactor);
            cnt.FocusedState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXFGRCLR), -0.3);
            cnt.FocusedState.PlaceholderForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXPCHCLR), -0.3);

            cnt.HoverState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXBRDCLR), -0.4);
            cnt.HoverState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -FillFactor);
            cnt.HoverState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXFGRCLR), -0.4);
            cnt.HoverState.PlaceholderForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXPCHCLR), -0.4);

            cnt.ShadowDecoration.BorderRadius = InitializedVariables.TBXBRDRDS;
            cnt.ShadowDecoration.Color = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.InitializedVariables.INVTHMCLR), -0.2);
            cnt.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(InitializedVariables.TBXSHDSPR);
            cnt.ShadowDecoration.Depth = 8;
            cnt.ShadowDecoration.Enabled = true;

            cnt.DisabledState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXBRDCLR), 0.3);
            cnt.DisabledState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -FillFactor);
            cnt.DisabledState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXFGRCLR), -0.3);
        }

        public static void InitializeToggleSwitch(Guna2ToggleSwitch cnt, String radbgr = null, String parentbgrclr = null, int borderRadius = -1, int borderThikness = -1, bool trnsprnt = false)
        {
            string ParentCNTBGR = InitializedVariables.GRPBGRCLR;
            string BGRColor = InitializedVariables.RADBGRCLR;
            string ACTBGRcolor = InitializedVariables.RADACTBGR;
            int BorderThikness = InitializedVariables.TBXBRDTKN;
            int BorderRadius = InitializedVariables.BTNBRDRDS;
            float FillFactor = 0.3f;
            if (parentbgrclr != null)
                ParentCNTBGR = parentbgrclr;
            if (radbgr != null)
                BGRColor = radbgr;
            if (borderThikness != -1)
                BorderThikness = borderThikness;
            if (trnsprnt)
            {
                FillFactor = 0;
                BGRColor = ParentCNTBGR;
                ACTBGRcolor = ParentCNTBGR;
            }
            cnt.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);

            cnt.UncheckedState.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.UncheckedState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXBRDCLR), -0.5);
            cnt.UncheckedState.BorderRadius = BorderRadius;
            cnt.UncheckedState.BorderThickness = BorderThikness;
            cnt.UncheckedState.InnerColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.LOGOCOLOR), -FillFactor);
            cnt.UncheckedState.InnerBorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -0.9);
            /*cnt.UncheckedState.InnerBorderRadius = BorderRadius;*/
            /*cnt.UncheckedState.InnerBorderThickness = BorderThikness;*/

            cnt.CheckedState.FillColor = ColorTranslator.FromHtml(ACTBGRcolor);
            cnt.CheckedState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.TBXBRDCLR), -0.5);
            cnt.CheckedState.BorderRadius = BorderRadius;
            cnt.CheckedState.BorderThickness = BorderThikness;
            cnt.CheckedState.InnerColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.LOGOCOLOR), -FillFactor);
            cnt.CheckedState.InnerBorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(ACTBGRcolor), 0.9);
            /*cnt.CheckedState.InnerBorderRadius = BorderRadius;*/
            /*cnt.CheckedState.InnerBorderThickness = BorderThikness;*/
        }

        public static void InitializeLabel(Label cnt, String clr = null)
        {
            string FGRColor = InitializedVariables.LBLFGRCLR;
            if (clr != null)
                FGRColor = clr;
            cnt.BackColor = Color.Transparent;
            cnt.ForeColor = ColorTranslator.FromHtml(FGRColor);

        }
        public static void InitializeControls(Guna2ControlBox cnt, String cntbgr = null, String parentbgrclr = null, bool trnsprnt = false)
        {
            string ParentCNTBGR = InitializedVariables.FRMBGRCLR;
            string BGRColor = InitializedVariables.CNTBGRCLR;
            int BorderThikness = InitializedVariables.TBXBRDTKN;
            float FillFactor = 0.2f;
            if (parentbgrclr != null)
                ParentCNTBGR = parentbgrclr;
            if (cntbgr != null)
                BGRColor = cntbgr;
            if (trnsprnt)
            {
                FillFactor = 0;
                BGRColor = ParentCNTBGR;
            } else
                cnt.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -2 * FillFactor);
            cnt.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);
            cnt.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.ForeColor = ColorTranslator.FromHtml(InitializedVariables.CNTFGRCLR);
            cnt.HoverState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -0.3);
            cnt.HoverState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -0.3);
            cnt.PressedColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -0.4);
            cnt.IconColor = ColorTranslator.FromHtml(InitializedVariables.CNTFGRCLR);
        }
        public static void InitializeButton(Guna2Button cnt, String btnbgr = null, String parentbgrclr = null, int borderRadius = -1, int borderThikness = -1, bool trnsprnt = false, string btnfgr = null)
        {
            string ParentCNTBGR = InitializedVariables.GRPBGRCLR;
            string BGRColor = InitializedVariables.BTNBGRCLR;
            string ButtonFGR = InitializedVariables.BTNFGRCLR;
            int BorderRadius = InitializedVariables.BTNBRDRDS;
            int BorderThikness = InitializedVariables.BTNBRDTKN;
            float FillFactor = 0.3f;
            if (parentbgrclr != null)
                ParentCNTBGR = parentbgrclr;
            if (btnbgr != null)
                BGRColor = btnbgr;
            if (borderRadius != -1)
                BorderRadius = borderRadius;
            if (borderThikness != -1)
                BorderThikness = borderThikness;
            if (trnsprnt)
            {
                FillFactor = 0.1f;
                BGRColor = ParentCNTBGR;
                cnt.UseTransparentBackground = true;
                cnt.HoverState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNFGRCLR), -0.1);
            } else cnt.HoverState.ForeColor = Color.White;
            if (btnfgr != null) ButtonFGR = btnfgr;
            cnt.BackColor = ColorTranslator.FromHtml(ParentCNTBGR);
            cnt.BorderColor = ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR);
            cnt.BorderRadius = BorderRadius;
            cnt.BorderThickness = BorderThikness;
            cnt.FillColor = ColorTranslator.FromHtml(BGRColor);
            cnt.ForeColor = ColorTranslator.FromHtml(ButtonFGR);
            cnt.CustomBorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR), -0.3);

            cnt.PressedColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNFGRCLR), 0.3);

            cnt.HoverState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR), -0.2);
            cnt.HoverState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -2 * FillFactor);

            cnt.ShadowDecoration.BorderRadius = InitializedVariables.BTNBRDRDS;
            cnt.ShadowDecoration.Color = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(Configurations.InitializedVariables.INVTHMCLR), -0.2);
            cnt.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(InitializedVariables.BTNSHDSPR);
            cnt.ShadowDecoration.Depth = 8;
            cnt.ShadowDecoration.Enabled = true;

            cnt.DisabledState.BorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR), 0.3);
            cnt.DisabledState.FillColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(BGRColor), -FillFactor);
            cnt.DisabledState.ForeColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(ButtonFGR), 0.3);
            cnt.DisabledState.CustomBorderColor = ColorConfiguration.ChangeColorBrightnessRTColor(ColorTranslator.FromHtml(InitializedVariables.BTNBRDCLR), -0.5);

        }

        /*
        public static void AddDropShadow(Form frm, Control cnt, Color clr, int opct, int lctn, int sze)
        {
            Panel shdpnl = new Panel();
            shdpnl.Size = cnt.Size;
            shdpnl.BackColor = Color.FromArgb(opct, clr);
            shdpnl.Paint += new PaintEventHandler((sender, e) =>
            {
                Graphics gr = e.Graphics;
                System.Drawing.Rectangle R = shdpnl.ClientRectangle;
                using (var p = new Pen(Color.FromArgb(opct - 20, clr), sze))
                {
                    p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    gr.DrawRectangle(p, new System.Drawing.Rectangle((int)(R.Left - p.Width / 2), (int)(R.Top - p.Width / 2), (int)(R.Width + p.Width), (int)(R.Height + p.Width)));

                }
            });
            PictureBox shdpicbx = new PictureBox();
            shdpicbx.Size = cnt.Size;
            shdpicbx.SizeMode = PictureBoxSizeMode.StretchImage;
            shdpicbx.Image = new Bitmap(cnt.Width, cnt.Height);
            cnt.DrawToBitmap((Bitmap)shdpicbx.Image, new System.Drawing.Rectangle(System.Drawing.Point.Empty, cnt.Size));
            shdpnl.Location = new System.Drawing.Point(cnt.Location.X + lctn, cnt.Location.Y + lctn);
            frm.Controls.Add(shdpicbx);
            frm.Controls.Add(shdpnl);
        }
        */

    }
    public class CreateDataGridView 
    {
        private Guna2DataGridView datagridview;
        public CreateDataGridView()
        {
            datagridview = new Guna2DataGridView();
            ControlsConfigurations.InitializeGridView(datagridview, null, null, null, -1, null, true, true);
        }
    }


}
