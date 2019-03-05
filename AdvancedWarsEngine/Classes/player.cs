using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        private bool isControllable;                    // Checks if this player can be controlled by an actual person
        private bool isTurn;                            // Checks if it's this players its turn
        private List<GameObject> gameObjects;           // All the gameObject that are owned by this player
        private Player nextPlayer;                      // This is the who gets the turn when this players turn ends

        public Player(bool isControllable)
        {
            this.isControllable = isControllable;
            this.isTurn         = false;
        }
        
        public bool GetIsTurn()
        {
            // Returns the bool isTurn
            return isTurn;
        }

        public void SetIsTurn(bool isTurn)
        {
            // Set the bool isTurn
            this.isTurn = isTurn;
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
        
        public Player GetNextPlayer()
        {
            // Returns the nextPlayer
            return nextPlayer;
        }

        public void SetNextPlayer(Player nextPlayer)
        {
            // Sets the nextPlayer
            this.nextPlayer = nextPlayer;
        }
    }
}
