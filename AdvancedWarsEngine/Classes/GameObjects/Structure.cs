namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;                          // Health of the structure
        protected IProduceBehaviour produceBehaviour;
        private int productionCooldownMax;
        private int productionCooldown;

        public Structure(float width, float height, float fromTop, float fromLeft, string sprite, IProduceBehaviour produceBehaviour)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            productionCooldownMax = 8;
            ProductionCooldown = 8;

            productionCooldown = productionCooldownMax;
            capturePoints = 30;
            this.produceBehaviour = produceBehaviour;
        }

        public float CapturePoints
        {
            get { return capturePoints; }
        }

        public IProduceBehaviour ProduceBehaviour
        {
            get { return produceBehaviour; }
        }

        public int ProductionCooldownMax
        {
            get { return productionCooldownMax; }
            set
            {
                if (ProductionCooldown > value)
                {
                    ProductionCooldown = value;
                };
                productionCooldownMax = value;
            }
        }

        public int ProductionCooldown { get; set; }

        public string GetProduced()
        {
            if (produceBehaviour == null)
            {
                return null;
            }
            return produceBehaviour.Produce();
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
    }
}
