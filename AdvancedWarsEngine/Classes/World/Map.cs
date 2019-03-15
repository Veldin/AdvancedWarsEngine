﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private int size;

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
            this.size = 16;
            //DO SOMETHING

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                BitmapImage newBitmap = new BitmapImage(new Uri("pack://application:,,,/AdvancedWarsEngine;component/" + sprite, UriKind.Absolute));
                
                rectangle = new Rectangle();
                rectangle.Fill = new ImageBrush{ImageSource = newBitmap};

                rectangle.Width = tiles.GetLength(0) * size;
                rectangle.Height = tiles.GetLength(1) * size;
            }));


        }

        public Tile SelectTile(int x, int y)
        {
            selectedTile = GetTile (x, y);
            return selectedTile;
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
                return GetTile(tiles.GetLength(0) -1, y);
            }

            //If its below zero recall the function with a zero
            if (y < 0)
            {
                return GetTile(x, 0);
            }

            //If its above the limit recall the function on the limit
            if (y >= tiles.GetLength(1))
            {
                return GetTile(x, tiles.GetLength(1)-1);
            }

            return tiles[x, y];
        }

        public void DeselectAll()
        {
            for (int fromLeft = 0; fromLeft < tiles.GetLength(0); fromLeft += 1)
            {
                for (int fromTop = 0; fromTop < tiles.GetLength(1); fromTop += 1)
                {
                    tiles[fromLeft, fromTop].Selected = false;
                }
            }
        }
    }
}
