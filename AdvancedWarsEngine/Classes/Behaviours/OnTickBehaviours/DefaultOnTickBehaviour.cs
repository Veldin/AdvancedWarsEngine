using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class DefaultOnTickBehaviour : IOnTickBehaviour
    {
        public bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta, Camera Camera)
        {
            /* Default Unit behaviour*/
            if (gameobject is Prompt)
            {
                Prompt prompt = gameobject as Prompt;

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

            /* Default Unit behaviour*/
            if (gameobject is Unit && gameobject.Target != null)
            {
                Unit unit = gameobject as Unit;

                //The difference between the target and this object
                float differenceLeftAbs = Math.Abs(gameobject.Target.GetFromLeft() - gameobject.FromLeft);
                float differenceTopAbs = Math.Abs(gameobject.Target.GetFromTop() - gameobject.FromTop);

                float totalDifferenceAbs = differenceLeftAbs + differenceTopAbs;

                float differenceTopPercent = differenceTopAbs / (totalDifferenceAbs / 100);
                float differenceLeftPercent = differenceLeftAbs / (totalDifferenceAbs / 100);

                float moveTopDistance = unit.MovementSpeed * (differenceTopPercent / 100);
                float moveLeftDistance = unit.MovementSpeed * (differenceLeftPercent / 100);

                if (totalDifferenceAbs < 2)
                {
                    return true;
                }

                if (gameobject.Target.GetFromLeft() > gameobject.FromLeft)
                {
                    gameobject.AddFromLeft((moveLeftDistance * delta) / 10000);
                }
                else
                {
                    gameobject.AddFromLeft(((moveLeftDistance * delta) / 10000) * -1);
                }

                if (gameobject.Target.GetFromTop() > gameobject.FromTop)
                {
                    gameobject.AddFromTop((moveTopDistance * delta) / 10000);
                }
                else
                {
                    gameobject.AddFromTop(((moveTopDistance * delta) / 10000) * -1);
                }
            }
            return true;
        }
    }
}
