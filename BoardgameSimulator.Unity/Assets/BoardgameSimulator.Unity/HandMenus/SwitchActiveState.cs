using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;

namespace BoardgameSimulator.Unity.HandMenus
{
    [RequireComponent(typeof(Interactable))]
    public class SwitchActiveState : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject = default!;

        private Interactable _interactable = default!;

        private void Awake()
        {
            _interactable = gameObject.GetComponent<Interactable>();

            if (_interactable == null) throw new InvalidOperationException("Interaction should not be null");
            if (_gameObject == null) throw new InvalidOperationException("GameObject should not be null");

            _interactable.OnClick.AddListener(ShowGameSelector);
        }

        private void OnDestroy() => _interactable.OnClick.RemoveListener(ShowGameSelector);

        private void ShowGameSelector() => _gameObject.SetActive(!_gameObject.activeSelf);
    }
}
