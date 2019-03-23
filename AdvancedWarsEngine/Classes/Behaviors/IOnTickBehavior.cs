using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    public interface IOnTickBehavior
    {
        bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta);
    }
}
