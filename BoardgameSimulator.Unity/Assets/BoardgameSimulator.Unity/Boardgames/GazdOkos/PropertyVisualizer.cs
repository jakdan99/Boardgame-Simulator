using BoardgameSimulator.Unity.Boardgames.GazdOkos;
using UnityEngine;

public class PropertyVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _house;
    [SerializeField] private GameObject _kitchen;
    [SerializeField] private GameObject _livingRoom;
    [SerializeField] private GameObject _fridge;
    [SerializeField] private GameObject _tv;
    [SerializeField] private GameObject _radio;
    [SerializeField] private GameObject _washingMachine;
    [SerializeField] private GameObject _pingPongTable;
    [SerializeField] private GameObject _bicycle;
    [SerializeField] private GameObject _vaccum;
    [SerializeField] private GameObject _bookVoucher1;
    [SerializeField] private GameObject _bookVoucher2;
    [SerializeField] private GameObject _savings;
    [SerializeField] private GameObject _lifeInsurace;
    [SerializeField] private GameObject _houseInsurace;

    public void ShowProperties(PlayerProperties playerProperties)
    {
        _house.SetActive(playerProperties.HouseOwned);
        _kitchen.SetActive(playerProperties.KitchenOwned);
        _livingRoom.SetActive(playerProperties.LivingRoomOwned);
        _fridge.SetActive(playerProperties.FridgeOwned);
        _tv.SetActive(playerProperties.TvOwned);
        _radio.SetActive(playerProperties.RadioOwned);
        _washingMachine.SetActive(playerProperties.WashingMashineOwned);
        _pingPongTable.SetActive(playerProperties.PingPongTableOwned);
        _bicycle.SetActive(playerProperties.BicycleOwned);
        _vaccum.SetActive(playerProperties.VacuumOwned);
        _bookVoucher1.SetActive(playerProperties.BookVoucherAmount >= 2000);
        _bookVoucher2.SetActive(playerProperties.BookVoucherAmount >= 4000);
        _savings.SetActive(playerProperties.SavingsAmount >= 50000);
        _lifeInsurace.SetActive(playerProperties.LifeInsuranceOwned);
        _houseInsurace.SetActive(playerProperties.HouseInsuranceOwned);
    }
}