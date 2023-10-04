using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace TawheedBasitPvtLtd.Configurations
{
    public static class InitializedVariables
    {


        public static bool REMEMBERMEFLAG { get; set; }


        public static string LOGOCOLOR { get; set; }
        public static string ORGANIZATIONNAME { get; set; }
        public static string ABOUTORGANIZATION { get; set; }

        public static int BTNBRDRDS { get; set; }
        public static int BTNBRDTKN { get; set; }
        public static int BTNSHDSPR { get; set; }
        public static int TBXBRDRDS { get; set; }
        public static int TBXBRDTKN { get; set; }
        public static int TBXSHDSPR { get; set; }
        public static int DGWHDRHGT { get; set; }


        public static string FRMBGRCLR { get; set; }
        public static string FRMSHDCLR { get; set; }
        public static string PNLBGRCLR { get; set; }
        public static string PNLSHDCLR { get; set; }
        public static string LBLFGRCLR { get; set; }
        public static string BTNBGRCLR { get; set; }
        public static string BTNFGRCLR { get; set; }
        public static string BTNBRDCLR { get; set; }
        public static string TBXBGRCLR { get; set; }
        public static string TBXFGRCLR { get; set; }
        public static string TBXBRDCLR { get; set; }
        public static string TBXPCHCLR { get; set; }
        public static string CBXBGRCLR { get; set; }
        public static string CBXFGRCLR { get; set; }
        public static string CBXSHDCLR { get; set; }
        public static string LBXBGRCLR { get; set; }
        public static string LBXFGRCLR { get; set; }
        public static string LBXSHDCLR { get; set; }
        public static string DGWBGRCLR { get; set; }
        public static string DGWFGRCLR { get; set; }

        public static string DGWBRDCLR { get; set; }
        public static string GRPBGRCLR { get; set; }
        public static string GRPFGRCLR { get; set; }
        public static string GRPSHDCLR { get; set; }



        public static string RADBGRCLR { get; set; }
        public static string RADACTBGR { get; set; }
        public static string CNTBGRCLR { get; set; }
        public static string CNTFGRCLR { get; set; }



        public static string LBLBGRCLR { get; set; }
        public static string LBLSHDCLR { get; set; }


        public static float FILLFACTOR { get; set; }
        public static String INVTHMCLR { get; set; }   
        public static string CurrentThemedFile { get; set; }

        //data base
        public static String DataBaseName { get; set; }
        public static String DBServerName { get; set; }
        public static String DataBaseUser { get; set; }
        public static String DataBasePass { get; set; }



        static InitializedVariables()
        {

            REMEMBERMEFLAG = false;
            initializeControls();
            InitializeColorThem();
            InitializeFirmInformation();
            initializeDataBaseInfo();   

        }
        public static void InitializeFirmInformation()
        {
            ORGANIZATIONNAME = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ORGANIZATIONNAMEKEY, ConfigurationKeys.ControlConfigFileName);
            if (ORGANIZATIONNAME == null || ORGANIZATIONNAME == "") ORGANIZATIONNAME = "Your organization name here";
            ABOUTORGANIZATION = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ABOUTORGANIZATIONKEY, ConfigurationKeys.ControlConfigFileName);
            if (ABOUTORGANIZATION == null || ABOUTORGANIZATION == "") ABOUTORGANIZATION = "Your organization identification";
        }
        public static void initializeDataBaseInfo()
        {
            DBServerName = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DBSERVERNAMEKey, ConfigurationKeys.ConfigurationfileName);
            if (DBServerName != null && DBServerName != "") DBServerName = SecurityEncryption.DecryptString(DBServerName);
            DataBaseUser = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DATABASEUSERKey, ConfigurationKeys.ConfigurationfileName);
            if (DataBaseUser != null && DataBaseUser != "") DataBaseUser = SecurityEncryption.DecryptString(DataBaseUser);
            DataBasePass = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DATABASEPASSKey, ConfigurationKeys.ConfigurationfileName);
            if (DataBasePass != null && DataBasePass != "") DataBasePass = SecurityEncryption.DecryptString(DataBasePass);
            DataBaseName = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DATABASENAMEKey, ConfigurationKeys.ConfigurationfileName);
            if (DataBaseName != null && DataBaseName != "") DataBaseName = SecurityEncryption.DecryptString(DataBaseName);
        }
        public static void InitializeColorThem()
        {
            if (ColorConfiguration.CurrentColorThem() == 1)
            {
                InitializeDarkColorThem();
                FILLFACTOR = 0.3f;
                INVTHMCLR = "#ffffff";
                CurrentThemedFile = ConfigurationKeys.ColorConfigDarkThemFileName;
            }
            else if (ColorConfiguration.CurrentColorThem() == 0)
            {
                InitializeLightColorThem();
                FILLFACTOR = -0.3f;
                INVTHMCLR = "#000000";
                CurrentThemedFile = ConfigurationKeys.ColorConfigLightThemFileName;
            }
            else
            {
                ColorConfiguration.RegDefaultDarkColorThem();
                ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
                ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.DarkColorThem, ConfigurationKeys.ConfigurationfileName);
                InitializeColorThem();
            }
        }
        public static bool initializeControls()
        {
            if (File.Exists(ConfigurationKeys.ControlConfigFileName) )
            {
                string temp;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBRDRDSKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") BTNBRDRDS = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBRDTKNKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") BTNBRDTKN = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNSHDSPRKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") BTNSHDSPR = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LOGOCOLORKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") LOGOCOLOR = temp; else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBRDRDSKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") TBXBRDRDS = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBRDTKNKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") TBXBRDTKN = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXSHDSPRKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") TBXSHDSPR = int.Parse(temp); else return false;
                temp = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWHDRHGTKey, ConfigurationKeys.ControlConfigFileName);
                if (temp != null || temp != "") DGWHDRHGT = int.Parse(temp); else return false;
                return true;
            }
            ControlsConfigurations.regDefaultControls();
            initializeControls();
            return false;
        }
        public static void InitializeLightColorThem()
        {
            bool tmp = true;
            if (!File.Exists(ConfigurationKeys.ColorConfigLightThemFileName) || !Directory.Exists(Directory.GetCurrentDirectory() + "\\.Data\\")) ColorConfiguration.RegDefaultLightColorThem();
            InitializedVariables.FRMBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.FRMBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.FRMSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.FRMSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.PNLBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.PNLBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.PNLSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.PNLSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.LBLFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.BTNBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.BTNFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.BTNBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBRDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.TBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.TBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.TBXBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBRDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.TBXPCHCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXPCHCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.CBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.CBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.CBXSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.LBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.LBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.LBXSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.DGWBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.DGWFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.DGWBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWBRDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.GRPBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.GRPFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.GRPSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);

            InitializedVariables.RADBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.RADBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.RADACTBGR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.RADACTBGRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.CNTBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CNTBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.CNTFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CNTFGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);


            InitializedVariables.LBLBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLBGRCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            InitializedVariables.LBLSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLSHDCLRKey, ConfigurationKeys.ColorConfigLightThemFileName);
            if (FRMBGRCLR == null) tmp = false;
            if (FRMSHDCLR == null) tmp = false;
            if (PNLBGRCLR == null) tmp = false;
            if (PNLSHDCLR == null) tmp = false;
            if (LBLFGRCLR == null) tmp = false;
            if (BTNBGRCLR == null) tmp = false;
            if (BTNFGRCLR == null) tmp = false;
            if (BTNBRDCLR == null) tmp = false;
            if (TBXBGRCLR == null) tmp = false;
            if (TBXFGRCLR == null) tmp = false;
            if (TBXBRDCLR == null) tmp = false;
            if (TBXPCHCLR == null) tmp = false;
            if (CBXBGRCLR == null) tmp = false;
            if (CBXFGRCLR == null) tmp = false;
            if (CBXSHDCLR == null) tmp = false;
            if (LBXBGRCLR == null) tmp = false;
            if (LBXFGRCLR == null) tmp = false;
            if (LBXSHDCLR == null) tmp = false;
            if (DGWBGRCLR == null) tmp = false;
            if (DGWFGRCLR == null) tmp = false;
            if (DGWBRDCLR == null) tmp = false;
            if (GRPBGRCLR == null) tmp = false;
            if (GRPFGRCLR == null) tmp = false;
            if (GRPSHDCLR == null) tmp = false;
            if (RADBGRCLR == null) tmp = false;
            if (RADACTBGR == null) tmp = false;
            if (CNTBGRCLR == null) tmp = false;
            if (CNTFGRCLR == null) tmp = false;
            if (LBLBGRCLR == null) tmp = false;
            if (LBLSHDCLR == null) tmp = false;
            if (!tmp)
            {
                ColorConfiguration.RegDefaultLightColorThem();
                InitializeLightColorThem();
            }
        }
        public static void InitializeDarkColorThem()
        {
            bool tmp = true;
            if (!File.Exists(ConfigurationKeys.ColorConfigDarkThemFileName) || !Directory.Exists(Directory.GetCurrentDirectory() + "\\.Data\\"))
            {

                ColorConfiguration.RegDefaultDarkColorThem();
            }
            FRMBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.FRMBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (FRMBGRCLR == null) tmp = false;
            FRMSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.FRMSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (FRMSHDCLR == null) tmp = false;
            PNLBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.PNLBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (PNLBGRCLR == null) tmp = false;
            PNLSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.PNLSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (PNLSHDCLR == null) tmp = false;
            LBLFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBLFGRCLR == null) tmp = false;
            BTNBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (BTNBGRCLR == null) tmp = false;
            BTNFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (BTNFGRCLR == null) tmp = false;
            BTNBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.BTNBRDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (BTNBRDCLR == null) tmp = false;
            TBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (TBXBGRCLR == null) tmp = false;
            TBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (TBXFGRCLR == null) tmp = false;
            TBXBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXBRDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (TBXBRDCLR == null) tmp = false;
            TBXPCHCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.TBXPCHCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (TBXPCHCLR == null) tmp = false;
            CBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (CBXBGRCLR == null) tmp = false;
            CBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (CBXFGRCLR == null) tmp = false;
            CBXSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CBXSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (CBXSHDCLR == null) tmp = false;
            LBXBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBXBGRCLR == null) tmp = false;
            LBXFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBXFGRCLR == null) tmp = false;
            LBXSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBXSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBXSHDCLR == null) tmp = false;
            DGWBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (DGWBGRCLR == null) tmp = false;
            DGWFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (DGWFGRCLR == null) tmp = false;
            DGWBRDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.DGWBRDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (DGWBRDCLR == null) tmp = false;
            GRPBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (GRPBGRCLR == null) tmp = false;
            GRPFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (GRPFGRCLR == null) tmp = false;
            GRPSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.GRPSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (GRPSHDCLR == null) tmp = false;

            RADBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.RADBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (RADBGRCLR == null) tmp = false;
            RADACTBGR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.RADACTBGRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (RADACTBGR == null) tmp = false;
            CNTBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CNTBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (CNTBGRCLR == null) tmp = false;
            CNTFGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.CNTFGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (CNTFGRCLR == null) tmp = false;


            LBLBGRCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLBGRCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBLBGRCLR == null) tmp = false;
            LBLSHDCLR = ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.LBLSHDCLRKey, ConfigurationKeys.ColorConfigDarkThemFileName); if (LBLSHDCLR == null) tmp = false;

            if (!tmp)
            {
                ColorConfiguration.RegDefaultDarkColorThem();
                InitializeDarkColorThem();
            }
        }
    }
    class HighLightingColors
    {
        public static String PIMARYHIGHLIGHTCOLOR { get; set; }
        public static String CurrentPIMARYHIGHLIGHTCOLOR { get; set; }

        public static List<String> ColorList = new List<string>()
    {
                                                            "#3F51B5",
                                                            "#009688",
                                                            "#FF5722",
                                                            "#607DBB",
                                                            "#FF9800",
                                                            "#cb52df",
                                                            "#2196F3",
                                                            "#EA676C",
                                                            "#E41A4A",
                                                            "#5978BB",
                                                            "#018790",
                                                            "#0E3441",
                                                            "#00B0AD",
                                                            "#721D47",
                                                            "#EA4833",
                                                            "#EF937E",
                                                            "#F37521",
                                                            "#A12059",
                                                            "#126881",
                                                            "#8BC240",
                                                            "#96ddff",
                                                            "#C7DC5B",
                                                            "#0094BC",
                                                            "#E4126B",
                                                            "#43B76E",
                                                            "#7BCFE9",
                                                            "#B71C46"
                                                                };
    }
}
