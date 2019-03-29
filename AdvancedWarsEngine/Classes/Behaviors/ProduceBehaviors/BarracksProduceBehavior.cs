using System;

namespace AdvancedWarsEngine.Classes
{
    class BarracksProduceBehavior : IProduceBehaviour
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
