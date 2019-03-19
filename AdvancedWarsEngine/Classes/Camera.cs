using System.Diagnostics;

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

            bottomLine = -currentMapHeight + 90;
            rightLine = 306 - currentMapWidth + (float)56.25;
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

            if (GetLeftOffSet() <= rightLine)
            {
                this.fromLeft = rightLine - 90;
            }
        }

        public void AddFromTop(float fromTop)
        {
            this.fromTop += fromTop;

            if (GetTopOffSet() >= 0)
            {
                this.fromTop = (float)-56.25;
            }

            if (GetTopOffSet() <= bottomLine + 112.5)
            {
                this.fromTop = bottomLine + (float)56.25;
            }
        }

        public void MoveTo(GameObject gameObject)
        {
            if (gameObject.FromLeft >= 0 && gameObject.FromLeft <= currentMapWidth)
            {
                float horizontalDifference = (gameObject.FromLeft + 8 - 90) * -1 - fromLeft;
                AddFromLeft(horizontalDifference);
            }
            if (gameObject.FromTop >= 0 && gameObject.FromTop <= currentMapHeight)
            {
                float verticalDifference = (gameObject.FromTop + 8 - (float)56.25) * -1 - fromTop;
                AddFromTop(verticalDifference);
            }
        }
    }
}
