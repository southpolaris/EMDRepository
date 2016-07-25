using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;

namespace WifiMonitor
{
    /// <summary>
    /// 数据库操作函数
    /// </summary>
    class DataBase
    {
        List<string> varName;
        List<int> varValue;
        string nodeName;
        private object datalock = new object();

        public DataBase() { }

        /// <summary>
        /// 获取所有需要保存数据的变量名称和变量数值
        /// </summary>
        /// <param name="remoteNode"></param>
        public void InitalParameter(RemoteNode remoteNode)
        {
            varName = new List<string>();
            varValue = new List<int>();

            for (int dIn = 0; dIn < remoteNode.varDiscreteInput.Length; dIn++)
            {
                if (remoteNode.varDiscreteInput[dIn].inDataBase != DataSave.DoNotSave)
                {
                    varName.Add(remoteNode.varDiscreteInput[dIn].varName);
                    varValue.Add(Convert.ToInt32(remoteNode.varDiscreteInput[dIn].boolValue));
                }
            }
            for (int dOut = 0; dOut < remoteNode.varDiscreteOutput.Length; dOut++)
            {
                if (remoteNode.varDiscreteOutput[dOut].inDataBase != DataSave.DoNotSave)
                {
                    varName.Add(remoteNode.varDiscreteOutput[dOut].varName);
                    varValue.Add(Convert.ToInt32(remoteNode.varDiscreteOutput[dOut].boolValue));
                }
            }
            for (int inRes = 0; inRes < remoteNode.varInputRegister.Length; inRes++)
            {
                if (remoteNode.varInputRegister[inRes].inDataBase != DataSave.DoNotSave)
                {
                    varName.Add(remoteNode.varInputRegister[inRes].varName);
                    varValue.Add(remoteNode.varInputRegister[inRes].intValue);
                }
            }
            for (int holdRes = 0; holdRes < remoteNode.varHoldingRegister.Length; holdRes++)
            {
                if (remoteNode.varHoldingRegister[holdRes].inDataBase != DataSave.DoNotSave)
                {
                    varName.Add(remoteNode.varHoldingRegister[holdRes].varName);
                    varValue.Add(remoteNode.varHoldingRegister[holdRes].intValue);
                }
            }

            nodeName = remoteNode.name;
        }

        /// <summary>
        /// 获取所有需要保存记录的变量名称和变量数值
        /// </summary>
        /// <param name="remoteNode"></param>
        public void InitalLogParameter(RemoteNode remoteNode)
        {
            varName = new List<string>();
            varValue = new List<int>();

            for (int dIn = 0; dIn < remoteNode.varDiscreteInput.Length; dIn++)
            {
                if (remoteNode.varDiscreteInput[dIn].inDataBase == DataSave.SaveAsLog)
                {
                    varName.Add(remoteNode.varDiscreteInput[dIn].varName);
                    varValue.Add(Convert.ToInt32(remoteNode.varDiscreteInput[dIn].boolValue));
                }
            }
            for (int dOut = 0; dOut < remoteNode.varDiscreteOutput.Length; dOut++)
            {
                if (remoteNode.varDiscreteOutput[dOut].inDataBase == DataSave.SaveAsLog)
                {
                    varName.Add(remoteNode.varDiscreteOutput[dOut].varName);
                    varValue.Add(Convert.ToInt32(remoteNode.varDiscreteOutput[dOut].boolValue));
                }
            }
            for (int inRes = 0; inRes < remoteNode.varInputRegister.Length; inRes++)
            {
                if (remoteNode.varInputRegister[inRes].inDataBase == DataSave.SaveAsLog)
                {
                    varName.Add(remoteNode.varInputRegister[inRes].varName);
                    varValue.Add(remoteNode.varInputRegister[inRes].intValue);
                }
            }
            for (int holdRes = 0; holdRes < remoteNode.varHoldingRegister.Length; holdRes++)
            {
                if (remoteNode.varHoldingRegister[holdRes].inDataBase == DataSave.SaveAsLog)
                {
                    varName.Add(remoteNode.varHoldingRegister[holdRes].varName);
                    varValue.Add(remoteNode.varHoldingRegister[holdRes].intValue);
                }
            }
        }

        // 建立机床模块映射表
        public int CreateMappingTable()
        {
            //Check if the table has been created
            string findDataTableSql = string.Format("select `TABLE_NAME` from `INFORMATION_SCHEMA`.`TABLES` where `TABLE_SCHEMA`='{0}' and `TABLE_NAME`='{1}';",
                GlobalVar.schemaName, "ipmap");
            int lineNumber = -1;
            try
            {
                string tempStr = Convert.ToString(MySQLHelper.ExecuteScalar(CommandType.Text, findDataTableSql, null));
                if (string.Compare(tempStr, "ipmap") == 0)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请检查与数据库'" + GlobalVar.schemaName + "'的连接并尝试重新启动程序", "数据库访问出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            string createSql = string.Format("CREATE TABLE `{0}`.`ipmap` (`IP` VARCHAR(20) NOT NULL COMMENT '', `name` VARCHAR(45) NULL COMMENT '', PRIMARY KEY (`IP`)  COMMENT '');",
                GlobalVar.schemaName);
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, createSql, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "机床数据表建立出错");
                return -1;
            }

            string insertCmd = "INSERT INTO `remotemonitor`.`ipmap` (`IP`, `name`) ";
            foreach (var node in GlobalVar.ipNodeMapping)
            {
                insertCmd += string.Format("VALUES ('{0}', '{1}')", node.Key, node.Value.name);
            }
            insertCmd += ";";
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, insertCmd, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "机床数据表建立出错");
                return -1;
            }
            return lineNumber;
        }

        /// <summary>
        /// Create new data table for every node connect to the mechine
        /// </summary>
        /// <param name="remoteNode">Struct of remote node</param>
        /// <returns>SQL语句影响的行数,-1代表函数执行错误</returns>
        public int CreateDataTable(IPAddress nodeIP, RemoteNode remoteNode)
        {
            /*CREATE TABLE `remotemonitor`.`new_table` (
              `datetime` DATETIME NOT NULL COMMENT '',
              `开关量` TINYINT(1) NULL DEFAULT NULL COMMENT '',
              `数值量` INT(32) NULL DEFAULT NULL COMMENT '',
              PRIMARY KEY (`datetime`)  COMMENT '');*/
            string findDataTableSql = string.Format("select `TABLE_NAME` from `INFORMATION_SCHEMA`.`TABLES` where `TABLE_SCHEMA`='{0}' and `TABLE_NAME`='{1}';",
                GlobalVar.schemaName, nodeIP.ToString());
            int lineNumber = -1;
            try
            {
                string tempStr = Convert.ToString(MySQLHelper.ExecuteScalar(CommandType.Text, findDataTableSql, null));
                if (string.Compare(tempStr,  nodeIP.ToString()) == 0)
                {
                    //Insert a blank log record to update
                    InsertBlankRecord(nodeIP, remoteNode);
                    return 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请检查与数据库'" + GlobalVar.schemaName + "'的连接并尝试重新启动程序", "数据库访问出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            //Get the coloumn names and coloumn type of current remote node.
            List<string> varNameBool = new List<string>();
            List<string> varNameInt = new List<string>();
            for (int dIn = 0; dIn < remoteNode.varDiscreteInput.Length; dIn++)
            {
                if (remoteNode.varDiscreteInput[dIn].inDataBase != DataSave.DoNotSave)
                {
                    varNameBool.Add(remoteNode.varDiscreteInput[dIn].varName);
                }
            }
            for (int dOut = 0; dOut < remoteNode.varDiscreteOutput.Length; dOut++)
            {
                if (remoteNode.varDiscreteOutput[dOut].inDataBase != DataSave.DoNotSave)
                {
                    varNameBool.Add(remoteNode.varDiscreteOutput[dOut].varName);
                }
            }
            for (int inRes = 0; inRes < remoteNode.varInputRegister.Length; inRes++)
            {
                if (remoteNode.varInputRegister[inRes].inDataBase != DataSave.DoNotSave)
                {
                    varNameInt.Add(remoteNode.varInputRegister[inRes].varName);
                }
            }
            for (int holdRes = 0; holdRes < remoteNode.varHoldingRegister.Length; holdRes++)
            {
                if (remoteNode.varHoldingRegister[holdRes].inDataBase != DataSave.DoNotSave)
                {
                    varNameInt.Add(remoteNode.varHoldingRegister[holdRes].varName);
                }
            }

            string createSql = string.Format("CREATE TABLE `{0}`.`{1}` (`IP` VARCHAR(20) NOT NULL, `datetime` DATETIME NOT NULL, ", GlobalVar.schemaName, remoteNode.name);
            foreach (string varname in varNameBool)
            {
                createSql += string.Format("`{0}` TINYINT(1) NULL DEFAULT NULL, ", varname);
            }
            foreach (string varname in varNameInt)
            {
                createSql += string.Format("`{0}` INT(32) NULL DEFAULT NULL, ", varname);
            }
            createSql += "PRIMARY KEY (`IP`));";
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, createSql, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据表建立出错");
                return -1;
            }

            //Insert a blank log record to update
            InsertBlankRecord(nodeIP, remoteNode);
            return 0;
        }

        private int InsertBlankRecord(IPAddress nodeIP, RemoteNode remoteNode)
        {
            InitalParameter(remoteNode);
            int lineNumber = -1;
            string coloumnName = "(`IP`, `datetime`, ";
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varName.Count - 1)
                {
                    coloumnName += string.Format("`{0}`) ", varName[index]);
                }
                else
                {
                    coloumnName += string.Format("`{0}`, ", varName[index]);
                }
            }
            string values = string.Format("VALUES ('{0}', '{1}',", nodeIP, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            for (int index = 0; index < varValue.Count; index++)
            {
                if (index == varValue.Count - 1)
                {
                    values += string.Format("'{0}');", varValue[index]);
                }
                else
                {
                    values += string.Format("'{0}', ", varValue[index]);
                }
            }
            string insertCmd = string.Format("INSERT INTO `{0}`.`{1}` ", GlobalVar.schemaName, remoteNode.name) + coloumnName + values;
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, insertCmd, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, remoteNode.name + "初始化出错");
                return -1;
            }      
        }

        /// <summary>
        /// Create new data log table for every remote node connected
        /// </summary>
        /// <param name="remoteNode">Struct of remote node</param>
        /// <param name="ip">节点模块IP</param>
        /// <returns>SQL语句影响的行数</returns>
        public int CreateDataLogTable(IPAddress nodeIP, RemoteNode remoteNode)
        {
            string findDataTableSql = string.Format("select `TABLE_NAME` from `INFORMATION_SCHEMA`.`TABLES` where `TABLE_SCHEMA`='{0}' and `TABLE_NAME`='{1}';",
                GlobalVar.schemaName, nodeIP + "log");
            int lineNumber = -1;
            try
            {
                string tempStr = Convert.ToString(MySQLHelper.ExecuteScalar(CommandType.Text, findDataTableSql, null));
                if (string.Compare(tempStr,  nodeIP + "log") == 0)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请检查与数据库'" + GlobalVar.schemaName + "'的连接并尝试重新启动程序", "数据库访问出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            //Get the coloumn names and coloumn type of current remote node.
            List<string> varNameBool = new List<string>();
            List<string> varNameInt = new List<string>();
            for (int dIn = 0; dIn < remoteNode.varDiscreteInput.Length; dIn++)
            {
                if (remoteNode.varDiscreteInput[dIn].inDataBase == DataSave.SaveAsLog)
                {
                    varNameBool.Add(remoteNode.varDiscreteInput[dIn].varName);
                }
            }
            for (int dOut = 0; dOut < remoteNode.varDiscreteOutput.Length; dOut++)
            {
                if (remoteNode.varDiscreteOutput[dOut].inDataBase == DataSave.SaveAsLog)
                {
                    varNameBool.Add(remoteNode.varDiscreteOutput[dOut].varName);
                }
            }
            for (int inRes = 0; inRes < remoteNode.varInputRegister.Length; inRes++)
            {
                if (remoteNode.varInputRegister[inRes].inDataBase == DataSave.SaveAsLog)
                {
                    varNameInt.Add(remoteNode.varInputRegister[inRes].varName);
                }
            }
            for (int holdRes = 0; holdRes < remoteNode.varHoldingRegister.Length; holdRes++)
            {
                if (remoteNode.varHoldingRegister[holdRes].inDataBase == DataSave.SaveAsLog)
                {
                    varNameInt.Add(remoteNode.varHoldingRegister[holdRes].varName);
                }
            }

            if (varNameInt.Count == 0 && varNameBool.Count == 0) //没有需要存储为记录的内容
            {
                return 0;
            }

            string createSql = string.Format("CREATE TABLE `{0}`.`{1}` (`IP` VARCHAR(20) NOT NULL, `datetime` DATETIME NOT NULL, ", GlobalVar.schemaName, nodeIP.ToString() + "log");
            foreach (string varname in varNameBool)
            {
                createSql += string.Format("`{0}` TINYINT(1) NULL DEFAULT NULL, ", varname);
            }
            foreach (string varname in varNameInt)
            {
                createSql += string.Format("`{0}` INT(32) NULL DEFAULT NULL, ", varname);
            }
            createSql += "PRIMARY KEY (`datetime`));";
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, createSql, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据表建立出错");
                return -1;
            }
        }

        public int CreateTrigger(IPAddress nodeIP, RemoteNode remoteNode)
        {
            InitalLogParameter(remoteNode);
            //Prepare trigger condition
            string conditionStr = "IF ";
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varName.Count - 1)
                {
                    conditionStr += string.Format("NEW.{0} <> OLD.{0} then\n", varName[index]);
                }
                else
                {
                    conditionStr += string.Format("NEW.{0} <> OLD.{0} OR ", varName[index]);
                }
            }

            //Prepare insert sql
            string coloumnName = "(`IP`, `datetime`, ";
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varName.Count - 1)
                {
                    coloumnName += string.Format("`{0}`) ", varName[index]);
                }
                else
                {
                    coloumnName += string.Format("`{0}`, ", varName[index]);
                }
            }
            string values = string.Format("VALUES ('{0}', NEW.datetime, ", nodeIP.ToString());
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varValue.Count - 1)
                {
                    values += string.Format("NEW.{0});\n", varName[index]);
                }
                else
                {
                    values += string.Format("NEW.{0}, ", varName[index]);
                }
            }
            string insertCmd = string.Format("INSERT INTO `{0}`.`{1}` ", GlobalVar.schemaName, nodeIP + "log") + coloumnName + values;
            string triggerCmd = string.Format("CREATE TRIGGER updatedata{0} AFTER UPDATE ON `{1}`.`{2}`\n", nodeIP.ToString().Split('.')[3], GlobalVar.schemaName, remoteNode.name.ToLower()) +
                "FOR EACH ROW\nBEGIN\n" +
                conditionStr +
                insertCmd +
                "END IF;\nEND";
            try
            {
                int lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, string.Format("DROP TRIGGER IF EXISTS updatedata{0};", nodeIP.ToString().Split('.')[3]), null);
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, triggerCmd, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "触发器建立出错");
                return -1;
            }
        }

        /// <summary>
        /// 建立节点当前状态视图，暂时感觉没卵用
        /// </summary>
        /// <param name="nodeIP"></param>
        /// <param name="remoteNode"></param>
        /// <returns></returns>
        public int CreateView(IPAddress nodeIP, RemoteNode remoteNode)
        {
            int lineNumber = -1;
            string createViewSql = string.Format("CREATE VIEW `{0}view` AS\n SELECT `ipmap`.`name` AS `节点名称`,\n `{0}`.*\nFROM (`ipmap` JOIN `{0}`)",
                remoteNode.name.ToLower());
            try
            {
                lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, createViewSql, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "视图建立出错");
                return -1;
            }
        }

        /// <summary>
        /// 插入新纪录
        /// </summary>
        /// <param name="remoteNode">远程终端</param>
        /// <returns>SQL语句影响的行数</returns>
        public int InsertNewRecord(IPAddress nodeIP, RemoteNode remoteNode)
        {
            InitalParameter(remoteNode);
            //"INSERT INTO `remotemonitor`.`modbusnode` (`datetime`, `开关量`, `只读数值1`, `读写数值1`) VALUES ('2015-12-25 8:35', '0', '23333', '-66666');";
            string coloumnName = "(`datetime`, ";
            for (int index = 0; index < varName.Count; index++)
            {
                if (index == varName.Count - 1)
                {
                    coloumnName += string.Format("`{0}`) ", varName[index]);
                }
                else
                {
                    coloumnName += string.Format("`{0}`, ", varName[index]);
                }
            }
            string values = string.Format("VALUES ('{0}', ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            for (int index = 0; index < varValue.Count; index++)
            {
                if (index == varValue.Count - 1)
                {
                    values += string.Format("'{0}');", varValue[index]);
                }
                else
                {
                    values += string.Format("'{0}', ", varValue[index]);
                }
            }
            string insertCmd = string.Format("INSERT INTO `{0}`.`{1}` ", GlobalVar.schemaName, nodeName) + coloumnName + values;
            try
            {
                int lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, insertCmd, null);
                return lineNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库插入出错");
                return -1;
            }              
        }

        /// <summary>
        /// Update the record of current node value
        /// </summary>
        /// <param name="remoteNode">远程终端</param>
        /// <returns>SQL语句影响的行数</returns>
        public int UpdateRecord(IPAddress nodeIP, RemoteNode remotenode)
        {
            //UPDATE `remotemonitor`.`modbusnode` SET `开关量`='1', `只读数值1`='234', `读写数值1`='-567' WHERE `datetime`='2015-12-25';
            lock (datalock) 
            {
                InitalParameter(remotenode);
                string updateString = "";
                for (int index = 0; index < varName.Count; index++)
                {
                    try
                    {
                        if (index == varName.Count - 1)
                        {
                            updateString += "`" + varName[index] + "`=";
                            updateString += "'" + varValue[index] + "', ";
                        }
                        else
                        {
                            updateString += "`" + varName[index] + "`=";
                            updateString += "'" + varValue[index] + "', ";
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "插入数据库");
                        return -1;
                    }
                }
                updateString += string.Format("`datetime`='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string updateCmd = string.Format("UPDATE `{0}`.`{1}` SET ", GlobalVar.schemaName, nodeIP.ToString()) + updateString + "WHERE `IP` = '" + nodeIP.ToString() + "';";
                try
                {
                    int lineNumber = MySQLHelper.ExecuteNonQuerty(CommandType.Text, updateCmd, null);
                    return lineNumber;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, nodeIP + "数据库更新出错");
                    return -1;
                }
            }              
        }
    }
}
