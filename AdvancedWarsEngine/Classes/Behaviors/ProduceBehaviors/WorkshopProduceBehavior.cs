using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class WorkshopProduceBehavior : IProduceBehavior
    {
        public string Produce()
        {
            string[] units = { "AI_Vehicle", "AV_Vehicle", "AA_Vehicle" };

            Random rand = new Random();
            int random = rand.Next(0, 2);
            return units[random];
        }
    }
}
