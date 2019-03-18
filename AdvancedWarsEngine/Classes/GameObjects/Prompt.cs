using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        private float       maxDuration;
        private float       currentDuration;
        private TextBlock   textBlock;

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite = "Undefined", float duration = 130, string text = "Undefined")
            : base(width, height, fromTop, fromLeft, sprite)
        {
            // Set the durations of this prompt
            maxDuration     = duration;
            currentDuration = 0;

            // Create the brushes for the text and background
            SolidColorBrush backgroundBrush = new SolidColorBrush(Colors.Transparent);
            SolidColorBrush textBrush       = new SolidColorBrush(Colors.Red);

            // Create a textBlock and set the necessary attributes
            textBlock = new TextBlock
            {
                Text        = text,
                Background  = backgroundBrush,
                Foreground  = textBrush,
                Focusable   = false
            };
        }

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            maxDuration     = 120;
            currentDuration = 120;

        }

        // Return the textBox
        public TextBlock GetTextBox()
        {
            return textBlock;
        }

        
        public void IncreaseCurrentDuration(float time)
        {
            // Increase the currentDuration by the given value
            currentDuration += time;

            // If the currentDuration is bigger than maxDuration, make it invisible and destroy it
            if (currentDuration >= maxDuration)
            {
                textBlock.Opacity = 0;
                destroyed = true;
            }
        }
    }
}
