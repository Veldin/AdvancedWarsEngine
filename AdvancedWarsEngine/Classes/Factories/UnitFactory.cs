using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class UnitFactory : IAbstractFactory
    {
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {

            // Check which Unit shoud be created and returned
         /*   switch (value)
            {
                case "AI_Infantry":
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft);
                    return infantry;
                case "AV_Infantry":
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft);
                    return infantry;
                case "AA_Infantry":
                    GameObject infantry = new Unit(width, height, fromTop, fromLeft);
                    return infantry;
                case "AI_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AV_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AA_Vehicle":
                    GameObject vehicle = new Unit(width, height, fromTop, fromLeft);
                    return vehicle;
                case "AI_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;
                case "AV_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;
                case "AA_Air":
                    GameObject air = new Unit(width, height, fromTop, fromLeft);
                    return air;
            }
            */
            // Give feedback and return null
            Debug.WriteLine("No unit is created because there is no unit with that name.");
            return null;
        }
    }
}
