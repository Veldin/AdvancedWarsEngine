using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class StructureFactory : IAbstractFactory
    {
        /**********************************************************************
         * This class has one function which returns a Structure as GameObject.
         * ********************************************************************/
        public GameObject GetGameObject(string type, float width, float height, float fromTop, float fromLeft, string colour)
        {
            if (colour != "Yellow" && colour != "Green" && colour != "Blue" && colour != "Red")
            {
                colour = "Yellow";
            }

            // Define some local variables
            IProduceBehavior produceBehavior;                                   // The produceBehavior of the Structure
            IOnTickBehavior onTickBehavior = new DefaultOnTickBehavior();       // The default onTick of the Unit
            GameObject structure = null;                                        // The GameObject is the Structure that will be returned

            Debug.WriteLine(type);

            // Check which Structure shoud be created and returned
            switch (type)
            {
                case "Airport":
                    // Create the behaviors
                    produceBehavior = new AirportProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Airport.gif", produceBehavior);
                    (structure as Structure).ProductionCooldownMax = 8;
                    break;

                case "Barracks":
                    // Create the behaviors
                    produceBehavior = new BarracksProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Barracks.gif", produceBehavior);
                    (structure as Structure).ProductionCooldownMax = 5;
                    structure.HightOffset = 16 / 2;
                    break;

                case "Factory":
                    // Create the behaviors
                    produceBehavior = new WorkshopProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Workshop.gif", produceBehavior);
                    (structure as Structure).ProductionCooldownMax = 7;
                    structure.HightOffset = 16 / 2;
                    break;
            }

            //Set the onTickBehavior
            structure.OnTickBehavior = onTickBehavior;

            // Return the Unit as GameObject
            return structure;
        }
    }
}
