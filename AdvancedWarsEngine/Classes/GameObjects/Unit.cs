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
        protected float health;
        private float movement;
        private float range;
        protected ITargetableBehavior targetableBehavior;
        protected IAttackBehavior attackBehavior;
        protected IDefenceBehavior defenceBehavior;

        public Unit(float width, float height, float fromTop, float fromLeft, BitmapImage sprite, ITargetableBehavior isTargetable, IAttackBehavior attack, IDefenceBehavior defence, float range)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.range = range;
            health = 100;
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

        public float AddHealth(float value)
        {
            return health += value;
        }

        public void Target(Tile tile)
        {
            targetableBehavior.IsTargetable(this, tile);
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

        public IDefenceBehavior DefenceBehavior
        {
            get { return defenceBehavior; }
            set { defenceBehavior = value; }
        }
    }
}
