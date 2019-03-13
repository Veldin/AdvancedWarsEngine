using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class UnitFactory : IAbstractFactory
    {
        /**********************************************************************
         * This class has one function which returns a Unit as GameObject.
         * ********************************************************************/
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {
            // Define some local variables
            IRangeBehavior      rangeBehavior;                  // The rangeBehavior of the Unit
            IAttackBehavior     attackBehavior;                 // The attackBehavior of the Unit
            IDefenceBehavior    defenceBehavior;                // The defenceBehavrior of the Unit
            GameObject          unit                = null;     // The GameObject is the Unit that will be returned

            // Check which Unit should be created and create it
            switch (value)
            {
                case "AA_Infantry":     // Anti-Air Infantry
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AA_Infantry", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AV_Infantry":     // Anti-Vehicle Infantry
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AV_Infantry", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                    case "AI_Infantry":     // Anti-Infantry Infantry
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AI_Infantry", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AI_Vehicle":          // Anti_Infantry Vehicle
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "IV_Vehicle", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AV_Vehicle":          // Anti-Vehicle Vehicle
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AV_Vehicle", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AA_Vehicle":          // Anti-Air Vehicle
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AA_Vehicle", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AI_Air":              // Anti_Infantry Air
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AI_Air", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AV_Air":              // Anti-Vehicle Air
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AV_Air", rangeBehavior, attackBehavior, defenceBehavior);
                    break;

                case "AA_Air":              // Anti-Air Air
                    // Create the behaviors for this unit
                    rangeBehavior   = new InfantryRangeBehavior();
                    attackBehavior  = new AA_InfantryAttackBehavior();
                    defenceBehavior = new AA_InfantryDefenceBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "AA_Air", rangeBehavior, attackBehavior, defenceBehavior);
                    break;
            }

            // If there is no Unit give feedback and return null
            if (unit == null)
            {
                Debug.WriteLine("No unit is created because there is no unit with that name.");
                return null;
            }

            // Return the Unit as GameObject
            return unit;
        }
    }
}
