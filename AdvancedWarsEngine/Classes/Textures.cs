﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AdvancedWarsEngine.Classes
{
    static class Textures
    {
        public static Dictionary<string, BitmapImage> textures = new Dictionary<string, BitmapImage>();

        /*private static async Task LoadTextureAsync(CanvasControl sender, string location)
        {
            // Define the new texture
            CanvasBitmap sprite;

            // Try to load the new texture
            try
            {
                //Load the recource.
                sprite = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///" + location));

                // Add new texture to the dictionary
                textures.Add(location, sprite);
            }
            catch (Exception e)
            {
                // Give feedback about the exeption
                Debug.WriteLine(location);
                Debug.WriteLine(e.StackTrace);
            }
        }*/


        /**********************************************************************
         * This function returns the texture. If the texture does not yet exist
         * it calls the setTextureAsync to set the texture. Afterwards it still
         * returns the new set texture, unless the texture could not be found.
         * ********************************************************************/
        /*public static void GetTexture(CanvasControl sender, string location)
        {
            // Check if the dictionary has the key already
            if (!textures.Keys.Contains(location))
            {
                // Get the new texture and set it in the dictionary
                await AddTexturesAsync(sender, location);

                // Check if the new texture is set in the dictionary  
                if (textures.Keys.Contains(location))
                {
                    // Return the requested texture
                    return textures[location];

                }
                else
                {
                    // Give feedback that a texture is missing
                    Debug.WriteLine("Texture is missing", location);

                    // Return null
                    return null;
                }
            }

            // Return the requested texture
            return textures[location];
        
        }*/
    }
}

