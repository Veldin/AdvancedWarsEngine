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

        public Tile(int size)
        {
            this.size = size;
        }

        public bool toggleSelection()
        {
            return true;
        }

        public Structure GetOccupiedStructure()
        {
            return occupiedStructure;
        }

        public Unit GetOccupiedUnit()
        {
            return occupiedUnit;
        }

        public void SetOccupiedStructure(Structure occupiedStructure)
        {
            this.occupiedStructure = occupiedStructure;
        }

        public void SetOccupiedUnit(Unit occupiedUnit)
        {
            this.occupiedUnit = occupiedUnit;
        }
    }
}
