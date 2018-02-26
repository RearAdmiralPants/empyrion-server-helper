namespace EmpyrionManager.Helpers
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using Inheritors;

    public class FadeHelper
    {
        public Control FadingControl { get; set; }

        public Color DestinationColor { get; set; }

        public int Steps { get; set; } = 15;

        public int StepInterval { get; set; } = 75;

        private int currStep = 0;
        private int GreenStep;
        private int RedStep;
        private int BlueStep;

        private Timer SchedulerTimer;

        private Timer FadeTimer;

        public FadeHelper(Control toFade)
        {
            this.FadingControl = toFade;
            this.DestinationColor = toFade.BackColor;
        }

        private void CalculateSteps(Color targetColor)
        {
            this.CalculateRedStep(targetColor);
            this.CalculateGreenStep(targetColor);
            this.CalculateBlueStep(targetColor);
        }

        private void CalculateGreenStep(Color targetColor)
        {
            this.GreenStep = (targetColor.G - FadingControl.ForeColor.G) / this.Steps;
        }

        private void CalculateRedStep(Color targetColor)
        {
            this.RedStep = (targetColor.R - FadingControl.ForeColor.R) / this.Steps;
        }

        private void CalculateBlueStep(Color targetColor)
        {
            this.BlueStep = (targetColor.B - FadingControl.ForeColor.B) / this.Steps;
        }

        public void ScheduleFade(int milsBeforeFade)
        {
            if (!this.FadingControl.Visible)
            {
                return;
            }

            this.CalculateSteps(this.DestinationColor);
            this.SchedulerTimer = new Timer();
            this.SchedulerTimer.Interval = milsBeforeFade;

            this.SchedulerTimer.Tick += SchedulerTimer_Tick;
            this.SchedulerTimer.Start();
        }

        private void SchedulerTimer_Tick(object sender, EventArgs e)
        {
            this.SchedulerTimer.Stop();
            this.FadeTimer = new Timer();
            this.FadeTimer.Interval = this.StepInterval;
            this.FadeTimer.Tick += FadeTimer_Tick;
            this.FadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (currStep >= this.Steps)
            {
                this.FadeTimer.Stop();
                this.FadingControl.ForeColor = this.DestinationColor;
                return;
            }
            var newRed = this.FadingControl.ForeColor.R + this.RedStep;
            var newGreen = this.FadingControl.ForeColor.G + this.GreenStep;
            var newBlue = this.FadingControl.ForeColor.B + this.BlueStep;

            this.FadingControl.ForeColor = Color.FromArgb(newRed, newGreen, newBlue);

            currStep++;
        }

        private static void FadeStepTimer_Tick(object sender, EventArgs e)
        {
            var sendingFadeTimer = (Timer)sender;
            var fadeControl = (TransparentLabel)sendingFadeTimer.Tag;
            //fadeControl.ForeColor = IncreaseColorAlpha(fadeControl.ForeColor, 20);
            //fadeControl.Refresh();
            var newOpacity = fadeControl.Opacity - 20;
            if (newOpacity < 0) { newOpacity = 0; }
            fadeControl.Opacity = newOpacity;
            //if (fadeControl.ForeColor.A == 255)
            if (newOpacity == 0) 
            {
                sendingFadeTimer.Stop();
            }
        }

        private static Color IncreaseColorAlpha(Color baseColor, int alphaStep)
        {
            var newAlpha = baseColor.A + alphaStep;
            if (newAlpha > 255) { newAlpha = 255; }

            return Color.FromArgb(newAlpha, baseColor.R, baseColor.G, baseColor.B);
        }
    }
}
