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
        protected bool isAscending;

        public Prompt(float width, float height, float fromTop, float fromLeft, string text, float maxDuration)
            : base(width, height, fromTop, fromLeft)
        {
            // Set the durations of this prompt
            isUsingDuration = true;
            this.maxDuration = maxDuration;
            currentDuration = 0;
            isAscending = true;

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
            currentDuration = 0;
            isUsingDuration = false;
            isAscending = false;
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

        public bool IsAscending
        {
            get { return isAscending; }
            set { isAscending = value; }
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
                if (textBlock != null)
                {
                    textBlock.Opacity = 0;
                }
                destroyed = true;
            }
        }
    }
}
