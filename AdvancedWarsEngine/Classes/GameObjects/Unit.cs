using AdvancedWarsEngine.Classes.Enums;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        protected float health = 100;                     // The health of the Unit
        protected float movementSpeed = 100;              // The movement speed of the Unit (for animations)

        protected IRangeBehavior rangeBehavior;              // The rangeBehavior calculates the range of the Unit
        protected IAttackBehavior attackBehavior;             // The attackBehavior calculates the dammageValue
        protected IDefenceBehavior defenceBehavior;            // The defenceBehavior calculates the defenceValue
        protected EUnitType unitType;                   // The unitType specifice the type of this Unit for example infantry or vehicle

        public Unit(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft, "Sprites/Units/Icons/Vehicle/Green_AV_Vehicle2.gif")
        {
            unitType = EUnitType.Vehicle;
        }

        public Unit(float width, float height, float fromTop, float fromLeft, string sprite, IRangeBehavior rangeBehavior, IAttackBehavior attackBehavior, IDefenceBehavior defenceBehavior, EUnitType unitType)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.rangeBehavior = rangeBehavior;
            this.attackBehavior = attackBehavior;
            this.defenceBehavior = defenceBehavior;
            this.unitType = unitType;

        }

        /**********************************************************************
         * This function does the actual attack
         * ARGUMENTS:
         * gameObject:  The gameObject that gets attacked
         * tile:        The Tile where the gameObject that gets attacked stands on.
         * ********************************************************************/
        public float Attack(GameObject gameObject, Tile tile)
        {
            // If the gameObject is a prompt give some feedback and return.
            if (gameObject is Prompt)
            {
                Debug.WriteLine("A prompt cannot be attacked");
                return -1;
            }

            // Check if the attacked gameobject is an Unit
            if (gameObject is Unit)
            {
                // Cast the gameObject to an Unit
                Unit unit = gameObject as Unit;

                // Calculate the damageValue
                float damageValue = attackBehavior.Attack(this, unit) - unit.Defence(tile);

                // Deal the damage to the enemy unit by decreasing it's health
                unit.AddHealth(-damageValue);

                if (unit.Health < 0)
                {
                    destroyed = true;
                }

                // return the damageValue
                return damageValue;
            }

            // Check if the gameObject is a Structure
            if (gameObject is Structure)
            {
                // Cast the gameObject to an Unit
                Structure structure = gameObject as Structure;

                // Decrease the capturePoints of this structure by 10
                structure.AddCapturePoints(-10);

                // Return the damageValue
                return 10;
            }

            // Return -1 because nothing is done
            return -1;
        }

        public float MovementSpeed
        {
            get { return movementSpeed; }
        }

        public float Defence(Tile tile)
        {
            return defenceBehavior.Defence(this, tile);
        }

        public float Health
        {
            get { return health; }
        }

        public EUnitType UnitType
        {
            get { return unitType; }
        }

        public float AddHealth(float value)
        {
            // Add the heath of this Unit by the given value
            health += value;

            // Checks if the health of this Unit is zero of below
            // If so set destroyed on true so the Engine will destroy it
            if (health <= 0)
            {
                destroyed = true;
            }
            return health;
        }

        /**********************************************************************
         * This function checks if the requested action is allowed and returns a bool
         * ARGUMENTS:
         * fromFromLeft:    The fromLeft from the tile from where the requested action takes place.
         * fromFromTop:     The fromTop from the tile from where the requested action takes place.
         * tile:            The Tile where the requested action takes place.
         * targetFromLeft:  The fromLeft from the tile where the requested action takes place to.
         * targetFromTop:   The fromTop from the tile where the requested action takes place to.
         * ********************************************************************/
        public bool CanTarget(float fromFromLeft, float fromFromTop, Tile tile, float targetFromLeft, float targetFromTop)
        {
            // The rangeBehavior will check if the requested action is allowed and return true or false
            return rangeBehavior.Range(this, fromFromLeft, fromFromTop, tile, targetFromLeft, targetFromTop);
        }

        public void AutoMove()
        {
            //DO SOMETHING
        }
    }
}
