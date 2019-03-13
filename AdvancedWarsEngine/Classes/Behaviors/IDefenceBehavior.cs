namespace AdvancedWarsEngine.Classes
{
    interface IDefenceBehavior
    {
        /****************************************************
         * Unit     = The Unit that defends
         * Tile     = The Tile were the Unit stands on
         * **************************************************/
        float Defence(Unit unit, Tile tile);
    }
}