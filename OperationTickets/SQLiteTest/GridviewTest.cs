using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SQLiteTest
{
    public partial class GridviewTest : DevExpress.XtraEditors.XtraForm
    {
        public GridviewTest()
        {
            InitializeComponent();
        }
        private void Test()
        {
            //tabcontrol最大化,去掉tab标签
            //this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabPage1.Width, this.tabPage1.Height));
            
        }
    }
}