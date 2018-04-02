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
    using System.Windows.Forms;
    using System.Threading.Tasks;
    using Timers = System.Timers;

    public class FadingPictureBox : PictureBox
    {
        private Timers.Timer FadeDelayTimer = null;
        private Timers.Timer FadeStepTimer = null;
        private double millisecondsPerStep;
        private DateTime targetFadeComplete;
        private double fadeStep = 0.01;
        private double currentOpacity = 1;

        /// <summary>
        /// Gets or sets a value indicating how long the fade should take ideally; the application will try to adjust the number of steps
        /// to achieve this goal. If not set, the fade will take arbitrarily long but be smoother in transition. Setting this property
        /// overrides <see cref="FadeStepSeconds"/>.
        /// </summary>
        public double FadeSeconds { get; set; } = -1;

        private double _fadeStepSeconds = 2d;
        /// <summary>
        /// Gets or sets a value indicating how long to wait between individual steps of an ideal smooth fade. If <see cref="FadeSeconds"/> 
        /// is set, this property may be ignored or adjusted.
        /// </summary>
        public double FadeStepSeconds
        {
            get
            {
                return this._fadeStepSeconds;
            }
            set
            {
                this.OnFadeSecondsChanged(value);
                this._fadeStepSeconds = value;
            }
        }

        private double _fadeDelaySeconds = 5d;
        public double FadeDelaySeconds
        {
            get
            {
                return this._fadeDelaySeconds;
            }
            set
            {
                this.OnFadeDelaySecondsChanged(value);
                this._fadeDelaySeconds = value;
            }
        }

        public bool Finished { get; set; } = false;

        public string Log { get; set; } = "";

        public FadingPictureBox() : base()
        {
            this.InitializeTimers();
        }

        public FadingPictureBox(double fadeStepSeconds, double fadeDelaySeconds)
        {
            this.FadeStepSeconds = fadeStepSeconds;
            this.FadeDelaySeconds = fadeDelaySeconds;
        }

        public void StartFade()
        {
            this.Finished = false;
            this.FadeDelayTimer.Start();
        }
        
        protected void OnFadeSecondsChanged(double newValue)
        {
            if (this.FadeStepTimer != null)
            {
                this.FadeStepTimer.Interval = newValue;
            }
        }

        protected void OnFadeDelaySecondsChanged(double newValue)
        {
            if (this.FadeDelayTimer != null)
            {
                this.FadeDelayTimer.Interval = newValue;
            }
        }

        private void InitializeTimers()
        {
            this.FadeStepTimer = new Timers.Timer(this.FadeStepSeconds * 1000d);
            this.FadeStepTimer.Elapsed += FadeStepTimer_Elapsed;

            this.FadeDelayTimer = new Timers.Timer(this.FadeDelaySeconds * 1000d);
            this.FadeDelayTimer.Interval = this.FadeDelaySeconds * 1000d;
            this.FadeDelayTimer.Stop();
            this.FadeDelayTimer.Elapsed += FadeDelayTimer_Elapsed;
        }

        private void FadeDelayTimer_Elapsed(object sender, Timers.ElapsedEventArgs e)
        {
            this.FadeDelayTimer.Stop();
            this.targetFadeComplete = DateTime.UtcNow.AddSeconds(this.FadeSeconds);
            this.millisecondsPerStep = -1;
            this.FadeStepTimer.Start();
        }

        private void FadeStepTimer_Elapsed(object sender, Timers.ElapsedEventArgs e)
        {
            this.FadeStepTimer.Stop();

            // Time the operation so we know how long one step requires
            var start = DateTime.UtcNow;

            this.FadeImage();
            Application.DoEvents();
            /*
            if (this.millisecondsPerStep == -1)
            {
                var end = DateTime.UtcNow;
                this.millisecondsPerStep = end.Subtract(start).TotalMilliseconds;
                this.LogMessage("New milliseconds per step: " + this.millisecondsPerStep);
                this.fadeStep = this.millisecondsPerStep / this.FadeSeconds * 1000d;
                this.LogMessage("New fade step: " + this.fadeStep);
            }*/

            ////INCOMPLETE: Simply hide the image and implement fade at a later date
            //this.FadeImage();

            if (!this.Finished)
            {
                this.FadeStepTimer.Start();
            }
        }

        private void FadeImage()
        {
            ////TODO: NEEDS WORK
            this.Image = this.CreateFadedImage(0);
            return;

            var newOpacity = this.currentOpacity - this.fadeStep;
            if (newOpacity < 0) { newOpacity = 0; }
            this.LogMessage("Fading to " + newOpacity);
            var targetOpacity = Convert.ToSingle(newOpacity);

            //// INCOMPLETE: See above
            //targetOpacity = 0;

            this.Image = this.CreateFadedImage(targetOpacity);
            this.currentOpacity = targetOpacity;

            if (this.currentOpacity == 0)
            {
                this.Finished = true;
            }
            this.LogMessage("Done.");
        }

        private Image CreateFadedImage(float absoluteOpacity)
        {
            var oldImage = this.Image;
            return ImageTransparency.ChangeOpacity(oldImage, absoluteOpacity);
        }

        private void LogMessage(string log)
        {
            this.Log += "\r\n" + log;
        }
    }
}
