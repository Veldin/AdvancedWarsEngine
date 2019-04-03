namespace AdvancedWarsEngine.Classes
{
    class Urban : Tile
    {
        public Urban() : base()
        {
        }

        public override void CalculateAirDefenceDefenceMultiplier()
        {
            airDefenceMultiplier = defaultDefenceMultiplier + 1;
        }

        public override void CalculateInfantryDefenceMultiplier()
        {
            infantryDefenceMultiplier = defaultDefenceMultiplier + 1;
        }

        public override void CalculateVehicleDefenceMultiplier()
        {
            vehicleDefenceMultiplier = defaultDefenceMultiplier + 1;
        }
    }
}
