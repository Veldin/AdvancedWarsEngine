using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        protected string text;
        private float maxDuration;
        private float currentDuration;

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite = "Undefined", float duration = 130, string text = "Undefined")
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.text = text;
            maxDuration = duration;
            currentDuration = duration;
        }

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.text = "Test";
            maxDuration = 120;
            currentDuration = 120;

        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
