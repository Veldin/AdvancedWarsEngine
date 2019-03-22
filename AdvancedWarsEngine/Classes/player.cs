﻿using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        private bool isControllable;                      // Checks if this player can be controlled by an actual person
        private List<GameObject> gameObjects;               // All the gameObject that are owned by this player
        private Player nextPlayer;                        // This is the who gets the turn when this players turn ends

        //Holds the selected unit.
        private Unit selectedUnit;                        //Holds the currently selected unit
        private Structure selectedStructure;              //Holds the currently selected structure

        private string colour;

        public Player(bool isControllable = false, string colour = "green")
        {
            this.isControllable = isControllable;
            this.colour = colour;
            gameObjects = new List<GameObject>();
        }

        public bool IsControllable
        {
            get { return isControllable; }
            set { isControllable = value; }
        }

        public Unit SelectedUnit
        {
            get { return selectedUnit; }
            set { selectedUnit = value; }
        }

        public Structure SelectedStructure
        {
            get { return selectedStructure; }
            set { selectedStructure = value; }
        }

        public void AddGameObject(GameObject gameObject)
        {
            // Adds a GameObject to the list gameObjects 
            gameObjects.Add(gameObject);
        }

        // Returns the whole list gameObjects
        public List<GameObject> GetGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();



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
                if (gameObject is Structure)
                {
                    list.Add(gameObject as Structure);
                }
            }

            return list;
        }

        public void AllowedAllToAct()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.IsAllowedToAct = true;
            }
        }

        public void AllowedNoneToAct()
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

        public Player NextPlayer
        {
            get {
                return nextPlayer;
            }
            set { nextPlayer = value; }
        }

        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }
    }
}
