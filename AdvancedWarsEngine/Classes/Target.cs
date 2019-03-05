using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Target
    {
        private float fromTop           { get; set; }
        private float fromLeft          { get; set; }
        private GameObject gameObject   { get; }

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
         * gameObject. If gameObject is null it returns -1.
         * ***************************************************/
        public float GetFromLeft()
        {
            // Check if gameObject exists
            if (gameObject != null)
            {
                // returns the fromTop from the gameObject
                return gameObject.GetFromLeft();
            }
            else
            {
                // Returns -1 because the gameObject doesn't exist
                return -1;
            }
        }


        /******************************************************
         * This function returns the fromTop from the selected
         * gameObject. If gameObject is null it returns -1.
         * ***************************************************/
        public float GetFromTop()
        {
            // Check if gameObject exists
            if (gameObject != null)
            {
                // returns the fromTop from the gameObject
                return gameObject.GetFromTop();
            } else
            {
                // Returns -1 because the gameObject doesn't exist
                return -1;
            }
        }
    }
}
