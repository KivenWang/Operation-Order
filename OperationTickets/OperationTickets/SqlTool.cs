using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net;
using System.Data.SQLite;

namespace OperationTickets
{
    class SqlTool
    {
        private static readonly ILog logger = LogManager.GetLogger("Main");
        private SQLiteDBHelper sqliteHelper = new SQLiteDBHelper();
        public SqlTool()
        {

        }
        /// <summary>
        /// 按CreateTime逆序查询Tickets表,返回一个Datatable
        /// </summary>
        /// <returns></returns>
        public DataTable GetTickets()
        {
            DataTable table = new DataTable();
            string commandText = "SELECT * FROM Tickets ORDER BY CreateTime DESC";
            try
            {
                table = sqliteHelper.ExecuteDataTable(commandText, null);
            }
            catch (Exception ex)
            {
                logger.Info(string.Format("查询数据库表Tickets记录失败:"), ex);
            }
            return table;
        }
        /// <summary>
        /// 按CreateTime逆序查询Tickets表的前几条,返回一个Datatable
        /// </summary>
        /// <param name="quantity">查询条数</param>
        /// <returns></returns>
        public DataTable GetTickets(int quantity)
        {
            DataTable table = new DataTable();
            string commandText = string.Format("SELECT * FROM Tickets ORDER BY CreateTime DESC LIMIT 0,{0}", quantity.ToString());
            try
            {
                table = sqliteHelper.ExecuteDataTable(commandText, null);
            }
            catch (Exception ex)
            {
                logger.Info(string.Format("查询数据库表Tickets中前{0}条记录失败:", quantity.ToString()), ex);
            }
            return table;
        }
        /// <summary>
        /// 查询Tickets表中指定CreateTime字段的值,返回一个Datatable
        /// </summary>
        /// <param name="quantity">查询条数</param>
        /// <returns></returns>
        public DataTable GetTickets(string createTime)
        {
            DataTable table = new DataTable();
            string commandText = string.Format(@"SELECT * FROM Tickets WHERE CreateTime='{0}'", createTime);
            try
            {
                table = sqliteHelper.ExecuteDataTable(commandText, null);
            }
            catch (Exception ex)
            {
                logger.Info(string.Format("查询数据库表Tickets中{0}值记录失败:", createTime, ex));
            }
            return table;
        }
        /// <summary>
        /// 按CreateTime逆序查询相应表名,返回一个Datatable.
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="quantity">查询条数</param>
        /// <returns></returns>
        public DataTable GetInitData(string tableName, int quantity)
        {
            DataTable table = new DataTable();
            string commandText = string.Format("SELECT * FROM {0} ORDER BY CreateTime DESC LIMIT 0,{1}", tableName, quantity.ToString());
            try
            {
                table = sqliteHelper.ExecuteDataTable(commandText, null);
            }
            catch (Exception ex)
            {
                logger.Info(string.Format("查询数据库表{0}中前{1}条记录失败:", tableName, quantity.ToString()), ex);
            }
            return table;
        }
        /// <summary>
        /// 根据CreateTime删除Tickets表和Operations表中数据
        /// </summary>
        /// <param name="createTime">CreateTime字段</param>
        public void DeleteTicket(string createTime)
        {
            string commandTicketsText = string.Format(@"DELETE FROM Tickets WHERE CreateTime = '{0}'", createTime);
            string commandOperationsText = string.Format(@"DELETE FROM OperationSteps WHERE CreateTime = '{0}'", createTime);
            try
            {
                sqliteHelper.ExecuteNonQuery(commandOperationsText);
                sqliteHelper.ExecuteNonQuery(commandTicketsText);
            }
            catch (Exception ex)
            {
                logger.Info("删除数据库记录失败:", ex);
            }
        }
        /// <summary>
        /// 根据CreateTime查询Operations表中数据,返回一个Datatable
        /// </summary>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public DataTable GetOperationStepsByCreateTime(string createTime)
        {
            DataTable table = new DataTable();
            string commandText = string.Format(@"SELECT * FROM OperationSteps WHERE CreateTime='{0}' ORDER BY StepNo ASC", createTime);
            try
            {
                table = sqliteHelper.ExecuteDataTable(commandText, null);
            }
            catch (Exception ex)
            {
                logger.Info("查询数据库表OperationSteps失败:", ex);
            }
            return table;
        }
        /// <summary>
        /// 向Tickets表中增加一条记录
        /// </summary>
        /// <param name="dicTicket">对应字段名及值</param>
        /// <returns></returns>
        public bool AddTicket(Dictionary<string, string> dicTicket)
        {
            string commandText = "INSERT INTO Tickets (No,Name,Task,OperationDate,User,StartTime,EndTime,CreateTime,CreateComment) VALUES (@No,@Name,@Task,@OperationDate,@User,@StartTime,@EndTime,@CreateTime,@CreateComment)";
            SQLiteParameter[] parameters = { new SQLiteParameter("@No"),
                                           new SQLiteParameter("@Name"),
                                           new SQLiteParameter("@Task"),
                                           new SQLiteParameter("@OperationDate"),
                                           new SQLiteParameter("@User"),
                                           new SQLiteParameter("@StartTime"),
                                           new SQLiteParameter("@EndTime"),
                                           new SQLiteParameter("@CreateTime"),
                                           new SQLiteParameter("@CreateComment"),};
            parameters[0].Value = dicTicket["No"];
            parameters[1].Value = dicTicket["Name"];
            parameters[2].Value = dicTicket["Task"];
            parameters[3].Value = dicTicket["OperationDate"];
            parameters[4].Value = dicTicket["User"];
            parameters[5].Value = dicTicket["StartTime"];
            parameters[6].Value = dicTicket["EndTime"];
            parameters[7].Value = dicTicket["CreateTime"];
            parameters[8].Value = dicTicket["CreateComment"];

            try
            {
                sqliteHelper.ExecuteNonQuery(commandText, parameters);
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("添加Ticket失败:", ex);
                return false;
            }
        }
        /// <summary>
        /// 向Tickets表中增加一条记录
        /// </summary>
        /// <param name="dtTicket"></param>
        /// <returns></returns>
        public bool AddTicket(DataTable dtTicket)
        {
            string commandText = "INSERT INTO Tickets (No,Name,Task,OperationDate,User,StartTime,EndTime,CreateTime,CreateComment) VALUES (@No,@Name,@Task,@OperationDate,@User,@StartTime,@EndTime,@CreateTime,@CreateComment)";
            SQLiteParameter[] parameters = { new SQLiteParameter("@No"),
                                           new SQLiteParameter("@Name"),
                                           new SQLiteParameter("@Task"),
                                           new SQLiteParameter("@Comment"),
                                           new SQLiteParameter("@OperationDate"),
                                           new SQLiteParameter("@User"),
                                           new SQLiteParameter("@StartTime"),
                                           new SQLiteParameter("@EndTime"),
                                           new SQLiteParameter("@CreateTime"),
                                           new SQLiteParameter("@CreateComment"),};
            try
            {
                foreach (DataRow row in dtTicket.Rows)
                {
                    parameters[0].Value = row["No"];
                    parameters[1].Value = row["Name"];
                    parameters[2].Value = row["Task"];
                    parameters[3].Value = row["OperationDate"];
                    parameters[4].Value = row["User"];
                    parameters[5].Value = row["StartTime"];
                    parameters[6].Value = row["EndTime"];
                    parameters[7].Value = row["CreateTime"];
                    parameters[8].Value = row["CreateComment"];

                    sqliteHelper.ExecuteNonQuery(commandText, parameters);

                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("添加Ticket失败:", ex);
                return false;
            }
        }
        /// <summary>
        /// 将Datatable中的数据插入Operations表中
        /// </summary>
        /// <param name="dtOperations">对应的Datatable</param>
        /// <returns></returns>
        public bool AddOperations(DataTable dtOperations)
        {
            string commandText = string.Format("INSERT INTO OperationSteps (CreateTime,StepNo,Operation,Comment,ImagePathCircuit,ImagePathCapacity) VALUES (@CreateTime,@StepNo,@Operation,@Comment,@ImagePathCircuit,@ImagePathCapacity)");

            List<SQLiteParameter[]> parametersList = new List<SQLiteParameter[]>();

            
            foreach (DataRow row in dtOperations.Rows)
            {
                SQLiteParameter[] parameters = { new SQLiteParameter("@CreateTime"),
                                           new SQLiteParameter("@StepNo"),
                                           new SQLiteParameter("@Operation"),
                                           new SQLiteParameter("@Comment"),
                                           new SQLiteParameter("@ImagePathCircuit"),
                                           new SQLiteParameter("@ImagePathCapacity"),};

                parameters[0].Value = row["CreateTime"];
                parameters[1].Value = row["StepNo"];
                parameters[2].Value = row["Operation"];
                parameters[3].Value = row["Comment"];
                parameters[4].Value = row["ImagePathCircuit"];
                parameters[5].Value = row["ImagePathCapacity"];

                parametersList.Add(parameters);
            }

            try
            {
                sqliteHelper.ExecuteNonQuery(commandText, parametersList);
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("添加OperationSteps失败:", ex);
                return false;
            }
        }



    }
}
