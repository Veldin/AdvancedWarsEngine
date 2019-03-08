using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Target
    {
        private float fromTop;
        private float fromLeft;
        private GameObject gameObject;

        /******************************************************
         * This is the constructor for when the target are just
         * coordinates.
         * ***************************************************/
        public Target(float fromTop, float fromLeft)
        {
            this.fromTop = fromTop;
            this.fromLeft = fromLeft;
        }

        /******************************************************
         * This is the constructor for when the target is a 
         * gameObject.
         * ***************************************************/
        public Target(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        /******************************************************
         * This function returns the fromLeft from the selected
         * gameObject. If gameObject is null it returns the fromLeft
         * from this class.
         * ***************************************************/
        public float GetFromLeft()
        {
            // Check if gameObject exists
            if (gameObject != null)
            {
                // returns the fromLeft from the gameObject
                return gameObject.FromLeft;
            }
            else
            {
                // Returns fromLeft because the gameObject doesn't exist
                return fromLeft;
            }
        }

        /******************************************************
         * This function adds a value to fromLeft.
         * ***************************************************/
        public void AddFromLeft(float fromLeft)
        {
            // Add the new value
            this.fromLeft += fromLeft;
        }

        /******************************************************
         * This function returns the fromTop from the selected
         * gameObject. If gameObject is null it returns the fromTop
         * from this class.
         * ***************************************************/
        public float GetFromTop()
        {
            // Check if gameObject exists
            if (gameObject != null)
            {
                // returns the fromTop from the gameObject
                return gameObject.FromTop;
            } else
            {
                // Returns fromTop because the gameObject doesn't exist
                return fromTop;
            }
        }

        /******************************************************
         * This function adds a value to fromLeft.
         * ***************************************************/
        public void AddFromTop(float fromTop)
        {
            // Add the new value
            this.fromTop = fromTop;
        }
    }
}
