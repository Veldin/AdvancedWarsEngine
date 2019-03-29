using AdvancedWarsEngine.Classes.Enums;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class UnitFactory : IAbstractFactory
    {
        /**********************************************************************
         * This class has one function which returns a Unit as GameObject.
         * ARGUMENTS:
         * value: The Unit preset
         * width: The width of the Unit
         * height: The height of the Unit
         * fromTop: The y position of the Unit
         * fromLeft: The x position of the Unit
         * ********************************************************************/
        public GameObject GetGameObject(string type, float width, float height, float fromTop, float fromLeft, string colour = "Green")
        {
            if (colour == "")
            {
                colour = "Green";
            }

            // Define some local variables
            IAttackBehavior attackBehavior;                                     // The attackBehavior of the Unit
            IDefenceBehavior defenceBehavior;                                   // The defenceBehavior of the Unit
            ITileBehavior tileBehavior;                                         // The tileBehavior of the Unit
            IOnTickBehavior onTickBehavior = new DefaultOnTickBehavior();       // The default onTick of the Unit
            Unit unit = null;                                                   // The GameObject is the Unit that will be returned

            // Check which Unit should be created and create it
            Debug.WriteLine(type);
            switch (type)
            {
                case "AA_Infantry":     // Anti-Air Infantry
                    // Create the behaviors for this unit
                    attackBehavior = new AA_InfantryAttackBehavior();
                    defenceBehavior = new InfantryDefenceBehavior();
                    tileBehavior = new InfantryTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AA_Infantry.gif", 2, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Infantry);
                    break;

                case "AV_Infantry":     // Anti-Vehicle Infantry
                    // Create the behaviors for this unit
                    attackBehavior = new AV_InfantryAttackBehavior();
                    defenceBehavior = new InfantryDefenceBehavior();
                    tileBehavior = new InfantryTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AV_Infantry.gif", 2, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Infantry);
                    break;

                case "AI_Infantry":     // Anti-Infantry Infantry
                    // Create the behaviors for this unit
                    attackBehavior = new AI_InfantryAttackBehavior();
                    defenceBehavior = new InfantryDefenceBehavior();
                    tileBehavior = new InfantryTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AI_Infantry.gif", 2, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Infantry);
                    break;

                case "AI_Vehicle":          // Anti_Infantry Vehicle
                    // Create the behaviors for this unit
                    attackBehavior = new AI_VehicleAttackBehavior();
                    defenceBehavior = new VehicleDefenceBehavior();
                    tileBehavior = new VehicleTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AI_Vehicle.gif", 3, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Vehicle);
                    break;

                case "AV_Vehicle":          // Anti-Vehicle Vehicle
                    // Create the behaviors for this unit
                    attackBehavior = new AV_VehicleAttackBehavior();
                    defenceBehavior = new VehicleDefenceBehavior();
                    tileBehavior = new VehicleTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AV_Vehicle.gif", 3, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Vehicle);
                    break;

                case "AA_Vehicle":          // Anti-Air Vehicle
                    // Create the behaviors for this unit
                    attackBehavior = new AA_VehicleAttackBehavior();
                    defenceBehavior = new VehicleDefenceBehavior();
                    tileBehavior = new VehicleTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AA_Vehicle.gif", 3, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Vehicle);
                    break;

                case "AI_Air":              // Anti_Infantry Air
                    // Create the behaviors for this unit
                    attackBehavior = new AI_AirAttackBehavior();
                    defenceBehavior = new AirDefenceBehavior();
                    tileBehavior = new AirTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AI_Air.gif", 5, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Air);
                    break;

                case "AV_Air":              // Anti-Vehicle Air
                    // Create the behaviors for this unit
                    attackBehavior = new AV_AirAttackBehavior();
                    defenceBehavior = new AirDefenceBehavior();
                    tileBehavior = new AirTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AV_Air.gif", 5, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Air);
                    break;

                case "AA_Air":              // Anti-Air Air
                    // Create the behaviors for this unit
                    attackBehavior = new AA_AirAttackBehavior();
                    defenceBehavior = new AirDefenceBehavior();
                    tileBehavior = new AirTileBehavior();

                    // Create the Unit with the created behaviors
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AA_Air.gif", 5, attackBehavior, defenceBehavior, tileBehavior, EUnitType.Air);
                    break;
            }

            //Set the onTickBehavior
            unit.OnTickBehavior = onTickBehavior;

            // Return the Unit as GameObject
            return unit;
        }
    }
}
