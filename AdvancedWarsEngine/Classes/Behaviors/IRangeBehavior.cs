namespace AdvancedWarsEngine.Classes
{
    interface IRangeBehavior
    {
        /***********************************************************************************
         * Unit             = The Unit that wants to move.
         * fromLeft         = The fromTop of the current Tile where the Unit is standing on.
         * fromTop          = The fromTop of the current Tile where the Unit is standing on.
         * targetTile       = Tile the Unit wants to move to
         * targetFromLeft   = The fromLeft of the tile the Unit wants to move to.
         * targetFromTop    = The fromTop of the tile the Unit wants to move to.
         * *********************************************************************************/
        bool Range(Unit unit, float fromLeft, float fromTop, Tile targetTile, float targetFromLeft, float targetFromTop);
    }
}