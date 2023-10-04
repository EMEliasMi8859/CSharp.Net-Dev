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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            PaintForm();
        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
        }
    }
}
