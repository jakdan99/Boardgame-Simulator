using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos
{
    [RequireComponent(typeof(Rigidbody))]
    public class Die : MonoBehaviour
    {
        [SerializeField] private Gameplay _gameplay = default!;
        private Rigidbody _rigidbody;
        private bool _isDieThrown = false;
        private bool _checkNumber = true;

        private void Awake() => _rigidbody = gameObject.GetComponent<Rigidbody>();

        private void Update()
        {
            if (!_isDieThrown || !_checkNumber) return;
            if (_rigidbody.velocity != Vector3.zero) return;

            var rolledNumber = GetRolledNumber();

            _isDieThrown = false;
            _checkNumber = false;

            _gameplay.StepAmount(rolledNumber);
        }

        public void SetDieThrown() => _isDieThrown = true;

        public void SetCheckNumber() => _checkNumber = true;

        public void RollDie() => _rigidbody.AddTorque(1000 * new Vector3(0.5f, 0.3f, 0.8f), ForceMode.Impulse);

        private int GetRolledNumber()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) > 0.99f)
                return 3;
            if (Vector3.Dot(-transform.forward, Vector3.up) > 0.99f)
                return 4;
            if (Vector3.Dot(transform.up, Vector3.up) > 0.99f)
                return 2;
            if (Vector3.Dot(-transform.up, Vector3.up) > 0.99f)
                return 5;
            if (Vector3.Dot(transform.right, Vector3.up) > 0.99f)
                return 6;
            if (Vector3.Dot(-transform.right, Vector3.up) > 0.99f)
                return 1;
            return 0;
        }
    }
}
