using AdvancedWarsEngine.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedWarsEngine
{
    public partial class MainWindow : Window
    {
        //For timekeeping (we need to know when the last frame happend when the next frame happens and the delta between)
        private long delta;     //The lenght in time the last frame lasted (so we can use it to calculate speeds of things without slowing down due to low fps)
        private long now;       //This is the time of the frame. (To calculate the delta)
        private long then;      //This is the time of the previous frame. (To calculate the delta)

        private Camera camera;
        private World world;
        private Player player;
        private Player ai;

        private List<GameObject> gameObjects;

        //The max fps we want to run at
        private float fps;  //The set FPS limit
        private float interval; //Interfal that gets calculated based on the fps

        //The brush used to fill in the background
        private SolidColorBrush backgroundBrush;

        //Holds string interpertation of all keys that are pressed right now
        private HashSet<string> pressedKeys;

        private Cursor cursor;                  //Holds information about the cursor
        private Prompt crosshair;               //Holds information of the crosshair
        private Prompt selectedTileIndicator;   //Holds information about the selected crosshair

        //Holds the factoryProducer
        FactoryProducer factoryProducer;

        public MainWindow()
        {
            pressedKeys = new HashSet<string>();

            backgroundBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 24, 40, 80));

            InitializeComponent();
            
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            Topmost = true;

            //Bind the keyup/down to the window's keyup/down
            GetWindow(this).KeyUp += KeyUp;
            GetWindow(this).KeyDown += KeyDown;

            GetWindow(this).MouseDown += MouseDown;
            GetWindow(this).MouseUp += MouseDown;

            //Make a new player and set the variable to that player
            player = new Player(true);  //First player is controlable
            ai = new Player(true);      //Second player is not controllable

            //the players are each others next players so the player loop is created
            player.NextPlayer = ai;
            ai.NextPlayer = player;

            world = new World();

            camera = new Camera(world.Map.Tiles.GetLength(0),world.Map.Tiles.GetLength(0));

            gameObjects = new List<GameObject>();

            Cursor = Cursors.None; //Hide the default Cursor
            cursor = new Cursor(12, 12, 300, 300, "Sprites/Cursors/defaultCursor.gif"); //Create the default cursor to use
            gameObjects.Add(cursor);

            //Create the default crosshair to use
            crosshair = new Prompt(16, 16, 300, 300, "Sprites/TileSelectors/TileSelectorWhite.gif");
            gameObjects.Add(crosshair);

            //Create the default cursor to use
            selectedTileIndicator = new Prompt(16, 16, 300, 300, "Sprites/TileSelectors/TileSelectorGreen.gif");
            gameObjects.Add(selectedTileIndicator);

            factoryProducer = new FactoryProducer();
            IAbstractFactory factory = factoryProducer.GetFactory("UnitFactory");

            Unit testUnit = (Unit)factory.GetGameObject("AA_Infantry", 16, 16, 0, 0);
            gameObjects.Add(testUnit);
            Unit testUnit2 = (Unit)factory.GetGameObject("AI_Vehicle", 16, 16, 32, 32);
            gameObjects.Add(testUnit2);

            player.AddGameObject(testUnit); //player one owns unit one
            ai.AddGameObject(testUnit2); //player two own unit two

            Tile TestTile = world.Map.GetTile(6, 9);
            Tile TestTile2 = world.Map.GetTile(7, 9);

            TestTile.OccupiedUnit = testUnit;
            TestTile.OccupiedUnit.Target = new Target(6 * 16, 9 * 16);

            TestTile2.OccupiedUnit = testUnit2;
            TestTile2.OccupiedUnit.Target = new Target(7 * 16, 9 * 16);

            fps = 999999999; //Desired max fps.
            interval = 1000 / fps;
            then = Stopwatch.GetTimestamp();

            //Allow the player's gameobject to act
            player.AllowedAllToAct();

            Run();
        }

        public void Run()
        {
            now = Stopwatch.GetTimestamp();
            delta = (now - then) / 1000; //Defide by 1000 to get the delta in MS

            if (delta > interval)
            {
                then = now; //Remember when this frame was.
                Logic(delta); //Run the logic of the simulation.
                Draw();
            }
            else
            {
                Thread.Sleep(1); //Sleep the thread so time is passed
            }

            Task.Yield();  //Force this task to complete asynchronously (This way the main thread is not blocked by this task calling itself.
            Task.Run(() => Run());  //Schedule new Run() task
        }

        private void Draw()
        {
            //Create a new arraylist used to hold the gameobjects for this loop.
            //The copy is made so it does the ontick methods on all the objects even the onces destroyed in the proces.
            ArrayList loopList;
            lock (gameObjects) //lock the gameobjects for duplication
            {
                try
                {
                    //Try to duplicate the arraylist.
                    loopList = new ArrayList(gameObjects);
                }
                catch
                {
                    //if it failes for any reason skip this frame.
                    return;
                }
            }

            //Run it in the UI thread
            try
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    TestCanvas.Background = backgroundBrush;

                    //Set the background
                    Rectangle BgRect = world.Map.rectangle;

                    Canvas.SetLeft(BgRect, 0 + camera.GetLeftOffSet());
                    Canvas.SetTop(BgRect, 0 + camera.GetTopOffSet());

                    if (!TestCanvas.Children.Contains(BgRect))
                    {
                        TestCanvas.Children.Add(BgRect);
                    }

                    //Draw the gameobjects in the loop list exept for the Prompts
                    foreach (GameObject gameObject in loopList)
                    {
                        // Don't draw the prompts just yet. These will be drawn later so it's on top of other gameObjects
                        if (gameObject is Prompt)
                        {
                            // Dont do anything
                        }
                        else
                        {
                            Rectangle rect = gameObject.rectangle;

                            rect.Width = gameObject.Width;
                            rect.Height = gameObject.Height;

                            Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                            if (!TestCanvas.Children.Contains(rect))
                            {
                                TestCanvas.Children.Add(rect);
                            }
                        }
                    }

                    // Draw the prompts
                    foreach (GameObject gameObject in loopList)
                    {
                        // todo make this prettier
                        if (gameObject is Prompt)
                        {
                            // Cast the gameObject to a Prompt
                            Prompt prompt = gameObject as Prompt;

                            // Checks if the prompt has a TextBlock
                            if (prompt.TextBlock != null)
                            {
                                // Get the textBlock from the prompt
                                TextBlock textBlock = prompt.TextBlock;

                                // Set the width and the height of the textBlock
                                textBlock.Width = gameObject.Width;
                                textBlock.Height = gameObject.Height;

                                Canvas.SetLeft(textBlock, gameObject.FromLeft + camera.GetLeftOffSet());
                                Canvas.SetTop(textBlock, gameObject.FromTop + camera.GetTopOffSet());

                                if (!TestCanvas.Children.Contains(textBlock))
                                {
                                    TestCanvas.Children.Add(textBlock);
                                }
                                // Move the prompt a little bit up
                                prompt.FromTop -= 0.02f;
                            }
                            else
                            {
                                // Get the rectangle from the prompt
                                Rectangle rect = prompt.rectangle;

                                // Set the width and height of the rect
                                rect.Width = gameObject.Width;
                                rect.Height = gameObject.Height;

                                Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                                Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                                if (!TestCanvas.Children.Contains(rect))
                                {
                                    TestCanvas.Children.Add(rect);
                                }
                            }
                            // Increase the currentDuration of this promt by the delta
                            prompt.IncreaseCurrentDuration(delta);
                        }
                    }
                });
            }
            catch
            {
                // if drawing failes do nothing
            }
        }

        private void Logic(long delta)
        {
            //Create a new arraylist used to hold the gameobjects for this loop.
            //The copy is made so it does the ontick methods on all the objects even the onces destroyed in the proces.
            ArrayList loopList;
            lock (gameObjects) //lock the gameobjects for duplication
            {
                try
                {
                    //Try to duplicate the arraylist.
                    loopList = new ArrayList(gameObjects);
                }
                catch
                {
                    //if it failes for any reason skip this frame.
                    return;
                }
            }

            //For every gameobject in the room
            foreach (GameObject gameObject in loopList)
            {
                gameObject.OnTick(gameObjects, delta);
            }

            //Set the new curser location
            try
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Point p = Mouse.GetPosition(TestCanvas);
                    cursor.FromLeft = (float)p.X - camera.GetLeftOffSet();
                    cursor.FromTop = (float)p.Y - camera.GetTopOffSet();
                });
            }
            catch
            {

            }

            //Key input
            if (IsKeyPressed("W"))
            {
                camera.AddFromTop((float)0.1);
            }

            if (IsKeyPressed("S"))
            {
                camera.AddFromTop((float)-0.1);
            }

            if (IsKeyPressed("A"))
            {
                camera.AddFromLeft((float)0.1);
            }

            if (IsKeyPressed("D"))
            {
                camera.AddFromLeft((float)-0.1);
            }

            //Get the tile at the location and put the crosshair on that location
            int selectedFromTop = (int)(cursor.FromTop / world.Map.Size);
            int selectedFromLeft = (int)(cursor.FromLeft / world.Map.Size);

            if (selectedFromTop < 0)
            {
                selectedFromTop = 0;
            }

            if (selectedFromTop >= world.Map.Tiles.GetLength(0) - 1)
            {
                selectedFromTop = world.Map.Tiles.GetLength(0) - 1;
            }

            if (selectedFromLeft < 0)
            {
                selectedFromLeft = 0;
            }

            if (selectedFromLeft >= world.Map.Tiles.GetLength(1) - 1)
            {
                selectedFromLeft = world.Map.Tiles.GetLength(1) - 1;
            }

            Tile pressedOnTile = world.Map.GetTile(selectedFromTop, selectedFromLeft);
            crosshair.FromTop = world.Map.Size * selectedFromTop;
            crosshair.FromLeft = world.Map.Size * selectedFromLeft;

            if (player.IsControllable)
            {
                if (IsKeyPressed("LeftMouseButton"))
                {
                    world.Map.DeselectAll(); //To make sure there is only one tile selected

                    pressedOnTile.Selected = true;

                    //If you select a unit, check if the current players owns that unit
                    if (player.InGameObjects(pressedOnTile.OccupiedUnit))
                    {
                        player.SelectedUnit = pressedOnTile.OccupiedUnit;

                        selectedTileIndicator.FromLeft = selectedFromLeft * world.Map.Size;
                        selectedTileIndicator.FromTop = selectedFromTop * world.Map.Size;
                    }
                    else if (pressedOnTile.OccupiedUnit != null || pressedOnTile.OccupiedStructure != null)
                    {   //Clicked a unit the current player doest own.
                        if (player.SelectedUnit != null) //Current player has a unit selected
                        {
                            if (player.SelectedUnit.IsAllowedToAct)
                            {
                                if (player.SelectedUnit.CanTarget(player.SelectedUnit.Target.GetFromLeft() / 16, player.SelectedUnit.Target.GetFromTop() / 16, pressedOnTile, selectedFromLeft, selectedFromTop))
                                {
                                    // Define a GameObject
                                    GameObject enemyGameObject;
                                    // Try to get the Unit from the selected tile
                                    enemyGameObject = pressedOnTile.OccupiedUnit;

                                    // If there is no Unit try to select the structure
                                    if (enemyGameObject == null)
                                    {
                                        enemyGameObject = pressedOnTile.OccupiedStructure;
                                    }

                                    // The selectedUnit attacks the Unit on the selectedTile
                                    float dmgValue = player.SelectedUnit.Attack(enemyGameObject, pressedOnTile);

                                    // Create a promptFactory
                                    IAbstractFactory promtFactory = factoryProducer.GetFactory("PromptFactory");

                                    // Display the damageValue in a prompt
                                    try
                                    {
                                        Application.Current.Dispatcher.Invoke(delegate
                                        {
                                            gameObjects.Add(promtFactory.GetGameObject(dmgValue.ToString(), 50, 20, enemyGameObject.FromTop, enemyGameObject.FromLeft));
                                        });
                                    }
                                    catch
                                    {

                                    }
                                    // End the turn for this Unit and deselect it
                                    player.SelectedUnit.IsAllowedToAct = false;
                                    player.SelectedUnit = null;

                                    // Remove the selectedTileIndicator from sight
                                    selectedTileIndicator.FromLeft = -1000;
                                    selectedTileIndicator.FromTop = -1000;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (player.SelectedUnit != null) //Current player has a unit selected
                        {
                            if (player.SelectedUnit.CanTarget(player.SelectedUnit.Target.GetFromLeft() / 16, player.SelectedUnit.Target.GetFromTop() / 16, pressedOnTile, selectedFromLeft, selectedFromTop))
                            {
                                Debug.WriteLine("In range");
                            }
                            else
                            {
                                Debug.WriteLine("Out of range");
                            }
                        }
                        else
                        {
                            //Do nothing
                        }
                    }
                }
            }

            /*
             * Selects the next Player.
             */
            if (player.HasAlowedUnits())
            {
                //Do nothing, because the player can still act with a unit
            }
            else
            {
                //The turn of this player has ended. Select the next player, and allow all units to act
                player = player.NextPlayer;
                player.AllowedAllToAct();

                //Hide the tile indicator
                selectedTileIndicator.FromLeft = -1000;
                selectedTileIndicator.FromTop = -1000;
            }

            /*
             * Destroy old units.
             */
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject.destroyed)
                {
                    foreach (Tile tile in world.Map.Tiles)
                    {
                        if (tile.OccupiedStructure == gameObject)
                        {
                            tile.OccupiedStructure = null;
                        }
                        if (tile.OccupiedUnit == gameObject)
                        {
                            tile.OccupiedUnit = null;
                        }
                    }
                    //If a gameObject is marked to be destroyed remove it from the list and remove them from the canvas
                    gameObjects.Remove(gameObject);
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        TestCanvas.Children.Remove(gameObject.rectangle);
                    });
                }
            }
            //Unpres the left mouse button (As there is no event that fires the mouse up)
            pressedKeys.Remove("LeftMouseButton");
        }
        
        /* 
        * Add the given key in the pressedKeys collection.
        * The argument is the given key represented as a string.
        */
        public new void KeyDown(object sender, KeyEventArgs args)
        {
            pressedKeys.Add(args.Key.ToString());
        }

        /* 
         * Remove the given key in the pressedKeys collection.
         * The argument is the given key represented as a string.
         */
        public new void KeyUp(object sender, KeyEventArgs args)
        {
            pressedKeys.Remove(args.Key.ToString());
        }
        
        /* 
        * Add the left mouse button key in the pressedKeys collection.
        */
        private new void MouseDown(object sender, MouseButtonEventArgs e)
        {
            pressedKeys.Add("LeftMouseButton");
        }

        /* 
         * Returns wheater the given key exists within the pressedKeys collection.
         * The argument is the given key represented as a string.
         */
        public bool IsKeyPressed(string virtualKey)
        {
            return pressedKeys.Contains(virtualKey);
        }
    }
}
