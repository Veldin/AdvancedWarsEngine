using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        protected float maxDuration;
        private float currentDuration;
        protected TextBlock textBlock;
        protected bool isUsingDuration;

        public Prompt(float width, float height, float fromTop, float fromLeft, string text, float maxDuration)
            : base(width, height, fromTop, fromLeft)
        {
            // Set the durations of this prompt
            isUsingDuration = true;
            this.maxDuration = maxDuration;
            currentDuration = 0;

            // Create the brushes for the text and background
            SolidColorBrush backgroundBrush = new SolidColorBrush(Colors.Transparent);
            SolidColorBrush textBrush = new SolidColorBrush(Colors.Red);

            // Create a textBlock and set the necessary attributes
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                textBlock = new TextBlock
                {
                    Text = text,
                    Background = backgroundBrush,
                    Foreground = textBrush,
                    Focusable = false,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            });
        }

        public Prompt(float width, float height, float fromTop, float fromLeft, string sprite)
            : base(width, height, fromTop, fromLeft, sprite)
        {
            isUsingDuration = false;
        }

        public void IncreaseCurrentDuration(float time)
        {
            // Check if the prompt uses a duration
            if (!isUsingDuration) { return; }

            // Increase the currentDuration by the given value
            currentDuration += time;

            // If the currentDuration is bigger than maxDuration, make it invisible and destroy it
            if (currentDuration >= maxDuration)
            {
                textBlock.Opacity = 0;
                destroyed = true;
            }
        }

        // Getter for the textBox
        public TextBlock TextBlock
        {
            get { return textBlock; }
        }

        // Getter and setter for maxDuration
        public float MaxDuration
        {
            get { return maxDuration; }
            set { maxDuration = value; }
        }

        // Getter and setter for isUsingDuration
        public bool IsUsingDuration
        {
            get { return isUsingDuration; }
            set { isUsingDuration = value; }
        }
    }
}
