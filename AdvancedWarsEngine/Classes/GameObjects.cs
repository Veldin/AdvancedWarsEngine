using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AdvancedWarsEngine.Classes
{
    public class GameObjects
    {
        private List<GameObject> gameObjects;

        public GameObjects()
        {
            gameObjects = new List<GameObject>();
        }

        public GameObjects(List<GameObject> list)
        {
            gameObjects = new List<GameObject>();
            foreach (GameObject needle in list)
            {
                gameObjects.Add(needle);
            }
        }

        public List<GameObject> list
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        //Implement gameObject functions
        public void AddWidth(float width)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.AddWidth(width);
            }
        }

        public void AddHeight(float height)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.AddHeight(height);
            }
        }

        public void AddFromTop(float fromTop)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.AddFromTop(fromTop);
            }
        }

        public void AddFromLeft(float fromLeft)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.AddFromLeft(fromLeft);
            }
        }

        public void SetActiveBitmap(string set)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.SetActiveBitmap(set);
            }
        }

        public void OnTick(List<GameObject> gameObjects, float delta)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.OnTick(gameObjects, delta);
            }
        }

        //Default list methods

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public bool Remove(GameObject gameObject)
        {
            return gameObjects.Remove(gameObject);
        }

        public int Count()
        {
            return gameObjects.Count();
        }

        public void AddRange(List<GameObject> list)
        {
            gameObjects.AddRange(list);
        }
    }
}
