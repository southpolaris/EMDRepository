using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;//ArrayList

namespace WifiMonitor
{
    class GlobalVar
    {
        ////给定INI文件路径  默认为应用启动的路径
        public static string sIniPath = Environment.CurrentDirectory + "\\WifiMonitor.ini";
        //主窗体宽 高
        public static int nMainFormWidth = 300;
        public static int nMainFormHeight = 300;
        public static int nMainFormPosX = 0 ;
        public static int nMainFormPosY = 0;
        public static string sMainFormTitle = "主界面";

        public static bool bEdit = false;

        public static string sPortName = "";
        public static int nBaudRate = 9600;

        //定义动态数组
        //public static List<string> RcvCmdLst = new List<string>();
        public static ArrayList TempCmdLst = new ArrayList();
        public static ArrayList RcvCmdLst = new ArrayList();

        //串口读入的参数的个数
        public static int RcvCmdNum = 0;    
  
        //运行 标志位
        public static bool runningFlag = false;
    }
}
