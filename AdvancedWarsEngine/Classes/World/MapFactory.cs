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

                    tiles[0, 0] = TileFactory.GetTile("Mountain");
                    tiles[0, 1] = TileFactory.GetTile("Mountain");
                    tiles[0, 2] = TileFactory.GetTile("Forest");
                    tiles[0, 3] = TileFactory.GetTile("Forest");
                    tiles[0, 4] = TileFactory.GetTile("Forest");
                    tiles[0, 5] = TileFactory.GetTile("Forest");
                    tiles[0, 6] = TileFactory.GetTile("Forest");
                    tiles[0, 7] = TileFactory.GetTile("Forest");
                    tiles[0, 8] = TileFactory.GetTile("Forest");
                    tiles[0, 9] = TileFactory.GetTile("Forest");
                    tiles[1, 0] = TileFactory.GetTile("Mountain");
                    tiles[1, 1] = TileFactory.GetTile("Plain");
                    tiles[1, 2] = TileFactory.GetTile("Forest");
                    tiles[1, 3] = TileFactory.GetTile("Forest");
                    tiles[1, 4] = TileFactory.GetTile("Forest");
                    tiles[1, 5] = TileFactory.GetTile("Forest");
                    tiles[1, 6] = TileFactory.GetTile("Forest");
                    tiles[1, 7] = TileFactory.GetTile("Forest");
                    tiles[1, 8] = TileFactory.GetTile("Forest");
                    tiles[1, 9] = TileFactory.GetTile("Forest");
                    tiles[2, 0] = TileFactory.GetTile("Plain");
                    tiles[2, 1] = TileFactory.GetTile("Plain");
                    tiles[2, 2] = TileFactory.GetTile("Road");
                    tiles[2, 3] = TileFactory.GetTile("Road");
                    tiles[2, 4] = TileFactory.GetTile("Road");
                    tiles[2, 5] = TileFactory.GetTile("Road");
                    tiles[2, 6] = TileFactory.GetTile("Plain");
                    tiles[2, 7] = TileFactory.GetTile("Plain");
                    tiles[2, 8] = TileFactory.GetTile("Plain");
                    tiles[2, 9] = TileFactory.GetTile("Plain");
                    tiles[3, 0] = TileFactory.GetTile("Plain");
                    tiles[3, 1] = TileFactory.GetTile("Plain");
                    tiles[3, 2] = TileFactory.GetTile("Road");
                    tiles[3, 3] = TileFactory.GetTile("Mountain");
                    tiles[3, 4] = TileFactory.GetTile("Mountain");
                    tiles[3, 5] = TileFactory.GetTile("Road");
                    tiles[3, 6] = TileFactory.GetTile("Road");
                    tiles[3, 7] = TileFactory.GetTile("Road");
                    tiles[3, 8] = TileFactory.GetTile("Road");
                    tiles[3, 9] = TileFactory.GetTile("Urban");
                    tiles[4, 0] = TileFactory.GetTile("Plain");
                    tiles[4, 1] = TileFactory.GetTile("Plain");
                    tiles[4, 2] = TileFactory.GetTile("Road");
                    tiles[4, 3] = TileFactory.GetTile("Road");
                    tiles[4, 4] = TileFactory.GetTile("Urban");
                    tiles[4, 5] = TileFactory.GetTile("Mountain");
                    tiles[4, 6] = TileFactory.GetTile("Forest");
                    tiles[4, 7] = TileFactory.GetTile("Forest");
                    tiles[4, 8] = TileFactory.GetTile("Forest");
                    tiles[4, 9] = TileFactory.GetTile("Forest");
                    tiles[5, 0] = TileFactory.GetTile("Forest");
                    tiles[5, 1] = TileFactory.GetTile("Forest");
                    tiles[5, 2] = TileFactory.GetTile("Forest");
                    tiles[5, 3] = TileFactory.GetTile("Forest");
                    tiles[5, 4] = TileFactory.GetTile("Mountain");
                    tiles[5, 5] = TileFactory.GetTile("Plain");
                    tiles[5, 6] = TileFactory.GetTile("Plain");
                    tiles[5, 7] = TileFactory.GetTile("Plain");
                    tiles[5, 8] = TileFactory.GetTile("Plain");
                    tiles[5, 9] = TileFactory.GetTile("Plain");
                    tiles[6, 0] = TileFactory.GetTile("Water");
                    tiles[6, 1] = TileFactory.GetTile("Water");
                    tiles[6, 2] = TileFactory.GetTile("Water");
                    tiles[6, 3] = TileFactory.GetTile("Water");
                    tiles[6, 4] = TileFactory.GetTile("Water");
                    tiles[6, 5] = TileFactory.GetTile("Water");
                    tiles[6, 6] = TileFactory.GetTile("Water");
                    tiles[6, 7] = TileFactory.GetTile("Water");
                    tiles[6, 8] = TileFactory.GetTile("Water");
                    tiles[6, 9] = TileFactory.GetTile("Plain");
                    tiles[7, 0] = TileFactory.GetTile("Water");
                    tiles[7, 1] = TileFactory.GetTile("Water");
                    tiles[7, 2] = TileFactory.GetTile("Water");
                    tiles[7, 3] = TileFactory.GetTile("Water");
                    tiles[7, 4] = TileFactory.GetTile("Water");
                    tiles[7, 5] = TileFactory.GetTile("Water");
                    tiles[7, 6] = TileFactory.GetTile("Water");
                    tiles[7, 7] = TileFactory.GetTile("Water");
                    tiles[7, 8] = TileFactory.GetTile("Water");
                    tiles[7, 9] = TileFactory.GetTile("Plain");
                    tiles[8, 0] = TileFactory.GetTile("Plain");
                    tiles[8, 1] = TileFactory.GetTile("Plain");
                    tiles[8, 2] = TileFactory.GetTile("Plain");
                    tiles[8, 3] = TileFactory.GetTile("Plain");
                    tiles[8, 4] = TileFactory.GetTile("Plain");
                    tiles[8, 5] = TileFactory.GetTile("Plain");
                    tiles[8, 6] = TileFactory.GetTile("Plain");
                    tiles[8, 7] = TileFactory.GetTile("Plain");
                    tiles[8, 8] = TileFactory.GetTile("Plain");
                    tiles[8, 9] = TileFactory.GetTile("Plain");
                    tiles[9, 0] = TileFactory.GetTile("Mountain");
                    tiles[9, 1] = TileFactory.GetTile("Mountain");
                    tiles[9, 2] = TileFactory.GetTile("Mountain");
                    tiles[9, 3] = TileFactory.GetTile("Mountain");
                    tiles[9, 4] = TileFactory.GetTile("Mountain");
                    tiles[9, 5] = TileFactory.GetTile("Mountain");
                    tiles[9, 6] = TileFactory.GetTile("Mountain");
                    tiles[9, 7] = TileFactory.GetTile("Mountain");
                    tiles[9, 8] = TileFactory.GetTile("Mountain");
                    tiles[9, 9] = TileFactory.GetTile("Mountain");
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
