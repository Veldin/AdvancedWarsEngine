using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        protected bool isControllable;                    // Checks if this player can be controlled by an actual person
        protected bool isTurn;                            // Checks if it's this players its turn
        private List<GameObject> gameObjects;           // All the gameObject that are owned by this player
        protected Player nextPlayer;                      // This is the who gets the turn when this players turn ends

        public Player(bool isControllable)
        {
            this.isControllable = isControllable;
            this.isTurn         = false;
        }
        
        public bool IsTurn
        {
            get { return isTurn; }
            set { isTurn = value; }
        }

        public bool IsControllable
        {
            get { return isControllable; }
            set { isControllable = value; }
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
