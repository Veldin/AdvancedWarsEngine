using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        private float capturePoints;

        public Structure(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft)
        {
            // DO SOMETHING
        }

        public override bool CollisionEffect(GameObject gameObject)
        {
            //Check collision from the left or right.
            if ((gameObject.FromLeft + gameObject.Width) > (FromLeft + Width))
            {
                AddFromLeft(-1);
            }
            else if ((gameObject.FromLeft + gameObject.Width) < (FromLeft + Width))
            {
                AddFromLeft(1);
            }

            //Check collision from top or bottom.
            if ((gameObject.FromTop + gameObject.Height) > (FromTop + Height))
            {
                AddFromTop(-1);
            }
            else if ((gameObject.FromTop + gameObject.Height) < (FromTop + Height))
            {
                AddFromTop(1);
            }
        

            //If a player is coliding with an object their CollisionEffect is triggered instantly and not after this resolves.
            //This is so the collision of the enemy still goes even though they are not colliding anymore.
            gameObject.CollisionEffect(this);
            return true;
        }

        public float GetCapturePoints()
        {
            return capturePoints;
        }

        public override bool OnTick(List<GameObject> gameObjects, float delta)
        {
            throw new NotImplementedException();
        }

        public void SetCapturePoints(float capturePoints)
        {
            this.capturePoints = capturePoints;
        }
    }
}
