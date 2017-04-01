using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using log4net;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace OperationTickets
{
    class ExportWordUtility
    {
        private static readonly ILog logger = LogManager.GetLogger("Main");
        private SQLiteDBHelper sqliteHelper = new SQLiteDBHelper();

        public ExportWordUtility()
        {
        }

        /// <summary>
        /// 创建Word
        /// </summary>
        /// <param name="createTime">当前选中行的CreateTime值</param>
        /// <returns>创建成功返回成功</returns>
        public  bool WordEstablish(string savedPath,string createTime)
        {            
            string templatePath = string.Format(@"{0}\File\WordTemplate.docx", Application.StartupPath);
            if (string.IsNullOrEmpty(templatePath) || !File.Exists(templatePath))
            {
                logger.Error("Mainform发生错误:Word模板不存在!");
                return false;
            }

            DataTable dtExportWordTicket = GetDtExportWordTicket(createTime);
            DataTable dtExportWordOperationSteps = GetDtExportWordOperationSteps(createTime);
            int dtStepsCount = dtExportWordOperationSteps.Rows.Count;
            //每页最多存储13条操作记录
            int wordPageCount = (dtStepsCount - 1) / 13 + 1;

            //声明参数
            Dictionary<string, string> DList = new Dictionary<string, string>();

            for (int i = 1; i <= wordPageCount; i++)
            {
                DList.Add(string.Format("操作票名称{0}", i.ToString()), dtExportWordTicket.Rows[0]["Name"].ToString());
                DList.Add(string.Format("操作任务{0}", i.ToString()), dtExportWordTicket.Rows[0]["Task"].ToString());
                DList.Add(string.Format("编号{0}", i.ToString()), dtExportWordTicket.Rows[0]["No"].ToString());
                DList.Add(string.Format("操作日期{0}", i.ToString()), dtExportWordTicket.Rows[0]["OperationDate"].ToString());
                DList.Add(string.Format("开始时间{0}", i.ToString()), dtExportWordTicket.Rows[0]["StartTime"].ToString());
                DList.Add(string.Format("结束时间{0}", i.ToString()), dtExportWordTicket.Rows[0]["EndTime"].ToString());
            }
            for (int i = 1; i <= dtStepsCount; i++)
            {
                DList.Add("顺序" + i.ToString(), i.ToString());
                DList.Add("操作" + i.ToString(), dtExportWordOperationSteps.Rows[i - 1][1].ToString());
            }

            if (WordHelper.ExportWord(templatePath, savedPath, DList, wordPageCount))
            {
                return true;
            }
            return false;
        }

        private DataTable GetDtExportWordTicket(string focusedRowsCreateTime)
        {
            string sqlQueryTickets = string.Format(@"SELECT Name,Task,No,OperationDate,StartTime,EndTime FROM Tickets WHERE CreateTime='{0}'",
                focusedRowsCreateTime);
            try
            {
                DataTable dtReturn = sqliteHelper.ExecuteDataTable(sqlQueryTickets, null);
                return dtReturn;
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
                return null;
            }
        }

        private DataTable GetDtExportWordOperationSteps(string focusedRowsCreateTime)
        {
            string sqlQueryOperationSteps = string.Format(@"SELECT StepNo,Operation,Comment FROM OperationSteps WHERE CreateTime='{0}' ORDER BY StepNo ASC",
                focusedRowsCreateTime);
            try
            {
                DataTable dtReturn = sqliteHelper.ExecuteDataTable(sqlQueryOperationSteps, null);
                return dtReturn;
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
                return null;
            }
        }
    }
}
