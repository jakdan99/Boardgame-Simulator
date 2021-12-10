using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections.Generic;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.DeckOfCards
{
    public class DeckOfCards : MonoBehaviour, IMixedRealityPointerHandler
    {
        [SerializeField] private Transform _games;
        [SerializeField] private List<GameObject> _cards;
        private List<GameObject> _cardsInDeck = new List<GameObject>();
        private List<GameObject> _cardsOnField = new List<GameObject>();
        private GameObject instCard;


        private void Awake()
        {
            _cardsInDeck = _cards;

            Shuffle(_cardsInDeck);
        }

        public void ShuffleDeck()
        {
            foreach (var card in _cardsOnField)
            {
                Destroy(card);
            }

            _cardsOnField = new List<GameObject>();

            _cardsInDeck = _cards;

            Shuffle(_cardsInDeck);
        }

        private void Shuffle<T>(List<T> list)
        {
            var random = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                var k = random.Next(n);
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            if (TryGetComponent(out SurfaceMagnetism _)) return;
            instCard = Instantiate(_cardsInDeck[0], eventData.Pointer.Position, Quaternion.identity, _games);
            instCard.name = instCard.name.Replace("(Clone)", "");
            _cardsOnField.Add(instCard);
            _cardsInDeck.RemoveAt(0);
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            if (instCard == null) return;
            instCard.transform.position = eventData.Pointer.Position;
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            instCard = null;
        }
    }
}
