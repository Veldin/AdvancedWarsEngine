using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AdvancedWarsEngine.Classes.Enums;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        protected float health;
        private float movement;
        protected IRangeBehavior rangeBehavior;
        protected IAttackBehavior attackBehavior;
        protected IDefenceBehavior defenceBehavior;
        private EUnitType unitType;

        public Unit(float width, float height, float fromTop, float fromLeft, string sprite, IRangeBehavior rangeBehavior, IAttackBehavior attackBehavior, IDefenceBehavior defenceBehavior, EUnitType unitType)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            health = 100;
            this.rangeBehavior = rangeBehavior;
            this.attackBehavior = attackBehavior;
            this.defenceBehavior = defenceBehavior;
            this.unitType = unitType;
        }

        public void Attack(GameObject gameObject)
        {
            attackBehavior.Attack(this, gameObject);
        }

        public void Move()
        {
            // DO SOMETHING
        }

        public void Defence(Tile tile)
        {
            defenceBehavior.Defence(this, tile);
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
            return health += value;
        }

        public void Target(Tile tile)
        {
            //rangeBehavior.Range(this, float fromLeft, float fromTop, tile, float targetFromLeft, float targetFromTop);
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
