using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace OperationTickets
{
    public partial class AddOperationTicket : DevExpress.XtraEditors.XtraForm
    {
        private int currentStep = 0;
        public AddOperationTicket()
        {
            InitializeComponent();
            LoadTableControl();
            visibleButton(currentStep);
        }
        private void LoadTableControl()
        {
            CreateTicket createTicket=new CreateTicket ();
            createTicket.Dock = DockStyle.Fill;
            XtraTabPage tabPageCreate = new XtraTabPage();
            
            tabPageCreate.Controls.Add(createTicket);
            this.tableControlMain.TabPages.Add(tabPageCreate);

            OperationSteps operationSteps = new OperationSteps();
            operationSteps.Dock = DockStyle.Fill;
            XtraTabPage tabPageSteps = new XtraTabPage();
            tabPageSteps.Controls.Add(operationSteps);
            this.tableControlMain.TabPages.Add(tabPageSteps);
            

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentStep == 0)
            {
                this.tableControlMain.SelectedTabPageIndex=1;

            }
            //SaveStepImage();
            //AddCurrentOperation(m_CurrentStep, this.memoEditStep.Text, this.memoEditRemark.Text, this.imageCircuitPath, this.imageCapacityPath);
            //m_CurrentStep += 1;
            //this.layoutControlItemStep.Text = "  顺 序 " + m_CurrentStep.ToString() + " :";
            //this.Text = "添加操作-" + m_CurrentStep.ToString();
            //this.memoEditStep.Text = "";
            //this.memoEditRemark.Text = "";

            currentStep++;
            visibleButton(currentStep);
        }
        private void AddStepImage()
        {

        }
        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentStep == 1)
            {
                this.tableControlMain.SelectedTabPageIndex = 0;

            }
            this.tableControlMain.SelectedTabPageIndex = 0;
            currentStep--;
            visibleButton(currentStep);

        }

        private void visibleButton(int step)
        {
            switch (step)
            {
                case 0:
                    this.btnPre.Visible = false;
                    break ;
                default :
                    this.btnPre.Visible = true;
                    
                    break;
            }
        }
    }
}