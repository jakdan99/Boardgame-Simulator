using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;

public class Storable : MonoBehaviour
{
    [SerializeField] private Transform _sceneContentTransform = default!;

    private Vector3 _originalScale;

    private void Awake()
    {
        if (_sceneContentTransform == null) throw new InvalidOperationException("SceneContentTransform should not be null");
    }

    public void PlaceInInventory(GameObject parent)
    {
        _originalScale = transform.localScale;

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<ObjectManipulator>());

        transform.SetParent(parent.transform, false);

        var scaleToFit = GetComponent<Collider>().bounds.GetScaleToFitInside(parent.GetComponent<Collider>().bounds);
        transform.localScale *= scaleToFit * 0.25f;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        parent.GetComponent<ButtonConfigHelper>().MainLabelText = gameObject.name;
    }

    public void RemoveFromInventory(Transform parent)
    {
        transform.SetParent(_sceneContentTransform.transform, true);
        transform.localScale = _originalScale;
        transform.localPosition = parent.forward * 0.2f;
        transform.localRotation = parent.rotation;
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<ObjectManipulator>();
    }
}
