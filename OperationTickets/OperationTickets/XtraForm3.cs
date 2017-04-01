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
    public partial class XtraForm3 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm3()
        {
            InitializeComponent();
        }



        private void btnNext_Click_1(object sender, EventArgs e)
        {
            int i = 0;
            string []typeArray=new string [100];
            foreach ( var txtControl in layoutControlGroup1 .Items )
            {
                
                string type = txtControl.GetType().ToString();
                typeArray[i] = type;
                i++;
            }
        }
    }
}