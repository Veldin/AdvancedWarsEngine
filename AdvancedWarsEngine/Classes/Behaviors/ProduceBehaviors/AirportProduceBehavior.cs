using System;
using System.Linq;

namespace AdvancedWarsEngine.Classes
{
    class AirportProduceBehavior : IProduceBehaviour
    {
        public string Produce()
        {
            string[] units = { "AI_Air", "AV_Air", "AA_Air" };
        
            Random rand = new Random();
            int random = rand.Next(0, units.Count());
            return units[random];
        }
    }
}
