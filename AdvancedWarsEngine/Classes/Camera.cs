using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Camera
    {
        private float fromTop;
        private float fromLeft;

        private float currentMapHeight;
        private float currentMapWidth;

        private float bottomLine;
        private float rightLine;

        public Camera(int mapHeight, int mapWidth)
        {
            fromTop = (float)-56.25;
            fromLeft = -90;

            currentMapHeight = mapHeight * 16;
            currentMapWidth = mapWidth * 16;

            bottomLine = currentMapHeight - 360 - (float)56.25;
            rightLine = currentMapHeight - 360 - (float)56.25;
        }

        public float GetLeftOffSet()
        {
            return fromLeft + (180 / 2);
        }

        public float GetTopOffSet()
        {
            return fromTop + ((float)112.5 / 2);
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

            if (GetLeftOffSet() >= 0)
            {
                this.fromLeft = -90;
            }
        }

        public void AddFromTop(float fromTop)
        {
            this.fromTop += fromTop;

            Debug.WriteLine(this.fromTop);
            Debug.WriteLine(GetTopOffSet());
            Debug.WriteLine(bottomLine);
            Debug.WriteLine("");

            if (GetTopOffSet() >= 0)
            {
                this.fromTop = (float)-56.25;
            }

            if (GetTopOffSet() < bottomLine)
            {
                this.fromTop = bottomLine - (float)72.25;
            }
        }
    }
}
