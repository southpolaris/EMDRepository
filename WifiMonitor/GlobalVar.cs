using System;
using System.Collections.Generic;
using System.Net;

namespace WifiMonitor
{
    static class GlobalVar
    {
        //This is the manifest file of this program, default in application floder
        public static string xmlPath = Environment.CurrentDirectory + "\\Manifest.xml";

        //Property of mainform
        public static int nMainFormWidth = 600;
        public static int nMainFormHeight = 400;
        public static int nMainFormPosX = 0 ;
        public static int nMainFormPosY = 0;
        public static string sMainFormTitle = "主界面";

        //The sleep time between each cycle
        public static int cycleTime = 60;

        //The sub system ID 
        public static string subSysID = "000000";

        //This is the dictionary of wifi232 converter ip address and variables on each of it.
        public static Dictionary<IPAddress, RemoteNode> ipNodeMapping = new Dictionary<IPAddress, RemoteNode>();
          
        //The running flag, true means system monitor running
        public static bool runningFlag = false;
        //The edit flag, true means mainform is under editing
        public static bool editFlag = false;
        //Database connector parameters, here is the default paramerers
        public static string schemaName = "remotemonitor";
        public static string dataSource = "127.0.0.1";
        public static int portNumber = 3306;
        public static string userID = "root";
        public static string password = "12345";
        //Thress types of data, using in database storge
        public static string[] strDataSave = { "不保存", "保存实时数据", "保留历史数据" };
    }

    public struct RemoteNode
    {
        public string protocolType;                 //协议名称
        public string name;                         //设备型号（类型）
        public DataCount dataCount;                 //数据长度
        public Variables[] varDiscreteInput;        //开关型只读数据
        public Variables[] varDiscreteOutput;       //开关型写入数据
        public Variables[] varInputRegister;        //数值型只读数据
        public Variables[] varHoldingRegister;      //数值型写入数据

        public RemoteNode(string protocol, DataCount dataLength, string modelName)
        {
            protocolType = protocol;
            dataCount = dataLength;
            name = modelName;
            varDiscreteInput = new Variables[dataCount.discreteInput];
            varDiscreteOutput = new Variables[dataCount.coil];
            varInputRegister = new Variables[dataCount.inputRegister];
            varHoldingRegister = new Variables[dataCount.holdingRegiter];
        }

        public RemoteNode(DataCount dataLength, string modelName)
        {
            protocolType = "";
            dataCount = dataLength;
            name = modelName;
            varDiscreteInput = new Variables[dataCount.discreteInput];
            varDiscreteOutput = new Variables[dataCount.coil];
            varInputRegister = new Variables[dataCount.inputRegister];
            varHoldingRegister = new Variables[dataCount.holdingRegiter];
        }
    }

    public struct Variables
    {
        public string varName;
        public int intValue;
        public bool boolValue;
        public DataSave inDataBase;
    }

    //Variables length to read in each remote node.
    public struct DataCount
    {
        public ushort discreteInput;
        public ushort coil;
        public ushort inputRegister;
        public ushort holdingRegiter;
    }

    public enum DataInterface
    {
        DiscretesInputs = 1,    //Single bit read-only
        Coils = 2,              //Single bit read-write
        InputRegister = 3,      //16 bit read-only
        HoldingRegister = 4     //16 bit read-write     
    }

    public enum DataType
    {
        UnsignedShort = 0,
        SignedInt = 1,
        Float = 2
    }

    public enum DataSave
    {
        DoNotSave = 0,
        SaveCurrnetValue = 1,
        SaveAsLog = 2
    }
}
