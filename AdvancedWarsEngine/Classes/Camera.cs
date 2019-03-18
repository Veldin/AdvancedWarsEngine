namespace AdvancedWarsEngine.Classes
{
    class Camera
    {
        private float fromTop;
        private float fromLeft;

        public Camera()
        {
            fromTop = 0;
            fromLeft = 0;
        }

        public Camera(float fromTop, float fromLeft)
        {
            this.fromTop = fromTop;
            this.fromLeft = fromLeft;
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
        }

        public void AddFromTop(float fromTop)
        {
            this.fromTop += fromTop;
        }
    }
}
