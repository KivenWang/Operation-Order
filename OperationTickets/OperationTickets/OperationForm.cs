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

namespace OperationTickets
{
    public partial class OperationForm : DevExpress.XtraEditors.XtraForm
    {
        private int currentStep = 1;
        private string imagePathCapacity = string.Empty;
        private string imagePathCircuit = string.Empty;
        private DataTable dtGridView = new DataTable();
        private TicketForm createTicket;
        private Dictionary<int, string> circuitAbsolutePath = new Dictionary<int, string>();
        private Dictionary<int, string> capacityAbsolutePath = new Dictionary<int, string>();
        public OperationForm(TicketForm addTicket)
        {
            createTicket = addTicket;
            InitializeComponent();
            InitCreateInfo();
            InitDtGridView();
            RefreshGridView();
        }
        private void InitCreateInfo()
        {
            this.txtCreateTime.Text = createTicket.CreateTime;
            this.txtEndtime.Text = createTicket.EndTime;
            this.txtNo.Text = createTicket.TicketNo;
            this.txtOperationDate.Text = createTicket.OperationDate;
            this.txtOperationUser.Text = createTicket.User;
            this.txtStarttime.Text = createTicket.StartTime;
            this.txtEndtime.Text = createTicket.EndTime;
            this.txtTicketName.Text = createTicket.TicketName;
            this.txtTaskName.Text = createTicket.TicketTask;

        }
        private void RefreshGridView()
        {
            this.gridSteps.DataSource = dtGridView;

        }

        private void InitDtGridView()
        {
            SQLiteDBHelper sqliteHelper = new SQLiteDBHelper();
            try
            {
                string sqlSelectTickets = @"SELECT CreateTime,StepNo,Operation,Comment,ImagePathCircuit,ImagePathCapacity FROM OperationSteps";
                dtGridView = sqliteHelper.ExecuteDataTable(sqlSelectTickets, null);
                dtGridView.Clear();
            }
            catch (Exception ex)
            {

            }

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentStep == 1)
            {
                createTicket.Show();
                this.Hide();
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            AddAbsolutePath();
            string sqlImageCircuitPath;
            string sqlImageCapacityPath;
            CopyImage(out sqlImageCircuitPath, out sqlImageCapacityPath);

            AddCurrentOperation(currentStep, this.memoEditStep.Text, this.memoEditComment.Text, sqlImageCircuitPath, sqlImageCapacityPath);
            currentStep++;
            InitTxtNext();
            InitImageNext();
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

        private void btnComplete_Click(object sender, EventArgs e)
        {
            SQLiteDBHelper sqliteHelper = new SQLiteDBHelper();

            string sqlInsertTickets = string.Format(@"INSERT INTO Tickets (No,Name,Task,OperationDate,User,StartTime,EndTime,CreateTime,CreateComment) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            this.txtNo.Text, this.txtTicketName.Text, this.txtTaskName.Text, this.txtOperationDate.Text, this.txtOperationUser.Text,
                            this.txtStarttime.Text, this.txtEndtime.Text, this.txtCreateTime.Text, createTicket.CreateComment);
            string sqlInsertOperationSteps = string.Format("INSERT INTO OperationSteps (CreateTime,StepNo,Operation,Comment,ImagePathCircuit,ImagePathCapacity) VALUES (@CreateTime,@StepNo,@Operation,@Comment,@ImagePathCircuit,@ImagePathCapacity)");
            try
            {
                sqliteHelper.ExecuteNonQuery(sqlInsertTickets);

                SQLiteParameter[] parameters = { new SQLiteParameter("@CreateTime"),
                                           new SQLiteParameter("@StepNo"),
                                           new SQLiteParameter("@Operation"),
                                           new SQLiteParameter("@Comment"),
                                           new SQLiteParameter("@ImagePathCircuit"),
                                           new SQLiteParameter("@ImagePathCapacity"),};

                foreach (DataRow row in dtGridView.Rows)
                {
                    parameters[0].Value = row["CreateTime"];
                    parameters[1].Value = row["StepNo"];
                    parameters[2].Value = row["Operation"];
                    parameters[3].Value = row["Comment"];
                    parameters[4].Value = row["ImagePathCircuit"];
                    parameters[5].Value = row["ImagePathCapacity"];
                    sqliteHelper.ExecuteNonQuery(sqlInsertOperationSteps, parameters);
                }
                PublicStaticMember.OperationTicketsMain.RefreshUI();
                this.Close();
                PublicStaticMember.OperationTicketsMain.Show();
                createTicket.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("新增操作票失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch
            {
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imagePathCapacity = string.Empty;
                return;
            }
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
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imagePathCircuit = string.Empty;
                return;
            }
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
            catch
            {
                XtraMessageBox.Show("图片加载失败,请确保图片没被移动或删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddCurrentOperation(int currentStep, string currentOperationInfo, string currentOperationComment, string currentCircuitSimulationPath, string currentCapacitySimulationPath)
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

        private void CopyImage(out string outImageCircuitPath, out string outImageCapacityPath)
        {
            outImageCircuitPath = string.Empty;
            outImageCapacityPath = string.Empty;
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