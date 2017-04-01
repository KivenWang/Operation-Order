using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Xml;
using System.Data.SQLite;
using log4net;

namespace OperationTickets
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dtGridView = new DataTable();
        private SQLiteDBHelper sqliteHelper = new SQLiteDBHelper();
        private static readonly ILog logger = LogManager.GetLogger("Main");
        private SqlTool sqlTool = new SqlTool();
        public MainForm()
        {
            InitializeComponent();
            RefreshUI();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new NewTicketForm().ShowDialog() == DialogResult.OK)
            {
                RefreshUI();
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确认删除这条操作票吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string deleteRowsCreateTime = this.viewRoom.GetFocusedRowCellValue("CreateTime").ToString();
                DeleteViewTicket(deleteRowsCreateTime);
                sqlTool.DeleteTicket(deleteRowsCreateTime);

                DeleteImageFile(deleteRowsCreateTime);
                this.gridOperationTickMain.DataSource = dtGridView;
            }
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string savedPath = GetSavedPath();
            string focusedRowsCreateTime = this.viewRoom.GetFocusedRowCellValue("CreateTime").ToString();

            if (string.IsNullOrEmpty(savedPath))
            {
                return;
            }

            ExportWordUtility exportWordUtility = new ExportWordUtility();
            if (exportWordUtility.WordEstablish(savedPath, focusedRowsCreateTime))
            {
                XtraMessageBox.Show("Word导出成功!", "提示", MessageBoxButtons.OK);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savedPath = GetSavedPath();
            string focusedRowsCreateTime = this.viewRoom.GetFocusedRowCellValue("CreateTime").ToString();

            if (string.IsNullOrEmpty(savedPath))
            {
                return;
            }
            ExportWordUtility exportWordUtility = new ExportWordUtility();
            if (exportWordUtility.WordEstablish(savedPath, focusedRowsCreateTime))
            {
                XtraMessageBox.Show("Word导出成功!", "提示", MessageBoxButtons.OK);
            }
        }

        private void queryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.viewRoom.RowCount > 0)
            {
                string focusedRowsCreateTime = this.viewRoom.GetFocusedRowCellValue("CreateTime").ToString();
                new QueryForm(focusedRowsCreateTime).Show ();
            }
            else
            {
                XtraMessageBox.Show("当前没有操作票记录,请先新增操作票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确认删除所选操作票吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int[] rowsSelected = this.viewRoom.GetSelectedRows();
                for (int i = rowsSelected.Length - 1; i >= 0; i--)
                {
                    string deleteRowsCreateTime = this.viewRoom.GetRowCellValue(rowsSelected[i], "CreateTime").ToString();

                    DeleteViewTicket(deleteRowsCreateTime);
                    sqlTool.DeleteTicket(deleteRowsCreateTime);

                    DeleteImageFile(deleteRowsCreateTime);
                }
                this.gridOperationTickMain.DataSource = dtGridView;
            }
        }

        private void DeleteImageFile(string creatTime)
        {
            string imageFileName = TransformCreateTime(creatTime);
            string imageDirectoryPath = string.Format(@"{0}\Image", System.Windows.Forms.Application.StartupPath);
            DirectoryInfo imageDirectory = new DirectoryInfo(imageDirectoryPath);

            foreach (FileInfo file in imageDirectory.GetFiles("*.png"))
            {
                if (file.Name.Contains(imageFileName))
                {
                    file.Delete();
                }
            }
        }

        private string TransformCreateTime(string createTime)
        {
            StringBuilder createTimeBuilder = new StringBuilder(createTime);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
            {
                createTimeBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            }
            return createTimeBuilder.ToString();
        }

        private void DeleteViewTicket(string creatTime)
        {
            DataRow[] deleteRow = dtGridView.Select(string.Format("CreateTime='{0}'", creatTime));
            dtGridView.Rows.Remove(deleteRow[0]);
        }
        private void DeleteSqlTicket(string createTime)
        {
            sqlTool.DeleteTicket(createTime);
        }

        public void RefreshUI()
        {
            dtGridView = sqlTool.GetTickets(1000);
            this.gridOperationTickMain.DataSource = dtGridView;
        }


        //选择存储路径
        private string GetSavedPath()
        {
            string selectPath = string.Empty;
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                if (browserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectPath = string.Format(@"{0}\广东电网责任公司电力调度中心操作票{1}.docx",
                        browserDialog.SelectedPath, DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
            }
            return selectPath;
        }

        private void addStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (this.viewRoom.RowCount > 0)
            {
                string focusedRowsCreateTime = this.viewRoom.GetFocusedRowCellValue("CreateTime").ToString();
                if (new TemplateTicketForm(focusedRowsCreateTime).ShowDialog() == DialogResult.OK)
                {
                    RefreshUI();
                }
            }
            else
            {
                XtraMessageBox.Show("当前没有操作票记录,请先新增操作票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}