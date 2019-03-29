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
        protected Border border;
        protected bool isUsingDuration;
        protected bool isAscending;
        protected bool isFollowingCamera;

        public Prompt(float width, float height, float fromTop, float fromLeft, string text, float maxDuration)
            : base(width, height, fromTop, fromLeft)
        {
            // Set the durations of this prompt
            isUsingDuration = true;
            this.maxDuration = maxDuration;
            currentDuration = 0;
            isAscending = true;

            // Create the brushes for the text and background
            SolidColorBrush backgroundBrush = new SolidColorBrush(Colors.Black)
            {
                Opacity = 0.6
            };
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
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Padding = new Thickness(2, 0, 0, 8),
                    FontSize = 10
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

        public bool IsFollowingCamera
        {
            get { return isFollowingCamera; }
            set { isFollowingCamera = value; }
        }
    }
}
