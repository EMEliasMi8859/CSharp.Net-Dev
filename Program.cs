using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TawheedBasitPvtLtd
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Directory.Exists("//.Data//"))
                Configurations.ProjectConfigurations.createDirectory(".Data");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Forms.AuthenticationForm AuthFrm;
            if (Configurations.ProjectConfigurations.ReadRememberMe())
            {
                Configurations.InitializedVariables.REMEMBERMEFLAG = true;
                Application.Run(new Forms.MainGUI());
            }
            else
            {
                Application.Run(AuthFrm = new Forms.AuthenticationForm());
                if (AuthFrm.DialogResult == DialogResult.OK)
                {
                    Application.Run(new Forms.MainGUI());
                }
            }
        }
    }
}
