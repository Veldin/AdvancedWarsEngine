namespace AdvancedWarsEngine.Classes
{
    class World
    {
        protected Map map;

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public World()
        {
            map = MapFactory.GetMap("plainlevel");
        }
    }
}
