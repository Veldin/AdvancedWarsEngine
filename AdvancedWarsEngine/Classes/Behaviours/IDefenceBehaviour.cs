namespace AdvancedWarsEngine.Classes
{
    interface IDefenceBehaviour
    {
        /****************************************************
         * Unit     = The Unit that defends
         * Tile     = The Tile were the Unit stands on
         * **************************************************/
        float Defence(GameObject unit, Tile tile);
    }
}