using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    static class MapFactory
    {
        public static Map GetMap(string level)
        {
            Tile[,] tiles = new Tile[25,25];
            string sprite = "MAP_SPRITE"; //rename to whatever it is called later

            switch (level)
            {
                //TODO: add more maps
                case "mountainLevel":
                    sprite = "MOUNTAIN_LEVEL_SPRITE";

                    for (int fromLeft = 0; fromLeft < tiles.GetLength(0); fromLeft +=1)
                    {
                        for (int fromTop = 0; fromTop < tiles.GetLength(1); fromTop += 1)
                        {
                            tiles[fromLeft, fromTop] = TileFactory.GetTile("Mountain");
                        }
                    }
                    break;

                default: //plainLevel
                    sprite = "PLAIN_LEVEL_SPRITE";

                    for (int fromLeft = 0; fromLeft < tiles.GetLength(0); fromLeft += 1)
                    {
                        for (int fromTop = 0; fromTop < tiles.GetLength(1); fromTop += 1)
                        {
                            tiles[fromLeft, fromTop] = TileFactory.GetTile("Plain");
                        }
                    }
                    break;
            }
            return new Map(tiles, sprite);
        }
    }
}
