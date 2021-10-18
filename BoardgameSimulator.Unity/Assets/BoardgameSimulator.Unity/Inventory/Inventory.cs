using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.Inventory
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
            if (!other.gameObject.TryGetComponent(out Storable storable)) return;
            var storage = Instantiate(_storageSpace);
            storage.transform.SetParent(GetComponentInChildren<GridObjectCollection>().transform, false);
            storage.GetComponent<Interactable>().OnClick.AddListener(delegate { RemoveItem(storage); });
            storable.PlaceInInventory(storage);

            _storedItems.Add(storage, other.gameObject);

            GetComponentInChildren<GridObjectCollection>().UpdateCollection();

            storage.gameObject.GetComponentInChildren<TextMeshPro>().transform.parent.transform.position = Vector3.zero;
        }

        private void RemoveItem(GameObject item)
        {
            _storedItems[item].GetComponent<Storable>().RemoveFromInventory(item.transform);
            Destroy(item);
            GetComponentInChildren<GridObjectCollection>().UpdateCollection();
        }
    }
}
