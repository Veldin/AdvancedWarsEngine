namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;
        protected IProduceBehavior produceBehavior;

        private int productionCooldown;
        private int productionCooldownMax;


        public Structure(float width, float height, float fromTop, float fromLeft, string sprite, IProduceBehavior produceBehavior)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            productionCooldownMax = 8;
            productionCooldown = productionCooldownMax;
            capturePoints = 1;
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

        public string GetProduced()
        {
            if (produceBehavior == null)
            {
                return null;
            }
            return produceBehavior.Produce();
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

        public int ProductionCooldownMax
        {
            get { return productionCooldownMax; }
            set {
                if (productionCooldown > value) {
                    productionCooldown = value; };
                productionCooldownMax = value;
            }
        }
    }
}
