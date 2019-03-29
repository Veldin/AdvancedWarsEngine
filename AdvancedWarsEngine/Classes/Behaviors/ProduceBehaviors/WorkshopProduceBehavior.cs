using System;
using System.Linq;

namespace AdvancedWarsEngine.Classes
{
    class WorkshopProduceBehavior : IProduceBehaviour
    {
        public string Produce()
        {
            string[] units = { "AV_Vehicle", "AV_Vehicle", "AA_Vehicle" };

            Random rand = new Random();
            int random = rand.Next(0, units.Count());
            return units[random];
        }
    }
}
