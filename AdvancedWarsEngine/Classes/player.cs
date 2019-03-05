using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Player
    {
        private bool isControllable;
        private bool isTurn;
        private List<GameObject> gameObjects;
        private Player nextPlayer;

        public Player(bool isControllable)
        {
            this.isControllable = isControllable;
            //DO SOMETHING MORE
        }
        
        public bool GetIsTurn()
        {
            return isTurn;
        }

        public void SetIsTurn(bool isTurn)
        {
            this.isTurn = isTurn;
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        public void DeleteGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
        
        public Player GetNextPlayer()
        {
            return nextPlayer;
        }

        public void SetNextPlayer(Player nextPlayer)
        {
            this.nextPlayer = nextPlayer;
        }
    }
}
