namespace EmpyrionManager.Inheritors
{
    using System.Drawing;
    using System.Windows.Forms;


    /// <summary>
    /// Attempt to override label class' paint methodology to create an opacity - currently nonfunctional
    /// </summary>
    public class TransparentLabel : Label
    {
        private Control _parent;
        private Color _backColor = Color.Blue;

        public TransparentLabel()
        {
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            //this.Font = new Font("Microsoft Sans Serif", 9);
            this.HandleCreated += TransparentLabel_HandleCreated;
            this.TransparentLabel_HandleCreated(null, null);
        }

        private void TransparentLabel_HandleCreated(object sender, System.EventArgs e)
        {
            _parent = this.Parent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        private int _opacity = 0;
        public int Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (value >= 0 && value <= 255) { _opacity = value; }
                this.Invalidate();
            }
        }

        public override Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bmp = new Bitmap(this.Width, this.Height);
            var gfx = Graphics.FromImage(bmp);

            var backColor = _backColor;
            if (backColor == Color.Transparent)
            {
                if (_parent != null)
                {
                    backColor = _parent.BackColor;
                }
            }

            var paintEventArgs = new PaintEventArgs(gfx, e.ClipRectangle);
            
            base.OnPaint(paintEventArgs);

            for (var y = 0; y < this.Height; y++)
            {
                for (var x = 0; x < this.Width; x++)
                {
                    var pColor = bmp.GetPixel(x, y);
                    if (pColor.A != 0)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(_opacity, pColor));
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(255, pColor));
                    }
                        
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }

    }
}
