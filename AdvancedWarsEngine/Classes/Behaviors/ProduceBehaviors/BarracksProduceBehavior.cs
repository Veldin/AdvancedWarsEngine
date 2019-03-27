using System;
using System.Linq;

namespace AdvancedWarsEngine.Classes
{
    class BarracksProduceBehavior : IProduceBehavior
    {
        public string Produce()
        {
            string[] units = { "AI_Infantry", "AV_Infantry", "AA_Infantry" };

            Random rand = new Random();
            int random = rand.Next(0, units.Count());
            return units[random];
        }
    }
}
