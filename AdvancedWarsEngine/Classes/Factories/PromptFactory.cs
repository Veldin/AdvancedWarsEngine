using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class PromptFactory : IAbstractFactory
    {
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {
            // Check which Prompt shoud be created and returned
            GameObject prompt;

            switch (value)
            {
                case "":
                    prompt = new Prompt(width, height, fromTop, fromLeft, "sprite");
                    break;
                default:
                    prompt = new Prompt(width, height, fromTop, fromLeft);
                    break;
            }

            return prompt;
        }
    }
}
