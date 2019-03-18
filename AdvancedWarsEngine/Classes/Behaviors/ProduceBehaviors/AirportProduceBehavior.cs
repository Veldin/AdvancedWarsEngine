using System;

namespace AdvancedWarsEngine.Classes
{
    class AirportProduceBehavior : IProduceBehavior
    {
        public string Produce()
        {
            string[] units = { "AI_Air", "AV_Air", "AA_Air" };
        
            Random rand = new Random();
            int random = rand.Next(0, 2);
            return units[random];
        }
    }
}
