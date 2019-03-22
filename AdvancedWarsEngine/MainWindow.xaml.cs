using AdvancedWarsEngine.Classes;
using System;
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
        private Pathing pathing;

        //Holds the factoryProducer
        FactoryProducer factoryProducer;

        public MainWindow()
        {
            pressedKeys = new HashSet<string>();

            backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 24, 40, 80));

            InitializeComponent();
            
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            //Bind the keyup/down to the window's keyup/down
            GetWindow(this).KeyUp += KeyUp;
            GetWindow(this).KeyDown += KeyDown;

            GetWindow(this).MouseDown += MouseDown;
            GetWindow(this).MouseUp += MouseDown;

            factoryProducer = new FactoryProducer();
            IAbstractFactory factory = factoryProducer.GetFactory("UnitFactory");

            world = new World(factoryProducer, "lavalevel");

            camera = new Camera(world.Map.Tiles.GetLength(0),world.Map.Tiles.GetLength(1));

            gameObjects = new List<GameObject>();

            Cursor = Cursors.None; //Hide the default Cursor
            cursor = new Cursor(12, 12, 300, 300, "Sprites/Cursors/defaultCursor.gif"); //Create the default cursor to use
            gameObjects.Add(cursor);

            //Create the default crosshair to use
            crosshair = new Prompt(16, 16, 0, 0, "Sprites/TileSelectors/TileSelectorWhite.gif");
            gameObjects.Add(crosshair);

            //Create the default cursor to use
            selectedTileIndicator = new Prompt(16, 16, 0, 0, "Sprites/TileSelectors/TileSelectorGreen.gif");
            gameObjects.Add(selectedTileIndicator);

            //Add all the gameObjects from the world to the main gameplay loop.
            gameObjects.AddRange(world.GetGameObjects());

            //Desired max fps
            fps = 90; //Desired max fps.
            interval = 1000 / fps;
            then = Stopwatch.GetTimestamp();

            //Allow the world.Player's gameobject to act
            //world.Player.AllowedAllToAct();

            // Create a Pathing class
            pathing = new Pathing();

            RunAsync();
        }

        public void RunAsync()
        {
            now = Stopwatch.GetTimestamp();
            delta = (now - then) / 1000; //Defide by 1000 to get the delta in MS

  
            then = now; //Remember when this frame was.
            lock (this)
            {
                Logic(delta); //Run the logic of the simulation.
                Draw();
            }

            Task.Delay((int)interval);

            //Task.Yield();  //Force this task to complete asynchronously (This way the main thread is not blocked by this task calling itself.
            Task.Run(() => RunAsync());  //Schedule new Run() task
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
                    TestCanvas.Children.Clear();    //Remove all recs from the canvas, start clean every loop
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
                                Rectangle rect = gameObject.rectangle;

                                rect.Width = gameObject.Width;
                                rect.Height = gameObject.Height;


                                Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                                Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                                if (Double.IsNaN(gameObject.FromLeft) || Double.IsNaN(gameObject.FromTop))
                                {

                                    if (gameObject.Target != null)
                                    {
                                        gameObject.FromLeft = gameObject.Target.GetFromLeft();
                                    }
                                    else
                                    {
                                        gameObject.FromLeft = 0;
                                    }

                                    if (gameObject.Target != null)
                                    {
                                        gameObject.FromTop = gameObject.Target.GetFromLeft();
                                    }
                                    else
                                    {
                                        gameObject.FromTop = 0;
                                    }


                                    foreach (GameObject gameObject2 in loopList)
                                    {
                                        Debug.WriteLine("lel");
                                        Debug.WriteLine(loopList.Count);
                                        Debug.WriteLine(gameObject2);
                                    }

                                }

                                Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                                Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                                //Draw the rect if the rect isnt in yet
                                if (!TestCanvas.Children.Contains(rect))
                                {
                                    TestCanvas.Children.Add(rect);
                                }
                            }

                            prompt.IncreaseCurrentDuration(delta);
                        }
                        else //gameobject is not a prompt
                        {
                            Rectangle rect = gameObject.rectangle;

                            rect.Width = gameObject.Width;
                            rect.Height = gameObject.Height;

                            Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                            if (Double.IsNaN(gameObject.FromLeft) || Double.IsNaN(gameObject.FromTop))
                            {

                                if (gameObject.Target != null)
                                {
                                    gameObject.FromLeft = gameObject.Target.GetFromLeft();
                                }
                                else
                                {
                                    gameObject.FromLeft = 0;
                                }

                                if (gameObject.Target != null)
                                {
                                    gameObject.FromTop = gameObject.Target.GetFromLeft();
                                }
                                else
                                {
                                    gameObject.FromTop = 0;
                                }


                                foreach (GameObject gameObject2 in loopList)
                                {
                                    Debug.WriteLine("lel");
                                    Debug.WriteLine(loopList.Count);
                                    Debug.WriteLine(gameObject2);
                                }

                            }

                            Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                            //Draw the rect if the rect isnt in yet
                            if (!TestCanvas.Children.Contains(rect))
                            {
                                TestCanvas.Children.Add(rect);
                            }
                        }

                        
                    }
                });
            }
            catch
            {

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
            if (IsKeyPressed("Space"))
            {
                camera.MoveTo(selectedTileIndicator);
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


            #region CreateArrowPrompts
                Tile selectedTile = world.Map.GetTile((int)(selectedTileIndicator.FromTop/16), (int)(selectedTileIndicator.FromLeft/16));
            
                if (selectedTile.OccupiedUnit != null && pathing.ColourOverlay.Count != 0)
                {
                    // Remove already existing Arrows
                    List<GameObject> arrowPrompts = pathing.ArrowPrompts;
                    foreach (GameObject gameObject in arrowPrompts)
                    {
                        gameObjects.Remove(gameObject);
                    }

                    Target start = new Target((selectedTileIndicator.FromTop / 16), ((selectedTileIndicator.FromLeft / 16)));
                    Target end = new Target(crosshair.FromTop / 16, crosshair.FromLeft /16);

                    
                    if (start.GetFromTop() != end.GetFromTop() || start.GetFromLeft() != end.GetFromLeft())
                    {
                        // Create a promptFactory
                        IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");

                        // Get the Arrows as prompt
                        List<GameObject> prompts = pathing.CreateArrows(start, end, selectedTile.OccupiedUnit, promptFactory, world.Map);

                        if (prompts != null)
                        {
                            // Add the prompts to the gameObjectsList
                            foreach (GameObject gameObject in prompts)
                            {
                                // Debug.WriteLine("ArrowPrompt Coords: " + gameObject.FromTop / 16 + "   " + gameObject.FromLeft / 16);
                                gameObjects.Add(gameObject);
                            }
                        }
                    }
                }
            #endregion

            if (world.Player.IsControllable)
            {
                if (IsKeyPressed("LeftMouseButton"))
                {
                    world.Map.DeselectAll(); //To make sure there is only one tile selected

                    #region removePathingPrompts
                    // Remove colourOverlay
                    List<GameObject> colourOverlay = pathing.ColourOverlay;
                    foreach (GameObject gameObject in colourOverlay)
                    {
                        gameObjects.Remove(gameObject);
                    }

                    // Clear the list in the pathing class
                    pathing.EmptyColorOverlay();

                    // Remove already existing Arrows
                    List<GameObject> arrowPrompts = pathing.ArrowPrompts;
                    foreach (GameObject gameObject in arrowPrompts)
                    {
                        gameObjects.Remove(gameObject);
                    }
                    #endregion

                    pressedOnTile.Selected = true;

                    //If you select a unit, check if the current world.Players owns that unit
                    if (world.Player.InGameObjects(pressedOnTile.OccupiedUnit))
                    {
                        world.Player.SelectedUnit = pressedOnTile.OccupiedUnit;

                        selectedTileIndicator.FromLeft = selectedFromLeft * world.Map.Size;
                        selectedTileIndicator.FromTop = selectedFromTop * world.Map.Size;

                        #region setColourOverlay

                        // Set the start Target
                        Target start = new Target((world.Player.SelectedUnit.Target.GetFromTop() / 16), (world.Player.SelectedUnit.Target.GetFromLeft() / 16));

                        // Create a promptFactory
                        IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");

                        // Get the Arrows as prompt
                        List<GameObject> colorOverlay = pathing.SetColorOverlay(world.Player.SelectedUnit, start, promptFactory, world.Player, world.Map);

                        // Add the prompts to the gameObjectsList
                        foreach (GameObject gameObject in colorOverlay)
                        {
                            gameObjects.Add(gameObject);
                        }
                        #endregion
                    }
                    else if (pressedOnTile.OccupiedUnit != null || pressedOnTile.OccupiedStructure != null)
                    {   //Clicked a unit the current world.Player doest own.
                        if (world.Player.SelectedUnit != null) //Current world.Player has a unit selected
                        {
                            if (world.Player.SelectedUnit.IsAllowedToAct)
                            {
                                if (world.Player.SelectedUnit.CanTarget(world.Player.SelectedUnit.Target.GetFromLeft() / 16, world.Player.SelectedUnit.Target.GetFromTop() / 16, pressedOnTile, selectedFromLeft, selectedFromTop))
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
                                    float dmgValue = world.Player.SelectedUnit.Attack(enemyGameObject, pressedOnTile);

                                    #region CreateDmgNumbers
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
                                    #endregion

                                    // End the turn for this Unit and deselect it
                                    world.Player.SelectedUnit.IsAllowedToAct = false;
                                    world.Player.SelectedUnit = null;

                                    // Remove the selectedTileIndicator from sight

                                    selectedTileIndicator.FromLeft = -1000;
                                    selectedTileIndicator.FromTop = -1000;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (world.Player.SelectedUnit != null) //Current world.Player has a unit selected
                        {

                            //Check if the selected unit is allowed to act and if it can target
                            if (
                                world.Player.SelectedUnit.CanTarget(world.Player.SelectedUnit.Target.GetFromLeft() / 16, world.Player.SelectedUnit.Target.GetFromTop() / 16, pressedOnTile, selectedFromLeft, selectedFromTop)
                                && world.Player.SelectedUnit.IsAllowedToAct
                            )
                            {

                                world.Player.SelectedUnit.IsAllowedToAct = false;

                                lock (world.Map.Tiles)
                                {
                                    //If you can move, remove the reference to the tile the unit is in now
                                    for (int fromLeft = 0; fromLeft < world.Map.Tiles.GetLength(0); fromLeft += 1)
                                    {
                                        for (int fromTop = 0; fromTop < world.Map.Tiles.GetLength(1); fromTop += 1)
                                        {
                                            lock (world.Player.SelectedUnit)
                                            {
                                                if(world.Map.GetTile(fromLeft,fromTop).OccupiedUnit != null &&
                                                    world.Map.GetTile(fromLeft, fromTop).OccupiedUnit == world.Player.SelectedUnit
                                                )
                                                {
                                                    world.Map.GetTile(fromLeft, fromTop).OccupiedUnit = null;
                                            
                                                    fromLeft = world.Map.Tiles.GetLength(0);    //Get out of the outer loop
                                                    break;                                      //Get out of the inner loop
                                                }
                                            }
                                        }
                                    }

                                    //Put the unit in the new tile
                                    pressedOnTile.OccupiedUnit = world.Player.SelectedUnit;
                                    world.Player.SelectedUnit.Target = new Target(selectedFromTop * 16, selectedFromLeft * 16);

                                    world.Player.SelectedUnit.IsAllowedToAct = false;
                                }
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
             * Selects the next world.Player.
             */
            if (world.Player.HasAllowedUnits())
            {
                //Do nothing, because the world.Player can still act with a unit
            }
            else
            {
                //Other player has a turn.
                world.Player.SelectedStructure = null;
                world.Player.SelectedUnit = null;

                //The turn of this world.Player has ended. Select the next world.Player, and allow all units to act
                world.Player = world.Player.NextPlayer;

                Debug.WriteLine(world.Player.Colour);

                world.Player.AllowedAllToAct();

                //Hide the tile indicator
                if (world.Player.SelectedUnit != null && world.Player.SelectedUnit.Target != null)
                {
                    selectedTileIndicator.FromLeft = world.Player.SelectedUnit.Target.GetFromLeft();
                    selectedTileIndicator.FromTop = world.Player.SelectedUnit.Target.GetFromTop();
                }
                else
                {
                    //world.Player has no unit selected or selected unit has no tile
                    selectedTileIndicator.FromLeft = -99999;
                    selectedTileIndicator.FromTop = -99999;
                }


                //Check all structures and reduce the production cooldown.
                foreach (GameObject gameObject in world.Player.GetGameObjects())
                {
                    if (gameObject is Structure)
                    {
                        Structure factoryNeedle = gameObject as Structure;
                        if (factoryNeedle.ProductionCooldown > 0)
                        {
                            factoryNeedle.ProductionCooldown = factoryNeedle.ProductionCooldown - 1;
                        }

                        //Use two sprites to have an animation

                        string spriteNow;
                        String spriteLast;
                        switch (factoryNeedle.ProductionCooldown)
                        {
                            case 1:
                                spriteNow = "Sprites/Timer/timer7.gif";
                                spriteLast = "Sprites/Timer/timer6.gif";
                                break;
                            case 2:
                                spriteNow = "Sprites/Timer/timer6.gif";
                                spriteLast = "Sprites/Timer/timer5.gif";
                                break;
                            case 3:
                                spriteNow = "Sprites/Timer/timer5.gif";
                                spriteLast = "Sprites/Timer/timer4.gif";
                                break;
                            case 4:
                                spriteNow = "Sprites/Timer/timer4.gif";
                                spriteLast = "Sprites/Timer/timer3.gif";
                                break;
                            case 5:
                                spriteNow = "Sprites/Timer/timer3.gif";
                                spriteLast = "Sprites/Timer/timer2.gif";
                                break;
                            case 6:
                                spriteNow = "Sprites/Timer/timer2.gif";
                                spriteLast = "Sprites/Timer/timer1.gif";
                                break;
                            case 7:
                                spriteNow = "Sprites/Timer/timer1.gif";
                                spriteLast = "Sprites/Timer/timer0.gif";
                                break;
                            case 8:
                                spriteNow = "Sprites/Timer/timer0.gif";
                                spriteLast = "Sprites/Timer/timer8.gif";
                                break;
                            default:
                                spriteNow = "Sprites/Timer/timer8.gif";
                                spriteLast = "Sprites/Timer/timer7.gif";
                                break;
                        }

                        IAbstractFactory factory = factoryProducer.GetFactory("PromptFactory");

                        Prompt timerLast = (Prompt)factory.GetGameObject(spriteLast, 12, 12, factoryNeedle.FromTop - 6, factoryNeedle.FromLeft - 6);
                        timerLast.IsUsingDuration = true;
                        timerLast.MaxDuration = 4000;

                        Prompt timerNow = (Prompt)factory.GetGameObject(spriteNow, 12, 12, factoryNeedle.FromTop - 6, factoryNeedle.FromLeft - 6);
                        timerNow.IsUsingDuration = true;
                        timerNow.MaxDuration = 9000;

                        gameObjects.Add(timerNow);
                        gameObjects.Add(timerLast);


                        if (factoryNeedle.ProductionCooldown == 0)
                        {
                            factoryNeedle.ProductionCooldown = 8;
                        }

                        //The factory is done acting, so its not allowed to act again.
                        factoryNeedle.IsAllowedToAct = false;
                    }
                }
            }

            /*
             * Remove references in map to destroyed units
             */

            for (int fromLeft = 0; fromLeft < world.Map.Tiles.GetLength(0); fromLeft += 1)
            {
                for (int fromTop = 0; fromTop < world.Map.Tiles.GetLength(1); fromTop += 1)
                {
                    if(world.Map.GetTile(fromLeft,fromTop).OccupiedUnit != null &&
                        world.Map.GetTile(fromLeft, fromTop).OccupiedUnit.destroyed
                    )
                    {
                        world.Map.GetTile(fromLeft, fromTop).OccupiedUnit = null;
                    }
                }
            }

             /*
             * set destroyed on true for units
             */
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject is Unit)
                {
                    Unit unit = gameObject as Unit;
                    if (unit.Health < 0)
                    {
                        gameObject.destroyed = true;
                    }
                }
            }

            /*
             * Destroy destroyed gameobjects.
             */
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject.destroyed)
                {
                    //Remove the gameobject from the tile.
                    for (int fromLeft = 0; fromLeft < world.Map.Tiles.GetLength(0); fromLeft += 1)
                    {
                        for (int fromTop = 0; fromTop < world.Map.Tiles.GetLength(1); fromTop += 1)
                        {
                            if(world.Map.GetTile(fromLeft,fromTop).OccupiedUnit != null &&
                                world.Map.GetTile(fromLeft, fromTop).OccupiedUnit == gameObject
                            )
                            {
                                world.Map.GetTile(fromLeft, fromTop).OccupiedUnit = null;
                            }
                        }
                    }

                    //Remove the gameObject from the array
                    gameObjects.Remove(gameObject);

                    int i = 50;
                    while (world.Player.NextPlayer != null && i > 0)
                    {
                        world.Player.DeleteGameObject(gameObject);
                        i--;
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
