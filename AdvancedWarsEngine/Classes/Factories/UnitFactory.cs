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
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {

            // Check which Unit shoud be created and returned
            switch (value)
            {
                case "AA_Infantry":     // Anti-Air Infantry

                    // Get the sprite
                    BitmapImage sprite = Textures.textures["AA_Infantry"];

                    // If the sprite is missing, give feedback and return null
                    if (sprite == null)
                    {
                        Debug.WriteLine("There is a texture missing!");
                        return null;
                    }

                    // Create the behaviors for this unit
                    ITargetableBehavior targetableBehavior  = new AA_InfantryTargetableBehavior();
                    IAttackBehavior     attackBehavior      = new AA_InfantryAttackBehavior();
                    IDefenceBehavior    defenceBehavior     = new AA_InfantryDefenceBehavior();
                    IHealthBehavior     healthBehavior      = new AA_InfantryHealthBehavior();
                    IMovementBehavior   movementBehavior    = new AA_InfantryMovementBehavior();
                    // IRangeBehavior   rangebehavior       = new behavior

                    // Create the Unit with the created behaviors
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft, sprite, targetableBehavior, attackBehavior, healthBehavior, movementBehavior, defenceBehavior);

                    // Return the Unit
                    return infantry;
               /* case "AV_Infantry":
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft);
                    return infantry;
                case "AI_Infantry":
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft);
                    return infantry;
                case "AI_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AV_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AA_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AI_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;
                case "AV_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;
                case "AA_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;*/
            }
            
            // Give feedback and return null
            Debug.WriteLine("No unit is created because there is no unit with that name.");
            return null;
        }
    }
}
