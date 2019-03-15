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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //For keeping the sizes of the window (on the canvas, not IRL)
        private int width;
        private int height;

        //For timekeeping (we need to know when the last frame happend when the next frame happens and the delta between)
        private long delta;     //The lenght in time the last frame lasted (so we can use it to calculate speeds of things without slowing down due to low fps)
        private long now;       //This is the time of the frame. (To calculate the delta)
        private long then;      //This is the time of the previous frame. (To calculate the delta)

        private Camera camera;
        private World world;
        private Player player;

        private List<GameObject> gameObjects;

        private string sprite;

        //The max fps we want to run at
        private float fps;  //The set FPS limit
        private float interval; //Interfal that gets calculated based on the fps

        //The brush used to fill in the background
        private SolidColorBrush backgroundBrush;
        private int renderDistance;
        
        //Holds string interpertation of all keys that are pressed right now
        private HashSet<string> pressedKeys;

       
        private Cursor cursor;
        private Prompt crosshair;

        public MainWindow()
        {

            //Setting game dimensions
            width = 1280;
            height = 720;
            renderDistance = 1200;

            pressedKeys = new HashSet<string>();

            backgroundBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 24, 40, 80));

            InitializeComponent();

            //Bind the keyup/down to the window's keyup/down
            GetWindow(this).KeyUp += KeyUp;
            GetWindow(this).KeyDown += KeyDown;

            GetWindow(this).MouseDown += MouseDown;
            GetWindow(this).MouseUp += MouseDown;

            camera = new Camera();

            gameObjects = new List<GameObject>();

            Cursor = Cursors.None; //Hide the default Cursor

            //Create the default crosshair to use
            crosshair = new Prompt(30, 30, 300, 300, "Sprites/TileSelectors/TileSelectorWhite.gif");
            gameObjects.Add(crosshair);

            //Create the default cursor to use
            cursor = new Cursor(30, 30, 300, 300, "Sprites/Cursors/defaultCursor.gif");
            gameObjects.Add(cursor);

            GameObject testUnit = new Unit(30,30, -1,-1);

            gameObjects.Add(testUnit);

            world = new World();

            Tile TestTile = world.Map.GetTile(0, 0);

            Unit testUnitt = testUnit as Unit;
            TestTile.OccupiedUnit = testUnitt;

            fps = 999999999; //Desired max fps.
            interval = 1000 / fps;
            then = Stopwatch.GetTimestamp();

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
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    //TestCanvas.Children.Clear();    //Remove all recs from the canvas, start clean every loop

                    TestCanvas.Background = backgroundBrush;

                    //Set the background
                    Rectangle BgRect = world.Map.rectangle;
                    Canvas.SetLeft(BgRect, 0 + camera.GetLeftOffSet());
                    Canvas.SetTop(BgRect, 0 + camera.GetTopOffSet());


                    if (!TestCanvas.Children.Contains(BgRect))
                    {
                        TestCanvas.Children.Add(BgRect);
                    }

                    //Draw the gameobjects in the loop list
                    foreach (GameObject gameObject in loopList)
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
                Application.Current.Dispatcher.Invoke((Action)delegate
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

            if (selectedFromTop >= world.Map.Tiles.GetLength(1)-1)
            {
                selectedFromTop = world.Map.Tiles.GetLength(1) -1;
            }

            if (selectedFromLeft < 0)
            {
                selectedFromLeft = 0;
            }

            if (selectedFromLeft >= world.Map.Tiles.GetLength(0)-1)
            {
                selectedFromLeft = world.Map.Tiles.GetLength(0) -1;
            }

            world.Map.GetTile(selectedFromTop, selectedFromLeft);
            crosshair.FromTop = world.Map.Size * selectedFromTop;
            crosshair.FromLeft = world.Map.Size * selectedFromLeft;


            if (IsKeyPressed("LeftMouseButton"))
            {
                //world.Map.DeselectAll();
                world.Map.GetTile(selectedFromTop, selectedFromLeft).Selected = true;

            }

            //Destory old objects
            foreach (GameObject gameObject in loopList)
            {
                if (gameObject.destroyed)
                {
                    //Do the deathrattle effect
                    //gameObject.OnDeath(GameObjects, pressedKeys);

                    //If a gameObject is marked to be destroyed remove it from the list and remove them from the canvas
                    gameObjects.Remove(gameObject);
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        TestCanvas.Children.Remove(gameObject.rectangle);
                    });
                }
            }

            //Unpres the left mouse button (As there is no event that fires the mouse up)
            pressedKeys.Remove("LeftMouseButton");
        }

        /* KeyDown */
        /* 
        * Add the given key in the pressedKeys collection.
        * The argument is the given key represented as a string.
        */
        public new void KeyDown(object sender, KeyEventArgs args)
        {
            pressedKeys.Add(args.Key.ToString());
        }


        /* KeyDown */
        /* 
         * Remove the given key in the pressedKeys collection.
         * The argument is the given key represented as a string.
         */
        public new void KeyUp(object sender, KeyEventArgs args)
        {
            pressedKeys.Remove(args.Key.ToString());
        }

        /* MouseDown */
        /* 
        * Add the left mouse button key in the pressedKeys collection.
        */
        private new void MouseDown(object sender, MouseButtonEventArgs e)
        {
            pressedKeys.Add("LeftMouseButton");
        }

        
        /* IsKeyPressed */
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
