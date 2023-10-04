using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TawheedBasitPvtLtd.Configurations
{
    class ConfigurationKeys
    {
        //filesKeys
        public static string ConfigurationfileName = Directory.GetCurrentDirectory() + "\\.Data\\ProjectConfiguration.cnfg";
        public static readonly string ColorConfigLightThemFileName = Directory.GetCurrentDirectory() + "\\.Data\\ColorConfigLight.cnfg";
        public static readonly string ColorConfigDarkThemFileName = Directory.GetCurrentDirectory() + "\\.Data\\ColorConfigDark.cnfg";
        public static readonly string ControlConfigFileName = Directory.GetCurrentDirectory() + "\\.Data\\ControlConfiguration.cnfg";
        public static readonly string SQLServerDATADefaulDirectory = "\\.SQLSERVERData";
        public static readonly string ImagesDataDirecotry = "\\.IMAGES";
        public static readonly string PRODUCTTABLEIMAGES = "\\.PRODUCTIMAGES";
        public static readonly string CUSTOMERTABLEIMAGES = "\\.CUSTOMERSIMAGES";
        public static readonly string STOCKHOLDERTABLEIMAGES = "\\.STOCKHOLDERIMAGES";

        // about firms
        public static readonly string ORGANIZATIONNAMEKEY = "ORGNAME";
        public static readonly string ABOUTORGANIZATIONKEY = "ABOUTORGANIZATION";

        //AuthenticationDetails
        public static string AuthUsernameKey = "SYSUSRNAME";
        public static string AuthPasswordKey = "SYSUSRPASS";

        //remember me
        public static string REMEMBERME = "REMEMBERME";
        public static string REMEMBERHOURS = "REMEMBERHOURS";

        //DataBase keys
        public static String DATABASENAMEKey = "DATABASENAME";
        public static String DBSERVERNAMEKey = "DBSERVERNAME";
        public static String DATABASEUSERKey = "DATABASEUSER";
        public static String DATABASEPASSKey = "DATABASEPASS";

        //Controls keys
        public static readonly string BTNBRDRDSKey = "BTNBRDRDS";
        public static readonly string BTNBRDTKNKey = "BTNBRDTKN";
        public static readonly string BTNSHDSPRKey = "BTNSHDSPR";

        public static readonly string TBXBRDRDSKey = "TBXBRDRDS";
        public static readonly string TBXBRDTKNKey = "TBXBRDTKN";
        public static readonly string TBXSHDSPRKey = "TBXSHDSPR";

        public static readonly string LOGOCOLORKey = "LOGOCOLOR";

        //colorthemkeys
        public static string ColorThemKey = "COLORTHEM";
        public static string DarkColorThem = "DARKTHEM";
        public static string LightColorThem = "LIGHTTHEM";


        // color keys
        public static readonly string FRMBGRCLRKey = "FRMBGRCLR";
        public static readonly string FRMSHDCLRKey = "FRMSHDCLR";

        public static readonly string PNLBGRCLRKey = "PNLBGRCLR";
        public static readonly string PNLSHDCLRKey = "PNLSHDCLR";

        public static readonly string LBLFGRCLRKey = "LBLFGRCLR";

        public static readonly string BTNBGRCLRKey = "BTNBGRCLR";
        public static readonly string BTNFGRCLRKey = "BTNFGRCLR";
        public static readonly string BTNBRDCLRKey = "BTNBRDCLR";

        public static readonly string TBXBGRCLRKey = "TBXBGRCLR";
        public static readonly string TBXFGRCLRKey = "TBXFGRCLR";
        public static readonly string TBXBRDCLRKey = "TBXBRDCLR";
        public static readonly string TBXPCHCLRKey = "TBXPCHCLR";

        public static readonly string CBXBGRCLRKey = "CBXBGRCLR";
        public static readonly string CBXFGRCLRKey = "CBXFGRCLR";
        public static readonly string CBXSHDCLRKey = "CBXSHDCLR";

        public static readonly string LBXBGRCLRKey = "LBXBGRCLR";
        public static readonly string LBXFGRCLRKey = "LBXFGRCLR";
        public static readonly string LBXSHDCLRKey = "LBXSHDCLR";

        public static readonly string DGWBGRCLRKey = "DGWBGRCLR";
        public static readonly string DGWFGRCLRKey = "DGWFGRCLR";
        public static readonly string DGWBRDCLRKey = "DGWSHDCLR";
        public static readonly string DGWHDRHGTKey = "DGWSHDCLR";

        public static readonly string GRPBGRCLRKey = "GRPBGRCLR";
        public static readonly string GRPFGRCLRKey = "GRPFGRCLR";
        public static readonly string GRPSHDCLRKey = "GRPSHDCLR";




        public static readonly string RADBGRCLRKey = "RADBGRCLR";
        public static readonly string RADACTBGRKey = "RADACTBGR";
        public static readonly string CNTBGRCLRKey = "CNTBGRCLR";
        public static readonly string CNTFGRCLRKey = "CNTFGRCLR";



        public static readonly string LBLBGRCLRKey = "LBLBGRCLR";
        public static readonly string LBLSHDCLRKey = "LBLSHDCLR";
    }
}
