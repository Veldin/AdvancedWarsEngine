using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Unit : GameObject
    {
        private bool isTargetable;
        private float attack;
        private float health;
        private float movement;
        private float defence;
        private ITargetableBehavior targetableBehavior;
        private IAttackBehavior attackBehavior;
        private IHealthBehavior healthBehavior;
        private IMovementBehavior movementBehavior;
        private IDefenceBehavior defenceBehavior;

        public Unit(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft)
        {

        }

        public void Attack(GameObject iets)
        {
            //DO SOMETHING
        }

        public void Move()
        {
            //DO SOMETHING
        }

        public float GetAttack()
        {
            return attack;
        }

        public float GetHealth()
        {
            return health;
        }

        public void SetHealth(float value)
        {
            health += value;
        }

        public float GetDefence()
        {
            return defence;
        }

        public void AutoMove()
        {
            //DO SOMETHING
        }

        public ITargetableBehavior GetTargetableBehavior()
        {
            return targetableBehavior;
        }

        public void SetTargetableBehavior(ITargetableBehavior targetableBehavior)
        {
            this.targetableBehavior = targetableBehavior;
        }

        public IAttackBehavior GetAttackBehavior()
        {
            return attackBehavior;
        }

        public void SetAttackBehavior(IAttackBehavior attackBehavior)
        {
            this.attackBehavior = attackBehavior;
        }

        public IHealthBehavior GetHealthBehavior()
        {
            return healthBehavior;
        }

        public void SetHealthBehavior(IHealthBehavior healthBehavior)
        {
            this.healthBehavior = healthBehavior;
        }

        public IMovementBehavior GetMovementBehavior()
        {
            return movementBehavior;
        }

        public void SetMovementBehavior(IMovementBehavior movementBehavior)
        {
            this.movementBehavior = movementBehavior;
        }

        public IDefenceBehavior GetDefenceBehavior()
        {
            return defenceBehavior;
        }

        public void SetDefenceBehavior(IDefenceBehavior defenceBehavior)
        {
            this.defenceBehavior = defenceBehavior;
        }
    }
}
