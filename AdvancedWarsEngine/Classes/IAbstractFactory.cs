namespace AdvancedWarsEngine.Classes
{
    interface IAbstractFactory
    {
        GameObject GetGameObject(string type, float width, float height, float fromTop, float fromLeft, string value = "");
    }
}