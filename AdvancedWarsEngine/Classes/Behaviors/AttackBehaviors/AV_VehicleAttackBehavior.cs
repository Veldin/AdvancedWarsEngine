using AdvancedWarsEngine.Classes.Enums;

namespace AdvancedWarsEngine.Classes
{
    class AV_VehicleAttackBehavior : IAttackBehavior
    {
        public float Attack(Unit unit, GameObject gameObject)
        {
            // Setting some local variables
            float baseValue = 20;                           // The baseValue
            float attackValue = baseValue;                  // The total attack value which will be returned

            // Check if the attacked gameObject is a Unit
            if (gameObject is Unit)
            {
                // Set the gameObject as Unit
                Unit enemyUnit = gameObject as Unit;

                // Check if the unitType is Vehicle. If so increase attackValue by 50%
                if (enemyUnit.UnitType == EUnitType.Vehicle)
                {
                    attackValue += baseValue * 0.5f;
                }
            }
            return attackValue;
        }
    }
}
