namespace EmpyrionManager.Extensions
{
    using System.Drawing;

    public static class SizeExtensions
    {
        public static Rectangle ToRectangle(this Size size)
        {
            var result = new Rectangle();
            result.X = 0;
            result.Y = 0;
            result.Width = size.Width;
            result.Height = size.Height;

            return result;
        }
    }
}
