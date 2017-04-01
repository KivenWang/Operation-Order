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
    public partial class TicketStep2 : DevExpress.XtraEditors.XtraForm
    {
        TicketStep addTickStepPre = null;
        public TicketStep2()
        {
            InitializeComponent();
        }
        public TicketStep2(TicketStep addTickStep1)
        {
            addTickStepPre = addTickStep1;
            InitializeComponent();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            addTickStepPre.Show();
            this.Hide();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TicketStep3 addTickStep3 = new TicketStep3(this );
            addTickStep3.Show();
            this.Hide();
        }
    }
}