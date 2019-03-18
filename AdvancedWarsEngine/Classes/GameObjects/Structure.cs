using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedWarsEngine.Classes;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;
        protected IProduceBehavior produceBehavior;

        public Structure(float width, float height, float fromTop, float fromLeft, string sprite, IProduceBehavior produceBehavior)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            capturePoints = 100;
            this.produceBehavior = produceBehavior;
        }

        public float CapturePoints
        {
            get { return capturePoints; }
        }

        public IProduceBehavior ProduceBehavior
        {
            get { return produceBehavior; }
        }

        public void AddCapturePoints(float value)
        {
            capturePoints += value;
        }
        
        public void DecreaseCapturePoints(float value)
        {
            // Decrease the capturePoints by the given value
            capturePoints -= value;

            // If capturePoints is equal or smaller than 0, set destroy true
            if (capturePoints <= 0)
            {
                destroyed = true;
            }
        }
    }
}
