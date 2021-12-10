using UnityEngine;

namespace BoardgameSimulator.Unity.HandMenus
{
    public class GameSelector : MonoBehaviour
    {
        [SerializeField] private Transform _games;

        public void SpawnGame(GameObject game)
        {
            Instantiate(game, _games);
            gameObject.SetActive(false);
        }
    }
}
