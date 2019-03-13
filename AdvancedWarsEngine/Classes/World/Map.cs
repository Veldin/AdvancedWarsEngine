using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace AdvancedWarsEngine.Classes
{
    class Map
    {
        protected Tile[,] tiles;
        protected string sprite;
        public Rectangle rectangle;

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

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                rectangle = new Rectangle();
            }));
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }
    }
}
