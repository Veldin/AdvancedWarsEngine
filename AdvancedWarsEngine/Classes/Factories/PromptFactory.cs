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
            GameObject prompt = null;

            // Check if the value is the location of an image
            bool isImage = (value.Contains(".png") || value.Contains(".gif"));

            // Create the Prompt
            switch (isImage)
            {
                case true:
                    prompt = new Prompt(width, height, fromTop, fromLeft, value);
                    break;
                case false:
                    prompt = new Prompt(width, height, fromTop, fromLeft, value, 2500);
                    break;
            }

            // Return the Prompt
            return prompt;
        }
    }
}
