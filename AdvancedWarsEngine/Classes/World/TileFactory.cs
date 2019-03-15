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
                case "Forest":
                    return new Forest();
                case "Mountain":
                    return new Mountain();
                case "Water":
                    return new Water();
                case "Road":
                    return new Road();
                case "Urban":
                    return new Urban();
                default: //plain
                    return new Plain();
            }
        }
    }
}
