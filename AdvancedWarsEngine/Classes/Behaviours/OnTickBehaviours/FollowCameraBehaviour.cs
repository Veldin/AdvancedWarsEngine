using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class FollowCameraBehaviour : IOnTickBehaviour
    {
        public bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta, Camera Camera)
        {
            if (gameobject is Prompt)
            {
                Prompt prompt = gameobject as Prompt;
                if (prompt.IsFollowingCamera)
                {
                    gameobject.FromLeft = (Camera.FromLeft * -1);
                    gameobject.FromTop = (Camera.FromTop * -1);

                }

                if (prompt.IsUsingDuration)
                {
                    prompt.IncreaseCurrentDuration(delta);
                }

                if (prompt.CurrentDuration > prompt.MaxDuration && prompt.IsUsingDuration)
                {
                    prompt.Destroyed = true;
                }

                if (prompt.IsAscending)
                {
                    prompt.AddFromTop(-0.002f * delta);
                }

                
            }
            return true;
        }
    }
}
