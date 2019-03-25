using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdvancedWarsEngine.Classes
{
    class Map
    {
        protected Tile[,] tiles;

        protected Tile selectedTile;

        protected string sprite;
        public Rectangle rectangle;
        protected int size;

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

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public Tile SelectedTile
        {
            get { return selectedTile; }
            set { selectedTile = value; }
        }

        public Map(Tile[,] tiles, string sprite)
        {
            this.tiles = tiles;
            this.sprite = sprite;
            size = 16;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                BitmapImage newBitmap = new BitmapImage(new Uri("pack://application:,,,/AdvancedWarsEngine;component/" + sprite, UriKind.Absolute));

                rectangle = new Rectangle
                {
                    Fill = new ImageBrush { ImageSource = newBitmap },

                    Width = tiles.GetLength(1) * size,
                    Height = tiles.GetLength(0) * size
                };
            }));
        }

        public Tile GetTile(int x, int y)
        {
            //If its below zero recall the function with a zero
            if (x < 0)
            {
                return GetTile(0, y);
            }

            //If its above the limit recall the function on the limit
            if (x >= tiles.GetLength(0))
            {
                return GetTile(tiles.GetLength(0) - 1, y);
            }

            //If its below zero recall the function with a zero
            if (y < 0)
            {
                return GetTile(x, 0);
            }

            //If its above the limit recall the function on the limit
            if (y >= tiles.GetLength(1))
            {
                return GetTile(x, tiles.GetLength(1) - 1);
            }

            return tiles[x, y];
        }

        public void DeselectAll()
        {
            for (int fromLeft = 0; fromLeft < tiles.GetLength(0); fromLeft++)
            {
                for (int fromTop = 0; fromTop < tiles.GetLength(1); fromTop++)
                {
                    tiles[fromLeft, fromTop].Selected = false;
                }
            }
        }

        public Target GetTileCoords(Tile tile)
        {
            for (int fromLeft = 0; fromLeft < tiles.GetLength(1); fromLeft++)
            {
                for (int fromTop = 0; fromTop < tiles.GetLength(0); fromTop++)
                {
                    if (tiles[fromTop, fromLeft] == tile)
                    {
                        Target coords = new Target(fromTop, fromLeft);
                        return coords;
                    }
                }
            }
            return null;
        }

        public Tile GetTileFromGameobject(GameObject search)
        {
            for (int fromLeft = 0; fromLeft < tiles.GetLength(0); fromLeft++)
            {
                for (int fromTop = 0; fromTop < tiles.GetLength(1); fromTop++)
                {
                    if (tiles[fromLeft, fromTop].OccupiedUnit == search || tiles[fromLeft, fromTop].OccupiedStructure == search)
                    {
                        return tiles[fromLeft, fromTop];
                    }
                }
            }
            return null;
        }
    }
}