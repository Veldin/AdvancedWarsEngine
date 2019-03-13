using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class StructureFactory : IAbstractFactory
    {
        /**********************************************************************
         * This class has one function which returns a Structure as GameObject.
         * ********************************************************************/
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {
            // Define some local variables
            IProduceBehavior    produceBehavior;                // The produceBehavior of the Structure
            GameObject          structure           = null;     // The GameObject is the Structure that will be returned

            // Check which Structure shoud be created and returned
            switch (value)
            {
                case "Airport":
                    // Create the behaviors
                    produceBehavior = new AirportProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Airport", produceBehavior);
                    break;

                case "Barracks":
                    // Create the behaviors
                    produceBehavior = new BarracksProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Barracks", produceBehavior);
                    break;
\
                case "Factory":
                    // Create the behaviors
                    produceBehavior = new FactoryProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Factory", produceBehavior);
                    break;
            }

            // If there is no structure give feedback and return null
            if (structure == null)
            {
                Debug.WriteLine("No structure is created because there is no structure with that name.");
                return null;
            }

            // Return the structure as a GameObject
            return structure;
        }
    }
}
