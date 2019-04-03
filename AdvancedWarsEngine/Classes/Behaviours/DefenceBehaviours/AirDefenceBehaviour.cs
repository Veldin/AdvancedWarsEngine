namespace AdvancedWarsEngine.Classes
{
    class AirDefenceBehaviour : IDefenceBehaviour
    {
        public float Defence(GameObject unit, Tile tile)
        {
            // Setting some local variables
            float baseValue = 5;                            // The baseValue
            float defenceValue = baseValue;                 // The total defence value which will be returned

            // Multiply the defenceValue based on the type of tile it's standing on
            switch (tile.GetType().Name)                    //Todo make enums or something for this
            {
                case "Mountain":
                    defenceValue += baseValue * tile.GetDefenceValue("air");
                    break;
                case "Forest":
                    break;
                case "Plain":
                    break;
                case "Urban":
                    break;
                case "Water":
                    break;
                case "Road":
                    break;
            }
            return defenceValue;
        }
    }
}
