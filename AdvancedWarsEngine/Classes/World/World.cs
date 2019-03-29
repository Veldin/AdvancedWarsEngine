using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    class World
    {
        protected Map map;
        protected Player currentPlayer;

        private Player firstPlayer; //red
        private Player secondPlayer; //blue
        private Player thirdPlayer; //green
        private Player fourthPlayer; //yellow

        private readonly IAbstractFactory factory;

        public World(FactoryProducer factoryProducer, string level = "plainlevel")
        {
            map = MapFactory.GetMap(level);

            //Maps have units in them.
            switch (level)
            {
                case "desertlevel":
                    firstPlayer = new Player(true, "Red");           //First player is controlable
                    secondPlayer = new Player(true, "Blue");         //Second player is not controllable
                    thirdPlayer = new Player(true, "Green");         //Third player is not controllable

                    factory = factoryProducer.GetFactory("UnitFactory");

                    //Adding the units to the first player
                    Unit testUnit3 = (Unit)factory.GetGameObject("AA_Infantry", 16, 16, 0, 0, firstPlayer.Colour);
                    testUnit3.Target = new Target(6 * 16, 9 * 16);
                    map.GetTile(6, 9).OccupiedUnit = testUnit3;
                    firstPlayer.AddGameObject(testUnit3);

                    //Adding the units to the second player
                    Unit testUnit4 = (Unit)factory.GetGameObject("AI_Vehicle", 16, 16, 32, 32, secondPlayer.Colour);
                    testUnit4.Target = new Target(8 * 16, 9 * 16);
                    map.GetTile(8, 9).OccupiedUnit = testUnit4;
                    secondPlayer.AddGameObject(testUnit4);

                    //Adding the units to the third player
                    Unit testUnit5 = (Unit)factory.GetGameObject("AI_Vehicle", 16, 16, 32, 32, thirdPlayer.Colour);
                    testUnit5.Target = new Target(9 * 16, 9 * 16);
                    map.GetTile(9, 9).OccupiedUnit = testUnit5;
                    thirdPlayer.AddGameObject(testUnit5);

                    factory = factoryProducer.GetFactory("StructureFactory");

                    ////SpawnStructure(firstPlayer, factory, "HQ", 16, 16, 3, 5, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Airport", 16, 16, 3, 6, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 3, 7, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 3, 8, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 4, 4, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 4, 7, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 5, 4, firstPlayer.Colour);

                    //SpawnStructure(secondPlayer, factory, "HQ", 16, 16, 16, 4, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 13, 6, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 14, 4, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 14, 5, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 16, 8, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 17, 5, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Airport", 16, 16, 17, 6, secondPlayer.Colour);

                    SpawnStructure(thirdPlayer, factory, "HQ", 16, 16, 10, 24, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 6, 22, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 6, 23, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 6, 25, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 10, 23, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 11, 21, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Airport", 16, 16, 11, 23, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 11, 24, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 12, 23, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 13, 22, thirdPlayer.Colour);

                    //the players are each others next players so the player loop is created
                    firstPlayer.NextPlayer = secondPlayer;
                    secondPlayer.NextPlayer = thirdPlayer;
                    thirdPlayer.NextPlayer = firstPlayer;

                    //Set the reference to the first player
                    currentPlayer = firstPlayer;
                    break;
                case "lavalevel":
                    firstPlayer = new Player(true, "Red");           //First player is controlable
                    secondPlayer = new Player(true, "Blue");         //Second player is not controllable
                    thirdPlayer = new Player(true, "Green");         //Third player is not controllable
                    fourthPlayer = new Player(true, "Yellow");         //Fourth player is not controllable

                    factory = factoryProducer.GetFactory("StructureFactory");

                    //SpawnStructure(firstPlayer, factory, "HQ", 16, 16, 25, 26, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Airport", 16, 16, 22, 22, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 22, 24, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 25, 25, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 26, 26, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 26, 29, firstPlayer.Colour);


                    //SpawnStructure(secondPlayer, factory, "HQ", 16, 16, 33, 20, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 3, 3, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 4, 4, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 7, 3, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Airport", 16, 16, 32, 21, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 33, 19, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 3, 3, secondPlayer.Colour);

                    //SpawnStructure(thirdPlayer, factory, "HQ", 16, 16, 26, 2, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 26, 4, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 27, 10, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 37, 22, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 37, 28, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Factory", 16, 16, 38, 23, thirdPlayer.Colour);
                    SpawnStructure(thirdPlayer, factory, "Barracks", 16, 16, 38, 28, thirdPlayer.Colour);

                    //SpawnStructure(fourthPlayer, factory, "HQ", 16, 16, 12, 22, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Factory", 16, 16, 7, 19, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Airport", 16, 16, 8, 17, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Barracks", 16, 16, 12, 20, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Factory", 16, 16, 13, 21, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Factory", 16, 16, 14, 26, fourthPlayer.Colour);
                    SpawnStructure(fourthPlayer, factory, "Barracks", 16, 16, 19, 21, fourthPlayer.Colour);

                    //the players are each others next players so the player loop is created
                    firstPlayer.NextPlayer = secondPlayer;
                    secondPlayer.NextPlayer = thirdPlayer;
                    thirdPlayer.NextPlayer = fourthPlayer;
                    fourthPlayer.NextPlayer = firstPlayer;

                    //Set the reference to the first player
                    currentPlayer = firstPlayer;
                    break;
                default: //plainlevel
                    //This map has two players
                    firstPlayer = new Player(true, "Red");           //First player is controlable
                    secondPlayer = new Player(true, "Blue");         //Second player is not controllable

                    factory = factoryProducer.GetFactory("UnitFactory");

                    //Adding the units to the first player
                    Unit testUnit = (Unit)factory.GetGameObject("AA_Infantry", 16, 16, 0, 0, firstPlayer.Colour);
                    testUnit.Target = new Target(6 * 16, 15 * 16);
                    map.GetTile(6, 15).OccupiedUnit = testUnit;
                    firstPlayer.AddGameObject(testUnit);

                    //Adding the units to the second player
                    Unit testUnit2 = (Unit)factory.GetGameObject("AI_Vehicle", 16, 16, 32, 32, secondPlayer.Colour);
                    testUnit2.Target = new Target(7 * 16, 9 * 16);
                    map.GetTile(7, 9).OccupiedUnit = testUnit2;
                    secondPlayer.AddGameObject(testUnit2);

                    //Change factory to produce structures
                    factory = factoryProducer.GetFactory("StructureFactory");

                    ////SpawnStructure(firstPlayer, factory, "HQ", 16, 16, 11, 9, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 10, 8, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 11, 8, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Factory", 16, 16, 12, 9, firstPlayer.Colour);
                    SpawnStructure(firstPlayer, factory, "Barracks", 16, 16, 12, 10, firstPlayer.Colour);

                    //SpawnStructure(secondPlayer, factory, "HQ", 16, 16, 7, 19, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Barracks", 16, 16, 6, 18, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 6, 20, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 7, 20, secondPlayer.Colour);
                    SpawnStructure(secondPlayer, factory, "Factory", 16, 16, 8, 20, secondPlayer.Colour);

                    //the players are each others next players so the player loop is created
                    firstPlayer.NextPlayer = secondPlayer;
                    secondPlayer.NextPlayer = firstPlayer;

                    //Set the reference to the first player
                    currentPlayer = firstPlayer;
                    break;
            }
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public void SpawnStructure(Player player, IAbstractFactory factory, string type, int width, int  height, int fromTop, int fromLeft, string colour)
        {
            Structure structure = (Structure)factory.GetGameObject(type, width, height, fromTop * 16, fromLeft * 16, colour);
            structure.Target = new Target(fromTop * 16, fromLeft * 16);
            map.GetTile(fromTop, fromLeft).OccupiedStructure = structure;
            player.AddGameObject(structure);
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
