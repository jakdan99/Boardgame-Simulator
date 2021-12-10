namespace BoardgameSimulator.Unity.Boardgames.GazdOkos
{
    public class PlayerProperties
    {
        public bool HouseOwned = true;
        public bool LivingRoomOwned = false;
        public bool KitchenOwned = false;
        public bool TvOwned = false;
        public bool RadioOwned = false;
        public bool FridgeOwned = false;
        public bool WashingMashineOwned = false;
        public bool VacuumOwned = false;
        public bool PingPongTableOwned = false;
        public bool BicycleOwned = false;
        public bool LifeInsuranceOwned = false;
        public bool HouseInsuranceOwned = false;
        public int BookVoucherAmount = 0;
        public int SavingsAmount = 0;
        public int Debt = 0;

        public bool CheckWinCondition()
        {
            return HouseOwned && LivingRoomOwned && KitchenOwned && TvOwned && RadioOwned &&
                   FridgeOwned && WashingMashineOwned && VacuumOwned && PingPongTableOwned &&
                   BicycleOwned && BookVoucherAmount >= 4000 && SavingsAmount >= 50000 && Debt == 0;
        }
    }
}
