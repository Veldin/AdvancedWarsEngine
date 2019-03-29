namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;
        protected IProduceBehaviour produceBehaviour;
        private int productionCooldownMax;


        public Structure(float width, float height, float fromTop, float fromLeft, string sprite, IProduceBehaviour produceBehaviour)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            productionCooldownMax = 8;
<<<<<<< HEAD
            productionCooldown = productionCooldownMax;
            capturePoints = 1;
            this.produceBehavior = produceBehavior;
=======
            ProductionCooldown = productionCooldownMax;
            capturePoints = 100;
            this.produceBehaviour = produceBehaviour;
>>>>>>> 2c384e2e406608e80567449ad83064b4ba40a36c
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

<<<<<<< HEAD
=======
        public int ProductionCooldown { get; set; }

>>>>>>> 2c384e2e406608e80567449ad83064b4ba40a36c
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
