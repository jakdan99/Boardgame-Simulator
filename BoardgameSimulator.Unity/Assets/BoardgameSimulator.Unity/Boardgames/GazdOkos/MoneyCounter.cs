using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames.GazdOkos
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _moneyTypes;
        [SerializeField] private TextMeshPro _counterText;

        public int MoneyAmount { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Money money)) return;
            MoneyAmount += money.value;
            Destroy(other.gameObject);

            _counterText.text = MoneyAmount + " Ft";
        }

        private void ReturnMoney()
        {
            var cameraTransform = Camera.main.transform;
            while (MoneyAmount > 0)
            {
                if (MoneyAmount >= 20000)
                {
                    Instantiate(_moneyTypes[0],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 20000;
                }
                else if (MoneyAmount >= 10000)
                {
                    Instantiate(_moneyTypes[1],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 10000;
                }
                else if (MoneyAmount >= 5000)
                {
                    Instantiate(_moneyTypes[2],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 5000;

                }
                else if (MoneyAmount >= 2000)
                {
                    Instantiate(_moneyTypes[3],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 1000;
                }
                else if (MoneyAmount >= 1000)
                {
                    Instantiate(_moneyTypes[4],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 1000;
                }
                else if (MoneyAmount >= 500)
                {
                    Instantiate(_moneyTypes[5],
                        cameraTransform.position + 0.2f * cameraTransform.forward,
                        Quaternion.identity);
                    MoneyAmount -= 500;
                }
            }
        }

        public bool Evaluate(int neededMoney, bool canceled = false)
        {
            if (neededMoney <= MoneyAmount && !canceled)
            {
                MoneyAmount -= neededMoney;
                _counterText.text = MoneyAmount + " Ft";
                ReturnMoney();
                return true;
            }

            if (canceled)
            {
                ReturnMoney();
                return false;
            }

            return false;
        }
    }
}
