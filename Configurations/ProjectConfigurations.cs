using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.AccessControl;

namespace TawheedBasitPvtLtd.Configurations
{

    //securityEncryption
    //EncryptString(str)                                                 return enc of str
    //DecryptSrring(Encstr)                                              return dec str of enc str

    //ProjectConfiguration
    //ResetDefaultConfiguration()                                        reset all configuration to default
    //createConfigurationFile(fileName)                                  createfile
    //WriteOrReplaceLineConfig(string key, string value, string file)    write or replace value to key line
    //ReadValueOfKeyConfig(string key, string file)                      read value give a kkey
    //ReadLineNoOfKeyConfig(String key, string file)                     give key read line number
    //ReplaceKeyValueConfig(string key, string value, string file)       replace old value with new value of a key
    //RemoveLineByKeyConfig( key, file)                                 search a key then remove the entire line from file

    //DataBaseConnection
    //ConnectToSqlServer()                                              return connection from config file
    //CreateNewDataBaseSystem(dbname)                                    create all tables take dbname
    //RegesterDBconfigFile(srvr, db , usr , pass)                       register to file take servername -f db user pass
    //InitializeDBConfig()                                              initialize all values from configuration file

    //ColorConfiguration
    //setDefaulColorThem()                                              reset default colors value
    //initDefaultLightColorThem()                                       reset only light color values
    //initDefaultDarkColorThem()                                        reset only dark color values
    //SetLightColorThem()                                               initialize light values
    //SetDarkColorThem()                                                initialize dark values
    //ChangeColor(Key , value)                                          change a color in config file
    //CurrentCollorThemIsDark                                           return true if current color them is dark
    //CurrentCollorThemIsLight                                          return true if currecnt color them is light
    //initializeColorsThem()                                            initialize all colors to current color them             run when app starts
    //ChangeColorBrightnessRTCOLOR(String color, double fact)           tack color return color with factor brightness
    //ChangeColorBrightnessRTHEX(String color, double fact)             tack string return string with factor brightness
    //ConvertColorToHex
    //ConvertHexToColor
    class ProjectConfigurations
    {
        public static void StartupConfiguration()
        {
            RegProjectConfiguration();
            //contorls
            //coloring tools
            if (!File.Exists(ConfigurationKeys.ColorConfigLightThemFileName))
                ColorConfiguration.RegDefaultLightColorThem();
            if (!File.Exists(ConfigurationKeys.ColorConfigDarkThemFileName))
                ColorConfiguration.RegDefaultDarkColorThem();
            InitializedVariables.InitializeColorThem();
        }
        public static void RegProjectConfiguration()
        {
            if(createConfigurationFile(ConfigurationKeys.ConfigurationfileName))
                WriteOrReplaceLineConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.DarkColorThem, ConfigurationKeys.ConfigurationfileName);
            else
                if (ReadValueOfKeyConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.ConfigurationfileName) == null)
                    WriteOrReplaceLineConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.DarkColorThem, ConfigurationKeys.ConfigurationfileName);
        }
        public static bool RegisterRememberMe()
        {
            DateTime currentDateTime = DateTime.Now;

            createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
            if (WriteOrReplaceLineConfig(ConfigurationKeys.REMEMBERME, SecurityEncryption.EncryptString(currentDateTime.ToString()), ConfigurationKeys.ConfigurationfileName)) return true;
            return false;
        }
        public static bool ReadRememberMe()
        {
            DateTime currentDateTime = DateTime.Now;
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
            string tmpstring = ReadValueOfKeyConfig(ConfigurationKeys.REMEMBERME, ConfigurationKeys.ConfigurationfileName);
            if (tmpstring != null && tmpstring != "")
            {
                Console.WriteLine("String:    " + SecurityEncryption.DecryptString(tmpstring));
                DateTime tmpdttm = DateTime.ParseExact(SecurityEncryption.DecryptString(tmpstring), "M/d/yyyy h:mm:ss tt", formatProvider);
                TimeSpan timediff = currentDateTime - tmpdttm;
                string rememberhs = ReadValueOfKeyConfig(ConfigurationKeys.REMEMBERHOURS, ConfigurationKeys.ConfigurationfileName);
                int remembhours = -1;
                if (rememberhs != null && rememberhs != "")
                    remembhours = int.Parse(SecurityEncryption.DecryptString(rememberhs));
                else
                {
                    createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
                    WriteOrReplaceLineConfig(ConfigurationKeys.REMEMBERHOURS, SecurityEncryption.EncryptString("2"), ConfigurationKeys.ConfigurationfileName);
                    remembhours = 2;
                }

                if (timediff.TotalHours <= remembhours && timediff.TotalHours >= 0) return true;
            }
            else return false;
            return false;
        }

        public static void RegisterFirmDetails(string firmname, string aboutfirm = null)
        {
            createConfigurationFile(ConfigurationKeys.ControlConfigFileName);
            WriteOrReplaceLineConfig(ConfigurationKeys.ORGANIZATIONNAMEKEY, firmname, ConfigurationKeys.ControlConfigFileName);
            if (aboutfirm != null && aboutfirm != "") WriteOrReplaceLineConfig(ConfigurationKeys.ABOUTORGANIZATIONKEY, aboutfirm, ConfigurationKeys.ControlConfigFileName);
        }

        public static bool createConfigurationFile(string filename)
        {
            if (!File.Exists(filename))
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\.Data\\"))
                    createDirectory(".Data");
                StreamWriter writer = File.CreateText(filename);
                writer.Close();
                return true;

            }
            return false;
        }
        public static bool createDirectory(string dirname)
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + dirname)) return true;
            string dirpath = Directory.GetCurrentDirectory() + "\\" + dirname;
            Directory.CreateDirectory(dirpath);
            DirectoryInfo dir = new DirectoryInfo(dirpath);
            dir.Attributes = FileAttributes.Hidden;
            dir.Refresh();
            if (Directory.Exists(Directory.GetCurrentDirectory() + dirname)) return true;
            return false;
        }
        public static void RegisterAuthentication(string usrname, string passwrd)
        {
            createDirectory(".Data");
            createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
            WriteOrReplaceLineConfig(ConfigurationKeys.AuthUsernameKey, SecurityEncryption.EncryptString(usrname), ConfigurationKeys.ConfigurationfileName);
            WriteOrReplaceLineConfig(ConfigurationKeys.AuthPasswordKey, SecurityEncryption.EncryptString(passwrd), ConfigurationKeys.ConfigurationfileName);
        }
 
        public static bool WriteOrReplaceLineConfig(string key, string Value, string file)
        {
            createConfigurationFile(file);
            if (ReadValueOfKeyConfig(key, file) == null)
            {
                string[] allLines = File.ReadAllLines(file);
                Array.Resize(ref allLines, allLines.Length + 1);
                allLines[allLines.Length - 1] = key+":"+Value;
                File.WriteAllLines(file, allLines);
                return true;
            }else if(ReadValueOfKeyConfig(key, file) != Value)
                return ReplaceKeyValueConfig(key, Value, file);
            return false;
        }
        public static string ReadValueOfKeyConfig(string key , string file)
        {
            string line;
            StreamReader reader = new StreamReader(file);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains($"{key}:"))
                {
                    reader.Close();
                    return (line.Substring(line.IndexOf(':') + 1));
                }
            }
            reader.Close();
            return null;

        }
        public static int ReadLineNoOfKeyConfig(String str, string file)
        {
            int temp = 0;
            string line;
            StreamReader reader = new StreamReader(file);
            while ((line = reader.ReadLine()) != null)
            {

                if (line.Contains($"{str}:"))
                {
                    reader.Close();
                    return temp;
                }
                temp++;
            }
            reader.Close();
            return -1;
        }
        public static bool ReplaceKeyValueConfig(string oldKey, string newValue, string file)
        {
            int lineNo = ReadLineNoOfKeyConfig(oldKey, file);
            string[] allLines = File.ReadAllLines(file);
            if (lineNo != -1 && lineNo <= allLines.Length)
            {
                allLines[lineNo] = allLines[lineNo].Substring(0, allLines[lineNo].IndexOf(':') + 1) + newValue;
                File.WriteAllLines(file, allLines);
                return true;
            }
            return false;
            /*
            int lineNo = ReadLineNoOfKeyConfig(oldKey, file);
            string[] allLines;
            List<string> allRecords = new List<string>();
            string templine;
            int tmpNo = 0;
            using (StreamReader reader = new StreamReader(file))
            {
                while ((templine = reader.ReadLine()) != null)
                {
                    allRecords.Add(templine);
                    tmpNo++;
                }
            }
            allLines = allRecords.ToArray();
            if (lineNo != -1 && lineNo <= allLines.Length)
            {
                allLines[lineNo] = allLines[lineNo].Substring(0, allLines[lineNo].IndexOf(':') + 1) + newValue;
                int tmpNo2 = 0;
                using (StreamWriter writer = new StreamWriter(file))
                {
                    while (tmpNo2 < tmpNo)
                    {
                        writer.WriteLine(allLines[tmpNo2]);
                        tmpNo2++;
                    }
                }
            }*/
        }
        public static bool RemoveLineByKeyConfig(string key, string file)
        {
            int lineNo = ReadLineNoOfKeyConfig(key, file);
            string[] allLines;
            List<string> allRecords = new List<string>();
            string templine;
            int tmpNo = 0;
            using (StreamReader reader = new StreamReader(file))
            {
                while ((templine = reader.ReadLine()) != null)
                {
                    allRecords.Add(templine);
                    tmpNo++;
                }
            }
            List<string> updatedRecords = new List<string>(allRecords);

            if (lineNo != -1 && lineNo <= updatedRecords.Count && lineNo >= 0)
            {
                updatedRecords.RemoveAt(lineNo);
                allLines = updatedRecords.ToArray();
                int tmpNo2 = 0;
                using (StreamWriter writer = new StreamWriter(file))
                {
                    while (tmpNo2 < tmpNo-1)
                    {
                        writer.WriteLine(allLines[tmpNo2]);
                        tmpNo2++;
                    }
                }
                return true;
            }
            return false;
        }
    }
    class DataBaseConnection
    {
        public static void RegisterSQLServerAuth(string srvrname, string usrname = null, string usrpass = null, string dbname = null)
        {
            ProjectConfigurations.createDirectory(".Data");
            ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
            ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DBSERVERNAMEKey, SecurityEncryption.EncryptString(srvrname), ConfigurationKeys.ConfigurationfileName);
            if (usrname != null && usrname != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASEUSERKey, SecurityEncryption.EncryptString(usrname), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASEUSERKey, ConfigurationKeys.ConfigurationfileName);
            if (usrpass != null && usrpass != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASEPASSKey, SecurityEncryption.EncryptString(usrpass), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASEPASSKey, ConfigurationKeys.ConfigurationfileName);
            if (dbname != null && dbname != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASENAMEKey, SecurityEncryption.EncryptString(dbname), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASENAMEKey, ConfigurationKeys.ConfigurationfileName);
        }
        public static bool CheckDataBaseSystem(SqlConnection con, bool CheckDatabase = false)
        {
            bool dbCheck = false;
            object result;
            try
            {
                if (CheckDatabase)
                {
                    SqlCommand cmd = new SqlCommand($"Select db_id('{Configurations.InitializedVariables.DataBaseName}')", con);
                    result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int id = int.Parse(result.ToString());
                        if (id > 0) dbCheck = true;
                    }
                    else return false;
                }

                SqlCommand cmd2 = new SqlCommand($"SELECT COUNT(*) AS table_count from {Configurations.InitializedVariables.DataBaseName}.sys.tables where type_desc  ='USER_TABLE'", con);
                result = cmd2.ExecuteScalar();
                if (int.Parse(result.ToString()) > 0 && dbCheck == true) return true;
                else return false;
            }catch(Exception exc)
            {
                return false;
            }
        }
        public static int NumberTablesInDB(SqlConnection con)
        {
            object result;
            try
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) AS table_count from {Configurations.InitializedVariables.DataBaseName}.sys.tables where type_desc  ='USER_TABLE'", con);
                result = cmd.ExecuteScalar();
                if (result !=null && result != DBNull.Value)return int.Parse(result.ToString());else return -1;
            }
            catch (Exception exc)
            {
                return -2;
            }
        }
        public static int NumberOfColumnsDB(SqlConnection con)
        {

            object result;
            try
            {
                SqlCommand cmd = new SqlCommand($"USE {InitializedVariables.DataBaseName}; SELECT COUNT(*) as comcount FROM SYS.COLUMNS", con);
                result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value) return int.Parse(result.ToString()); else return -1;
            }
            catch (Exception exc)
            {
                Console.WriteLine("exception ::::::"+ exc);
                return -2;
            }
        }
        public static SqlConnection ConnectToSqlServer(bool server, bool user = true, bool pass = true, bool database = true)
        {
            String ConnectinString = null;
            if (InitializedVariables.DBServerName != null && InitializedVariables.DBServerName != "" && server)
            {
                ConnectinString = $"Data source={InitializedVariables.DBServerName};";
                if (InitializedVariables.DataBaseName != null && InitializedVariables.DataBaseName != "" && database)
                    ConnectinString += $"Initial Catalog = {InitializedVariables.DataBaseName};";
                else ConnectinString += "Initial Catalog=;";
                if (InitializedVariables.DataBaseUser!= null && InitializedVariables.DataBaseUser != "" && user)
                {
                    ConnectinString += $"User ID ={InitializedVariables.DataBaseUser};";
                    if (InitializedVariables.DataBasePass != null && InitializedVariables.DataBasePass != "" && pass) ConnectinString += $"Password={InitializedVariables.DataBasePass};";
                }
                /*else ConnectinString += $"Integreted Security = true;";*/
            }
            SqlConnection con = new SqlConnection(ConnectinString);
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                    return con;
                else return null;
            }catch(Exception exc)
            {
                return null;
            }
        }
        public static bool createDatabse(SqlConnection con, string path = null)
        {
            if (path == null || path == "")
            {
                path = ConfigurationKeys.SQLServerDATADefaulDirectory;
                ProjectConfigurations.createDirectory(path);
                path = Directory.GetCurrentDirectory() + path;
            }
            if (Directory.Exists(path))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand($"CREATE DATABASE {Configurations.InitializedVariables.DataBaseName.ToUpper()} ON PRIMARY (NAME = '{Configurations.InitializedVariables.DataBaseName.ToUpper()}MASTER', FILENAME = '{path}\\{Configurations.InitializedVariables.DataBaseName.ToUpper()}MASTER.MDF') LOG ON (NAME = '{Configurations.InitializedVariables.DataBaseName.ToUpper()}LOG', FILENAME = '{path}\\{Configurations.InitializedVariables.DataBaseName.ToUpper()}LOG.LDF')", con);
                    cmd.ExecuteNonQuery();
                    return true;
                }catch(Exception exc)
                {
                    return false;
                }
            }
            return false;
        }
        public static bool CreateNewTables(SqlConnection con)
        {
            try
            {

                SqlCommand cmd1 = new SqlCommand($"USE {Configurations.InitializedVariables.DataBaseName}", con);
                SqlCommand cmd = new SqlCommand(@"		             
                        create table Customers(ID int PRIMARY KEY IDENTITY not null, Name varchar(50) not null, FatherName varchar(50) not null, Address varchar(70) null, Phone varchar(15) not null, Image varchar(30) null);
                        create table Products(ID int PRIMARY KEY IDENTITY not null, Name varchar(50) not null, CompanyName varchar(70) not null, Price money not null, Currency varchar(3) default 'AFG' not null , Image varchar(70) null);
                        

                        create table Vendors(ID int PRIMARY KEY IDENTITY not null, Name varchar(50) not null, FatherName varchar(50) not null, Address varchar(70) null, Phone varchar(15) not null, Email varchar(50) null);
			
                        create table Tellers(ID int PRIMARY KEY IDENTITY(1, 1), Name varchar(50) not null, OfficeNo int not null, Phone varchar(15), Address varchar(70) null);

                        create table Sales(BellNo int IDENTITY(1, 1) PRIMARY KEY not null, CusID int not null, PrdID int not null, Quantity int, Price money not null, Echangerate MONEY default '1' not NULL, Currency varchar(3) not null default 'AFG', Date Date Default GETDATE() not null, FOREIGN KEY(CusID)REFERENCES Customers(ID), FOREIGN KEY(PrdID)REFERENCES Products(ID));
                        create table TellersTransactions(TnsNo int PRIMARY KEY IDENTITY(1, 1), TID int not null,CUSID int null, BellNo int null, HawalaNo int null, Payin money null, Payout money null, Echangerate MONEY NULL, Currency varchar(3) not null default 'AFG', Date date default GETDATE(), FOREIGN KEY(TID) REFERENCES Tellers(ID));
			            create table SalesPayments(BellNo int not null, Payin money not null, Echangerate MONEY default '1' not NULL,Currency varchar(3) default 'AFG', TellerTransID int null, Date Date Default GETDATE() not null, FOREIGN KEY(BellNo)REFERENCES Sales(BellNo), FOREIGN KEY(TellerTransID) REFERENCES TellersTransactions(TnsNo));
            	
                        create table VendorsTransactions(VID int NOT NULL, Payin money not null, Payout money not null, Currency varchar(3) not null default 'AFG', TellerTransID int null, Date Date Default GETDATE() not null, FOREIGN KEY(TellerTransID) REFERENCES TellersTransactions(TnsNo));

                        create table NRegCusSales(BellNo int IDENTITY(1, 1) PRIMARY KEY not null, PrdID int not null, Quantity int, Price money not null, Echangerate MONEY NULL, Currency varchar(3) not null default 'AFG', Date Date Default GETDATE() not null, FOREIGN KEY(PrdID)REFERENCES Products(ID));
                        create table NRegCusSalesPayments(BellNo int not null, Payin money not null, Echangerate MONEY default '1' not NULL, Currency varchar(3) default 'AFG', TellerTransID int null, Date Date Default GETDATE() not null, FOREIGN KEY(BellNo)REFERENCES NRegCusSales(BellNo), FOREIGN KEY(TellerTransID) REFERENCES TellersTransactions(TnsNo));

                        create table Purchases(BellNo int IDENTITY(1, 1) PRIMARY KEY not null, VndID int not null, PrdID int not null, Quantity int, Date Date Default GETDATE() not null, FOREIGN KEY(VndID)REFERENCES Vendors(ID), FOREIGN KEY(PrdID)REFERENCES Products(ID));
                        create table PurchasesPayments(BellNo int not null, Payin money not null, Echangerate MONEY default '1' not NULL, Currency varchar(3) not null default 'AFG', TellerTransID int null, Date Date Default GETDATE() not null, FOREIGN KEY(BellNo)REFERENCES Purchases(BellNo), FOREIGN KEY(TellerTransID) REFERENCES TellersTransactions(TnsNo));


                        create table ReturnStocks(BellNo int not null, Quantity int not null, Date date default GETDate() not null, CONSTRAINT UC_BellNo UNIQUE (BellNo), FOREIGN KEY(BellNo) REFERENCES Sales(BellNo));
                        create table ReturnStocksPayments(ID int PRIMARY KEY IDENTITY(1, 1), BellNo int null,Payin money null, Payout money null, Echangerate MONEY NULL, Currency varchar(3) not null default 'AFG', Date date default GETDate() not null, FOREIGN KEY(BellNo) REFERENCES ReturnStocks(BellNo));

                        create table NRegCusReturnStocks(BellNo int not null, Quantity int not null, Date date default GETDate() not null, CONSTRAINT UC_NRBellNo UNIQUE (BellNo), FOREIGN KEY(BellNo) REFERENCES NRegCusSales(BellNo));
                        create table NRegCusReturnStocksPayments(ID int PRIMARY KEY IDENTITY(1, 1), BellNo int null,Payin money null, Payout money null, ToPay money not null, Date date default GETDate() not null, FOREIGN KEY(BellNo) REFERENCES NRegCusReturnStocks(BellNo));

                        create table StockHolders(ID int PRIMARY KEY IDENTITY not null, Name varchar(50) not null, FName varchar(50) not null, Phone varchar(15) not null, Address varchar(70) null, Image varchar(30) null);
                        create table StockHoldersPayments(SHID int not null, Payin money not null, Payout money not null, ToPay money not null, Date date default getdate() not null, TransactionNo int null, FOREIGN KEY(TransactionNo) REFERENCES TellersTransactions(TnsNo), FOREIGN KEY(SHID)REFERENCES StockHolders(ID));

                        create table Expenses(ID int PRIMARY KEY IDENTITY(1, 1), Descriptions varchar(150) null, Amount money not null, Currency varchar(3) default 'AFG' not null, Date date default GETDate() not null);
            
                    ", con);
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();

                return true;

            }catch(Exception)
            {
                return false;
            }
        }
        public static void RegesterDBconfigFile(String srvr, String db = null, String usr = null, String pass = null)
        {
            ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ConfigurationfileName);
            if (srvr != null && srvr != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DBSERVERNAMEKey, SecurityEncryption.EncryptString(srvr), ConfigurationKeys.ConfigurationfileName);
            if (db != null && db != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASENAMEKey, SecurityEncryption.EncryptString(db), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASENAMEKey, ConfigurationKeys.ConfigurationfileName);
            if (usr != null && usr != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASEUSERKey, SecurityEncryption.EncryptString(usr), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASEUSERKey, ConfigurationKeys.ConfigurationfileName);
            if (pass != null && pass != "") ProjectConfigurations.WriteOrReplaceLineConfig(ConfigurationKeys.DATABASEPASSKey, SecurityEncryption.EncryptString(pass), ConfigurationKeys.ConfigurationfileName);
            else ProjectConfigurations.RemoveLineByKeyConfig(ConfigurationKeys.DATABASEPASSKey, ConfigurationKeys.ConfigurationfileName);
        }

    }
    class SecurityEncryption
    {
        private static readonly byte[] Enckey = Encoding.UTF8.GetBytes("!@#$%&E|?3F&5UVDMKXA913SCZXO0!%&");
        private static readonly byte[] EncIV = Encoding.UTF8.GetBytes("!@#$%0O&Z|?B3U8B");
        public static String EncryptString(string strtxt)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Enckey;
                aes.IV = EncIV;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(strtxt);
                        }
                    }
                    var encryptedByte = memoryStream.ToArray();
                    return Convert.ToBase64String(encryptedByte);
                }
            }
        }
        public static String DecryptString(string strenc)
        {
            var encryptedByte = Convert.FromBase64String(strenc);
            using (var aes = Aes.Create())
            {
                aes.Key = Enckey;
                aes.IV = EncIV;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var memoryStream = new MemoryStream(encryptedByte))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string AuthResetKeyGenerator(string text)
        {
            for(int i = 0; i < 9; i++)
            {
                text = EncryptString(text);
                text = text.Substring((text.Length - 12) / 2,12);
            }
            text = text.Substring((text.Length - 12) / 2, 12);
            text = EncryptString(text);
            text = text.Substring((text.Length - 12) / 2, 12);
            Console.WriteLine(text.Length);
            return (text.Substring(0,3)+"-"+text.Substring(3,3)+"-"+text.Substring(6,3)+"-"+text.Substring(9,3));
        }
    }
    class ColorConfiguration
    {

        //Form
        //Panel
        //Label
        //button
        //TextBox
        //ComboBox
        //ListBox
        //DataGridView
        //GroupBox
        public static int IsValidHexColor(string clr)
        {
            if (!clr.StartsWith("#"))
            {
                clr = "#" + clr;
                if (Regex.IsMatch(clr, @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"))
                {
                    return 0;
                }
            }
            
            if (Regex.IsMatch(clr, @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")) return 1;
            return -1;
        }
        public static void ChangeColorThem()
        {
            if (CurrentColorThem() == 1)
                ProjectConfigurations.ReplaceKeyValueConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.LightColorThem, ConfigurationKeys.ConfigurationfileName);
            else if (CurrentColorThem() == 0)
                ProjectConfigurations.ReplaceKeyValueConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.DarkColorThem, ConfigurationKeys.ConfigurationfileName);
            else ControlsConfigurations.regDefaultControls();
            InitializedVariables.InitializeColorThem();
        }
        public static void ResetDefaulColorThem()
        {
            RegDefaultDarkColorThem();
            RegDefaultLightColorThem();
            InitializedVariables.InitializeColorThem();
        }
        public static void RegDefaultLightColorThem()
        {
            File.Delete(ConfigurationKeys.ColorConfigLightThemFileName);
            ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ColorConfigLightThemFileName);
            using(StreamWriter swriter = new StreamWriter(ConfigurationKeys.ColorConfigLightThemFileName))
            {
                swriter.WriteLine($"{ConfigurationKeys.FRMBGRCLRKey}:#c8c8c8");
                swriter.WriteLine($"{ConfigurationKeys.FRMSHDCLRKey}:#000000");
                swriter.WriteLine($"{ConfigurationKeys.PNLBGRCLRKey}:#e1e1e1");
                swriter.WriteLine($"{ConfigurationKeys.PNLSHDCLRKey}:#232323");
                swriter.WriteLine($"{ConfigurationKeys.LBLFGRCLRKey}:#050505");
                swriter.WriteLine($"{ConfigurationKeys.LBLBGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.LBLSHDCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.BTNBGRCLRKey}:#e1e1e1");
                swriter.WriteLine($"{ConfigurationKeys.BTNFGRCLRKey}:#373737");
                swriter.WriteLine($"{ConfigurationKeys.BTNBRDCLRKey}:#4b4b4b");
                swriter.WriteLine($"{ConfigurationKeys.TBXBGRCLRKey}:#333333");
                swriter.WriteLine($"{ConfigurationKeys.TBXFGRCLRKey}:#9e9e9e");
                swriter.WriteLine($"{ConfigurationKeys.TBXBRDCLRKey}:#bebebe");
                swriter.WriteLine($"{ConfigurationKeys.TBXPCHCLRKey}:#bababa");
                swriter.WriteLine($"{ConfigurationKeys.CBXBGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.CBXFGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.CBXSHDCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.LBXBGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.LBXFGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.LBXSHDCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.DGWBGRCLRKey}:#dddddd");
                swriter.WriteLine($"{ConfigurationKeys.DGWFGRCLRKey}:#101010");
                swriter.WriteLine($"{ConfigurationKeys.DGWBRDCLRKey}:#020202");
                swriter.WriteLine($"{ConfigurationKeys.GRPBGRCLRKey}:#e1e1e1");
                swriter.WriteLine($"{ConfigurationKeys.GRPFGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.GRPSHDCLRKey}:#fff");


                swriter.WriteLine($"{ConfigurationKeys.RADBGRCLRKey}:#fafafa");
                swriter.WriteLine($"{ConfigurationKeys.RADACTBGRKey}:#141414");
                swriter.WriteLine($"{ConfigurationKeys.CNTBGRCLRKey}:#fff");
                swriter.WriteLine($"{ConfigurationKeys.CNTFGRCLRKey}:#1e1e1e");

    }
}
        public static void RegDefaultDarkColorThem()
        {
            File.Delete(ConfigurationKeys.ColorConfigDarkThemFileName);
            ProjectConfigurations.createConfigurationFile(ConfigurationKeys.ColorConfigDarkThemFileName);
            using (StreamWriter Swriter = new StreamWriter(ConfigurationKeys.ColorConfigDarkThemFileName))
            {
                Swriter.WriteLine($"{ConfigurationKeys.FRMBGRCLRKey}:#323232");
                Swriter.WriteLine($"{ConfigurationKeys.FRMSHDCLRKey}:#ffffff");
                Swriter.WriteLine($"{ConfigurationKeys.PNLBGRCLRKey}:#1e1e1e");
                Swriter.WriteLine($"{ConfigurationKeys.PNLSHDCLRKey}:#dddddd");
                Swriter.WriteLine($"{ConfigurationKeys.LBLFGRCLRKey}:#fefefe");
                Swriter.WriteLine($"{ConfigurationKeys.LBLBGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.LBLSHDCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.BTNBGRCLRKey}:#1e1e1e");
                Swriter.WriteLine($"{ConfigurationKeys.BTNFGRCLRKey}:#efefef");
                Swriter.WriteLine($"{ConfigurationKeys.BTNBRDCLRKey}:#b4b4b4");
                Swriter.WriteLine($"{ConfigurationKeys.TBXBGRCLRKey}:#ababab");
                Swriter.WriteLine($"{ConfigurationKeys.TBXFGRCLRKey}:#e9e9e9");
                Swriter.WriteLine($"{ConfigurationKeys.TBXBRDCLRKey}:#ebebeb");
                Swriter.WriteLine($"{ConfigurationKeys.TBXPCHCLRKey}:#ffffff");
                Swriter.WriteLine($"{ConfigurationKeys.CBXBGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.CBXFGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.CBXSHDCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.LBXBGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.LBXFGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.LBXSHDCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.DGWBGRCLRKey}:#303030");
                Swriter.WriteLine($"{ConfigurationKeys.DGWFGRCLRKey}:#e9e9e9");
                Swriter.WriteLine($"{ConfigurationKeys.DGWBRDCLRKey}:#d3d3d3");
                Swriter.WriteLine($"{ConfigurationKeys.GRPBGRCLRKey}:#1e1e1e");
                Swriter.WriteLine($"{ConfigurationKeys.GRPFGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.GRPSHDCLRKey}:#fff");


                Swriter.WriteLine($"{ConfigurationKeys.RADBGRCLRKey}:#909090");
                Swriter.WriteLine($"{ConfigurationKeys.RADACTBGRKey}:#dddddd");
                Swriter.WriteLine($"{ConfigurationKeys.CNTBGRCLRKey}:#fff");
                Swriter.WriteLine($"{ConfigurationKeys.CNTFGRCLRKey}:#dfdfdf");
            }
            

        }
        public static void ChangeColor(string key, string value)
        {
            if (CurrentColorThem() == 1) ProjectConfigurations.ReplaceKeyValueConfig(key, value, ConfigurationKeys.ColorConfigDarkThemFileName);
            else if(CurrentColorThem() == 0) ProjectConfigurations.ReplaceKeyValueConfig(key, value, ConfigurationKeys.ColorConfigLightThemFileName);
        }
        public static int CurrentColorThem()
        {
            if (File.Exists(ConfigurationKeys.ConfigurationfileName))
            { 
                if (ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.ConfigurationfileName) == ConfigurationKeys.DarkColorThem) return 1;
                else if (ProjectConfigurations.ReadValueOfKeyConfig(ConfigurationKeys.ColorThemKey, ConfigurationKeys.ConfigurationfileName) == ConfigurationKeys.LightColorThem) return 0;
            }
            return -1;
        }
        public static Color ChangeColorBrightnessRTColor(Color color, double correctionFactore)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactore < 0)
            {
                correctionFactore = 1 + correctionFactore;
                red *= correctionFactore;
                green *= correctionFactore;
                blue *= correctionFactore;
            }
            else
            {
                red = (255 - red) * correctionFactore + red;
                green = (255 - green) * correctionFactore + green;
                blue = (255 - blue) * correctionFactore + blue;
            }
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
        public static string ChangeColorBrightnessRTHEX(String color, double correctionFactore)
        {
            color = color.TrimStart('#');
            int intcolor = Convert.ToInt32(color, 16);

            double red = (intcolor >> 16) & 0xFF;
            double green = (intcolor >> 8) & 0xFF;
            double blue = (intcolor) & 0xFF;

            if (correctionFactore < 0)
            {
                correctionFactore = 1 + correctionFactore;
                red *= correctionFactore;
                green *= correctionFactore;
                blue *= correctionFactore;
            }
            else
            {
                red = (255 - red) * correctionFactore + red;
                green = (255 - green) * correctionFactore + green;
                blue = (255 - blue) * correctionFactore + blue;
            }
            return ("#" + ((((intcolor >> 24) & 0xFF) << 24) + ((int)red << 16) + ((int)green << 8) + ((int)blue)).ToString("X8").Substring(2));
        }
        public static Color ConvertHexToColor(string clr)
        {
            return Color.FromArgb(int.Parse(clr.Substring(1, 2), System.Globalization.NumberStyles.HexNumber), int.Parse(clr.Substring(3, 2), System.Globalization.NumberStyles.HexNumber), int.Parse(clr.Substring(5, 2), System.Globalization.NumberStyles.HexNumber), int.Parse(clr.Substring(7, 2), System.Globalization.NumberStyles.HexNumber));
        }
        public static string ConvertColorToHex(Color clr)
        {
            return ("#" + ((clr.A << 24) + (clr.R << 16) + (clr.G << 8) + clr.B).ToString("X8").Substring(2));
        }
    }
}
