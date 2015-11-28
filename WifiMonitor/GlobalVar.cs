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

        public static Dictionary<IPAddress, string> ipPortocolMapping = new Dictionary<IPAddress,string>();
          
        //运行 标志位
        public static bool runningFlag = false;
        //编辑 标志位
        public static bool editFlag = false;
    }


    //各个模块对应的参数的个数
    public struct ModbusData
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
