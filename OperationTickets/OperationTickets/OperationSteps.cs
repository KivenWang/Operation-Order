using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace OperationTickets
{
    public partial class OperationSteps : DevExpress.XtraEditors.XtraUserControl
    {
        private string _imagePathCircuit;
        public string ImagePathCircuit
        {
            get
            {
                return _imagePathCircuit;
            }
        }
        private string _imagePathCapacity;
        public string ImagePathCapacity
        {
            get
            {
                return _imagePathCapacity;
            }
        }
        public OperationSteps()
        {
            InitializeComponent();
        }


        private void btnCapacitySimulation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PNG|*.png|GIF|*.gif|JPG|*.jpg|BMP|*.bmp";

            if (dlg.ShowDialog() != DialogResult.OK) return;
            _imagePathCapacity = dlg.FileName;
            dlg.Dispose();

            try
            {
                this.picCapacitySimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(_imagePathCapacity)));
            }
            catch
            {
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _imagePathCapacity = null;
                return;
            }
        }

        private void btnCircuitSimulation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PNG|*.png|GIF|*.gif|JPG|*.jpg|BMP|*.bmp";

            if (dlg.ShowDialog() != DialogResult.OK) return;
            _imagePathCircuit = dlg.FileName;
            dlg.Dispose();

            try
            {
                this.picLineSimulation.Image = Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(_imagePathCircuit)));
            }
            catch
            {
                XtraMessageBox.Show("图片不存在或无法识别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _imagePathCircuit = null;
                return;
            }
        }
    }
}
