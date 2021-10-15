using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.HandMenus
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject _storageSpace = default!;
        private readonly List<GameObject> _storedItems = new List<GameObject>();
        private void Awake()
        {
            if (_storageSpace == null) throw new InvalidOperationException("Storage Space should not be null");
        }

        private void OnCollisionEnter(Collision other)
        {
            var storage = Instantiate(_storageSpace);
            other.transform.SetParent(storage.transform, false);
            storage.transform.SetParent(GetComponentInChildren<GridObjectCollection>().transform, false);
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject.GetComponent<ObjectManipulator>());
            float scaleToFit = other.gameObject.GetComponent<Collider>().bounds.GetScaleToFitInside(storage.GetComponent<Collider>().bounds);
            other.transform.localScale *= scaleToFit;
            other.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.gameObject.transform.localPosition = Vector3.zero;
            storage.GetComponent<ButtonConfigHelper>().MainLabelText = other.gameObject.name;
            GetComponentInChildren<GridObjectCollection>().UpdateCollection();
            _storedItems.Add(_storageSpace);

        }
    }
}
