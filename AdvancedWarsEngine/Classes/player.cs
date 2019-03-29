using System.Collections.Generic;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        protected bool isControllable;                      // Checks if this player can be controlled by an actual person
        private List<GameObject> gameObjects;               // All the gameObject that are owned by this player
        protected Player nextPlayer;                        // This is the who gets the turn when this players turn ends
        protected bool isDefeated;                          // Checks if the players is already defeated

        //Holds the selected unit.
        private List<Unit> selectedUnit;                    //Holds the currently selected unit. It's a list because its easier with deselecting
        protected Structure selectedStructure;                //Holds the currently selected structure
        protected string colour;

        public Player(bool isControllable = false, string colour = "green")
        {
            this.isControllable = isControllable;
            isDefeated = false;
            this.colour = colour;
            gameObjects = new List<GameObject>();
            selectedUnit = new List<Unit>();
        }

        public bool IsControllable
        {
            get { return isControllable; }
            set { isControllable = value; }
        }

        public Player NextPlayer
        {
            get { return nextPlayer; }
            set { nextPlayer = value; }
        }

        public bool IsDefeated
        {
            get { return isDefeated; }
            set { isDefeated = value; }
        }

        /// <summary>
        /// This is the getter and setter of the selectedUnit list. It can only hold one unit.
        /// It's a list because it can be cleared without setting the Unit to null
        /// </summary>
        public Unit SelectedUnit
        {
            get
            {
                if (selectedUnit.Count == 1)
                {
                    return selectedUnit[0];
                }
                else if (selectedUnit.Count == 0)
                {
                    return null;
                }
                else
                {
                    selectedUnit.Clear();
                    return null;
                }
            }
            set
            {
                selectedUnit.Clear();
                selectedUnit.Add(value);
            }
        }

        public Structure SelectedStructure
        {
            get { return selectedStructure; }
            set { selectedStructure = value; }
        }

        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// Adds a GameObject to the list gameObjects 
        /// </summary>
        /// <param name="gameObject"> The gameObject that will be added to the list</param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        /// <summary>
        /// Returns the whole list gameObjects that the player owns
        /// </summary>
        /// <returns>The list of gameObjects that the player owns</returns>
        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        /* 
        * Returns all the structures in the gameObject list
        * Optional argument to only return structures of a certain player
        */
        public List<Structure> GetStructures()
        {
            List<Structure> list = new List<Structure>();

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Structure && !gameObject.Destroyed)
                {
                    list.Add(gameObject as Structure);
                }
            }
            return list;
        }

        public void AllowAllToAct()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.IsAllowedToAct = true;
            }
        }

        public void AllowNoneToAct()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.IsAllowedToAct = false;
            }
        }

        public bool HasAllowedUnits()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.IsAllowedToAct)
                {
                    return true;
                }
            }
            return false;
        }

        public bool InGameObjects(GameObject search)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (search == gameObject)
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteGameObject(GameObject gameObject)
        {
            // Deletes a GameObject from the list gameObjects
            gameObjects.Remove(gameObject);
        }

        /// <summary>
        /// Deselect the selectedUnit by clearing the list
        /// </summary>
        public void DeselectUnit()
        {
            selectedUnit.Clear();
        }
    }
}
