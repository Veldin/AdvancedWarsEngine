using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    interface ITargetableBehavior
    {
        void IsTargetable(Unit unit);
    }
}
