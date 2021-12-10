using BoardgameSimulator.Unity.Inventory;
using UnityEngine;

public class ClearGamePieces : MonoBehaviour
{
    [SerializeField] private Transform _gamePieceContainer = default!;
    [SerializeField] private Inventory _inventory = default!;

    public void Clear()
    {
        foreach (Transform child in _gamePieceContainer.transform)
        {
            Destroy(child.gameObject);
        }

        _inventory.RemoveAll();
    }
}
