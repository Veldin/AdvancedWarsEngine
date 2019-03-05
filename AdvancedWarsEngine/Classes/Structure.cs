using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        private float capturePoints;

        public Structure(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft)
        {
            // DO SOMETHING
        }

        public float GetCapturePoints()
        {
            return capturePoints;
        }

        public void SetCapturePoints(float capturePoints)
        {
            this.capturePoints = capturePoints;
        }
    }
}
