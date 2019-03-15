using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Camera
    {
        private float fromTop;
        private float fromLeft;

        public Camera()
        {
            this.fromTop = 0;
            this.fromLeft = 0;
        }

        public Camera(float fromTop, float fromLeft)
        {
            this.fromTop = fromTop;
            this.fromLeft = fromLeft;
        }

        public float GetLeftOffSet()
        {
            return fromLeft + (360 / 2);
        }

        public float GetTopOffSet()
        {
            return fromTop + (225 / 2);
        }

        public float GetFromTop()
        {
            return fromTop;
        }

        public float GetFromLeft()
        {
            return fromLeft;
        }

        public void SetFromLeft(float fromLeft)
        {
            this.fromLeft = fromLeft;
        }

        public void AddFromLeft(float fromLeft)
        {
            this.fromLeft += fromLeft;
        }

        public void AddFromTop(float fromTop)
        {
            this.fromTop += fromTop;
        }
    }
}
