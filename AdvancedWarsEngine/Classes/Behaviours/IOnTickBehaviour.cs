using System.Collections.Generic;

namespace AdvancedWarsEngine.Classes
{
    public interface IOnTickBehaviour
    {
        bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta, Camera Camera);
    }
}
