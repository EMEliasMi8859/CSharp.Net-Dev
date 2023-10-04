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
    public partial class StocksForm : Form
    {
        public StocksForm()
        {
            InitializeComponent();
            PaintForm();
            List<datag> dt = new List<datag>();
            dt.Add(new datag { id = 1, name = "khan", age = 21 });
            dt.Add(new datag { id = 2, name = "ali", age = 22 });
            this.guna2DataGridView1.DataSource = dt;
            Configurations.ControlsConfigurations.InitializeGridView(this.guna2DataGridView1, null, null,null, -1, null, true, true);


        }

        public void PaintForm()
        {
            this.BackColor = ColorTranslator.FromHtml(Configurations.InitializedVariables.FRMBGRCLR);
            Configurations.ControlsConfigurations.InitializeTabController(this.ACCTabCNT, null, null, null, false, true);
        }
    }
    public class datag
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }
}
