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
            IProduceBehaviour produceBehaviour;                                   // The produceBehaviour of the Structure
            IOnTickBehavior onTickBehavior = new DefaultOnTickBehavior();       // The default onTick of the Unit
            GameObject structure = null;                                        // The GameObject is the Structure that will be returned

            // Check which Structure shoud be created and returned
            switch (type)
            {
                case "Airport":
                    // Create the behaviors
                    produceBehaviour = new AirportProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Airport.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 8;
                    break;

                case "Barracks":
                    // Create the behaviors
                    produceBehaviour = new BarracksProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Barracks.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 5;
                    break;

                case "HQ":
                    // Create the behaviors
                    produceBehaviour = new HQProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_HQ.gif", produceBehaviour);
                    break;

                case "Factory":
                    // Create the behaviors
                    produceBehaviour = new WorkshopProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Workshop.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 7;
                    break;
            }

            //Set the onTickBehavior
            structure.OnTickBehavior = onTickBehavior;

            // Return the Unit as GameObject
            return structure;
        }
    }
}
