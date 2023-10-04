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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            PaintForm();
        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeTabController(this.ACCTabCNT, null, null, null, false, true);
        }
    }
}
