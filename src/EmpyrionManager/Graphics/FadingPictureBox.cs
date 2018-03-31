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
        private double fadeStep = 0.1;
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

        public FadingPictureBox() : base()
        {
            this.InitializeTimers();
            
        }

        public FadingPictureBox(double fadeStepSeconds, double fadeDelaySeconds)
        {
            this.FadeStepSeconds = fadeStepSeconds;
            this.FadeDelaySeconds = fadeDelaySeconds;
        }

        public void Test()
        {
            this.Image = this.CreateFadedImage(0.5f);
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
            this.FadeStepTimer = new Timers.Timer(this.FadeStepSeconds);
            this.FadeStepTimer.Elapsed += FadeStepTimer_Elapsed;

            this.FadeDelayTimer = new Timers.Timer(this.FadeDelaySeconds);
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
            DateTime targetEnd;

            // Time the operation so we know how long one step requires
            var start = DateTime.UtcNow;

            if (this.millisecondsPerStep == -1)
            {
                targetEnd = start.AddSeconds(this.FadeSeconds);
            }

            ////INCOMPLETE: Simply hide the image and implement fade at a later date
            this.FadeImage();
        }

        private void FadeImage()
        {
            var targetOpacity = Convert.ToSingle(this.currentOpacity - this.fadeStep);

            //// INCOMPLETE: See above
            targetOpacity = 0;

            this.Image = this.CreateFadedImage(targetOpacity);
        }

        private Image CreateFadedImage(float absoluteOpacity)
        {
            var oldImage = this.Image;
            return ImageTransparency.ChangeOpacity(oldImage, absoluteOpacity);
        }
    }
}
