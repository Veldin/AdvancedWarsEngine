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
                Unit unit = gameObject as Unit;
                unit.AddHealth(attack);
            }
            else
            {
                Prompt prompt = new Prompt(50, 20, 615, 350, "", 130, "Out of range!");
            }
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
            if (!owner.GetIsControllable())
            {
                //DO SOMETHING
            }
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

        public override bool OnTick(List<GameObject> gameObjects, float delta)
        {
            throw new NotImplementedException();
        }

        public override bool CollisionEffect(GameObject gameObject)
        {
            //Check collision from the left or right.
            if ((gameObject.FromLeft + gameObject.Width) > (FromLeft + Width))
            {
                AddFromLeft(-1);
            }
            else if ((gameObject.FromLeft + gameObject.Width) < (FromLeft + Width))
            {
                AddFromLeft(1);
            }

            //Check collision from top or bottom.
            if ((gameObject.FromTop + gameObject.Height) > (FromTop + Height))
            {
                AddFromTop(-1);
            }
            else if ((gameObject.FromTop + gameObject.Height) < (FromTop + Height))
            {
                AddFromTop(1);
            }

            //If a player is coliding with an object their CollisionEffect is triggered instantly and not after this resolves.
            //This is so the collision of the enemy still goes even though they are not colliding anymore.
            gameObject.CollisionEffect(this);
            return true;
        }
    }
}
