using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWarsEngine.Classes.Behaviors
{
    interface IOnTickBehavior
    {
        bool OnTick(GameObject gameobject, List<GameObject> gameObjects, float delta);
    }
}
