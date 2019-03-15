using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    class StructureFactory : IAbstractFactory
    {
        public GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft)
        {
            // Check which Structure shoud be created and returned
            switch (value)
            {
                case "":
                    GameObject structure = new Structure(width, height, fromTop, fromLeft, "sprite");
                    return structure;
            }

            // Give feedback and return null
            Debug.WriteLine("No structure is created because there is no structure with that name.");
            return null;
        }
    }
}
