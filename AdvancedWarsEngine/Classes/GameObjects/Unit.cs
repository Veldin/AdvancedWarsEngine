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
        private float range;
        protected ITargetableBehavior targetableBehavior;
        protected IAttackBehavior attackBehavior;
        protected IHealthBehavior healthBehavior;
        protected IMovementBehavior movementBehavior;
        protected IDefenceBehavior defenceBehavior;

        public Unit(float width, float height, float fromTop, float fromLeft, bool isTargetable, float attack, float health, float movement, float defence, float range)
            : base(width, height, fromTop, fromLeft)
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
            if (CalculateDistance(this, gameObject) < range)
            {
                //DO SOMETHING
                if (gameObject is Unit)
                {
                    Unit unit = gameObject as Unit;
                    unit.AddHealth(attack * -1);
                }
                else if (gameObject is Structure)
                {
                    Structure structure = gameObject as Structure;
                    structure.AddCapturePoints(attack * -1);
                }
            }
            else
            {
                Prompt prompt = new Prompt(50, 20, 615, 350, 130, "Out of range!");
            }
        }

        public void Move()
        {
            if (isAllowedToAct)
            {
                
            }
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

        private bool getThenSetTarget(List<GameObject> gameObjects)
        { 
            return false;
        }

    }
}
