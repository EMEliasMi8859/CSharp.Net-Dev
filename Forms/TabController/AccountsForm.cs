using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd.Forms.TabController
{
    public partial class AccountsForm : Form
    {
        public AccountsForm()
        {
            InitializeComponent();
            PaintForm();
        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeTabController(this.ACCTabCNT,null,null,null,false,true);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.TOTALAMMGRP, Configurations.InitializedVariables.GRPBGRCLR, null,null,-1,-1,true,false,true);
            Configurations.ControlsConfigurations.InitializeLabel(this.TotalAmountAFG, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.TotalAmountUSD, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.TotalAmountPRS, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));

            Configurations.ControlsConfigurations.InitializeLabel(this.label2);
            Configurations.ControlsConfigurations.InitializeLabel(this.label3);
            Configurations.ControlsConfigurations.InitializeLabel(this.label4);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.LOANSGRP, Configurations.InitializedVariables.GRPBGRCLR, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.LoansAFG, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.LoansPRS, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.LoansUSD, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));



            Configurations.ControlsConfigurations.InitializeGroupBox(this.CASHGRP, Configurations.InitializedVariables.GRPBGRCLR, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.CashAFG, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.CashUSD, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.CashPRS, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));


            Configurations.ControlsConfigurations.InitializeGroupBox(this.TELLERGRP, Configurations.InitializedVariables.GRPBGRCLR, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.TellerAFG, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.TellerUSD, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));
            Configurations.ControlsConfigurations.InitializeLabel(this.TellerPRS, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));

            Configurations.ControlsConfigurations.InitializeGroupBox(this.CURRENCYGRP, Configurations.InitializedVariables.GRPBGRCLR, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeLabel(this.label12);
            Configurations.ControlsConfigurations.InitializeLabel(this.label10);
            Configurations.ControlsConfigurations.InitializeLabel(this.label11);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.EXCHBGRP, null, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHFILTER, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHTELLERID, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHAMMOUNT, null,null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.EXCHRATE, null,null, -1, -1, true);

            this.EXCHFROMCURR.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.EXCHFROMCURR.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.EXCHFROMCURR.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.EXCHFROMCURR.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            this.EXCHTOCURR.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.EXCHTOCURR.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.EXCHTOCURR.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.EXCHTOCURR.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            Configurations.ControlsConfigurations.InitializeButton(this.EXCHANGEBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);

            Configurations.ControlsConfigurations.InitializeGroupBox(this.RECVGRP, null, null, null, -1, -1, true, false, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TRANSFILTER, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TRANSFROM, null,null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TRANSTO, null, null, -1, -1, true);
            Configurations.ControlsConfigurations.InitializeTextBox(this.TRANSAMOUNT, null, null, -1, -1, true);

            this.TRANSCURRENCY.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.TRANSCURRENCY.ForeColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.LBLFGRCLR);
            this.TRANSCURRENCY.FillColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.GRPBGRCLR);
            this.TRANSCURRENCY.BorderColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.BTNBRDCLR);

            Configurations.ControlsConfigurations.InitializeButton(this.TRANSBTN, Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FRMBGRCLR, -1, -1, false, Configurations.InitializedVariables.LBLFGRCLR);


            Configurations.ControlsConfigurations.InitializeLabel(this.label5);
            Configurations.ControlsConfigurations.InitializeLabel(this.label10);
            Configurations.ControlsConfigurations.InitializeLabel(this.label11);
            Configurations.ControlsConfigurations.InitializeLabel(this.label12);
            Configurations.ControlsConfigurations.InitializeLabel(this.label13);
            Configurations.ControlsConfigurations.InitializeLabel(this.label14);
            Configurations.ControlsConfigurations.InitializeLabel(this.label18);
            Configurations.ControlsConfigurations.InitializeLabel(this.label19);
            Configurations.ControlsConfigurations.InitializeLabel(this.label25);
            Configurations.ControlsConfigurations.InitializeLabel(this.StatusMessage, Configurations.ColorConfiguration.ChangeColorBrightnessRTHEX(Configurations.HighLightingColors.PIMARYHIGHLIGHTCOLOR, Configurations.InitializedVariables.FILLFACTOR));


        }

    }
}
