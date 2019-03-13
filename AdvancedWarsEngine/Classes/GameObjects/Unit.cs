using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        private bool isTargetable;
        private float attack;
        private float health;
        private float movement;
        private float defence;
        private float range;
        protected ITargetableBehavior targetableBehavior;
        protected IAttackBehavior attackBehavior;
        protected IDefenceBehavior defenceBehavior;

        public Unit(float width, float height, float fromTop, float fromLeft, BitmapImage sprite, ITargetableBehavior isTargetable, IAttackBehavior attack, IDefenceBehavior defence, float range = 0)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.range = range;
        }

        public void Attack(GameObject gameObject)
        {
            attackBehavior.Attack(this, gameObject);
        }

        public void Move()
        {
        }

        public void Defence()
        {
            defenceBehavior.Defence(this);
        }

        public void Health()
        {
        }

        public void Target(Tile tile)
        {
            targetableBehavior.IsTargetable(this, tile);
        }

        public float GetAttack()
        {
            return attack;
        }

        public float GetHealth()
        {
            return health;
        }

        public float AddHealth(float value)
        {
            return health += value;
        }

        public float GetDefence()
        {
            return defence;
        }

        public void AutoMove()
        {
                //DO SOMETHING
        }

        public ITargetableBehavior TargetableBehavior
        {
            get { return targetableBehavior; }
            set { targetableBehavior = value; }
        }

        public IAttackBehavior AttackBehavior
        {
            get { return attackBehavior; }
            set { attackBehavior = value; }
        }

        public IHealthBehavior HealthBehavior
        {
            get { return healthBehavior; }
            set { healthBehavior = value; }
        }

        public IMovementBehavior MovementBehavior
        {
            get { return movementBehavior; }
            set { movementBehavior = value;}
        }

        public IDefenceBehavior DefenceBehavior
        {
            get { return defenceBehavior; }
            set { defenceBehavior = value; }
        }
    }
}
