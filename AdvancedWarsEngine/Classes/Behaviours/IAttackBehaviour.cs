﻿namespace AdvancedWarsEngine.Classes
{
    interface IAttackBehaviour
    {
        /****************************************************
         * Unit         = The unit that attacks
         * gameObject   = The gameObject that gets attacked
         * **************************************************/
        float Attack(Unit unit, GameObject gameObject);
    }
}