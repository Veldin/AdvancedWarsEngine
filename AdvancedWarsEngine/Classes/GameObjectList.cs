using System.Collections.Generic;
using System.Linq;

namespace AdvancedWarsEngine.Classes
{
    public class GameObjectList
    {
        protected List<GameObject> gameObjects;

        public GameObjectList()
        {
            gameObjects = new List<GameObject>();
        }

        public GameObjectList(List<GameObject> list)
        {
            gameObjects = new List<GameObject>();
            foreach (GameObject gameObject in list)
            {
                gameObjects.Add(gameObject);
            }
        }

        public List<GameObject> List
        {
            get { return gameObjects; }
            protected set { gameObjects = value; }
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
                if (!(gameObject is null))
                {
                    gameObject.OnTick(gameObjects, delta);
                }
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

        public void Clear()
        {
            gameObjects.Clear();
        }

        public void AddRange(List<GameObject> list)
        {
            gameObjects.AddRange(list);
        }
    }
}
