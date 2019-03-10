using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes
{
    class World
    {
        private Map map;
        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public World()
        {
            //DO SOMETHING
            CreateMap();
        }

        public void CreateMap()
        {
            map = MapFactory.GetMap("Plains");
        }
    }
}
