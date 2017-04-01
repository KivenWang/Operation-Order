using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
using System.IO;

namespace OperationTickets
{
    public partial class TemplateTicketForm : DevExpress.XtraEditors.XtraForm
    {
        private int currentStep = 1;
        private DataTable dtTicket = new DataTable();
        private DataTable dtGridView = new DataTable();
        private string templateCreateTime = string.Empty;
        private string imagePathCapacity = string.Empty;
        private string imagePathCircuit = string.Empty;
        private SqlTool sqlTool = new SqlTool();
        private static readonly ILog logger = LogManager.GetLogger("Main");
        public TemplateTicketForm(string template)
        {
            InitializeComponent();
            templateCreateTime = template;
            InitUI();
        }

        private void InitUI()
        {
            dtTicket = sqlTool.GetTickets(templateCreateTime);
            dtGridView = sqlTool.GetOperationStepsByCreateTime(templateCreateTime);

            if (dtTicket.Rows.Count < 1 | dtGridView.Rows.Count < 1)
            {
                return;
            }
            this.txtCreateComment.Text = dtTicket.Rows[0]["CreateComment"].ToString();
            this.txtCreateTime.Text = DateTime.Now.ToString();
            this.txtNo.Text = dtTicket.Rows[0]["No"].ToString();
            this.txtOperationDate.Text = dtTicket.Rows[0]["OperationDate"].ToString();
            this.txtUser.Text = dtTicket.Rows[0]["User"].ToString();
            //this.txtStartTime.Text = dtTicket.Rows[0]["StartTime"].ToString();
            //this.txtEndTime.Text = dtTicket.Rows[0]["EndTime"].ToString();
            this.txtTicketName.Text = dtTicket.Rows[0]["Name"].ToString();
            this.txtTaskName.Text = dtTicket.Rows[0]["Task"].ToString();
            this.txtOperationDate.Properties.MinValue = DateTime.Now;

            this.gridSteps.DataSource = dtGridView;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTicketName.Text))
            {
                XtraMessageBox.Show("请输入操作票名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtTaskName.Text))
            {
                XtraMessageBox.Show("请输入操作任务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtNo.Text))
            {
                XtraMessageBox.Show("请输入操作编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtOperationDate.Text))
            {
                XtraMessageBox.Show("请选择操作日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtUser.Text))
            {
                XtraMessageBox.Show("请输入开票人!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.viewRoom.RowCount < 1)
            {
                XtraMessageBox.Show("请录入操作步骤!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtStartTime.Text == "00:00:00")
            {
                if (XtraMessageBox.Show(string.Format("确认操作开始时间为 00:00:00 吗?"), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return;
                }
            }
            if (this.txtEndTime.Text == "00:00:00")
            {
                if (XtraMessageBox.Show(string.Format("确认操作结束时间为 00:00:00 吗?"), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return;
                }
            }
            System.Globalization.DateTimeFormatInfo dtForm = new System.Globalization.DateTimeFormatInfo();
            dtForm.ShortTimePattern = "HH:mm:ss";
            if (Convert.ToDateTime(this.txtStartTime.Text, dtForm) > Convert.ToDateTime(this.txtEndTime.Text, dtForm))
            {
                if (XtraMessageBox.Show(string.Format("确认操作开始时间大于结束时间吗?"), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return;
                }
            }
            if (this.txtStartTime.Text == this.txtEndTime.Text)
            {
                XtraMessageBox.Show("操作开始时间和结束时间相同!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dictionary<string, string> dicTicket = new Dictionary<string, string>();
            dicTicket["No"] = this.txtNo.Text; ;
            dicTicket["Name"] = this.txtTicketName.Text;
            dicTicket["Task"] = this.txtTaskName.Text;
            dicTicket["OperationDate"] = this.txtOperationDate.Text;
            dicTicket["User"] = this.txtUser.Text;
            dicTicket["StartTime"] = this.txtStartTime.Text;
            dicTicket["EndTime"] = this.txtEndTime.Text;
            dicTicket["CreateTime"] = this.txtCreateTime.Text;
            dicTicket["CreateComment"] = this.txtCreateComment.Text;

            for (int i = 0; i < dtGridView.Rows.Count; i++)
            {
                dtGridView.Rows[i]["CreateTime"] = this.txtCreateTime.Text;
            }

            if (!sqlTool.AddTicket(dicTicket))
            {
                XtraMessageBox.Show("新增操作票失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!sqlTool.AddOperations(dtGridView))
            {
                XtraMessageBox.Show("新增操作票步骤失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void viewRoom_DoubleClick(object sender, EventArgs e)
        {
            this.gridCol_OperationDescription.OptionsColumn.AllowEdit = true;
            this.gridCol_OperationCommand.OptionsColumn.AllowEdit = true;
        }

        private void viewRoom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.gridCol_OperationDescription.OptionsColumn.AllowEdit = false;
            this.gridCol_OperationCommand.OptionsColumn.AllowEdit = false;
            LoadStepImage();
        }

        private void cmsRemove_Click(object sender, EventArgs e)
        {
            string focusedStepNo = this.viewRoom.GetFocusedRowCellValue("StepNo").ToString();
            DataRow[] foundRow = dtGridView.Select(string.Format("StepNo={0}", focusedStepNo));
            foreach (DataRow row in foundRow)
            {
                dtGridView.Rows.Remove(row);
            }
            for (int i = 0; i < dtGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtGridView.Rows[i]["StepNo"]) != i + 1)
                {
                    dtGridView.Rows[i]["StepNo"] = i + 1;
                }

            }
            this.gridSteps.DataSource = dtGridView;

        }

        private void cmsAddNext_Click(object sender, EventArgs e)
        {
            string focusedStepNo = this.viewRoom.GetFocusedRowCellValue("StepNo").ToString();
            int stepNo = Convert.ToInt32(focusedStepNo);
            DataRow insertRow = dtGridView.NewRow();
            insertRow["StepNo"] = stepNo;
            dtGridView.Rows.InsertAt(insertRow, stepNo);
            for (int i = 0; i < dtGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtGridView.Rows[i]["StepNo"]) != i + 1)
                {
                    dtGridView.Rows[i]["StepNo"] = i + 1;
                }

            }
            this.gridSteps.DataSource = dtGridView;
        }

        private void cmsAddPre_Click(object sender, EventArgs e)
        {
            string focusedStepNo = this.viewRoom.GetFocusedRowCellValue("StepNo").ToString();
            int stepNo = Convert.ToInt32(focusedStepNo);
            DataRow insertRow = dtGridView.NewRow();
            insertRow["StepNo"] = stepNo;
            dtGridView.Rows.InsertAt(insertRow, stepNo - 1);
            for (int i = 0; i < dtGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtGridView.Rows[i]["StepNo"]) != i + 1)
                {
                    dtGridView.Rows[i]["StepNo"] = i + 1;
                }

            }
            this.gridSteps.DataSource = dtGridView;
        }

        private void btnCircuitSimulation_Click(object sender, EventArgs e)
        {
            currentStep = Convert.ToInt32(this.viewRoom.GetFocusedRowCellValue("StepNo"));
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp";

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                imagePathCircuit = dlg.FileName;
            }
            try
            {
                this.picCircuitSimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(imagePathCircuit)));

            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imagePathCircuit = string.Empty;
                return;
            }

            string sqlImageCircuitPath = string.Empty;
            string sqlImageCapacityPath = string.Empty;
            if (!string.IsNullOrEmpty(imagePathCapacity) | !string.IsNullOrEmpty(imagePathCircuit))
            {
                CopyImage(ref sqlImageCircuitPath, ref sqlImageCapacityPath);
            }

            dtGridView.Rows[currentStep - 1]["ImagePathCircuit"] = sqlImageCircuitPath;

        }

        private void btnCapacitySimulation_Click(object sender, EventArgs e)
        {
            currentStep = Convert.ToInt32(this.viewRoom.GetFocusedRowCellValue("StepNo"));
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp";

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                imagePathCapacity = dlg.FileName;
            }
            try
            {
                this.picCapacitySimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(imagePathCapacity)));
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imagePathCapacity = string.Empty;
                return;
            }

            string sqlImageCircuitPath = string.Empty;
            string sqlImageCapacityPath = string.Empty;
            if (!string.IsNullOrEmpty(imagePathCapacity) | !string.IsNullOrEmpty(imagePathCircuit))
            {
                CopyImage(ref sqlImageCircuitPath, ref sqlImageCapacityPath);
            }
            dtGridView.Rows[currentStep - 1]["ImagePathCapacity"] = sqlImageCapacityPath;

        }

        private void CopyImage(ref  string outImageCircuitPath, ref  string outImageCapacityPath)
        {
            string destDirectory = string.Format(@"{0}\Image", System.Windows.Forms.Application.StartupPath);
            string circuitImageName = CreatImageFileName("circuit");
            string capacityImageName = CreatImageFileName("capacity");

            if (!System.IO.Directory.Exists(destDirectory))
            {
                System.IO.Directory.CreateDirectory(destDirectory);
            }
            if (imagePathCircuit != string.Empty)
            {
                File.Copy(imagePathCircuit, Path.Combine(destDirectory, circuitImageName), true);
                outImageCircuitPath = string.Format(@"\Image\{0}", circuitImageName);
            }
            if (imagePathCapacity != string.Empty)
            {
                File.Copy(imagePathCapacity, Path.Combine(destDirectory, capacityImageName), true);
                outImageCapacityPath = string.Format(@"\Image\{0}", capacityImageName);
            }
        }

        private string CreatImageFileName(string simulationTypeName)
        {
            string invalidFileName = string.Format(@"{0}_{1}_{2}", this.txtCreateTime.Text.ToString(), currentStep.ToString(), simulationTypeName);
            StringBuilder rBuilder = new StringBuilder(invalidFileName);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
            {
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            }
            string validFileName = string.Format("{0}.png", rBuilder.ToString());
            return validFileName;
        }
        private void LoadStepImage()
        {
            int stepNo = Convert.ToInt32(this.viewRoom.GetFocusedRowCellValue("StepNo"));
            InitImageNext();
            string stepImagePathCircuit = dtGridView.Rows[stepNo - 1]["ImagePathCircuit"].ToString();
            string stepImagePathCapacity = dtGridView.Rows[stepNo - 1]["ImagePathCapacity"].ToString();
            try
            {
                if (stepImagePathCircuit != string.Empty)
                {
                    this.picCircuitSimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes
                        (Application.StartupPath + stepImagePathCircuit)));
                }
                if (stepImagePathCapacity != string.Empty)
                {
                    this.picCapacitySimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes
                        (Application.StartupPath + stepImagePathCapacity)));
                }
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
            }
        }
        private void InitImageNext()
        {
            this.picCapacitySimulation.Image = null;
            imagePathCapacity = string.Empty;
            this.picCircuitSimulation.Image = null;
            imagePathCircuit = string.Empty;
        }
    }
}