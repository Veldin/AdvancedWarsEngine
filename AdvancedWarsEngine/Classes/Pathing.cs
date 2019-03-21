using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdvancedWarsEngine.Classes
{
    class Pathing
    {
        private List<GameObject>    colourOverlay;
        private List<GameObject>    arrowPrompts;
        private List<Tile>          allowedTiles;
        private List<List<Tile>>    pathes;
        private World               world;

        public Pathing(World world)
        {
            // Initalize everything
            colourOverlay   = new List<GameObject>();
            arrowPrompts    = new List<GameObject>();
            allowedTiles    = new List<Tile>();
            pathes          = new List<List<Tile>>();
            this.world      = world;

            Debug.WriteLine("Pathing class created");
        }


        //selectedUnit
        // promptfactory
        // player of the selected unit
        public List<GameObject> SetColorOverlay(Unit unit, IAbstractFactory factory, Player player)
        {
            // Clear the colourOverlay list
            colourOverlay.Clear();
            

            // Get the map
            Map map = world.Map;


            // Tile[,] tiles = world.Map.Tiles;

            // Get on the right thread??
            Application.Current.Dispatcher.Invoke(delegate
            {
                foreach (Tile tile in allowedTiles)
                {
                    
                    float fromTop = map.GetTileCoords(tile).GetFromTop() * 16;
                    float fromLeft = map.GetTileCoords(tile).GetFromLeft() * 16;
                    //Debug.WriteLine("TileCoords" + fromTop / 16 + "  " + fromLeft / 16);


                    // The tile contains only a structure
                    if (tile.OccupiedUnit == null && tile.OccupiedStructure != null)
                    {
                        // Check if the Structure is allied
                        if (player.InGameObjects(tile.OccupiedStructure))
                        {
                            //Create move prompt
                            colourOverlay.Add(factory.GetGameObject("Sprites/TileSelectors/TileSelectorGreen.gif", 16, 16, fromTop, fromLeft));
                            continue;
                        } else {
                        //Create attack prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/TileSelectors/TileSelectorRed.gif", 16, 16, fromTop, fromLeft));
                        continue;
                        }
                    } // The tile contains only a Unit
                    else if (tile.OccupiedUnit != null && tile.OccupiedStructure == null)
                    {
                        // Check if the Unit is allied
                        if (player.InGameObjects(tile.OccupiedUnit))
                        {
                        //Create allied prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/TileSelectors/TileSelectorBlue.gif", 16, 16, fromTop, fromLeft));
                        continue;
                        }
                        else
                        {
                        //Create attack prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/TileSelectors/TileSelectorRed.gif", 16, 16, fromTop, fromLeft));
                        continue;
                        }
                    }
                    else if(tile.OccupiedUnit == null && tile.OccupiedStructure == null)
                    {
                        // Create move prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/TileSelectors/TileSelectorGreen.gif", 16, 16, fromTop, fromLeft));
                        continue;
                    }
                }
            });

            allowedTiles.Clear();
            return colourOverlay;
        }

        public List<GameObject> CreateArrows(List<Tile> path, Target start, Target end, IAbstractFactory promptFactory)
        {
            // Clear the list for the arrowPrompts
            arrowPrompts.Clear();
            allowedTiles.Clear();

            // Get the map
            Map map = world.Map;

            // Remove the last tile of the path because we dont want an arrow on the last tile
            if (path.Count > 1)
            {
                path.RemoveAt(path.Count - 1);
            }
            

            // Create a list with target of the path and fill it
            List<Target> targets = new List<Target>();
            foreach (Tile tile in path)
            {
                targets.Add(map.GetTileCoords(tile));
            }
            
            // Get on the right thread??
            Application.Current.Dispatcher.Invoke(delegate
            {
                // Declare some variables
                int lastTile = targets.Count -1;
                string imageLocation;

                // Loop through every target
                for (int i = 0; i < targets.Count; i++)
                {
                    // Check if this is the first target
                    if (i == 0)
                    {
                        // Get the imageLocation for the first target
                        imageLocation = GetImageLocation(start, targets[0], i == lastTile ? end : targets[i + 1], i == lastTile ? true : false);
                    } else
                    {
                        // Get the imageLocation
                        imageLocation = GetImageLocation(targets[i-1], targets[i], i == lastTile ? end : targets[i + 1], i == lastTile ? true : false);
                    }

                    // Add the arrow prompt
                    arrowPrompts.Add(promptFactory.GetGameObject(imageLocation, 16, 16, targets[i].GetFromTop() * 16, targets[i].GetFromLeft() * 16));
                }

            });

            return arrowPrompts;
        }

        public string GetImageLocation(Target previous, Target current, Target next, bool isLast)
        {
            // Get the coords
            float prevFromLeft  = previous.GetFromLeft();
            float prevFromTop   = previous.GetFromTop();
            float currFromLeft  = current.GetFromLeft();
            float currFromTop   = current.GetFromTop();
            float nextFromLeft = next.GetFromLeft();
            float nextFromTop = next.GetFromTop();

            // Compare the values with eachother
            int prevHor = prevFromLeft.CompareTo(currFromLeft);
            int prevVer = prevFromTop.CompareTo(currFromTop);
            int nextHor = currFromLeft.CompareTo(nextFromLeft);
            int nextVer = currFromTop.CompareTo(nextFromTop);


            // The meaning of the results of the comparision
            // -1 is prev left from curr 
            //  1 is prev right from cuur
            // -1 is prev above curr
            //  1 is perv under curr
            // -1 is curr left from next 
            // -1 is curr above next
            if (isLast)
            {
                if (prevHor < 0 && nextHor < 0) { return "Sprites/Arrows/ArrowHeadRight.gif"; }
            //    if (prevHor < 0 && nextVer < 0) { return null; } // ArrowHeadFromLeftToBottom
            //    if (prevHor < 0 && nextVer > 0) { return null; } // ArrowHeadFromLeftToTop
                if (prevHor > 0 && nextHor > 0) { return "Sprites/Arrows/ArrowHeadLeft.gif"; }
            //    if (prevHor > 0 && nextVer < 0) { return null; } // ArrowHeadFromRightToBottom
            //    if (prevHor > 0 && nextVer > 0) { return null; } // ArrowHeadFromRightToTop
                if (prevVer < 0 && nextVer < 0) { return "Sprites/Arrows/ArrowHeadBottom.gif"; }
            //    if (prevVer < 0 && nextHor < 0) { return null; } // ArrowHeadFromTopToRight
            //    if (prevVer < 0 && nextHor > 0) { return null; } // ArrowHeadFromTopToLeft
                if (prevVer > 0 && nextVer > 0) { return "Sprites/Arrows/ArrowHeadTop.gif"; }
           //     if (prevVer < 0 && nextHor < 0) { return null; } // ArrowHeadFromBottomToRight
           //     if (prevVer < 0 && nextHor > 0) { return null; } // ArrowHeadFromBottomToLeft
            } else
            {
                if (prevHor == 0 && nextHor == 0) { return "Sprites/Arrows/ArrowTopToBottom.gif"; }
                if (prevVer == 0 && nextVer == 0) { return "Sprites/Arrows/ArrowLeftToRight.gif"; }
                if (prevHor < 0 && nextVer < 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowBottomToLeft.gif"; }
                if (prevHor > 0 && nextVer < 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowBottomToRight.gif"; }
                if (prevHor < 0 && nextVer > 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowLeftToTop.gif"; }
                if (prevHor > 0 && nextVer > 0 || prevVer < 0 && nextHor < 0) { return "Sprites/Arrows/ArrowTopToRight.gif"; }
            }

            return "Sprites/Arrows/ArrowHeadRight.gif";
        }

        public bool CreatePaths(Target start, Target end, float movingDistance, List<Tile> path = null)
        {
            
            Map map = world.Map;

            int x = (int) (start.GetFromLeft() );
            int y = (int) (start.GetFromTop() );

            // Create new tiles
            List<Tile> temp = new List<Tile>
            {
                map.GetTile(y - 1, x), //top
                map.GetTile(y + 1, x), //bot
                map.GetTile(y, x - 1), // left
                map.GetTile(y, x + 1) // right
            };

            foreach (Tile tile in temp)
            {
                // Check if there can be moved to this tile
                if (!CheckTile(tile)) { continue; }

                // The tile is allowed so try to add it to the list
                if (!allowedTiles.Contains(tile))
                {
                    float a = map.GetTileCoords(tile).GetFromTop();
                    float b = map.GetTileCoords(tile).GetFromLeft();
                    Debug.WriteLine("TILELIST Tile coords:  " + a + "   " + b);
                    allowedTiles.Add(tile);
                }

                // Add a new tmpPath
                List<Tile> tmpPath = new List<Tile>();

                //Check if path exist
                if(path != null)
                {
                    // Copy all items of path into tempPath
                    foreach(Tile pathTile in path)
                    {
                        tmpPath.Add(pathTile);
                    }
                }

                // Add this tile to tmpPath
                tmpPath.Add(tile);

                // Get data about the tile
                Target tileTarget = map.GetTileCoords(tile);
                float fromLeft = map.GetTileCoords(tile).GetFromLeft();
                float fromTop = map.GetTileCoords(tile).GetFromTop();

                // If endTarget has been reached add path to pathes
                if (fromLeft == end.GetFromLeft() && fromTop == end.GetFromTop())
                {
                    pathes.Add(tmpPath);
                   // return true;
                   continue;
                } // If the path is as long or longer than the movementDistance return
                else if (tmpPath.Count >= movingDistance)
                {
                    
                    continue;
                } // Find the next tile in the possible path
                else
                {
                    CreatePaths(tileTarget, end, movingDistance, tmpPath);
                }
            }


            return true;
        }

        public List<Tile> GetPath(Target start, Target end, float movingDistance)
        {
            // Create the pathes
            CreatePaths(start, end, movingDistance, null);

            // Debug everything
   /*         for (int i = 0; i < pathes.Count; i++)
            {
                Debug.WriteLine("---Path " + i + "---");
                Debug.WriteLine("TotalTiles: " + pathes[i].Count);

                foreach (Tile tile in pathes[i])
                {
                    float fromLeft = world.Map.GetTileCoords(tile).GetFromLeft();
                    float fromTop = world.Map.GetTileCoords(tile).GetFromTop();
                    Debug.WriteLine("Tile coords: "  + fromTop + "  " + fromLeft + "  " +  tile.GetType().Name);
                }
            }
            if (pathes.Count == 0)
            {
                Debug.WriteLine("No pathes found");
            }*/

            // Set some variables
            int tilesDistance = -1;
            List<Tile> finalPath = null;

            // Pick the shortest path
            foreach (List<Tile> tmp in pathes)
            {
                // If no distance and path is set, set them
                if (tilesDistance == -1)
                {
                    tilesDistance = tmp.Count;
                    finalPath = tmp;
                } // if the path is shorter than the currently selected replace the old one with the shorter one
                else if (tmp.Count < tilesDistance)
                {
                    tilesDistance = tmp.Count;
                    finalPath = tmp;
                }
            }

            if (finalPath != null)
            {
                Debug.WriteLine("---The Final Path---");
                Debug.WriteLine("TotalTiles: " + finalPath.Count);

                foreach (Tile tile in finalPath)
                {
                    float fromLeft = world.Map.GetTileCoords(tile).GetFromLeft();
                    float fromTop = world.Map.GetTileCoords(tile).GetFromTop();
                    Debug.WriteLine("Tile coords: "  + fromTop + "  " + fromLeft + "  " + tile.GetType().Name);
                }
            }
            else
            {
                Debug.WriteLine("No path found");
            }


            return finalPath;
        }

        private void AddTileToList(Tile tile)
        {

        }

        private bool CheckTile(Tile targetTile)
        {
            //targetTile.GetType().Name
            switch (targetTile.GetType().Name)
            {
                case "Mountain":
                    //if (unit.UnitType == EUnitType.Vehicle) { return false; }
                return false;
                case "Plain":
                    break;
                case "Forest":
                    break;
                case "Water":
                 //   if (unit.UnitType == EUnitType.Vehicle) { return false; }
                   //     if (unit.UnitType == EUnitType.Infantry) { return false; }
                    return false;
                   //break;
                case "Urban":
                    break;
                case "Road":
                    break;
            }

            return true;
        }

        // ------------------GETTERS -------------------
        public List<GameObject> ColourOverlay
        {
            get { return colourOverlay; }
        }

        public List<GameObject> ArrowPrompts
        {
            get { return arrowPrompts; }
        }


        //        private List<GameObject>    colourOverlay;
        //          private List<GameObject> arrowPrompts;
        //    if (start == end) return true;

        //    //Sorted by shortest direct path first
        //    var q = new SortedList<double, Tile>(allowDiagonalMovement ? 6 : 4, new DupeKeyComparer<double>());
        //    if (start.x + 1 < map.X) q.Add((start + new vec2 { x = 1, y = 0 }) * end,    map[start.x + 1, start.y]);
        //    if (start.x - 1 >= 0)    q.Add((start + new vec2 { x = -1, y =  0 }) * end,  map[start.x - 1, start.y]);
        //    if (start.y + 1 < map.Y) q.Add((start + new vec2 { x = 0, y = 1 }) * end,    map[start.x, start.y + 1]);
        //    if (start.y - 1 >= 0)    q.Add((start + new vec2 { x =  0, y = -1 }) * end,  map[start.x, start.y - 1]);

        //    foreach(var p in q) {
        //        if (p.Value.ttype == Tile.TileType.OBSTACLE || 
        //            stack.Count != 0 && stack.Contains(p.Value)) continue;

        //        stack.Push(map[start.x, start.y]);
        //        if (findPath(p.Value.xy, end)) return true;
        //    }
        //    stack.Pop();
        //    return false;
        //}

    }
}
