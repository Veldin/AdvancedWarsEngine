using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    abstract class Tile
    {
        protected bool selected;
        protected Structure occupiedStructure;
        protected Unit occupiedUnit;
        protected int size;
        protected IDefenceBehavior defenceBehavior;

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

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public bool SelectedUnit { get; internal set; }

        public bool toggleSelection()
        {
            return true;
        }
    }
}
