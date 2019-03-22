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
        private List<GameObject> colourOverlay;              // This list keeps track of the last created colourOverlay which is necessary for the removal thereof
        private List<GameObject> arrowPrompts;               // This list keeps track of the last created arrowPrompts which is necessary for the removal thereof
        private List<Tile> allowedTiles;               // This list keeps track of which tiles the Unit is allowed to stand on
        private List<List<Tile>> paths;                      // This list keeps track of all possible paths

        public Pathing()
        {
            // Initalize everything
            colourOverlay = new List<GameObject>();
            arrowPrompts = new List<GameObject>();
            allowedTiles = new List<Tile>();
            paths = new List<List<Tile>>();
        }

        /***************************************************************
         * Getters and Setters
         * ************************************************************/
        public List<GameObject> ColourOverlay
        {
            get { return colourOverlay; }
        }

        public List<GameObject> ArrowPrompts
        {
            get { return arrowPrompts; }
        }

        /***************************************************************
         * Public functions
         * ************************************************************/

        /// <summary>
        /// This function clears the colorOverlay list
        /// </summary>
        public void EmptyColorOverlay()
        {
            colourOverlay.Clear();
        }

        /// <summary>
        /// This function creates een colorOvelay over the tiles where a movement or attack of a unit allowed is.
        /// </summary>
        /// <param name="unit"> The Unit wherefore the color overlay is created</param>
        /// <param name="unitLocation"> The current location of the unit</param>
        /// <param name="factory"> A PromptFactory</param>
        /// <param name="player"> The player that owns the Unit</param>
        /// <returns> Returns a list of prompts which is the color overlay</returns>
        public List<GameObject> SetColorOverlay(Unit unit, Target unitLocation, IAbstractFactory factory, Player player, Map map)
        {
            // Create the pathes necessary for this function
            Target end = new Target(0, 0);
            CreatePaths(unitLocation, end, unit, map, player);

            // Clear the list paths because it is not necessary for this function and breaks other functions if not cleared
            paths.Clear();

            // Clear the colourOverlay list
            colourOverlay.Clear();

            // Loops through each tile of the allowedTiles list and check which color the prompt should have
            foreach (Tile tile in allowedTiles)
            {
                // Get the fromTop and fromLeft of the tile in tiles
                float fromTop = map.GetTileCoords(tile).GetFromTop() * 16;
                float fromLeft = map.GetTileCoords(tile).GetFromLeft() * 16;

                // The tile contains only a structure
                if (tile.OccupiedUnit == null && tile.OccupiedStructure != null)
                {
                    // Check if the Structure is allied
                    if (player.InGameObjects(tile.OccupiedStructure))
                    {
                        //Create move prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/RangeIndicators/rangeIndicatorGreen.png", 16, 16, fromTop, fromLeft));
                        continue;
                    }
                    else
                    {
                        //Create attack prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/RangeIndicators/rangeIndicatorRed.png", 16, 16, fromTop, fromLeft));
                        continue;
                    }
                } // The tile contains only a Unit
                else if (tile.OccupiedUnit != null && tile.OccupiedStructure == null)
                {
                    // Check if the Unit is allied
                    if (player.InGameObjects(tile.OccupiedUnit))
                    {
                        //Create allied prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/RangeIndicators/rangeIndicatorBlue.png", 16, 16, fromTop, fromLeft));
                        continue;
                    }
                    else
                    {
                        //Create attack prompt
                        colourOverlay.Add(factory.GetGameObject("Sprites/RangeIndicators/rangeIndicatorRed.png", 16, 16, fromTop, fromLeft));
                        continue;
                    }
                }
                else if (tile.OccupiedUnit == null && tile.OccupiedStructure == null)
                {
                    // Create move prompt
                    colourOverlay.Add(factory.GetGameObject("Sprites/RangeIndicators/rangeIndicatorGreen.png", 16, 16, fromTop, fromLeft));
                    continue;
                }
            }

            // Clear the allowedTiles list because it has used it purpuse and should be empty for the next time it will be used
            allowedTiles.Clear();

            return colourOverlay;
        }

        /// <summary>
        /// This function creates the arrow prompts.
        /// </summary>
        /// <param name="start"> The Target where the Unit starts</param>
        /// <param name="end"> The Target of the destination of the Unit</param>
        /// <param name="unit"> The Unit wherefore the arrows are created</param>
        /// <param name="promptFactory"> The factory that creates the prompts </param>
        /// <returns> Returns a list of prompts which are the arrow images</returns>
        public List<GameObject> CreateArrows( Target start, Target end, Unit unit, IAbstractFactory promptFactory, Player player, Map map)
        {
            // Get the path
            List<Tile> path = GetPath(start, end, unit, map, player);

            // If there is no path found return
            if (path == null) { return null; }

            // Clear the list for the arrowPrompts
            arrowPrompts.Clear();
            allowedTiles.Clear();

            // Remove the last tile of the path because we dont want an arrow on the last tile
            if (path.Count >= 1)
            {
                path.RemoveAt(path.Count - 1);
            }

            // Create a list with target of the path and fill it
            List<Target> targets = new List<Target>();
            foreach (Tile tile in path)
            {
                targets.Add(map.GetTileCoords(tile));
            }

            // Declare some variables
            int lastTile = targets.Count - 1;
            string imageLocation;

            // Loop through every target
            for (int i = 0; i < targets.Count; i++)
            {
                // Check if this is the first target
                if (i == 0)
                {
                    // Get the imageLocation for the first target
                    imageLocation = GetImageLocation(start, targets[0], i == lastTile ? end : targets[i + 1], i == lastTile ? true : false);
                }
                else
                {
                    // Get the imageLocation
                    imageLocation = GetImageLocation(targets[i - 1], targets[i], i == lastTile ? end : targets[i + 1], i == lastTile ? true : false);
                }

                // Add the arrow prompt
                arrowPrompts.Add(promptFactory.GetGameObject(imageLocation, 16, 16, targets[i].GetFromTop() * 16, targets[i].GetFromLeft() * 16));
            }

            return arrowPrompts;
        }

        /***************************************************************
         * Private functions
         * ************************************************************/

        /// <summary>
        /// This function calculates which arrow image should be used and returns the image location of that image.
        /// </summary>
        /// <param name="previous"> The Tile which was before the current tile</param>
        /// <param name="current"> The Tile where the image should be placed</param>
        /// <param name="next"> The Tile which comes after the current tile</param>
        /// <param name="isLast"> True if it is the last Tile of the path</param>
        /// <returns>Returns the image location as string</returns>
        private string GetImageLocation(Target previous, Target current, Target next, bool isLast)
        {
            // Get the coords
            float prevFromLeft = previous.GetFromLeft();
            float prevFromTop = previous.GetFromTop();
            float currFromLeft = current.GetFromLeft();
            float currFromTop = current.GetFromTop();
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

            // Returns the correct image location
            if (isLast)
            {
                if (prevHor < 0 && nextHor < 0) { return "Sprites/Arrows/ArrowHeadRight.gif"; }
                if (prevHor < 0 && nextVer < 0) { return "Sprites/Arrows/ArrowHeadLeftToBottom.gif"; } // ArrowHeadLeftToBottom
                if (prevHor < 0 && nextVer > 0) { return "Sprites/Arrows/ArrowHeadLeftToTop.gif"; } // ArrowHeadLeftToTop
                if (prevHor > 0 && nextHor > 0) { return "Sprites/Arrows/ArrowHeadLeft.gif"; }
                if (prevHor > 0 && nextVer < 0) { return "Sprites/Arrows/ArrowHeadRightToBottom.gif"; } // ArrowHeadRightToBottom
                if (prevHor > 0 && nextVer > 0) { return "Sprites/Arrows/ArrowHeadRightToTop.gif"; } // ArrowHeadRightToTop
                if (prevVer < 0 && nextVer < 0) { return "Sprites/Arrows/ArrowHeadBottom.gif"; }
                if (prevVer < 0 && nextHor < 0) { return "Sprites/Arrows/ArrowHeadTopToRight.gif"; } // ArrowHeadTopToRight
                if (prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowHeadTopToLeft.gif"; } // ArrowHeadTopToLeft
                if (prevVer > 0 && nextVer > 0) { return "Sprites/Arrows/ArrowHeadTop.gif"; }
                if (prevVer > 0 && nextHor < 0) { return "Sprites/Arrows/ArrowHeadBottomToRight.gif"; } // ArrowHeadBottomToRight
                if (prevVer > 0 && nextHor > 0) { return "Sprites/Arrows/ArrowHeadBottomToLeft.gif"; } // ArrowHeadBottomToLeft
            }
            else
            {
                if (prevHor == 0 && nextHor == 0) { return "Sprites/Arrows/ArrowTopToBottom.gif"; }
                if (prevVer == 0 && nextVer == 0) { return "Sprites/Arrows/ArrowLeftToRight.gif"; }
                if (prevHor < 0 && nextVer < 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowBottomToLeft.gif"; }
                if (prevHor > 0 && nextVer < 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowBottomToRight.gif"; }
                if (prevHor < 0 && nextVer > 0 || prevVer < 0 && nextHor > 0) { return "Sprites/Arrows/ArrowLeftToTop.gif"; }
                if (prevHor > 0 && nextVer > 0 || prevVer < 0 && nextHor < 0) { return "Sprites/Arrows/ArrowTopToRight.gif"; }
            }

            // If the right image location is not found return this one
            return "Sprites/Arrows/ArrowHeadBottomToLeft.gif";
        }

        /// <summary>
        /// This funcion bruteforced all possible paths. It fills the lists allowedTiles and pathes.
        /// Note that this is a recursive function.
        /// </summary>
        /// <param name="start"> The Target where the Unit starts</param>
        /// <param name="end"> The Target of the destination of the Unit</param>
        /// <param name="unit"> The Unit wherefore the path is created</param>
        /// <param name="path"> A list used for the recursive function to track it's path</param>
        private void CreatePaths(Target start, Target end, Unit unit, Map map, Player player, List<Tile> path = null)
        {
            // Get the x and y value of the start Target
            int x = (int)(start.GetFromLeft());
            int y = (int)(start.GetFromTop());

            // Create new tiles
            List<Tile> temp = new List<Tile> // todo check if it is within the map
            {
                map.GetTile(y - 1, x), //top
                map.GetTile(y + 1, x), //bot
                map.GetTile(y, x - 1), // left
                map.GetTile(y, x + 1) // right
            };

            foreach (Tile tile in temp)
            {
                // Check if there can be moved to this tile
                if (!unit.IsTileAllowed(tile))
                {
                    if (tile.OccupiedUnit != null)
                    {
                        // The tile is allowed so try to add it to the list
                        if (!allowedTiles.Contains(tile))
                        {
                            allowedTiles.Add(tile);
                        }
                    }
                    continue;
                }

                // The tile is allowed so try to add it to the list
                if (!allowedTiles.Contains(tile))
                {
                    allowedTiles.Add(tile);
                }

                // If there is a Unit on the tile which the player doesn't own, continue.
                if (tile.OccupiedUnit != null)
                {
                    if (!player.InGameObjects(tile.OccupiedUnit))
                    {
                        continue;
                    }
                }

                // Add a new tmpPath
                List<Tile> tmpPath = new List<Tile>();

                //Check if path exist
                if (path != null)
                {
                    // Copy all items of path into tempPath
                    foreach (Tile pathTile in path)
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
                    paths.Add(tmpPath);
                    // return true;
                    continue;
                } // If the path is as long or longer than the movementDistance return
                else if (tmpPath.Count >= unit.MovementRange)
                {

                    continue;
                } // Find the next tile in the possible path
                else
                {
                    CreatePaths(tileTarget, end, unit, map, player, tmpPath);
                }
            }
        }

        /// <summary>
        /// This function calculates the shortest path of for a Unit to get from A to B.
        /// This function is also necessary for the creation of the colorOverlay and the arrowPrompts
        /// </summary>
        /// <param name="start"> The Target where the Unit starts</param>
        /// <param name="end"> The Target of the destination of the Unit</param>
        /// <param name="unit"> The Unit wherefore the path is created</param>
        /// <returns> Returns a list of Tiles which is the shortes way to get from A to B</returns>
        private List<Tile> GetPath(Target start, Target end, Unit unit, Map map, Player player)
        {
            // Clear the previous made paths
            paths.Clear();

            // Create the pathes
            CreatePaths(start, end, unit, map, player, null);

            // Set some variables
            int tilesDistance = -1;
            List<Tile> finalPath = null;

            // Pick the shortest path
            foreach (List<Tile> tmp in paths)
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

            return finalPath;
        }

    }
}
