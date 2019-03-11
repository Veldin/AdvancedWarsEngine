using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    static class TileFactory
    {
        public static Tile GetTile(string tileName)
        {
            switch (tileName)
            {
                //TODO: add more tile types
                case "Mountain":
                    return new Mountain();
                default: //plain
                    return new Plain();
            }
        }
    }
}
