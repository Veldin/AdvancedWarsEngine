using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class InfantryDefenceBehavior : IDefenceBehavior
    {
        public float Defence (Unit unit, Tile tile)
        {
            // Setting some local variables
            float baseValue     = 10;                       // The baseValue
            float defenceValue  = baseValue;                // The total defence value which will be returned

            // Multiply the defenceValue based on the type of tile it's standing on
            if (tile is Mountain) {                         // Mountain tile add 20%
                defenceValue += baseValue * 0.2f;
            } 

            // 
            return defenceValue;
        }
    }
}
