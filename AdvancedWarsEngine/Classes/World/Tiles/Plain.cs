namespace AdvancedWarsEngine.Classes
{
    class Plain : Tile
    {
        public Plain() : base()
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
