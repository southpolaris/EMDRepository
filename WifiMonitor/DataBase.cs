using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WifiMonitor
{
    class DataBase
    {
        Slave m_Slave;
        List<string> varName;
        List<int> varValue;
        string tableName;

        public DataBase()
        {
            
        }

        public void InitalParameter(Slave slave)
        {
            varName = new List<string>();
            varValue = new List<int>();

            for (int dIn = 0; dIn < slave.remoteNode.varDiscreteInput.Length; dIn++)
            {
                if (slave.remoteNode.varDiscreteInput[dIn].inDataBase)
                {
                    varName.Add(slave.remoteNode.varDiscreteInput[dIn].varName);
                    varValue.Add(Convert.ToInt32(slave.remoteNode.varDiscreteInput[dIn].boolValue));
                }
            }
            for (int dOut = 0; dOut < slave.remoteNode.varDiscreteOutput.Length; dOut++)
            {
                if (slave.remoteNode.varDiscreteOutput[dOut].inDataBase)
                {
                    varName.Add(slave.remoteNode.varDiscreteOutput[dOut].varName);
                    varValue.Add(Convert.ToInt32(slave.remoteNode.varDiscreteOutput[dOut].boolValue));
                }
            }
            for (int inRes = 0; inRes < slave.remoteNode.varInputRegister.Length; inRes++)
            {
                if (slave.remoteNode.varInputRegister[inRes].inDataBase)
                {
                    varName.Add(slave.remoteNode.varInputRegister[inRes].varName);
                    varValue.Add(slave.remoteNode.varInputRegister[inRes].intValue);
                }
            }
            for (int holdRes = 0; holdRes < slave.remoteNode.varHoldingRegister.Length; holdRes++)
            {
                if (slave.remoteNode.varHoldingRegister[holdRes].inDataBase)
                {
                    varName.Add(slave.remoteNode.varHoldingRegister[holdRes].varName);
                    varValue.Add(slave.remoteNode.varHoldingRegister[holdRes].intValue);
                }
            }

            tableName = slave.remoteNode.name;
            m_Slave = slave;
        }

        /// <summary>
        /// 数据更新，插入数据
        /// </summary>
        /// <param name="slave"></param>
        /// <returns></returns>
        public int InsertNewRecord(Slave slave)
        {
            InitalParameter(slave);
            //"INSERT INTO `remotemonitor`.`modbusnode` (`datetime`, `millisecond`, `开关量`, `只读数值1`, `读写数值1`) VALUES ('2015-12-25 8:35', '200', '0', '23333', '-66666');";
            string columnName = "(`datetime`, ";
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varName.Count - 1)
                {
                    columnName += string.Format("`{0}`", varName[index]);
                }
                else
                {
                    columnName += string.Format("`{0}`, ", varName[index]);
                }
            }
            columnName += ") ";
            string values = string.Format("VALUES ('{0}', ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + '.' + DateTime.Now.Millisecond.ToString());
            for (int index = 0; index < varValue.Count; index++)
            {
                if (index == varValue.Count - 1)
                {
                    values += string.Format("'{0}'", varValue[index]);
                }
                else
                {
                    values += string.Format("'{0}', ", varValue[index]);
                }
            }
            values += ");";
            string insertCmd = string.Format("INSERT INTO `remotemonitor`.`{0}` ", tableName) + columnName + values;
            try
            {
                int lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, insertCmd, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
            
        }
    }
}
