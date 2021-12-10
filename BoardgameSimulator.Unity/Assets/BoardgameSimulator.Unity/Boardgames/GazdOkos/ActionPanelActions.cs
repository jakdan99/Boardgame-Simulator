using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos
{
    public class ActionPanelActions : MonoBehaviour
    {
        [SerializeField] private Gameplay _gameplay = default!;
        [SerializeField] private List<Interactable> _buttons = new List<Interactable>();
        [SerializeField] private Interactable _confirmButton = default!;
        [SerializeField] private GameObject _closeButton = default!;
        [SerializeField] private MoneyCounter _moneyCounter = default!;
        [SerializeField] private TextMeshPro _actionText = default!;
        [SerializeField] private Die _die = default!;
        [SerializeField] private PropertyVisualizer _propertyVisualizer = default!;
        private bool extraTurn = false;

        public void ClosePanel()
        {
            _die.SetCheckNumber();
            _moneyCounter.Evaluate(0, true);
            gameObject.SetActive(false);
            if (!extraTurn)
            {
                _gameplay.NextPlayer();
            }
            extraTurn = false;
        }

        public void CheckMoney(int amount, Action action)
        {
            if (_moneyCounter.Evaluate(amount))
            {
                action();
            }
        }

        private void RemoveListeners()
        {
            foreach (var interactable in _buttons)
            {
                interactable.OnClick.RemoveAllListeners();
            }

            _confirmButton.OnClick.RemoveAllListeners();
        }

        private bool BuyItem(int amount) => _moneyCounter.Evaluate(amount);


        #region TileFunctions


        public void FirstTile()
        {
            RemoveListeners();

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(true);
            _closeButton.SetActive(false);

            _confirmButton.OnClick.AddListener(delegate { CheckMoney(1000, ClosePanel); });

            _actionText.text = "Szemetelt�l, fizess 1000 Ft b�ntet�st.";
        }

        public void ThirdTile(PlayerProperties playerProperties)
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);

            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            _actionText.text = "Takar�koskodj! \n Bet�teid ut�n 5% kamatot kapsz";

            _moneyCounter.MoneyAmount = (int)(0.05 * playerProperties.SavingsAmount);
            _moneyCounter.Evaluate(0, true);
        }

        public void ForthTile(PlayerProperties playerProperties)
        {
            RemoveListeners();


            for (int i = 0; i < 5; ++i)
            {
                _buttons[i].gameObject.SetActive(true);
            }

            for (int i = 5; i < _buttons.Count; ++i)
            {
                _buttons[i].gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);

            _closeButton.SetActive(true);

            _buttons[0].OnClick.AddListener(delegate { playerProperties.TvOwned = BuyItem(60000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[0].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Telev�zi� \n 60000 Ft";
            _buttons[1].OnClick.AddListener(delegate { playerProperties.RadioOwned = BuyItem(20000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[1].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "R�di� \n 20000 Ft";
            _buttons[2].OnClick.AddListener(delegate { playerProperties.FridgeOwned = BuyItem(40000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[2].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "H�t�szekr�ny \n 40000 Ft";
            _buttons[3].OnClick.AddListener(delegate { playerProperties.WashingMashineOwned = BuyItem(50000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[3].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Mos�g�p \n 50000 Ft";
            _buttons[4].OnClick.AddListener(delegate { playerProperties.VacuumOwned = BuyItem(10000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[4].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Porsz�v� \n 10000 Ft";
        }

        public void FifthTile()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);


            _actionText.text = "Nem z�rtad el j�l a csapot, �gy feleslegesen folyt. \n Egy dob�sb�l kimaradsz!";
        }

        public void SixthTile()
        {
            RemoveListeners();

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(true);
            _closeButton.SetActive(false);

            _confirmButton.OnClick.AddListener(delegate { CheckMoney(1500, ClosePanel); });

            _actionText.text = "A doh�nyz�s k�ros, m�gis r�gy�jtott�l. \n Fizess 1 500 Ft b�ntet�st!";

        }

        public void SeventhTile(PlayerProperties playerProperties)
        {
            RemoveListeners();


            for (int i = 0; i < 9; ++i)
            {
                _buttons[i].gameObject.SetActive(true);
            }

            for (int i = 9; i < _buttons.Count; ++i)
            {
                _buttons[i].gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);

            _closeButton.SetActive(true);

            _buttons[0].OnClick.AddListener(delegate { playerProperties.TvOwned = BuyItem(60000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[0].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Telev�zi� \n 60000 Ft";
            _buttons[1].OnClick.AddListener(delegate { playerProperties.RadioOwned = BuyItem(20000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[1].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "R�di� \n 20000 Ft";
            _buttons[2].OnClick.AddListener(delegate { playerProperties.FridgeOwned = BuyItem(40000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[2].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "H�t�szekr�ny \n 40000 Ft";
            _buttons[3].OnClick.AddListener(delegate { playerProperties.WashingMashineOwned = BuyItem(50000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[3].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Mos�g�p \n 50000 Ft";
            _buttons[4].OnClick.AddListener(delegate { playerProperties.VacuumOwned = BuyItem(10000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[4].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Porsz�v� \n 10000 Ft";
            _buttons[5].OnClick.AddListener(delegate { playerProperties.LivingRoomOwned = BuyItem(250000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[5].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Szobab�tor \n 250000 Ft";
            _buttons[6].OnClick.AddListener(delegate { playerProperties.KitchenOwned = BuyItem(150000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[6].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Konyhab�tor \n 150000 Ft";
            _buttons[7].OnClick.AddListener(delegate { playerProperties.PingPongTableOwned = BuyItem(20000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[7].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Pingpongasztal \n 20000 Ft";
            _buttons[8].OnClick.AddListener(delegate { playerProperties.BicycleOwned = BuyItem(15000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[8].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Ker�kp�r \n 15000 Ft";
        }
        public void EighthTile(PlayerProperties playerProperties)
        {
            RemoveListeners();


            for (int i = 0; i < 2; ++i)
            {
                _buttons[i].gameObject.SetActive(true);
            }

            for (int i = 2; i < _buttons.Count; ++i)
            {
                _buttons[i].gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);

            _closeButton.SetActive(true);

            _buttons[0].OnClick.AddListener(delegate { playerProperties.LifeInsuranceOwned = BuyItem(60000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[0].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "�letbiztos�t�s \n 1500 Ft";
            _buttons[1].OnClick.AddListener(delegate { playerProperties.HouseInsuranceOwned = BuyItem(20000); _propertyVisualizer.ShowProperties(playerProperties); });
            _buttons[1].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Lak�sbiztos�t�s \n 2000 Ft";
        }

        public void FourteenthTile(PlayerProperties playerProperties)
        {
            RemoveListeners();

            _buttons[0].gameObject.SetActive(true);

            for (int i = 1; i < _buttons.Count; ++i)
            {
                _buttons[i].gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);

            _closeButton.SetActive(true);

            _buttons[0].OnClick.AddListener(delegate
            {
                playerProperties.HouseOwned = BuyItem(300000);
                playerProperties.Debt = 400000; _propertyVisualizer.ShowProperties(playerProperties);
            });
            _buttons[0].gameObject.GetComponent<ButtonConfigHelper>().MainLabelText = "Banki lak�s�p�t�s \n Fizess be 300 000Ft-ot! \n Tartoz�s 400 000 Ft";
        }
        #endregion

        #region CardFunctions

        public void LC1()
        {
            RemoveListeners();

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            _actionText.text = "Nyerem�nyj�t�kon h�t�szekr�nyt nyert�l. \n" +
                               " Ha van m�r h�t�szekr�nyed, akkor a p�nzt�rost�l megkapod az �rt�k�t: \n" +
                               " 40 000 Ft-ot!";
        }

        public void LC2(PlayerProperties playerProperties)
        {
            RemoveListeners();

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(true);
            _closeButton.SetActive(true);

            _confirmButton.OnClick.AddListener(delegate { playerProperties.HouseInsuranceOwned = BuyItem(2000); });

            _actionText.text = "Lak�sbiztos�t�st k�thetsz \n" +
                               "A k�tv�ny �ra: 2 000 Ft.";
        }

        public void LC3()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            extraTurn = true;

            _actionText.text = "Rejtv�nyp�ly�zaton utaz�st nyert�l, \n" +
                               "l�pj a 11. mez�re!";
        }
        public void LC4()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            _actionText.text = "L�pj a 4. mez�re!";
        }

        public void LC5()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            _actionText.text = "Sportsorsjegyen pingpongasztalt nyert�l. \n" +
                               "Ha van m�r pingpongasztalod, akkor \n" +
                               " 20 000 Ft-ot kapsz a p�nzt�rost�l!";
        }

        public void LC6()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(false);
            _closeButton.SetActive(true);

            _actionText.text = "Bor�t�kos sorsjeggyel \n" +
                               "200 000 Ft-ot nyert�l!";
        }

        public void LC7()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _confirmButton.gameObject.SetActive(true);
            _closeButton.SetActive(false);

            _confirmButton.OnClick.AddListener(delegate { CheckMoney(2000, ClosePanel); });

            _actionText.text = "�tteremben eb�deltek, fizess \n" +
                               "2 000 Ft-ot a p�nzt�rnak!";
        }

        #endregion
    }
}
