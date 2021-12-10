using System.Collections.Generic;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos.Szerencse
{
    public class LuckyDeck
    {
        private List<int> _luckyCards = new List<int>();
        private List<int> _shuffledCards = new List<int>();

        public LuckyDeck()
        {
            for (int i = 0; i < 7; ++i)
            {
                _luckyCards.Add(i);
            }

            _shuffledCards = _luckyCards;

            Shuffle(_shuffledCards);
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

        public int DrawCard()
        {
            if (_shuffledCards.Count == 0)
            {
                _shuffledCards = _luckyCards;
                Shuffle(_shuffledCards);
            }

            var result = _shuffledCards[0];
            _shuffledCards.RemoveAt(0);

            return result;
        }
    }
}
