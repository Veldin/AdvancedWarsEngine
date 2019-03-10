﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class Structure : GameObject
    {
        protected float capturePoints;

        public Structure(float width, float height, float fromTop, float fromLeft)
            : base(width, height, fromTop, fromLeft)
        {
            // DO SOMETHING
        }

        public float CapturePoints
        {
            get { return capturePoints; }
            set { capturePoints = value; }
        }

        public void AddCapturePoints(float value)
        {
            capturePoints += value;
        }

        public override bool OnTick(List<GameObject> gameObjects, float delta)
        {
            return true;
        }
    }
}
