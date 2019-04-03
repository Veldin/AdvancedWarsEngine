namespace AdvancedWarsEngine.Classes
{
    abstract class Tile
    {
        protected bool selected;
        protected Structure occupiedStructure;
        protected Unit occupiedUnit;
        protected int size;

        /* Defence values */
        protected float defaultDefenceMultiplier;

        protected float infantryDefenceMultiplier;
        protected float airDefenceMultiplier;
        protected float vehicleDefenceMultiplier;

        public Tile()
        {
            selected = false;
            size = 16;
            defaultDefenceMultiplier = 1;
        }

        public abstract void CalculateInfantryDefenceMultiplier();
        public abstract void CalculateAirDefenceDefenceMultiplier();
        public abstract void CalculateVehicleDefenceMultiplier();

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public float GetDefenceValue(string type)
        {
            switch (type)
            {
                case "infantry":
                    return infantryDefenceMultiplier;
                case "air":
                    return airDefenceMultiplier;
                case "vehicle":
                    return vehicleDefenceMultiplier;
                default:
                    return defaultDefenceMultiplier;
            }
        }

        public Structure OccupiedStructure
        {
            get { return occupiedStructure; }
            set { occupiedStructure = value; }
        }

        public Unit OccupiedUnit
        {
            get { return occupiedUnit; }
            set { occupiedUnit = value; }
        }
    }
}
