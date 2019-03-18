namespace AdvancedWarsEngine.Classes
{
    interface IAbstractFactory
    {
        GameObject GetGameObject(string value, float width, float height, float fromTop, float fromLeft);
    }
}