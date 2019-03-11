using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    abstract class Tile
    {
        private bool selected;
        private Structure occupiedStructure;
        private Unit occupiedUnit;
        private int size;

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

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public Tile()
        {
            this.selected = false;
            this.size = 32;
        }

        public bool toggleSelection()
        {
            return true;
        }
    }
}
