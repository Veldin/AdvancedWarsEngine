namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;
        protected IProduceBehavior produceBehavior;

        private int productionCooldown; 

        public Structure(float width, float height, float fromTop, float fromLeft, string sprite, IProduceBehavior produceBehavior)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            productionCooldown = 5;
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
            // Decrease the capturePoints by the given value
            capturePoints += value;

            // If capturePoints is equal or smaller than 0, set destroy true
            if (capturePoints <= 0)
            {
                destroyed = true;
            }
        }

        public int ProductionCooldown
        {
            get { return productionCooldown; }
            set { productionCooldown = value; }
        }
    }
}
