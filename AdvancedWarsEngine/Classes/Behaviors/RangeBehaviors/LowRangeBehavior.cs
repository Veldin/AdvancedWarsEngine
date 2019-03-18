using AdvancedWarsEngine.Classes.Enums;
using System;

namespace AdvancedWarsEngine.Classes
{
    class LowRangeBehavior : IRangeBehavior
    {
        public bool Range(Unit unit, float fromLeft, float fromTop, Tile targetTile, float targetFromLeft, float targetFromTop)
        {
            //**************************************************DISTANCE CHECK****************************************************
            // Setting some base values
            float maxMoveDistance = 2;

            // Calculate the movement that will be made
            float horizontalMovement = fromLeft - targetFromLeft;
            float verticalMovement = fromTop - targetFromTop;

            // Calculate the absolute distance
            float distance = Math.Abs(horizontalMovement) + Math.Abs(verticalMovement);

            // If the distance is bigger than the maxMoveDistance, return false.
            if (distance > maxMoveDistance)
            {
                return false;
            }

            //**************************************************OCCUPIED TILE CHECK***********************************************
            // If there is a Unit or Structure on the tile, return true
            if (targetTile.OccupiedUnit != null || targetTile.OccupiedStructure != null)
            {
                return true;
            }

            //**************************************************TILE TYPE CHECK***************************************************
            // Check which type of tile the targetTile is and if the unit is allowed to move to that tile.
            switch (targetTile.GetType().Name)
            {
                case "Mountain":
                    if (unit.UnitType == EUnitType.Vehicle) { return false; }
                    break;
                case "Plain":
                    break;
                case "Forest":
                    break;
                case "Water":
                    if (unit.UnitType == EUnitType.Vehicle) { return false; }
                    if (unit.UnitType == EUnitType.Infantry) { return false; }
                    break;
                case "Urban":
                    break;
                case "Road":
                    break;
            }

            return true;
        }
    }
}
