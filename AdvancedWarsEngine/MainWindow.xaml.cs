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
        private long delta;                         //The lenght in time the last frame lasted (so we can use it to calculate speeds of things without slowing down due to low fps)
        private long now;                           //This is the time of the frame. (To calculate the delta)
        private long then;                          //This is the time of the previous frame. (To calculate the delta)

        private Camera camera;
        private World world;

        private GameObjectList gameObjects;
        private HashSet<string> pressedKeys;        //Holds string interpertation of all keys that are pressed right now

        private float fps;                          //The set FPS limit
        private float interval;                     //Interval that gets calculated based on the fps

        private SolidColorBrush backgroundBrush;    //The brush used to fill in the background

        private float skipTurnCooldown;             //Skiping turns cant be done directly at the start of the turn

        private Cursor cursor;                      //Holds information about the cursor
        private Prompt crosshair;                   //Holds information of the crosshair
        private Prompt selectedTileIndicator;       //Holds information about the selected crosshair
        private Pathing pathing;

        //Due to a known issue with StopWatch it runs slower on certain cpu architecture.
        private bool fastmode;                      //If true game is sped up

        //Holds the factoryProducer
        FactoryProducer factoryProducer;

        public MainWindow()
        {
            pressedKeys = new HashSet<string>();

            backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 24, 40, 80));

            InitializeComponent();

            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;

            //Bind the keyup/down to the window's keyup/down
            GetWindow(this).KeyUp += KeyUp;
            GetWindow(this).KeyDown += KeyDown;

            GetWindow(this).MouseDown += MouseDown;
            GetWindow(this).MouseUp += MouseDown;

            gameObjects = new GameObjectList();

            LoadWorld("plainlevel");

            camera = new Camera(world.Map.Tiles.GetLength(0), world.Map.Tiles.GetLength(1));

            Cursor = Cursors.None; //Hide the default Cursor

            //Desired max fps
            fps = 90; //Desired max fps.
            interval = 1000 / fps;
            then = Stopwatch.GetTimestamp();

            // Create a Pathing class
            pathing = new Pathing();

            /* Due to stopwatch returning defferent values for other CPU values we created a fast and a slow mode */
            fastmode = false;

            //check
            long stopWatchTest;
            stopWatchTest = Stopwatch.GetTimestamp();
            Thread.Sleep(1000); //Pas a precise second
            stopWatchTest = Stopwatch.GetTimestamp() - stopWatchTest;
            long change = stopWatchTest / 10000000;

            //slow pc gives 0
            //quick pc gives 1
            if (change < 1){fastmode = true;}

            Debug.WriteLine(fastmode);

            Debug.WriteLine(fastmode);

            RunAsync();

        }

        public void RunAsync()
        {
            now = Stopwatch.GetTimestamp();
            delta = (now - then) / 1000; //Defide by 1000 to get the delta in MS

            then = now; //Remember when this frame was.
            lock (this)
            {
                //Speed the game up on slower stopwatches  
                if (fastmode)
                {
                    Logic(delta * 3); //Run the logic of the simulation.
                }
                else
                {
                    Logic(delta); //Run the logic of the simulation.
                }
                Draw();
            }
            Task.Delay((int)interval);

            //Task.Yield();  //Force this task to complete asynchronously (This way the main thread is not blocked by this task calling itself.
            Task.Run(() => RunAsync());  //Schedule new Run() task
        }

        private void DrawUnits(ArrayList loopList)
        {
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject is Unit)
                {
                    Rectangle rect = gameObject.rectangle;

                    rect.Width = gameObject.Width;
                    rect.Height = gameObject.Height;

                    Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                    Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                    if (double.IsNaN(gameObject.FromLeft) || double.IsNaN(gameObject.FromTop))
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
        }

        private void DrawStructures(ArrayList loopList)
        {
            foreach (GameObject gameObject in loopList)
            {

                if (gameObject is Structure)
                {
                    Rectangle rect = gameObject.rectangle;

                    rect.Width = gameObject.Width;
                    rect.Height = gameObject.Height + gameObject.HightOffset;

                    //Set the background
                    Rectangle BgRect = world.Map.rectangle;

                    if (double.IsNaN(gameObject.FromLeft) || double.IsNaN(gameObject.FromTop))
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
                    }

                    Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                    Canvas.SetTop(rect, (gameObject.FromTop - gameObject.HightOffset) + camera.GetTopOffSet());

                    //Draw the rect if the rect isnt in yet
                    if (!TestCanvas.Children.Contains(rect))
                    {
                        TestCanvas.Children.Add(rect);
                    }
                }
            }
        }

        private void DrawCursor(ArrayList loopList)
        {
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject is Cursor)
                {
                    Rectangle rect = gameObject.rectangle;

                    rect.Width = gameObject.Width;
                    rect.Height = gameObject.Height;

                    Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                    Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());

                    if (double.IsNaN(gameObject.FromLeft) || double.IsNaN(gameObject.FromTop))
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
        }


        private void DrawPrompts(ArrayList loopList)
        {
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

                        if (prompt.IsFollowingCamera)
                        {
                            Canvas.SetLeft(textBlock, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(textBlock, gameObject.FromTop + camera.GetTopOffSet());
                        } else
                        {
                            Canvas.SetLeft(textBlock, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(textBlock, gameObject.FromTop + camera.GetTopOffSet());
                        }

                        if (!TestCanvas.Children.Contains(textBlock))
                        {
                            TestCanvas.Children.Add(textBlock);
                        }
                        // Move the prompt a little bit up if isAscending is true
                        if (prompt.IsAscending)
                        {
                            prompt.FromTop -= 0.02f;
                        }
                    }
                    else
                    {
                        // Move the prompt a little bit up if isAscending is true
                        if (prompt.IsAscending)
                        {
                            prompt.FromTop -= 0.02f;
                        }

                        Rectangle rect = gameObject.rectangle;

                        rect.Width = gameObject.Width;
                        rect.Height = gameObject.Height;
                        
                        if (prompt.IsFollowingCamera)
                        {
                            Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());
                        } else
                        {
                            Canvas.SetLeft(rect, gameObject.FromLeft + camera.GetLeftOffSet());
                            Canvas.SetTop(rect, gameObject.FromTop + camera.GetTopOffSet());
                        }

                        if (double.IsNaN(gameObject.FromLeft) || double.IsNaN(gameObject.FromTop))
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
                        }

                        //Draw the rect if the rect isnt in yet
                        if (!TestCanvas.Children.Contains(rect))
                        {
                            prompt.IncreaseCurrentDuration(delta);
                            TestCanvas.Children.Add(rect);
                        }
                    }

                    prompt.IncreaseCurrentDuration(delta);
                }
            }
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
                    loopList = new ArrayList(gameObjects.List);
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

                    Rectangle BgRect = world.Map.rectangle;


                    Canvas.SetLeft(BgRect, 0 + camera.GetLeftOffSet());
                    Canvas.SetTop(BgRect, 0 + camera.GetTopOffSet());

                    if (!TestCanvas.Children.Contains(BgRect))
                    {
                        TestCanvas.Children.Add(BgRect);
                    }

                    //Draw the gameobjects in the loop list exept for the Prompts
                    DrawStructures(loopList);
                    DrawUnits(loopList);
                    DrawPrompts(loopList);
                    DrawCursor(loopList);
                });
            }
            catch
            {
            }
        }

        private void Logic(long delta)
        {
            //Create a new instance of GameObjects used to hold the gameobjects for this loop.
            GameObjectList loopList;
            lock (gameObjects) //lock the gameobjects for duplication
            {
                try
                {
                    //Try to duplicate the arraylist.
                    loopList = new GameObjectList(gameObjects.List);
                }
                catch
                {
                    //if it failes for any reason skip this frame.
                    return;
                }
            }

            //For every gameobject in the room
            loopList.OnTick(gameObjects.List, delta);

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
                //THe application disatcher could not be invoked.
                // (The app is probably killed)
            }

            //Key input
            if (IsKeyPressed("W"))
            {
                camera.AddFromTop((float)0.1);
            }

            if (IsKeyPressed("M") && IsKeyPressed("D1"))
            {
                ClearWorld();
                LoadWorld("plainlevel");
            }
            else if (IsKeyPressed("M") && IsKeyPressed("D2"))
            {
                ClearWorld();
                LoadWorld("lavalevel");
            }
            else if (IsKeyPressed("M") && IsKeyPressed("D3"))
            {
                ClearWorld();
                LoadWorld("desertlevel");
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

            if (IsKeyPressed("Return"))
            {
                if (world.CurrentPlayer.IsControllable && skipTurnCooldown < 1)
                {
                    world.CurrentPlayer.AllowNoneToAct();
                }
            }

            if (IsKeyPressed("V"))
            {
                Debug.WriteLine("The selected unit is deselected");

                // Deselect the selected Unit
                world.CurrentPlayer.DeselectUnit();

                // Set the SelectedTileIndictor outside the map
                selectedTileIndicator.FromTop = -100;
                selectedTileIndicator.FromLeft = -100;

                //***********************Remove Pathing Prompts
                //Remove colourOverlay
                List<GameObject> colourOverlay = pathing.ColourOverlay;
                foreach (GameObject gameObject in colourOverlay)
                {
                    gameObject.Destroyed = true;
                }

                //Clear the list in the pathing class
                pathing.EmptyColorOverlay();

                //Remove already existing Arrows
                List<GameObject> arrowPrompts = pathing.ArrowPrompts;
                foreach (GameObject gameObject in arrowPrompts)
                {
                    gameObject.Destroyed = true;
                }
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
            Tile selectedTile = world.Map.GetTile((int)(selectedTileIndicator.FromTop / 16), (int)(selectedTileIndicator.FromLeft / 16));

            crosshair.FromTop = world.Map.Size * selectedFromTop;
            crosshair.FromLeft = world.Map.Size * selectedFromLeft;

            if (selectedTile.OccupiedUnit != null && pathing.ColourOverlay.Count != 0)
            {
                //Remove already existing Arrows
                List<GameObject> arrowPrompts = pathing.ArrowPrompts;
                foreach (GameObject gameObject in arrowPrompts)
                {
                    //Dont wait till draw phase for objects. (needs to be quicker)
                    gameObjects.Remove(gameObject);
                }

                Target start = new Target((selectedTileIndicator.FromTop / 16), ((selectedTileIndicator.FromLeft / 16)));
                Target end = new Target(crosshair.FromTop / 16, crosshair.FromLeft / 16);

                if (start.GetFromTop() != end.GetFromTop() || start.GetFromLeft() != end.GetFromLeft())
                {
                    //Create a promptFactory
                    IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");

                    // Get the Arrows as prompt
                    List<GameObject> prompts = pathing.CreateArrows(start, end, selectedTile.OccupiedUnit, promptFactory, world.CurrentPlayer, world.Map);


                    //Add the prompts to the gameObjectsList
                    if (prompts != null)
                    {
                        foreach (GameObject gameObject in prompts)
                        {
                            if (gameObject != null)
                            {
                                gameObjects.Add(gameObject);
                            }
                        }
                    }
                }
            }

            if (world.CurrentPlayer.IsControllable)
            {
                if (IsKeyPressed("LeftMouseButton"))
                {
                    world.Map.DeselectAll(); //To make sure there is only one tile selected

                    //Remove colourOverlay
                    List<GameObject> colourOverlay = pathing.ColourOverlay;
                    foreach (GameObject gameObject in colourOverlay)
                    {
                        gameObject.Destroyed = true;
                    }

                    //Clear the list in the pathing class
                    pathing.EmptyColorOverlay();

                    //Remove already existing Arrows
                    List<GameObject> arrowPrompts = pathing.ArrowPrompts;
                    foreach (GameObject gameObject in arrowPrompts)
                    {
                        gameObject.Destroyed = true;
                    }

                    pressedOnTile.Selected = true;

                    //If you select a unit, check if the current world.Players owns that unit and check if its not allowed to move
                    if (world.CurrentPlayer.InGameObjects(pressedOnTile.OccupiedUnit) && !pressedOnTile.OccupiedUnit.IsAllowedToAct)
                    {
                        //Create a promptFactory then create a Prompt
                        Prompt disabledMark = (Prompt)factoryProducer.GetFactory("PromptFactory").GetGameObject("Sprites/unitDisabled.gif", 6, 6, pressedOnTile.OccupiedUnit.FromTop - 3, pressedOnTile.OccupiedUnit.FromLeft - 3);
                        disabledMark.IsUsingDuration = true;
                        disabledMark.MaxDuration = 9000;

                        gameObjects.Add(disabledMark);
                    }
                    //If you select a unit, check if the current world.Players owns that unit
                    else if (world.CurrentPlayer.InGameObjects(pressedOnTile.OccupiedUnit))
                    {
                        world.CurrentPlayer.SelectedUnit = pressedOnTile.OccupiedUnit;

                        /*************************************** AUTOMOVE ***************************************/
                        /*
                        int stepsRemaining = 0;
                        bool okay = true;
                        while (!Move(world.CurrentPlayer.SelectedUnit.AutoMove(world, okay, stepsRemaining)))
                        {
                            if (stepsRemaining > 10)
                            {
                                okay = false;
                                stepsRemaining = 0;
                            }
                            else
                            {
                                stepsRemaining++;
                            }
                        }*/
                        /****************************************************************************************/

                        /*************************************** OLD MOVE ***************************************/
                        selectedTileIndicator.FromLeft = selectedFromLeft * world.Map.Size;
                        selectedTileIndicator.FromTop = selectedFromTop * world.Map.Size;

                        //Set the start Target
                        Target start = new Target((world.CurrentPlayer.SelectedUnit.Target.GetFromTop() / 16), (world.CurrentPlayer.SelectedUnit.Target.GetFromLeft() / 16));

                        //Create a promptFactory
                        IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");

                        //Get the Arrows as prompt
                        List<GameObject> colorOverlay = pathing.SetColorOverlay(world.CurrentPlayer.SelectedUnit, start, promptFactory, world.CurrentPlayer, world.Map);

                        //Add the prompts to the gameObjectsList
                        foreach (GameObject gameObject in colorOverlay)
                        {
                            // Dont give the tile where the unit stand on a colorOverlay
                            if (gameObject.FromTop == start.GetFromTop() * 16 && gameObject.FromLeft == start.GetFromLeft() * 16)
                            {
                                continue;
                            }

                            gameObjects.Add(gameObject);
                        }
                        /****************************************************************************************/
                    }
                    else if (pressedOnTile.OccupiedUnit != null || pressedOnTile.OccupiedStructure != null)
                    {   //Clicked a unit the current world.Player doest own.
                        if (world.CurrentPlayer.SelectedUnit != null) //Current world.Player has a unit selected
                        {
                            if (world.CurrentPlayer.SelectedUnit.IsAllowedToAct)
                            {
                                if (world.CurrentPlayer.SelectedUnit.CanTarget(world.CurrentPlayer.SelectedUnit.Target.GetFromLeft() / 16, world.CurrentPlayer.SelectedUnit.Target.GetFromTop() / 16, pressedOnTile, selectedFromLeft, selectedFromTop, world.CurrentPlayer))
                                {
                                    GameObject enemyGameObject;                    //Define a GameObject
                                    enemyGameObject = pressedOnTile.OccupiedUnit;  //Try to get the Unit from the selected tile

                                    //If there is no Unit try to select the structure
                                    if (enemyGameObject == null)
                                    {
                                        enemyGameObject = pressedOnTile.OccupiedStructure;
                                    }

                                    //The selectedUnit attacks the Unit on the selectedTile
                                    float dmgValue = world.CurrentPlayer.SelectedUnit.Attack(enemyGameObject, pressedOnTile);

                                    //Create a promptFactory
                                    IAbstractFactory promtFactory = factoryProducer.GetFactory("PromptFactory");

                                    //Display the damageValue in a prompt
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {                                       
                                        gameObjects.Add(promtFactory.GetGameObject("", 22, 15, enemyGameObject.FromTop - 15, enemyGameObject.FromLeft-5));
                                        gameObjects.Add(promtFactory.GetGameObject(dmgValue.ToString(), 20, 13, enemyGameObject.FromTop - 14, enemyGameObject.FromLeft-4));
                                    });

                                    //End the turn for this Unit and deselect it
                                    world.CurrentPlayer.SelectedUnit.IsAllowedToAct = false;
                                    world.CurrentPlayer.SelectedUnit = null;

                                    //Remove the selectedTileIndicator from sight
                                    selectedTileIndicator.FromLeft = -1000;
                                    selectedTileIndicator.FromTop = -1000;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (world.CurrentPlayer.SelectedUnit != null) //Current world.Player has a unit selected
                        {
                            Move(null);
                        }
                        else
                        {
                            //Do nothing
                        }
                    }
                }
            }

            //Selects the next world.Player.
            if (world.CurrentPlayer.HasAllowedUnits())
            {
                //Aslong as the player has allowed Units its his turn.
                //Reduce the turn timer
                skipTurnCooldown -= delta;

                // Check if the player is defeated
                if (CheckVictory(world.CurrentPlayer))
                {
                    CreateVictoryPrompt(true);
                }
                }
            else
            {
                //Other player has a turn.
                world.CurrentPlayer.SelectedStructure = null;
                world.CurrentPlayer.SelectedUnit = null;

                //The first 8000 units of delta the skip turn is disabled
                skipTurnCooldown = 8000;

                //The turn of this world.Player has ended. Select the next world.Player, and allow all units to act
                world.CurrentPlayer = world.CurrentPlayer.NextPlayer;

                // Create the prompt to shows whos turn it is
                CreateTurnPrompt();

                // Check if the player is defeated
                if (CheckVictory(world.CurrentPlayer))
                {
                    CreateVictoryPrompt(true);
                }
                
                Player nextPlayer = world.CurrentPlayer.NextPlayer;

                while (true)
                {
                    if (world.CurrentPlayer == nextPlayer)
                    {
                        //CreateDefeatPrompt(false);
                        break;
                    }

                    if (!nextPlayer.IsDefeated)
                    {
                        break;
                    }

                    nextPlayer = nextPlayer.NextPlayer;
                }

                world.CurrentPlayer.AllowAllToAct();

                //Hide the tile indicator
                if (world.CurrentPlayer.SelectedUnit != null && world.CurrentPlayer.SelectedUnit.Target != null)
                {
                    selectedTileIndicator.FromLeft = world.CurrentPlayer.SelectedUnit.Target.GetFromLeft();
                    selectedTileIndicator.FromTop = world.CurrentPlayer.SelectedUnit.Target.GetFromTop();
                }
                else
                {
                    //world.Player has no unit selected or selected unit has no tile
                    selectedTileIndicator.FromLeft = -99999;
                    selectedTileIndicator.FromTop = -99999;
                }

                //Check all structures and reduce the production cooldown.
                List<Structure> GetStructures = world.CurrentPlayer.GetStructures();
                foreach (GameObject gameObject in GetStructures)
                {
                    if (gameObject is Structure)
                    {
                        Structure factoryNeedle = gameObject as Structure;
                        if (factoryNeedle.ProductionCooldown > 0)
                        {
                            factoryNeedle.ProductionCooldown = factoryNeedle.ProductionCooldown - 1;
                        }

                        //Use two sprites to have an animation, one with a shorter and longer timeout.
                        string spriteNow;
                        string spriteLast;
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

                        //Check if the factory is allowed to produce
                        if (factoryNeedle.ProductionCooldown == 0)
                        {
                            Tile tileOfFactory = world.Map.GetTileFromGameobject(factoryNeedle);
                            if (tileOfFactory != null && tileOfFactory.OccupiedUnit == null)
                            {
                                factory = factoryProducer.GetFactory("UnitFactory");
                                GameObject spawn = factory.GetGameObject(factoryNeedle.GetProduced(), 16, 16, factoryNeedle.FromTop, factoryNeedle.FromLeft, world.CurrentPlayer.Colour);

                                spawn.Target = new Target(factoryNeedle.Target.GetFromTop(), factoryNeedle.Target.GetFromLeft());
                                spawn.IsAllowedToAct = true;

                                tileOfFactory.OccupiedUnit = spawn as Unit;

                                gameObjects.Add(spawn as GameObject);
                                world.CurrentPlayer.AddGameObject(spawn);
                            }

                            factoryNeedle.ProductionCooldown = 8;
                        }

                        //The factory is done acting, so its not allowed to act again.
                        factoryNeedle.IsAllowedToAct = false;
                    }
                }
            }

            //Remove references in map to destroyed units
            for (int fromLeft = 0; fromLeft < world.Map.Tiles.GetLength(0); fromLeft += 1)
            {
                for (int fromTop = 0; fromTop < world.Map.Tiles.GetLength(1); fromTop += 1)
                {
                    if (world.Map.GetTile(fromLeft, fromTop).OccupiedUnit != null &&
                        world.Map.GetTile(fromLeft, fromTop).OccupiedUnit.Destroyed)
                    {
                        world.Map.GetTile(fromLeft, fromTop).OccupiedUnit = null;
                    }
                }
            }

            //Set destroyed on true for units
            foreach (GameObject gameObject in loopList.List)
            {
                if (gameObject is Unit)
                {
                    Unit unit = gameObject as Unit;
                    if (unit.Health < 0)
                    {
                        gameObject.Destroyed = true;
                    }
                }
            }

            //Destroy destroyed gameobjects.
            foreach (GameObject gameObject in loopList.List)
            {
                if (gameObject.Destroyed)
                {
                    //Remove the gameobject from the tile.
                    for (int fromLeft = 0; fromLeft < world.Map.Tiles.GetLength(0); fromLeft += 1)
                    {
                        for (int fromTop = 0; fromTop < world.Map.Tiles.GetLength(1); fromTop += 1)
                        {
                            if (world.Map.GetTile(fromLeft, fromTop).OccupiedUnit != null &&
                                world.Map.GetTile(fromLeft, fromTop).OccupiedUnit == gameObject)
                            {
                                world.Map.GetTile(fromLeft, fromTop).OccupiedUnit = null;
                            }

                            if (world.Map.GetTile(fromLeft, fromTop).OccupiedStructure != null &&
                                world.Map.GetTile(fromLeft, fromTop).OccupiedStructure == gameObject)
                            {
                                world.Map.GetTile(fromLeft, fromTop).OccupiedStructure = null;
                            }
                        }
                    }

                    //Remove the gameObject from the array
                    gameObjects.Remove(gameObject);

                    int i = 50;
                    Player playerNeelde = world.CurrentPlayer;
                    while (playerNeelde.NextPlayer != null && i > 0)
                    {
                        playerNeelde = playerNeelde.NextPlayer;
                        playerNeelde.DeleteGameObject(gameObject);

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
        * Sets the world variable.
        * Add the gameobjects from the world to the main list.
        */
        public void LoadWorld(string worldString)
        {
            factoryProducer = new FactoryProducer();
            IAbstractFactory factory = factoryProducer.GetFactory("UnitFactory");

            world = new World(factoryProducer, worldString);

            //Add all the gameObjects from the world to the main gameplay loop.
            gameObjects.AddRange(world.GetGameObjects());

            cursor = new Cursor(12, 12, 300, 300, "Sprites/Cursors/defaultCursor.gif"); //Create the default cursor to use
            gameObjects.Add(cursor);

            //Create the default crosshair to use
            crosshair = new Prompt(16, 16, 0, 0, "Sprites/TileSelectors/TileSelectorWhite.gif");
            gameObjects.Add(crosshair);

            //Create the default cursor to use
            selectedTileIndicator = new Prompt(16, 16, 0, 0, "Sprites/TileSelectors/TileSelectorGreen.gif");
            gameObjects.Add(selectedTileIndicator);
        }

        private void ClearWorld()
        {
            foreach (Tile tile in world.Map.Tiles)
            {
                tile.OccupiedUnit = null;
                tile.OccupiedStructure = null;
                tile.Selected = false;
            }

            world = null;
            factoryProducer = null;
            gameObjects.Clear();
        }

        /* 
        * Add the given key in the pressedKeys collection.
        * The argument is the given key represented as a string.
        */
        public new void KeyDown(object sender, KeyEventArgs args)
        {
            pressedKeys.Add(args.Key.ToString());
        }

        /* Remove the given key in the pressedKeys collection.
         * The argument is the given key represented as a string. */
        public new void KeyUp(object sender, KeyEventArgs args)
        {
            pressedKeys.Remove(args.Key.ToString());
        }

        // Add the left mouse button key in the pressedKeys collection.
        private new void MouseDown(object sender, MouseButtonEventArgs e)
        {
            pressedKeys.Add("LeftMouseButton");
        }

        /* Returns wheater the given key exists within the pressedKeys collection.
         * The argument is the given key represented as a string. */
        public bool IsKeyPressed(string virtualKey)
        {
            return pressedKeys.Contains(virtualKey);
        }

        /// <summary>
        /// This function creates a prompt that shows whos turn it is
        /// </summary>
        private void CreateTurnPrompt()
        {
            // Create a promptFactory
            IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");

            // Get the correct sprite location
            //TODO: make 4 player support
            string spriteLocation;
            if (world.CurrentPlayer.IsControllable)
            {
                spriteLocation = "/Sprites/TurnBanners/RedPlayer.png";
            }
            else
            {
                spriteLocation = "/Sprites/TurnBanners/BluePlayer.png";
            }

            // Create the prompt and cast it to a prompt
            GameObject turnGameObject = promptFactory.GetGameObject(spriteLocation, 50, 16, 0, 0);
            Prompt turnPrompt = turnGameObject as Prompt;

            // Give the prompt a maxDuration and set isUsingDuration on true
            turnPrompt.MaxDuration = 10000;
            turnPrompt.IsUsingDuration = true;
            turnPrompt.IsFollowingCamera = true;

            // Add the prompt to the gameObjects list
            gameObjects.Add(turnPrompt);
        }

        /// <summary>
        /// Checks if the Player is defeated by checking if the player still has structures
        /// </summary>
        /// <param name="player"> The player of which the defeat is checked</param>
        /// <returns> Returns if the player is defeated</returns>
        private bool CheckVictory(Player player)
        {
            // Checks if the player is already marked as defeated
            if (player.IsDefeated)
            {
                return true;
            }

            // Get the list of gameObjects that the player has
            List<GameObject> playersGameObjects = world.CurrentPlayer.GetGameObjects();

            // Check if the player still has structures
            bool hasStructures = false;
            foreach (GameObject gameObject in playersGameObjects)
            {
                if (gameObject is Structure)
                {
                    hasStructures = true;
                    break;
                }
            }

            // If the player has no structues, mark the player as defeated
            if (!hasStructures)
            {
                player.IsDefeated = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates victory of defeat prompts
        /// </summary>
        /// <param name="isDefeated"> If is defeated create defeat promps else create victory prompts</param>
        private void CreateVictoryPrompt(bool isDefeated)
        {
            string spriteLocation = "";
            if (!isDefeated)
            {
                spriteLocation = "Sprites/defeat.gif";
            }
            else
            {
                spriteLocation = "Sprites/victory.gif";
            }

            // Create the defeat prompt
            IAbstractFactory promptFactory = factoryProducer.GetFactory("PromptFactory");
            GameObject defeat = promptFactory.GetGameObject(spriteLocation, 200, 64, camera.GetTopOffSet() + 75, camera.GetLeftOffSet() + 70);
            Prompt defeatPrompt = defeat as Prompt;

            // Give the prompt a maxDuration and set isUsingDuration on true
            defeatPrompt.MaxDuration = 100000;
            defeatPrompt.IsUsingDuration = true;

            // Add the prompt to the gameObjects list
            gameObjects.Add(defeatPrompt);
        }

        /// <summary>
        ///  This function moves the selected Unit to given tile/target if allowed
        /// </summary>
        /// <param name="target"> the target where the unit moves to</param>
        /// <returns></returns>
        private bool Move(Target target)
        {
            // Check if there is a player selected
            if (world.CurrentPlayer.SelectedUnit == null)
            {
                return false;
            }

            //Check if the selected unit is allowed to act and if it can target
            if (world.CurrentPlayer.SelectedUnit.IsAllowedToAct)
            {
                // Set the target in tiles
                Target tmp = new Target(world.CurrentPlayer.SelectedUnit.Target.GetFromTop() / 16, world.CurrentPlayer.SelectedUnit.Target.GetFromLeft() / 16);

                // If target is null set the crosshair as target
                Target pressedTileTarget;
                if (target == null)
                {
                    pressedTileTarget = new Target(crosshair.FromTop / 16, crosshair.FromLeft / 16);
                }
                else
                {
                    pressedTileTarget = target;
                }

                // Get the tile of the destination
                Tile pressedTile = world.Map.GetTile((int)pressedTileTarget.GetFromTop(), (int)pressedTileTarget.GetFromLeft());

                // Get the path
                List<Tile> path = pathing.GetPath(tmp, pressedTileTarget, world.CurrentPlayer.SelectedUnit, world.Map, world.CurrentPlayer);

                // If the path is null the target is not allowed 
                if (path == null)
                {
                    return false;
                }

                // Get the map from world
                Map map = world.Map;

                // Remember the old tile
                Tile oldTile = map.GetTile((int)world.CurrentPlayer.SelectedUnit.Target.GetFromTop() / 16, (int)world.CurrentPlayer.SelectedUnit.Target.GetFromLeft() / 16);

                // Remove unit form the old tile and set it on the new tile
                Tile newTile = path[path.Count - 1];
                oldTile.OccupiedUnit = null;
                newTile.OccupiedUnit = world.CurrentPlayer.SelectedUnit;

                // Get the new target in units
                Target newTargetInTiles = map.GetTileCoords(newTile);
                Target newTarget = new Target(newTargetInTiles.GetFromTop() * 16, newTargetInTiles.GetFromLeft() * 16);

                // Give the Unit the new target
                world.CurrentPlayer.SelectedUnit.Target = newTarget;

                // Set the selectedTileIndicator out of view
                selectedTileIndicator.FromTop = -100;
                selectedTileIndicator.FromLeft = -100;

                // Set allowedToAct on false because it has moved
                world.CurrentPlayer.SelectedUnit.IsAllowedToAct = false;

                return true;
            }
            return false;
        }
    }
}
