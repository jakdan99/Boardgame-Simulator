using BoardgameSimulator.Unity.Boardgames.GazdOkos.Szerencse;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gamePieces = default!;
        [SerializeField] private GameObject _markerParent = default!;
        [SerializeField] private List<GameObject> _moneyTypes = default!;
        [SerializeField] private TextMeshProUGUI _actualPlayerText = default!;
        [SerializeField] private ActionPanelActions _actionPanelActions = default!;
        [SerializeField] private PropertyVisualizer _propertyVisualizer = default!;
        [SerializeField] private TextMeshProUGUI _winnerText = default!;
        [SerializeField] private Die _die = default!;
        private Transform _cameraTransform;
        private readonly List<List<Transform>> _markers = new List<List<Transform>>();
        private int _currentPlayer = 0;
        private int[] _currentTileIndex = new int[6];

        private List<int> _playerPenalties = new List<int>();
        private PlayerProperties[] _playerProperties = new PlayerProperties[6];

        private List<Color> _playerColors = new List<Color>()
        {
            new Color(0,255,0),
            new Color(0,0,255),
            new Color(255,215,0),
            new Color(128, 0, 128),
            new Color(255, 255, 255),
            new Color(255,255,0)
        };

        private enum PlayerNames
        {
            Green,
            Blue,
            Orange,
            Purple,
            White,
            Yellow
        }

        private LuckyDeck _luckyDeck = new LuckyDeck();

        private readonly List<Action> _tileFunctions = new List<Action>();
        private readonly List<Action> _cardFunctions = new List<Action>();

        private void Awake()
        {
            if (_markerParent == null)
            {
                throw new InvalidOperationException("Marker parent should not be null");
            }

            if (_gamePieces == null)
            {
                throw new InvalidOperationException("Game Pieces should not be null");
            }

            _cameraTransform = Camera.main.transform;

            var i = 0;
            foreach (Transform child in _markerParent.transform)
            {
                _markers.Add(new List<Transform>());
                foreach (Transform marker in child)
                {
                    _markers[i].Add(marker);
                }

                ++i;
            }

            for (int j = 0; j < 6; ++j)
            {
                _playerProperties[j] = new PlayerProperties();
            }

            AddTileFunctions();
            AddCardFunctions();

            SetActualPlayer();

        }

        private void AddTileFunctions()
        {
            _tileFunctions.Add(StartTile);
            _tileFunctions.Add(FirstTile);
            _tileFunctions.Add(SecondTile);
            _tileFunctions.Add(ThirdTile);
            _tileFunctions.Add(ForthTile);
            _tileFunctions.Add(FifthTile);
            _tileFunctions.Add(SixthTile);
            _tileFunctions.Add(SeventhTile);
            _tileFunctions.Add(EighthTile);
            _tileFunctions.Add(NinethTile);
            _tileFunctions.Add(TenthTile);
            _tileFunctions.Add(EleventhTile);
            _tileFunctions.Add(TwelfthTile);
            _tileFunctions.Add(ThirteenthTile);
            _tileFunctions.Add(FourteenthTile);
            _tileFunctions.Add(FifteenthTile);
        }
        private void AddCardFunctions()
        {
            _cardFunctions.Add(LC1);
            _cardFunctions.Add(LC2);
            _cardFunctions.Add(LC3);
            _cardFunctions.Add(LC4);
            _cardFunctions.Add(LC5);
            _cardFunctions.Add(LC6);
            _cardFunctions.Add(LC7);
        }

        private void SetActualPlayer()
        {
            _actualPlayerText.text = (PlayerNames)_currentPlayer + " Player's \n Turn";
            _actualPlayerText.color = _playerColors[_currentPlayer];
            _actualPlayerText.outlineColor = Color.black;

            _propertyVisualizer.ShowProperties(_playerProperties[_currentPlayer]);
        }

        public void NextPlayer()
        {
            if (_playerProperties[_currentPlayer].CheckWinCondition())
            {
                _winnerText.gameObject.SetActive(true);
                _winnerText.text = (PlayerNames)_currentPlayer + " Player won";
                _winnerText.color = _playerColors[_currentPlayer];
            }

            _currentPlayer = (_currentPlayer + 1) % 6;

            while (_playerPenalties.Contains(_currentPlayer))
            {
                _playerPenalties.Remove(_currentPlayer);
                _currentPlayer = (_currentPlayer + 1) % 6;
            }

            SetActualPlayer();
        }

        private async Task MoveGamePiece(GameObject currentGamePiece, int endPoint) => await currentGamePiece.transform.DOJump(_markers[_currentPlayer][endPoint].position, 0.05f, 1, 1.5f).Play();

        public async void StepAmount(int amount)
        {
            for (var i = 0; i < amount; ++i)
            {
                ++_currentTileIndex[_currentPlayer];
                _currentTileIndex[_currentPlayer] %= 16;
                await MoveGamePiece(_gamePieces[_currentPlayer],
                    _currentTileIndex[_currentPlayer]);
            }

            if (_currentTileIndex[_currentPlayer] - amount < 0
                && _currentTileIndex[_currentPlayer] != 0)
                _tileFunctions[0]();

            _tileFunctions[_currentTileIndex[_currentPlayer]]();
        }

        #region TileFunctions
        private void StartTile()
        {
            if (_playerProperties[_currentPlayer].Debt == 0)
            {
                Instantiate(_moneyTypes[0],
                    _cameraTransform.position + 0.2f * _cameraTransform.forward,
                    Quaternion.identity);
            }
            else
            {
                _playerProperties[_currentPlayer].Debt -= 20000;
            }

            Instantiate(_moneyTypes[0],
                _cameraTransform.position + 0.2f * _cameraTransform.forward,
                Quaternion.identity);

            if (_currentTileIndex[_currentPlayer] != 0) return;
            Instantiate(_moneyTypes[0],
                _cameraTransform.position + 0.2f * _cameraTransform.forward,
                Quaternion.identity);

            NextPlayer();
        }

        private void FirstTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.FirstTile();
        }

        private void SecondTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _cardFunctions[_luckyDeck.DrawCard()]();
        }

        private void ThirdTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.ThirdTile(_playerProperties[_currentPlayer]);
        }

        private void ForthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.ForthTile(_playerProperties[_currentPlayer]);
        }

        private void FifthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.FifthTile();

            _playerPenalties.Add(_currentPlayer);
        }

        private void SixthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.SixthTile();
        }
        private void SeventhTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.SeventhTile(_playerProperties[_currentPlayer]);
        }
        private void EighthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.EighthTile(_playerProperties[_currentPlayer]);
        }
        private void NinethTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _cardFunctions[_luckyDeck.DrawCard()]();
        }
        private void TenthTile()
        {
            _currentTileIndex[_currentPlayer] = 15;
            MoveGamePiece(_gamePieces[_currentPlayer], _currentTileIndex[_currentPlayer]);
            _tileFunctions[_currentTileIndex[_currentPlayer]]();
        }
        private void EleventhTile()
        {
            _die.SetCheckNumber();
        }
        private void TwelfthTile()
        {
            _currentTileIndex[_currentPlayer] += 2;
            MoveGamePiece(_gamePieces[_currentPlayer], _currentTileIndex[_currentPlayer]);
            _tileFunctions[_currentTileIndex[_currentPlayer]]();

        }
        private void ThirteenthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.SeventhTile(_playerProperties[_currentPlayer]);
        }
        private void FourteenthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _actionPanelActions.FourteenthTile(_playerProperties[_currentPlayer]);
        }
        private void FifteenthTile()
        {
            _actionPanelActions.gameObject.SetActive(true);
            _cardFunctions[_luckyDeck.DrawCard()]();
        }

        #endregion

        #region LuckyCardFunctions

        private void LC1()
        {
            _actionPanelActions.LC1();
            if (_playerProperties[_currentPlayer].FridgeOwned)
            {
                Instantiate(_moneyTypes[0],
                    _cameraTransform.position + 0.2f * _cameraTransform.forward,
                    Quaternion.identity);
                Instantiate(_moneyTypes[0],
                    _cameraTransform.position + 0.2f * _cameraTransform.forward,
                    Quaternion.identity);
            }
            else
            {
                _playerProperties[_currentPlayer].FridgeOwned = true;
                _propertyVisualizer.ShowProperties(_playerProperties[_currentPlayer]);
            }
        }

        private void LC2()
        {
            _actionPanelActions.LC2(_playerProperties[_currentPlayer]);
        }

        private void LC3()
        {
            _actionPanelActions.LC3();
            var oldTile = _currentTileIndex[_currentPlayer];
            _currentTileIndex[_currentPlayer] = 11;
            MoveGamePiece(_gamePieces[_currentPlayer], _currentTileIndex[_currentPlayer]);

            if (oldTile > _currentTileIndex[_currentPlayer])
            {
                _tileFunctions[0]();
            }

            _tileFunctions[_currentTileIndex[_currentPlayer]]();

        }

        private void LC4()
        {
            _actionPanelActions.LC4();
            var oldTile = _currentTileIndex[_currentPlayer];
            _currentTileIndex[_currentPlayer] = 4;
            MoveGamePiece(_gamePieces[_currentPlayer], _currentTileIndex[_currentPlayer]);

            if (oldTile > _currentTileIndex[_currentPlayer])
            {
                _tileFunctions[0]();
            }

            _tileFunctions[_currentTileIndex[_currentPlayer]]();
        }

        private void LC5()
        {
            _actionPanelActions.LC5();
            if (_playerProperties[_currentPlayer].PingPongTableOwned)
            {
                Instantiate(_moneyTypes[0],
                    _cameraTransform.position + 0.2f * _cameraTransform.forward,
                    Quaternion.identity);
            }
            else
            {
                _playerProperties[_currentPlayer].PingPongTableOwned = true;
                _propertyVisualizer.ShowProperties(_playerProperties[_currentPlayer]);
            }
        }

        private void LC6()
        {
            _actionPanelActions.LC6();
            for (int i = 0; i < 10; ++i)
            {
                Instantiate(_moneyTypes[0],
                    _cameraTransform.position + 0.2f * _cameraTransform.forward,
                    Quaternion.identity);
            }
        }

        private void LC7()
        {
            _actionPanelActions.LC7();
        }

        #endregion
    }
}
