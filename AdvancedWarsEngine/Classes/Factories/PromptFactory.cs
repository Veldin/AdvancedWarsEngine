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
            switch (value)
            {
                case "":
                    BitmapImage sprite = new BitmapImage();
                    GameObject prompt = new Prompt(width, height, fromTop, fromLeft, sprite);
                    return prompt;
            }

            // Give feedback and return null
            Debug.WriteLine("No prompt is created because there is no prompt with that name.");
            return null;
        }
    }
}
