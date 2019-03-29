namespace AdvancedWarsEngine.Classes
{
    class VehicleDefenceBehaviour : IDefenceBehaviour
    {
        public float Defence(GameObject unit, Tile tile)
        {
            // Setting some local variables
            float baseValue = 20;                       // The baseValue
            float defenceValue = baseValue;                // The total defence value which will be returned

            // Multiply the defenceValue based on the type of tile it's standing on
            switch (tile.GetType().Name)                    //Todo make enums or something for this
            {
                case "Mountain":                            // Vehicles cannot stand on Mountain
                case "Forest":
                    defenceValue += baseValue * 0.1f;
                    break;
                case "Plain":
                    break;
                case "Urban":
                    defenceValue += baseValue * 0.2f;
                    break;
                case "Water":                               // Vehicles cannot stand on Water
                case "Road":
                    break;
            }
            return defenceValue;
        }
    }
}
