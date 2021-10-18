using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using TMPro;
using UnityEngine;

public class Storable : MonoBehaviour
{
    [SerializeField] private Transform _gamePieceContainer = default!;

    private Vector3 _originalScale;

    private void Awake()
    {
        if (_gamePieceContainer == null) throw new InvalidOperationException("GamePieceContainer should not be null");
    }

    public void PlaceInInventory(GameObject parent)
    {
        _originalScale = transform.localScale;

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<ObjectManipulator>());

        transform.SetParent(parent.transform, false);

        var scaleToFit = GetComponent<Collider>().bounds.GetScaleToFitInside(parent.GetComponent<Collider>().bounds);
        transform.localScale *= scaleToFit;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        parent.GetComponentInChildren<TextMeshPro>().text = gameObject.name;
    }

    public void RemoveFromInventory(Transform parent)
    {
        transform.SetParent(_gamePieceContainer.transform, true);
        transform.localScale = _originalScale;
        transform.localPosition -= parent.forward * 0.1f;
        transform.localRotation = parent.rotation;
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<ObjectManipulator>();
    }
}
