using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        protected string text;
        private float maxDuration;
        private float currentDuration;

        public Prompt(float width, float height, float fromTop, float fromLeft, float duration = 130, string text = "Undefined")
            : base(width, height, fromTop, fromLeft)
        {
            this.text = text;
            maxDuration = duration;
            currentDuration = duration;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override bool OnTick(List<GameObject> gameObjects, float delta)
        {
            currentDuration -= delta;

            if (currentDuration < 0)
            {
                gameObjects.Remove(this);
            }

            return true;
        }
    }
}
