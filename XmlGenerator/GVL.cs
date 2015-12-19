using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGenerator
{
    static class GVL
    {
        public static int alterornot = 0; //1-add 2-edit

        // 是否确定加载数据
        public static bool IFornotload = true;

        public static DataLength dataLength = new DataLength();

       //主窗体宽 高
        public static int nMainFormWidth = 600;
        public static int nMainFormHeight = 400;
        public static int nMainFormPosX = 0;
        public static int nMainFormPosY = 0;
        public static string sMainFormTitle = "主界面";

        //运行 标志位
        public static bool runningFlag = false;
        //编辑 标志位
        public static bool editFlag = false;
         
     }
    //各个模块对应的参数的个数
    public struct DataLength
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

    public enum ModbusDataType
    {
        UnsignedShort = 0,
        SignedInt = 1,
        Float = 2
    }  
}
