using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using log4net;

namespace OperationTickets
{
    public partial class QueryForm : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dtTicket = new DataTable();
        private DataTable dtOperationSteps = new DataTable();
        private string focusedCreatTime = string.Empty;
        private static readonly ILog logger = LogManager.GetLogger("Main");
        private SqlTool sqlTool = new SqlTool();
        public QueryForm(string focused)
        {
            InitializeComponent();
            focusedCreatTime = focused;
            InitUI();
        }

        private void InitUI()
        {
            dtTicket = sqlTool.GetTickets(focusedCreatTime);
            dtOperationSteps = sqlTool.GetOperationStepsByCreateTime(focusedCreatTime);

            if (dtTicket.Rows.Count < 1|dtOperationSteps .Rows .Count <1)
            {
                return;
            }
            this.txtCreatComment.Text = dtTicket.Rows[0]["CreateComment"].ToString();
            this.txtCreateTime.Text = dtTicket.Rows[0]["CreateTime"].ToString();
            this.txtNo.Text = dtTicket.Rows[0]["No"].ToString();
            this.txtOperationDate.Text = dtTicket.Rows[0]["OperationDate"].ToString();
            this.txtOperationUser.Text = dtTicket.Rows[0]["User"].ToString();
            this.txtStarttime.Text = dtTicket.Rows[0]["StartTime"].ToString();
            this.txtEndtime.Text = dtTicket.Rows[0]["EndTime"].ToString();
            this.txtTicketName.Text = dtTicket.Rows[0]["Name"].ToString();
            this.txtTaskName.Text = dtTicket.Rows[0]["Task"].ToString();

            this.gridStep.DataSource = dtOperationSteps;
        }
        private void viewRoom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadImage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string savedPath = GetSavedPath();
            if (string.IsNullOrEmpty(savedPath))
            {
                return;
            }

            ExportWordUtility exportWordUtility = new ExportWordUtility();
            if (exportWordUtility.WordEstablish(savedPath, focusedCreatTime))
            {
                XtraMessageBox.Show("Word导出成功!", "提示", MessageBoxButtons.OK);
            }
        }
        private void LoadImage()
        {
            int focusedRowIndex = this.viewRoom.GetFocusedDataSourceRowIndex();
            string applicationPath = System.Windows.Forms.Application.StartupPath;
            string sqlCircuitPath = dtOperationSteps.Rows[focusedRowIndex]["ImagePathCircuit"].ToString();
            string sqlCapacityPath = dtOperationSteps.Rows[focusedRowIndex]["ImagePathCapacity"].ToString();
            string imagePathCircuit = string.Format(@"{0}\{1}", applicationPath, sqlCircuitPath);
            string imagePathCapacity = string.Format(@"{0}\{1}", applicationPath, sqlCapacityPath);
            try
            {
                if (sqlCircuitPath != string.Empty)
                {
                    this.picCircuitSimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(imagePathCircuit)));
                }
                else
                {
                    this.picCircuitSimulation.Image = null;

                }
                if (sqlCapacityPath != string.Empty)
                {
                    this.picCapacitySimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(imagePathCapacity)));
                }
                else
                {
                    this.picCapacitySimulation.Image = null;
                }
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
            }
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
    }
}