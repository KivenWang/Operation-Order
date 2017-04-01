using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SQLite;
using System.Globalization;
using log4net;

namespace OperationTickets
{
    public partial class NewTicketForm : DevExpress.XtraEditors.XtraForm
    {
        private int currentStep = 1;
        private string imagePathCapacity = string.Empty;
        private string imagePathCircuit = string.Empty;
        private DataTable dtGridView = new DataTable();
        private Dictionary<int, string> circuitAbsolutePath = new Dictionary<int, string>();
        private Dictionary<int, string> capacityAbsolutePath = new Dictionary<int, string>();
        private static readonly ILog logger = LogManager.GetLogger("Main");
        private  SqlTool sqlTool = new SqlTool();

        public NewTicketForm()
        {
            InitializeComponent();
            InitDtGridView();
            RefreshGridView();
        }
        private void RefreshGridView()
        {
            this.gridSteps.DataSource = dtGridView;
        }

        private void InitDtGridView()
        {
            int quantity=0;
            dtGridView = sqlTool.GetInitData("OperationSteps", quantity);
            
        }

        private void NewTicketForm_Load(object sender, EventArgs e)
        {
            this.txtCreateTime.Text = DateTime.Now.ToString();
            this.txtOperationDate.Properties.MinValue = DateTime.Now;
        }

        private void btnTicketNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTicketName.Text))
            {
                XtraMessageBox.Show("请输入操作票名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtTaskName .Text))
            {
                XtraMessageBox.Show("请输入操作任务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtNo .Text))
            {
                XtraMessageBox.Show("请输入操作编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            if (string.IsNullOrEmpty(this.txtOperationDate.Text))
            {
                XtraMessageBox.Show("请选择操作日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.txtUser .Text))
            {
                XtraMessageBox.Show("请输入开票人!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DateTimeFormatInfo dtForm = new DateTimeFormatInfo();
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

            pnlTicket.Visible = false;
            pnlOperation.Visible = true;
            TxtEditEnable(false );
            InitTxtNext();
        }

        private void TxtEditEnable(bool editEnable)
        {
            foreach (DevExpress.XtraLayout.LayoutControlItem item in layoutControlGroupTicket.Items)
            {
                if (item.Control != null)
                {
                    if (item.Control.GetType() == typeof(TextEdit) | item.Control.GetType() == typeof(DateEdit ) | item.Control.GetType() == typeof(TimeEdit ))
                    {
                        if (editEnable)
                        {
                            (item.Control as TextEdit).Properties.ReadOnly = false;
                        }
                        else
                        {
                            (item.Control as TextEdit).Properties.ReadOnly = true  ;
                        }
                    }
                }
            }
        }
        private void btnPre_Click(object sender, EventArgs e)
        {            
            if (currentStep == 1)
            {
                pnlOperation.Visible = false;
                pnlTicket.Visible = true;
                this.Text = "创建操作票";
                TxtEditEnable(true);
            }
            else
            {
                currentStep--;
                InitTxtPre();
                LoadStepImage();
                RemoveAbsolutePath();
                dtGridView.Rows.RemoveAt(dtGridView.Rows.Count - 1);
            }
        }

        private void btnOperationNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (this .memoEditStep .Text))
            {
                XtraMessageBox.Show("请输入操作步骤内容!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddAbsolutePath();
            string sqlImageCircuitPath = string.Empty ;
            string sqlImageCapacityPath=string .Empty ;
            if (!string.IsNullOrEmpty(imagePathCapacity) | !string.IsNullOrEmpty(imagePathCircuit))
            {
                CopyImage(ref sqlImageCircuitPath, ref sqlImageCapacityPath);
            }

            AddCurrentOperation(currentStep, this.memoEditStep.Text, this.memoEditComment.Text, sqlImageCircuitPath, sqlImageCapacityPath);
            currentStep++;
            InitTxtNext();
            InitImageNext();
        }

        private void btnCircuitSimulation_Click(object sender, EventArgs e)
        {
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
        }

        private void btnCapacitySimulation_Click(object sender, EventArgs e)
        {
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
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
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

        private void AddCurrentOperation(int currentStep, string currentOperationInfo, string currentOperationComment,
            string currentCircuitSimulationPath, string currentCapacitySimulationPath)
        {
            try
            {
                DataRow dtGridViewRow = dtGridView.NewRow();
                dtGridViewRow["CreateTime"] = this.txtCreateTime.Text;
                dtGridViewRow["StepNo"] = currentStep;
                dtGridViewRow["Operation"] = currentOperationInfo;
                dtGridViewRow["Comment"] = currentOperationComment;
                dtGridViewRow["ImagePathCircuit"] = currentCircuitSimulationPath;
                dtGridViewRow["ImagePathCapacity"] = currentCapacitySimulationPath;
                dtGridView.Rows.Add(dtGridViewRow);
                RefreshGridView();
            }
            catch (Exception ex)
            {
                logger.Error("向gridview添加数据失败:", ex);
            }
        }

        private void AddAbsolutePath()
        {
            circuitAbsolutePath.Add(currentStep, imagePathCircuit);
            capacityAbsolutePath.Add(currentStep, imagePathCapacity);
        }

        private void RemoveAbsolutePath()
        {
            circuitAbsolutePath.Remove(currentStep);
            capacityAbsolutePath.Remove(currentStep);
        }

        private void InitTxtNext()
        {

            this.layoutControlItemStep.Text = "  顺 序 " + currentStep.ToString() + " :";
            this.Text = "添加操作-" + currentStep.ToString();
            this.memoEditStep.Text = string.Empty;
            this.memoEditComment.Text = string.Empty;
        }

        private void InitTxtPre()
        {

            this.layoutControlItemStep.Text = "  顺 序 " + currentStep.ToString() + " :";
            this.Text = "添加操作-" + currentStep.ToString();
            this.memoEditStep.Text = dtGridView.Rows[dtGridView.Rows.Count - 1]["Operation"].ToString();
            this.memoEditComment.Text = dtGridView.Rows[dtGridView.Rows.Count - 1]["Comment"].ToString();

        }
        private void InitImageNext()
        {
            this.picCapacitySimulation.Image = null;
            imagePathCapacity = string.Empty;
            this.picCircuitSimulation.Image = null;
            imagePathCircuit = string.Empty;
        }

        private void LoadStepImage()
        {
            InitImageNext();
            string stepImagePathCircuit = circuitAbsolutePath[currentStep];
            string stepImagePathCapacity = capacityAbsolutePath[currentStep];
            try
            {
                if (stepImagePathCircuit != string.Empty)
                {
                    this.picCircuitSimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(stepImagePathCircuit)));
                    imagePathCircuit = stepImagePathCircuit;
                }
                if (stepImagePathCapacity != string.Empty)
                {
                    this.picCapacitySimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(stepImagePathCapacity)));
                    imagePathCapacity = stepImagePathCapacity;
                }
            }
            catch (Exception ex)
            {
                logger.Info("图片加载失败", ex);
            }
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

        
    }
}