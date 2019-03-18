﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        protected bool isControllable;                      // Checks if this player can be controlled by an actual person
        private List<GameObject> gameObjects;               // All the gameObject that are owned by this player
        protected Player nextPlayer;                        // This is the who gets the turn when this players turn ends

        //Holds the selected unit.
        private Unit selectedUnit;                          //Holds the currently selected unit
        private Structure selectedStructure;                        //Holds the currently selected structure

        public Player(bool isControllable)
        {
            this.isControllable = isControllable;
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

        public List<GameObject> GetGameObjects()
        {
            // Returns the whole list gameObjects
            return gameObjects;
        }

        public void AllowedAllToAct()
        {
            foreach (GameObject needle in gameObjects)
            {
                needle.IsAllowedToAct = true;
            }
        }

        public void AllowedNoneToAct()
        {
            foreach (GameObject needle in gameObjects)
            {
                needle.IsAllowedToAct = false;
            }
        }

        public bool InGameObjects(GameObject search)
        {
            foreach (GameObject needle in gameObjects)
            {
                if (search == needle)
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
            get { return nextPlayer; }
            set { nextPlayer = value; }
        }



    }
}
