using AdvancedWarsEngine.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine
{
    class Cursor : GameObject
    {

        public Cursor(float width, float height, float fromLeft, float fromTop, string sprite)
        : base(width, height, fromLeft, fromTop, sprite)
        {
            //setActiveBitmap("Assets/Cursor1.gif");
        }

    }
}
