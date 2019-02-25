using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Tile
    {
        private bool selected;
        private bool occupiedStructure;
        private bool occupiedUnit;
        private int width;
        private ITerrainBehavior terrainBehavior;
    }
}
