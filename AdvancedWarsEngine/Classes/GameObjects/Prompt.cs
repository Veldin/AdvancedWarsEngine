﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdvancedWarsEngine.Classes
{
    class Prompt : GameObject
    {
        protected float maxDuration;
        protected float currentDuration;
        protected TextBlock textBlock;
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

            Debug.WriteLine(text);

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
                    FontSize = 10,
                    Opacity = 1
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

        // Getter and setter for maxDuration
        public float MaxDuration
        {
            get { return maxDuration; }
            set { maxDuration = value; }
        }

        // Getter for the textBox
        public float CurrentDuration
        {
            get { return currentDuration; }
        }

        // Getter for the textBox
        public TextBlock TextBlock
        {
            get { return textBlock; }
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

        public void IncreaseCurrentDuration(float time)
        {
            // Check if the prompt uses a duration
            if (!isUsingDuration) { return; }

            // Increase the currentDuration by the given value
            currentDuration += time;
        }
    }
}
