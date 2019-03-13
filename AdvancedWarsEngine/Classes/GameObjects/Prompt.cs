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

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite, float duration = 130, string text = "Undefined")
            : base(width, height, fromTop, fromLeft, sprite)
        {
            this.text = text;
            maxDuration = duration;
            currentDuration = duration;
            this.sprite = sprite;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
