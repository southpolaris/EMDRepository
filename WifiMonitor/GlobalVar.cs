using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using System.Collections;//ArrayList

namespace WifiMonitor
{
    static class GlobalVar
    {
        ////给定INI文件路径  默认为应用启动的路径
        public static string xmlPath = Environment.CurrentDirectory + "\\Manifest.xml";

        //主窗体宽 高
        public static int nMainFormWidth = 600;
        public static int nMainFormHeight = 400;
        public static int nMainFormPosX = 0 ;
        public static int nMainFormPosY = 0;
        public static string sMainFormTitle = "主界面";

        public static Dictionary<IPAddress, RemoteNode> ipNodeMapping = new Dictionary<IPAddress, RemoteNode>();
          
        //运行 标志位
        public static bool runningFlag = false;
        //编辑 标志位
        public static bool editFlag = false;
    }

    public struct RemoteNode
    {
        public string protocolType;                 //协议名称
        public string name;                         //远程终端名称
        public DataCount dataCount;                 //数据长度
        public Variables[] varDiscreteInput;        //开关型只读数据
        public Variables[] varDiscreteOutput;       //开关型写入数据
        public Variables[] varInputRegister;        //数值型只读数据
        public Variables[] varHoldingRegister;      //数值型写入数据

        public RemoteNode(string protocol, DataCount dataLength, string nodeName)
        {
            protocolType = protocol;
            dataCount = dataLength;
            name = nodeName;
            varDiscreteInput = new Variables[dataCount.discreteInput];
            varDiscreteOutput = new Variables[dataCount.coil];
            varInputRegister = new Variables[dataCount.inputRegister];
            varHoldingRegister = new Variables[dataCount.holdingRegiter];
        }

        public RemoteNode(DataCount dataLength, string nodeName)
        {
            protocolType = "";
            dataCount = dataLength;
            name = nodeName;
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
        public bool inDataBase;
    }

    //各个模块对应的参数的个数
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
}
