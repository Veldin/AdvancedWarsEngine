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
        protected IHealthBehavior healthBehavior;
        protected IMovementBehavior movementBehavior;
        protected IDefenceBehavior defenceBehavior;

        public Unit(float width, float height, float fromTop, float fromLeft, BitmapImage sprite, bool isTargetable, float attack, float health, float movement, float defence, float range)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.isTargetable = isTargetable;
            this.attack = attack;
            this.health = health;
            this.movement = movement;
            this.defence = defence;
            this.range = range;
        }

        public void Attack(GameObject gameObject)
        {
            attackBehavior.Attack(this, gameObject);
        }

        public void Move()
        {
            movementBehavior.Movement(this);
        }

        public void Defence()
        {
            defenceBehavior.Defence(this);
        }

        public void Health()
        {
            healthBehavior.Health(this);
        }

        public void Target()
        {
            targetableBehavior.IsTargetable(this);
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
