using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.Inventory
{
    public class Storable : MonoBehaviour
    {
        private Transform _originalParent;

        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void PlaceInInventory(GameObject parent)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<ObjectManipulator>());

            _originalParent = transform.parent;
            transform.SetParent(parent.transform, false);

            var scaleToFit = GetComponent<Collider>().bounds.GetScaleToFitInside(parent.GetComponent<Collider>().bounds);
            transform.localScale *= scaleToFit;
            transform.localRotation = Quaternion.Euler(new Vector3(90, 180, 0));
            transform.localPosition = Vector3.zero;
            parent.GetComponentInChildren<TextMeshPro>().text = gameObject.name;
        }

        public void RemoveFromInventory(Transform parent)
        {
            transform.SetParent(_originalParent, true);
            transform.localScale = _originalScale;
            transform.localPosition -= parent.forward * 0.1f;
            transform.localRotation = parent.rotation;
            gameObject.AddComponent<Rigidbody>().useGravity = false;
            gameObject.AddComponent<ObjectManipulator>();
        }
    }
}
