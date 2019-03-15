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
    }
}
