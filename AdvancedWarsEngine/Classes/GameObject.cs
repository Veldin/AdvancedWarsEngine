using System;

namespace AdvancedWarsEngine.Classes
{
    abstract class GameObject
    {
        private bool allowToAct;
        private int width;
        private int height;
        private int fromTop;
        private int fromLeft;
        private Target target;
        private string sprite;
        private Player owner;
    }
}
