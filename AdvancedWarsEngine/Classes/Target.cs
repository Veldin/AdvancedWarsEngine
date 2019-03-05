using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Target
    {
        private float fromTop;
        private float fromLeft;
        private GameObject gameObject;

        public Target(float fromTop, float fromLeft)
        {
            this.fromTop = fromTop;
            this.fromLeft = fromLeft;
        }

        public Target(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public float GetFromLeft()
        {
            // van de gameobject
            return 0;
        }

        public float GetFromTop()
        {
            // van de gameobject
            return 0;
        }

        public void SetFromTop(float fromTop)
        {
            this.fromTop = fromTop;
        }

        public void SetFromLeft(float fromLeft)
        {
            this.fromLeft = fromLeft;
        }
    }
}
