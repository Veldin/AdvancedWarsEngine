namespace AdvancedWarsEngine.Classes
{
    class InfantryDefenceBehavior : IDefenceBehavior
    {
        public float Defence(GameObject unit, Tile tile)
        {
            // Setting some local variables
            float baseValue = 10;                       // The baseValue
            float defenceValue = baseValue;                // The total defence value which will be returned

            // Multiply the defenceValue based on the type of tile it's standing on
            switch (tile.GetType().Name)     //Todo make enums or something for this
            {
                case "Mountain":
                    break;
                case "Forest":
                    defenceValue += baseValue * 0.5f;
                    break;
                case "Plain":
                    break;
                case "Urban":
                    defenceValue += baseValue * 0.2f;
                    break;
                case "Water":                           // Infantry cannot stand on Water
                case "Road":
                    break;
            }
            return defenceValue;
        }
    }
}
