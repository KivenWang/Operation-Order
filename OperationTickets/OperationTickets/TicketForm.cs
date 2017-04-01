using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace OperationTickets
{
    public partial class TicketForm : DevExpress.XtraEditors.XtraForm
    {
        #region  字段,只读属性
        private string _ticketName;
        public string TicketName
        {
            get { return _ticketName; }
        }

        private string _ticketTask;
        public string TicketTask
        {
            get { return _ticketTask; }
        }
        private string _ticketNo;
        public string TicketNo
        {
            get { return _ticketNo; }
        }
        private string _operationDate;
        public string OperationDate
        {
            get { return _operationDate; }
        }

        private string _user;
        public string User
        {
            get { return _user; }
        }

        private string _startTime;
        public string StartTime
        {
            get { return _startTime; }
        }

        private string _endTime;
        public string EndTime
        {
            get { return _endTime; }
        }
        private string _createTime;
        public string CreateTime
        {
            get { return _createTime; }
        }

        private string _createComment;
        public string CreateComment
        {
            get { return _createComment; }
        }
        #endregion

        public TicketForm()
        {
            InitializeComponent();
        }

        private void AddTicket_Load(object sender, EventArgs e)
        {
            this.txtCreateTime.Text = DateTime.Now.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetCreateInfo();
            OperationForm addTicketStep = new OperationForm(this);
            addTicketStep.Show();
            this.Hide();            
            
        }
        private void GetCreateInfo()
        {
            _ticketName = this.txtTicketName.Text;
            _ticketTask = this.txtTaskName.Text;
            _ticketNo = this.txtNo.Text;
            _user = this.txtUser.Text;
            _startTime = this.txtStartTime.Text;
            _endTime = this.txtEndtime.Text;
            _createComment = this.txtCreatComment.Text;
            _createTime = this.txtCreateTime.Text;
            _operationDate = this.txtOperationDate.Text;
        }
    }
}