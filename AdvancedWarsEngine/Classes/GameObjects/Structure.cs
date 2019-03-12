using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedWarsEngine.Classes.Behaviors;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;
        private ICapturePointsBehavior capturePointsBehavior;

        public Structure(float width, float height, float fromTop, float fromLeft, BitmapImage sprite)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            // DO SOMETHING
        }

        public float CapturePoints
        {
            get { return capturePoints; }
            set { capturePoints = value; }
        }

        public void AddCapturePoints(float value)
        {
            capturePoints += value;
        }

        public void Capture()
        {
            capturePointsBehavior.CapturePoints(this);
        }
    }
}
