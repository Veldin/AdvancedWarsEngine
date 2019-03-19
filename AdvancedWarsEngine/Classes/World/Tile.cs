namespace AdvancedWarsEngine.Classes
{
    abstract class Tile
    {
        protected bool selected;
        protected Structure occupiedStructure;
        protected Unit occupiedUnit;
        protected int size;

        public Tile()
        {
            selected = false;
            size = 16;
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
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
