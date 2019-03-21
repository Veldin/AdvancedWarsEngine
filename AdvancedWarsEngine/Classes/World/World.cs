using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    class World
    {
        protected Map map;
        protected Player player;

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public World(FactoryProducer factoryProducer, string level = "plainlevel")
        {
            map = MapFactory.GetMap(level);

            //Maps have units in them.
            switch (level)
            {
                case "": //TODO: other levels
                    break;
                default: //plainlevel
                    //This map has two players
                    Player firstPlayer = new Player(true, "Red");           //First player is controlable
                    Player secondPlayer = new Player(true, "Blue");         //Second player is not controllable

                    IAbstractFactory factory = factoryProducer.GetFactory("UnitFactory");

                    //Adding the units to the first player
                    Unit testUnit = (Unit)factory.GetGameObject("AA_Infantry", 16, 16, 0, 0);
                    testUnit.Target = new Target(6 * 16, 9 * 16);
                    map.GetTile(6, 9).OccupiedUnit = testUnit;
                    firstPlayer.AddGameObject(testUnit);

                    //Adding the units to the first player
                    Unit testUnit2 = (Unit)factory.GetGameObject("AI_Vehicle", 16, 16, 32, 32);
                    testUnit2.Target = new Target(7 * 16, 9 * 16);
                    map.GetTile(7, 9).OccupiedUnit = testUnit2;
                    secondPlayer.AddGameObject(testUnit2); 

                    //Change factory to produce structures
                    factory = factoryProducer.GetFactory("StructureFactory");

                    Structure structure = (Structure)factory.GetGameObject("Barracks", 16, 16, 9 * 16, 9 * 16);
                    map.GetTile(9, 9).OccupiedStructure = structure;
                    firstPlayer.AddGameObject(structure);

                    //the players are each others next players so the player loop is created
                    firstPlayer.NextPlayer = secondPlayer;
                    secondPlayer.NextPlayer = firstPlayer;

                    //Set the reference to the first player
                    player = firstPlayer;

                break;   
            }
        }

        /*
         * GetGameObjects
         * 
         * Gets all gameobjects that have a place on a tile. 
         * This includes both OccupiedUnit and OccupiedStructure of every tile.
         */
        public List<GameObject> GetGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int fromLeft = 0; fromLeft < Map.Tiles.GetLength(0); fromLeft += 1)
            {
                for (int fromTop = 0; fromTop < Map.Tiles.GetLength(1); fromTop += 1)
                {
                    if (Map.GetTile(fromLeft, fromTop).OccupiedUnit != null)
                    {
                        gameObjects.Add(Map.GetTile(fromLeft, fromTop).OccupiedUnit);
                    }

                    if (Map.GetTile(fromLeft, fromTop).OccupiedStructure != null)
                    {
                        gameObjects.Add(Map.GetTile(fromLeft, fromTop).OccupiedStructure);
                    }

                }
            }

            return gameObjects;
        }
    }
}
