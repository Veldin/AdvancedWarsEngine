using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdvancedWarsEngine.Classes
{
    public abstract class GameObject
    {
        protected bool isAllowedToAct;
        protected float width;
        protected float height;
        protected float fromTop;
        protected float fromLeft;

        protected float hightOffset;

        protected Target target;

        protected IOnTickBehavior onTickBehavior;

        public Rectangle rectangle;
        public string assemblyName;
        
        public bool destroyed;

        //The sprite location and the CanvasBitmap are stored seperatly
        //This is so the location gets changed more times in a frame the canvasBitmap doesn't have to get loaded more then once a frame.
        //protected CanvasBitmap sprite;
        protected string location;

        public GameObject(float width, float height, float fromTop, float fromLeft, string location = "Sprites/Units/Icons/Infantry/Green_AA_Infantry.png") //todo Change the default location
        {
            this.width = width;
            this.height = height;
            this.fromLeft = fromLeft;
            this.fromTop = fromTop;

            this.hightOffset = 0;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                rectangle = new Rectangle();
            }));

            assemblyName = "AdvancedWarsEngine";

            SetActiveBitmap(location);
            this.location = location;
        }

        public bool IsAllowedToAct
        {
            get { return isAllowedToAct; }
            set { isAllowedToAct = value; }
        }

        public Target Target
        {
            get { return target; }
            set { target = value; }
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

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public float HightOffset
        {
            get { return hightOffset; }
            set { hightOffset = value; }
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

        public BitmapImage GetActiveBitmap(string assemblyName, bool reload = false)
        {
            if (!Textures.textures.ContainsKey(location))
            {
                BitmapImage newBitmap = new BitmapImage(new Uri("pack://application:,,,/" + assemblyName + ";component/" + location, UriKind.Absolute));
                Textures.textures.Add(location, newBitmap);

                return newBitmap;
            }
            else
            {
                return Textures.textures[location];
            }
        }

        public bool SetActiveBitmap(string set)
        {
            if (location != set)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    location = set;
                    rectangle.Fill = new ImageBrush
                    {
                        ImageSource = GetActiveBitmap(assemblyName)
                    };
                });
                return true;
            }
            return false;
        }

        //Any object can edit the gameObjects of the game while the logic is running.
        //And Also get the delta for timed events.
        public bool OnTick(List<GameObject> gameObjects, float delta)
        {

            if (onTickBehavior == null)
            {
                return false;
            }
            return onTickBehavior.OnTick(this, gameObjects, delta);
            
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
