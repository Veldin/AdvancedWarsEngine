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
            IOnTickBehaviour onTickBehaviour = new DefaultOnTickBehaviour();       // The default onTick of the Unit
            GameObject structure = null;                                        // The GameObject is the Structure that will be returned

            // Check which Structure shoud be created and returned
            switch (type)
            {
                case "Airport":
                    // Create the behaviours
                    produceBehaviour = new AirportProduceBehaviour();

                    // Create the Structure with the created behaviours
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Airport.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 8;
                    break;

                case "Barracks":
                    // Create the behaviours
                    produceBehaviour = new BarracksProduceBehaviour();

                    // Create the Structure with the created behaviours
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Barracks.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 5;
                    break;

                case "HQ":
                    // Create the behaviours
                    produceBehaviour = new HQProduceBehaviour();

                    // Create the Structure with the created behaviours
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_HQ.gif", produceBehaviour);
                    break;

                case "Factory":
                    // Create the behaviours
                    produceBehaviour = new WorkshopProduceBehaviour();

                    // Create the Structure with the created behaviours
                    structure = new Structure(width, height, fromTop, fromLeft, "Sprites/Structures/" + colour + "_Workshop.gif", produceBehaviour);
                    (structure as Structure).HightOffset = 7;
                    break;
            }

            //Set the onTickBehaviour
            structure.OnTickBehaviour = onTickBehaviour;

            // Return the Unit as GameObject
            return structure;
        }
    }
}
