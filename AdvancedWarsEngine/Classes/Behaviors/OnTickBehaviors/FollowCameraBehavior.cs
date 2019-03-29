using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class FollowCameraBehavior : IOnTickBehavior
    {
        public bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta, Camera Camera)
        {
            if (gameobject is Prompt)
            {
                Prompt gameObjectPrompt = gameobject as Prompt;
                if (gameObjectPrompt.IsFollowingCamera)
                {
                    gameobject.FromLeft = (Camera.FromLeft + 50);
                    gameobject.FromTop = (Camera.FromTop + 50);

                }
            }
            return true;
        }
    }
}
