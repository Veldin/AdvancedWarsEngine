using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Map
    {
        private Tile[,] tiles;
        private string sprite;

        public Tile[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        public string Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Map(Tile[,] tiles, string sprite)
        {
            this.tiles = tiles;
            this.sprite = sprite;
            //DO SOMETHING
        }

        public Tile GetTile(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
