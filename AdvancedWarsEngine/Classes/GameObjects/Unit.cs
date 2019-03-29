﻿using AdvancedWarsEngine.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        protected float health = 100;                   // The health of the Unit
        protected float movementSpeed = 100;            // The movement speed of the Unit (for animations)
        protected int movementRange;                    // The amount of tiles the Unit can move
        protected IAttackBehaviour attackBehaviour;       // The attackBehaviour calculates the dammageValue
        protected IDefenceBehaviour defenceBehaviour;     // The defenceBehaviour calculates the defenceValue
        protected ITileBehaviour tileBehaviour;           // The tileBehaviour checks if the unit is allowed on the given tile
        protected EUnitType unitType;                   // The unitType specifice the type of this Unit for example infantry or vehicle
        private bool found = false;
        private Tile tempTile = null;
        private int steps;

        public Unit(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/Green_AV_Vehicle2.gif")
        {
            unitType = EUnitType.Vehicle;
        }

        public Unit(float width, float height, float fromTop, float fromLeft, string sprite, int movementRange, IAttackBehaviour attackBehaviour, IDefenceBehaviour defenceBehaviour, ITileBehaviour tileBehaviour, EUnitType unitType)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.movementRange = movementRange;
            this.attackBehaviour = attackBehaviour;
            this.defenceBehaviour = defenceBehaviour;
            this.tileBehaviour = tileBehaviour;
            this.unitType = unitType;
        }

        public float Health
        {
            get { return health; }
        }

        public float MovementSpeed
        {
            get { return movementSpeed; }
        }

        public int MovementRange
        {
            get { return movementRange; }
        }

        public EUnitType UnitType
        {
            get { return unitType; }
        }

        /**********************************************************************
         * This function does the actual attack
         * ARGUMENTS:
         * gameObject:  The gameObject that gets attacked
         * tile:        The Tile where the gameObject that gets attacked stands on.
         * ********************************************************************/
        public float Attack(GameObject gameObject, Tile tile)
        {
            // If the gameObject is a prompt give some feedback and return.
            if (gameObject is Prompt)
            {
                Debug.WriteLine("A prompt cannot be attacked");
                return -1;
            }

            // Check if the attacked gameobject is an Unit
            if (gameObject is Unit)
            {
                // Cast the gameObject to an Unit
                Unit unit = gameObject as Unit;

                // Calculate the damageValue
                float damageValue = attackBehaviour.Attack(this, unit) - unit.Defence(tile);

                // Make sure the Unit doesn't heal
                if (damageValue < 0)
                {
                    damageValue = 0;
                }

                // Deal the damage to the enemy unit by decreasing it's health
                unit.AddHealth(-damageValue);

                //Todo show here the damage prompt
                if (unit.Health < 0)
                {
                    unit.destroyed = true;
                }

                // return the damageValue
                return damageValue;
            }

            // Check if the gameObject is a Structure
            if (gameObject is Structure)
            {
                // Cast the gameObject to an Unit
                Structure structure = gameObject as Structure;

                // Decrease the capturePoints of this structure by 10
                structure.AddCapturePoints(-10);

                // Return the damageValue
                return 10;
            }

            // Return -1 because nothing is done
            return -1;
        }

        public float AddHealth(float value)
        {
            // Add the heath of this Unit by the given value
            health += value;

            // Checks if the health of this Unit is zero of below
            // If so set destroyed on true so the Engine will destroy it
            if (health <= 0)
            {
                destroyed = true;
            }
            return health;
        }

        public float Defence(Tile tile)
        {
            return defenceBehaviour.Defence(this, tile);
        }

        /// <summary>
        /// This function checks if a Units attack is allowed and returns a bool
        /// </summary>
        /// <param name="fromLeft">The fromLeft from the tile from where the requested action takes place.</param>
        /// <param name="fromTop">he fromTop from the tile from where the requested action takes place.</param>
        /// <param name="tile">The Tile where the requested action takes place</param>
        /// <param name="targetFromLeft">The fromLeft from the tile where the requested action takes place to</param>
        /// <param name="targetFromTop">The fromTop from the tile where the requested action takes place to</param>
        /// <param name="player">The player of whom the Unit belongs to</param>
        /// <returns>Returns a bool if the attack is allowed</returns>
        public bool CanTarget(float fromLeft, float fromTop, Tile tile, float targetFromLeft, float targetFromTop, Player player)
        {
            // Calculate the movement that will be made
            float horizontalMovement = fromLeft - targetFromLeft;
            float verticalMovement = fromTop - targetFromTop;

            // Calculate the absolute distance
            float distance = Math.Abs(horizontalMovement) + Math.Abs(verticalMovement);

            // If the distance is bigger than the maxMoveDistance, return false.
            if (distance > MovementRange)
            {
                return false;
            }

            // If there is a enemy Unit or enemy Structure on the tile, return true
            if (tile.OccupiedUnit != null && !player.InGameObjects(tile.OccupiedUnit) ||
                tile.OccupiedStructure != null && !player.InGameObjects(tile.OccupiedStructure))
            {
                return true;
            }

            return false;
        }

        public Target AutoMove(World world, bool okay, int steps)
        {
            this.steps = steps;
            if (steps < 0)
            {
                this.steps = 0;
                steps = 0;
            }
            int targetX = 0;
            int targetY = 0;
            for (int x = world.Map.Tiles.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = world.Map.Tiles.GetLength(1) - 1; y >= 0; y--)
                {
                    targetX = x;
                    targetY = y;
                    if (world.Map.Tiles[x, y].OccupiedUnit != this && !found && world.Map.Tiles[x, y].OccupiedUnit != null)
                    {
                        tempTile = world.Map.Tiles[x, y] as Tile;
                        found = true;
                    }
                    if (world.Map.Tiles[x, y].OccupiedUnit == this && found == true)
                    {
                        List<Tile> locations = new List<Tile>
                        {
                            world.Map.GetTile(x - 2, y),            //mostleft
                            world.Map.GetTile(x - 1, y),            //left
                            world.Map.GetTile(x - 1, y - 1),        //topleft
                            world.Map.GetTile(x - 1, y + 1),        //bottomleft
                            world.Map.GetTile(x, y -1),             //top
                            world.Map.GetTile(x + 1, y - 1),        //topright
                            world.Map.GetTile(x + 1, y),            //right
                            world.Map.GetTile(x + 1, y + 1),        //bottomright
                            world.Map.GetTile(x, y + 1),            //bottom
                            world.Map.GetTile(x, y - 2),            //mosttop
                            world.Map.GetTile(x + 2, y),            //mostright
                            world.Map.GetTile(x, y + 2)             //mostbottom
                        };
                        foreach (Tile location in locations)
                        {
                            if (!world.CurrentPlayer.InGameObjects(location.OccupiedUnit) && location.OccupiedUnit != null)
                            {
                                found = false;
                                Attack(location.OccupiedUnit, location);
                                return new Target(x, y);
                            }
                            if (!world.CurrentPlayer.InGameObjects(location.OccupiedStructure) && location.OccupiedStructure != null)
                            {
                                found = false;
                                //Attack(location.OccupiedStructure, location);
                                return new Target(x, y);
                            }
                        }
                        while (steps < movementRange)
                        {
                            if (y - world.Map.GetTileCoords(tempTile).GetFromLeft() < 0 && okay)
                            {
                                //target = new Target(x, y + 1);
                                targetY += 1;
                            }
                            else if (y - world.Map.GetTileCoords(tempTile).GetFromLeft() > 0)
                            {
                                //target = new Target(x, y - 1);
                                targetY -= 1;
                            }
                            else if (x - world.Map.GetTileCoords(tempTile).GetFromTop() < 0)
                            {
                                //target = new Target(x + 1, y);
                                targetX += 1;
                            }
                            else if (x - world.Map.GetTileCoords(tempTile).GetFromTop() > 0)
                            {
                                //target = new Target(x - 1, y);
                                targetX -= 1;
                            }
                            steps++;
                        }

                        tempTile = null;
                        found = false;
                        return new Target(targetX, targetY);
                    }
                }
            }
            return AutoMove(world, okay, this.steps);
        }

        /// <summary>
        /// This function checks if the Unit is allowed to move to the given Tile
        /// </summary>
        /// <param name="tile"> The tile that needs to be checked</param>
        /// <returns> Returns if the movement to this tile is allowed</returns>
        public bool IsTileAllowed(Tile tile)
        {
            return tileBehaviour.IsAllowed(tile);
        }
    }
}
