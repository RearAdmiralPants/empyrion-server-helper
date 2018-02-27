namespace EmpyrionManager.Graphics
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Drawing.Drawing2D;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConsoleBitmap
    {
        public Font OutputFont { get; set; }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }

        public Image GenerateConsoleOutputImage(string text)
        {
            if (SanityTestPropertiesFails()) { return null; }

            var extent = this.GetExtentForText(text, this.OutputFont);
            var output = new Bitmap(extent.Width, extent.Height);
            using (var gfx = Graphics.FromImage(output))
            {

                var backBrush = new SolidBrush(this.BackColor);

                gfx.FillRectangle(backBrush, extent);

                var foreBrush = new SolidBrush(this.ForeColor);

                gfx.DrawString(text, this.OutputFont, foreBrush, extent);
            }

            return output;
        }

        public static Image Resample(Image input, Rectangle destSize)
        {
            Bitmap output = new Bitmap(destSize.Width, destSize.Height);
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gfx.DrawImage(input, destSize);
            }

            return output;
        }

        private bool SanityTestPropertiesFails()
        {
            return this.OutputFont == null || this.ForeColor == null || this.BackColor == null;
        }

        private Rectangle GetExtentForText(string text, Font font)
        {
            text += "\r\n";
            var img = new Bitmap(10, 10);
            var gfx = Graphics.FromImage(img);

            var lineSize = gfx.MeasureString(text, font);

            var result = new Rectangle();
            result.X = 0;
            result.Y = 0;
            result.Width = Convert.ToInt32(Math.Round(lineSize.Width, 0));
            result.Height = Convert.ToInt32(Math.Round(lineSize.Height, 0));
            return result;
        }
    }
}
