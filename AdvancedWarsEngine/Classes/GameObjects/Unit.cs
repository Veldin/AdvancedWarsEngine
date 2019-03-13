using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AdvancedWarsEngine.Classes.Enums;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        protected float             health;                     // The health of the Unit
        protected IRangeBehavior    rangeBehavior;              // The rangeBehavior calculates the range of the Unit
        protected IAttackBehavior   attackBehavior;             // The attackBehavior calculates the dammageValue
        protected IDefenceBehavior  defenceBehavior;            // The defenceBehavior calculates the defenceValue
        private EUnitType           unitType;                   // The unitType specifice the type of this Unit for example infantry or vehicle
        private bool                isDestroyed;                // A bool that checks if this Unit should be destroyed

        public Unit(float width, float height, float fromTop, float fromLeft, string sprite, IRangeBehavior rangeBehavior, IAttackBehavior attackBehavior, IDefenceBehavior defenceBehavior, EUnitType unitType)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            health                  = 100;
            this.rangeBehavior      = rangeBehavior;
            this.attackBehavior     = attackBehavior;
            this.defenceBehavior    = defenceBehavior;
            this.unitType           = unitType;
            isDestroyed             = false;
        }

        /**********************************************************************
         * This function does the actual attack
         * ARGUMENTS:
         * gameObject:  The gameObject that gets attacked
         * tile:        The Tile where the gameObject that gets attacked stands on.
         * ********************************************************************/
        public void Attack(GameObject gameObject, Tile tile)
        {
            // If the gameObject is a prompt give some feedback and return.
            if (gameObject is Prompt)
            {
                Debug.WriteLine("A prompt cannot be attacked");
                return;
            }

            // Check if the attacked gameobject is an Unit
            if (gameObject is Unit)
            {
                // Cast the gameObject to an Unit
                Unit unit = gameObject as Unit;

                // Calculate the damageValue
                float damageValue = attackBehavior.Attack(this, unit) - unit.Defence(tile);

                // Deal the damage to the enemy unit by decreasing it's health
                unit.DecreaseHealth(damageValue);

                //Todo show here the damage prompt
            }

            // Check if the gameObject is a Structure
            if (gameObject is Structure)
            {
                throw new Exception("Still needs to be implemented"); //Todo
                // Just deal 10 damage/capture points to the structure no matter what??
            }
                 
            // The Unit has attacked so it is no longer allowed to act this turn.
            IsAllowedToAct = false;
        }

        public void Move()
        {
            // DO SOMETHING
        }

        public float Defence(Tile tile)
        {
            return defenceBehavior.Defence(this, tile);
        }

        public float Health
        {
            get { return health; }
        }

        public void DecreaseHealth(float value)
        {
            // Decrease the heath of this Unit by the given value
            health -= value;

            // Checks if the health of this Unit is zero of below
            // If so set isDestroyed on true so the Engine will destroy it
            if (health <= 0)
            {
                isDestroyed = true;
            }
        }

        public EUnitType UnitType
        {
            get { return unitType; }
        }

        public float AddHealth(float value)
        {
            return health += value;
        }

        /**********************************************************************
         * This function checks if the requested action is allowed and returns a bool
         * ARGUMENTS:
         * tile:            The Tile where the requested action takes place.
         * targetFromLeft:  The fromLeft from the tile where the requested action takes place.
         * targetFromLeft:  The fromTop from the tile where the requested action takes place.
         * ********************************************************************/
        public bool Target(Tile tile, float targetFromLeft, float targetFromTop)
        {
            // The rangeBehavior will check if the requested action is allowed and return true or false
            return rangeBehavior.Range(this, fromLeft, fromTop, tile, targetFromLeft, targetFromTop);
        }

        public void AutoMove()
        {
                //DO SOMETHING
        }

        public IRangeBehavior RangeBehavior
        {
            get { return rangeBehavior; }
        }

        public IAttackBehavior AttackBehavior
        {
            get { return attackBehavior; }
        }

        public IDefenceBehavior DefenceBehavior
        {
            get { return defenceBehavior; }
        }
    }
}
