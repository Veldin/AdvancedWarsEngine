using System.Diagnostics;

namespace AdvancedWarsEngine.Classes
{
    class PromptFactory : IAbstractFactory
    {
        public GameObject GetGameObject(string type, float width, float height, float fromTop, float fromLeft, string timeoutString = "20000")
        {
            if (timeoutString == "")
            {
                timeoutString = "20000";
            }

            // Check which Prompt shoud be created and returned
            GameObject prompt = null;

            // Check if the value is the location of an image
            bool isImage = (type.Contains(".png") || type.Contains(".gif"));

            IOnTickBehavior onTickBehavior = new DefaultOnTickBehavior();       // The default onTick of the Unit

            // Create the Prompt
            switch (isImage)
            {
                case true:
                    prompt = new Prompt(width, height, fromTop, fromLeft, type);
                    break;
                case false:
                    prompt = new Prompt(width, height, fromTop, fromLeft, type, float.Parse(timeoutString));
                    break;
            }

            if (prompt != null)
            {
                prompt.OnTickBehavior = onTickBehavior;
            }

            // Return the Prompt
            return prompt;
        }
    }
}
