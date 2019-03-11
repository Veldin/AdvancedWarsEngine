using AdvancedWarsEngine.Classes.Behaviors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    abstract class GameObject
    {
        protected bool isAllowedToAct;
        protected float width;
        protected float height;
        protected float fromTop;
        protected float fromLeft;
        private List<Target> targets;
        protected BitmapImage sprite;
        protected Player owner;
        protected IOnTickBehavior onTickBehavior;
        
        public GameObject(float width, float height, float fromTop, float fromLeft)
        {
            this.width = width;
            this.height = height;
            this.fromLeft = fromLeft;
            this.fromTop = fromTop;
        }

        public bool IsAllowedToAct
        {
            get { return isAllowedToAct; }
            set { isAllowedToAct = value; }
        }

        public void AddTarget(Target target)
        {
            targets.Add(target);
        }

        public void DeleteTarget(Target target)
        {
            targets.Remove(target);
        }

        public List<Target> GetTargets()
        {
            return targets;
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }
        
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float FromTop
        {
            get { return fromTop; }
            set { fromTop = value; }
        }

        public float FromLeft
        {
            get { return fromLeft; }
            set { fromLeft = value; }
        }

        public BitmapImage Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public IOnTickBehavior OnTickBehavior
        {
            get { return onTickBehavior; }
            set { onTickBehavior = value; }
        }

        public float AddWidth(float width)
        {
            return this.width += width;
        }

        public float AddHeight(float height)
        {
            return this.height += height;
        }

        public float AddFromTop(float fromTop)
        {
            return this.fromTop += fromTop;
        }

        public float AddFromLeft(float fromLeft)
        {
            return this.fromLeft += fromLeft;
        }

        public double CalculateDistance(GameObject gameObject1, GameObject gameObject2)
        {
            // Get the horizontal and vertical distance between the two gameobjects
            float horizontalDistance = (gameObject2.FromLeft + gameObject2.Width / 2) - (gameObject1.FromLeft + gameObject1.Width / 2);
            float verticalDistance = (gameObject2.FromTop + gameObject2.Height / 2) - (gameObject1.FromTop + gameObject2.Height / 2);

            // Using pythagoras theorem to calculate the distance
            float diagonalDistance = (horizontalDistance * horizontalDistance) + (verticalDistance * verticalDistance);
            double distance = Math.Sqrt(diagonalDistance);

            return distance;
        }

        //Any object can edit the gameObjects of the game while the logic is running.
        //And Also get the delta for timed events.
        public bool OnTick(List<GameObject> gameObjects, float delta)
        {
            return onTickBehavior.OnTick();
        }

        /* IsColliding */
        /*
         * Checks whether or not this gameobject is coliding with the given gameOjbect
         * The argument is the given gameObject
        */
        public bool IsColliding(GameObject gameObject)
        {
            //Check if you are comparing to youself.
            if (this == gameObject)
            {
                return false;
            }

            if (fromLeft < gameObject.fromLeft + gameObject.width && fromLeft + width > gameObject.fromLeft)
            {
                if (fromTop < gameObject.fromTop + gameObject.height && fromTop + height > gameObject.fromTop)
                {
                    return true;
                }
            }
            return false;
        }

        /* CollisionEffect */
        /*
         * Effect that happens when this GameObject collides with the given object.
         * The argument is the given gameObject
        */
        public bool CollisionEffect(GameObject gameObject)
        {
            //Check if you are comparing to youself.
            if (this == gameObject)
            {
                return false;
            }
            return true;
        }
    }
}
