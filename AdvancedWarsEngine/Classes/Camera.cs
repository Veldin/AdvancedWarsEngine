﻿using System;
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

        public float GetLeftOffSet()
        {
            return 0;
        }

        public float GetTopOffSet()
        {
            return 0;
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

        public void SetFromTop(float fromTop)
        {
            this.fromTop = fromTop;
        }
    }
}
