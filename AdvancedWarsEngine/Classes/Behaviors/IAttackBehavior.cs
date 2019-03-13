namespace AdvancedWarsEngine.Classes
{
    interface IAttackBehavior
    {
        /****************************************************
         * Unit         = The unit that attacks
         * gameObject   = The gameObject that gets attacked
         * **************************************************/
        float Attack(Unit unit, GameObject gameObject);
    }
}