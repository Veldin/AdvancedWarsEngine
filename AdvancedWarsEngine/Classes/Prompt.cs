using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        private string text;
        public Prompt(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft)
        {
            //DO SOMETHING
        }

        public string GetText()
        {
            return text;
        }

        public void SetText(string text)
        {
            this.text = text;
        }
    }
}
