using AdvancedWarsEngine.Classes.Behaviors;
using System;
using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    abstract class GameObject
    {
        private bool isAllowedToAct;
        private float width;
        private float height;
        private float fromTop;
        private float fromLeft;
        private List<Target> targets;
        private string sprite;
        private Player owner;
        private IOnTickBehavior onTickBehavior;
        
        public GameObject(float width, float height, float fromTop, float fromLeft)
        {
            this.width = width;
            this.height = height;
            this.fromLeft = fromLeft;
            this.fromTop = fromTop;
        }

        public void SetIsAllowedToAct(bool isAllowedToAct)
        {
            this.isAllowedToAct = isAllowedToAct;
        }

        public float GetWidth()
        {
            return width;
        }
        
        public float GetHeight()
        {
            return height;
        }

        public float GetFromTop()
        {
            return fromTop;
        }

        public float GetFromLeft()
        {
            return fromLeft;
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

        public IOnTickBehavior GetOnTickBehavior()
        {
            return onTickBehavior;
        }

        public void SetOnTickBehavior(IOnTickBehavior onTickBehavior)
        {
            this.onTickBehavior = onTickBehavior;
        }

        public bool OnTick(List<GameObject> gameObjects, float delta)
        {
            return true;
        }

        public bool IsColliding(GameObject gameObject)
        {
            return true;
        }

        public bool CollisionEffect(GameObject)
        {
            return true;
        }
    }
}
