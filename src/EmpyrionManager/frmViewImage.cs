using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpyrionManager
{
    public partial class frmViewImage : Form
    {
        public Image DisplayImage { get; set; }

        public string Title { get; set; }

        public frmViewImage()
        {
            InitializeComponent();
        }

        private void frmViewImage_Resize(object sender, EventArgs e)
        {
            pnlImage.Left = 0;
            pnlImage.Top = 0;
            pnlImage.Width = this.Width - 16;
            pnlImage.Height = this.Height - 48;
            pbImage.Left = 0;
            pbImage.Top = 0;
            pbImage.Width = pnlImage.Width;
            pbImage.Height = pnlImage.Height;
        }

        private void frmViewImage_Load(object sender, EventArgs e)
        {
            this.Text = this.Title;
            pbImage.SizeMode = PictureBoxSizeMode.AutoSize;
            pbImage.Image = this.DisplayImage;
            this.frmViewImage_Resize(sender, e);
        }
    }
}
