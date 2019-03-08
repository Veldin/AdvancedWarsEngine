using AdvancedWarsEngine.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine
{
    class Cursor : GameObject
    {

        public Cursor(float width, float height, float fromLeft, float fromTop)
        : base(width, height, fromLeft, fromTop)
        {
            //setActiveBitmap("Assets/Cursor1.gif");

        }

    }
}
