using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class BarracksProduceBehavior : IProduceBehavior
    {
        public string Produce()
        {
            string[] units = { "AI_Infantry", "AV_Infantry", "AA_Infantry" };

            Random rand = new Random();
            int random = rand.Next(0, 2);
            return units[random];
        }
    }
}
