using AdvancedWarsEngine.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        private Canvas canvas;
        private World world;
        private Player player;
        private List<GameObject> gameObjects;

        //The max fps we want to run at
        private float fps;  //The set FPS limit
        private float interval; //Interfal that gets calculated based on the fps

        //The brush used to fill in the background
        private SolidColorBrush backgroundBrush;
        private int renderDistance;
        
        //Holds string interpertation of all keys that are pressed right now
        private HashSet<String> pressedKeys;

        private Cursor cursor;

        public MainWindow()
        {

            //Setting game dimensions
            width = 1280;
            height = 720;
            renderDistance = 1200;

            pressedKeys = new HashSet<String>();

            backgroundBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 24, 40, 80));

            InitializeComponent();

            //Bind the keyup/down to the window's keyup/down
            GetWindow(this).KeyUp += KeyUp;
            GetWindow(this).KeyDown += KeyDown;

            this.Cursor = Cursors.None;
            cursor = new Cursor(30, 30, 300, 300);

            fps = 999999999; //Desired max fps.
            interval = 1000 / fps;
            then = Stopwatch.GetTimestamp();

            Run();
        }

        private void Run()
        {
            now = Stopwatch.GetTimestamp();
            delta = (now - then) / 1000; //Defide by 1000 to get the delta in MS

            if (delta > interval)
            {
                then = now; //Remember when this frame was.
            }
            else
            {
                /*
                 * Sleeping the thread for the minimum amount of time. This helps with the stability of the application.
                 * While not hindering the user experience.
                 * */
                Thread.Sleep(1);
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
                    //loopList.Add(cursor);
                }
                catch
                {
                    //if it failes for any reason skip this frame.
                    return;
                }
            }

                        //Run it in the UI thread
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                //TestCanvas.Children.Clear();    //Remove all recs from the canvas, start clean every loop

                TestCanvas.Background = backgroundBrush;


                foreach (GameObject gameObject in loopList)
                {
                    /* Old renderDistance code
                    if (player.distanceBetween(gameObject) > renderDistance)
                    {
                        TestCanvas.Children.Remove(gameObject.rectangle);
                    }
                    else
                    {
                        if (!(gameObject is TextBox))
                        {
                            Rectangle rect = gameObject.rectangle;

                            rect.Width = gameObject.Width + gameObject.RightDrawOffset + gameObject.LeftDrawOffset;
                            rect.Height = gameObject.Height + gameObject.TopDrawOffset + gameObject.BottomDrawOffset;

                            // Set up the position in the window, at mouse coordonate
                            Canvas.SetLeft(rect, gameObject.FromLeft - gameObject.LeftDrawOffset - cameraLeftOffset);
                            Canvas.SetTop(rect, gameObject.FromTop - gameObject.TopDrawOffset - cameraTopOffset);

                            if (!TestCanvas.Children.Contains(rect))
                                TestCanvas.Children.Add(rect);
                        }


                        if (gameObject is TextBox)
                        {
                            TextBox textObject = gameObject as TextBox;
                            textObject.textblock.Margin = new Thickness(textObject.FromLeft, textObject.FromTop, textObject.Width, textObject.Height);

                            if (!TestCanvas.Children.Contains(textObject.textblock))
                                TestCanvas.Children.Add(textObject.textblock);
                        }

                    }
                    */
                }
            });
        }

        private void Logic()
        {
            // DO SOMETHING
        }

        /* KeyDown */
        /* 
        * Add the given key in the pressedKeys collection.
        * The argument is the given key represented as a string.
        */
        public void KeyDown(object sender, KeyEventArgs args)
        {
            pressedKeys.Add(args.Key.ToString());
        }


        /* KeyDown */
        /* 
         * Remove the given key in the pressedKeys collection.
         * The argument is the given key represented as a string.
         */
        public void KeyUp(object sender, KeyEventArgs args)
        {
            pressedKeys.Remove(args.Key.ToString());
        }

        /* IsKeyPressed */
        /* 
         * Returns wheater the given key exists within the pressedKeys collection.
         * The argument is the given key represented as a string.
         */
        public bool IsKeyPressed(string virtualKey)
        {
            //return pressedKeys.Contains(virtualKey);
            return false;
        }
    }
}
