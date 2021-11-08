using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos.Szerencse
{
    public class LuckyDeck : MonoBehaviour
    {
        [SerializeField] private List<Texture> _cardTextures = default!;
        [SerializeField] private GameObject _cardTemplate = default!;

        private void Awake()
        {
            if (_cardTextures == null || _cardTextures.Count == 0)
            {
                throw new InvalidOperationException("Card textures should not be null");
            }

            if (_cardTemplate == null)
            {
                throw new InvalidOperationException("Card template should not be null");
            }

            Shuffle(_cardTextures);

            DrawCard();
        }

        private void Shuffle<T>(List<T> list)
        {
            System.Random random = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                var k = random.Next(n);
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public void DrawCard()
        {
            if (_cardTextures.Count != 0)
            {
                var card = Instantiate(_cardTemplate, transform.position, Quaternion.identity);

                card.GetComponent<MeshRenderer>().material.mainTexture = _cardTextures[0];

                _cardTextures.RemoveAt(0);

                card.transform.DOMove(Camera.main.transform.localPosition + (0.2f * Camera.main.transform.forward), 2.0f).Play();
                card.transform.DORotate(new Vector3(-90, 90, 90) + Camera.main.transform.rotation.eulerAngles, 2.0f, RotateMode.Fast).Play();
            }
        }
    }
}
