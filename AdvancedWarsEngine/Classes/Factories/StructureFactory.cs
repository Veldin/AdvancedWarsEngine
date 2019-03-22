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
            IProduceBehavior produceBehavior;   // The produceBehavior of the Structure
            IOnTickBehavior onTickBehavior =    new DefaultOnTickBehavior();       // The default onTick of the Unit
            GameObject structure = null;        // The GameObject is the Structure that will be returned

            // Check which Structure shoud be created and returned
            switch (type)
            {
                case "Airport":
                    // Create the behaviors
                    produceBehavior = new AirportProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AV_Air.png", produceBehavior);
                    break;

                case "Barracks":
                    // Create the behaviors
                    produceBehavior = new BarracksProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AV_Infantry.png", produceBehavior);
                    break;

                case "Factory":
                    // Create the behaviors
                    produceBehavior = new WorkshopProduceBehavior();

                    // Create the Structure with the created behaviors
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AV_Vehicle.png", produceBehavior);
                    break;
            }

            //Set the onTickBehavior
            structure.OnTickBehavior = onTickBehavior;

            // Return the Unit as GameObject
            return structure;
        }
    }
}
