using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoardgameSimulator.Unity.HandMenus
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject _storageSpace = default!;
        private readonly Dictionary<GameObject, GameObject> _storedItems = new Dictionary<GameObject, GameObject>();

        private void Awake()
        {
            if (_storageSpace == null) throw new InvalidOperationException("Storage Space should not be null");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Storable storable))
            {
                var storage = Instantiate(_storageSpace, GetComponentInChildren<GridObjectCollection>().transform, false);
                storage.GetComponent<Interactable>().OnClick.AddListener(delegate { RemoveItem(storage); });
                storable.PlaceInInventory(storage);

                _storedItems.Add(storage, other.gameObject);

                GetComponentInChildren<GridObjectCollection>().UpdateCollection();
            }
        }

        private void RemoveItem(GameObject item)
        {
            _storedItems[item].GetComponent<Storable>().RemoveFromInventory(item.transform);
            Destroy(item);
            GetComponentInChildren<GridObjectCollection>().UpdateCollection();
        }
    }
}
