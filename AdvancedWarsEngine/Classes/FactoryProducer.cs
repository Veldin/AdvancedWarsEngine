namespace AdvancedWarsEngine.Classes
{
    class FactoryProducer
    {
        public FactoryProducer()
        {
        }

        /**********************************************************************
         * This function creates a factory class. Which factory is created 
         * depends on the string given. If the string does not corresponds
         * with a factory it returns null.
         * ********************************************************************/
        public IAbstractFactory GetFactory(string factory)
        {
            // Check which factory shoud be created and returned
            switch (factory)
            {
                case "UnitFactory":         // Create and return an UnitFactory
                    IAbstractFactory unitFactory = new UnitFactory();
                    return unitFactory;

                case "StructureFactory":    // Create and return an StructureFactory
                    IAbstractFactory structureFactory = new StructureFactory();
                    return structureFactory;

                case "PromptFactory":       // Create and return an PromptFactory
                    IAbstractFactory promptFactory = new PromptFactory();
                    return promptFactory;
                default:
                    return null;
            }
        }
    }
}
