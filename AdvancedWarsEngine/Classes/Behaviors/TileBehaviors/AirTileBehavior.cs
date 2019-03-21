using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class AirTileBehavior : ITileBehavior
    {
        public bool IsAllowed(Tile targetTile)
        {
            // Check which type of tile the targetTile is and if the unit is allowed to move to that tile.
            switch (targetTile.GetType().Name)
            {
                case "Mountain":
                    break;
                case "Plain":
                    break;
                case "Forest":
                    break;
                case "Water":
                    break;
                case "Urban":
                    break;
                case "Road":
                    break;
            }

            return true;
        }
    }
}
