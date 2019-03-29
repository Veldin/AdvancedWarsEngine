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
            IAttackBehaviour attackBehaviour;                                     // The attackBehaviour of the Unit
            IDefenceBehaviour defenceBehaviour;                                   // The defenceBehaviour of the Unit
            ITileBehaviour tileBehaviour;                                         // The tileBehaviour of the Unit
            IOnTickBehaviour onTickBehaviour = new DefaultOnTickBehaviour();       // The default onTick of the Unit
            Unit unit = null;                                                   // The GameObject is the Unit that will be returned

            // Check which Unit should be created and create it
            switch (type)
            {
                case "AA_Infantry":     // Anti-Air Infantry
                    // Create the behaviours for this unit
                    attackBehaviour = new AA_InfantryAttackBehaviour();
                    defenceBehaviour = new InfantryDefenceBehaviour();
                    tileBehaviour = new InfantryTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AA_Infantry.gif", 2, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Infantry);
                    break;

                case "AV_Infantry":     // Anti-Vehicle Infantry
                    // Create the behaviours for this unit
                    attackBehaviour = new AV_InfantryAttackBehaviour();
                    defenceBehaviour = new InfantryDefenceBehaviour();
                    tileBehaviour = new InfantryTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AV_Infantry.gif", 2, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Infantry);
                    break;

                case "AI_Infantry":     // Anti-Infantry Infantry
                    // Create the behaviours for this unit
                    attackBehaviour = new AI_InfantryAttackBehaviour();
                    defenceBehaviour = new InfantryDefenceBehaviour();
                    tileBehaviour = new InfantryTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Infantry/" + colour + "_AI_Infantry.gif", 2, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Infantry);
                    break;

                case "AI_Vehicle":          // Anti_Infantry Vehicle
                    // Create the behaviours for this unit
                    attackBehaviour = new AI_VehicleAttackBehaviour();
                    defenceBehaviour = new VehicleDefenceBehaviour();
                    tileBehaviour = new VehicleTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AI_Vehicle.gif", 3, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Vehicle);
                    break;

                case "AV_Vehicle":          // Anti-Vehicle Vehicle
                    // Create the behaviours for this unit
                    attackBehaviour = new AV_VehicleAttackBehaviour();
                    defenceBehaviour = new VehicleDefenceBehaviour();
                    tileBehaviour = new VehicleTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AV_Vehicle.gif", 3, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Vehicle);
                    break;

                case "AA_Vehicle":          // Anti-Air Vehicle
                    // Create the behaviours for this unit
                    attackBehaviour = new AA_VehicleAttackBehaviour();
                    defenceBehaviour = new VehicleDefenceBehaviour();
                    tileBehaviour = new VehicleTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/" + colour + "_AA_Vehicle.gif", 3, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Vehicle);
                    break;

                case "AI_Air":              // Anti_Infantry Air
                    // Create the behaviours for this unit
                    attackBehaviour = new AI_AirAttackBehaviour();
                    defenceBehaviour = new AirDefenceBehaviour();
                    tileBehaviour = new AirTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AI_Air.gif", 5, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Air);
                    break;

                case "AV_Air":              // Anti-Vehicle Air
                    // Create the behaviours for this unit
                    attackBehaviour = new AV_AirAttackBehaviour();
                    defenceBehaviour = new AirDefenceBehaviour();
                    tileBehaviour = new AirTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AV_Air.gif", 5, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Air);
                    break;

                case "AA_Air":              // Anti-Air Air
                    // Create the behaviours for this unit
                    attackBehaviour = new AA_AirAttackBehaviour();
                    defenceBehaviour = new AirDefenceBehaviour();
                    tileBehaviour = new AirTileBehaviour();

                    // Create the Unit with the created behaviours
                    unit = new Unit(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Air/" + colour + "_AA_Air.gif", 5, attackBehaviour, defenceBehaviour, tileBehaviour, EUnitType.Air);
                    break;
            }

            //Set the onTickBehaviour
            unit.OnTickBehaviour = onTickBehaviour;

            // Return the Unit as GameObject
            return unit;
        }
    }
}
