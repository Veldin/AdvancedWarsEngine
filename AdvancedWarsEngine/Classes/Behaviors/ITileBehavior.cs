using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    interface ITileBehavior
    {
        /****************************************************
         * This function checks if the Unit is allowed on the
         * given tile.
         * Tile     = That needs to be checked
         * **************************************************/
        bool IsAllowed(Tile tile);
    }
}
