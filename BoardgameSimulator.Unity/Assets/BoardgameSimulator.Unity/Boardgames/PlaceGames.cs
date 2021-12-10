using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

namespace BoardgameSimulator.Unity.Boardgames
{
    public class PlaceGames : MonoBehaviour, IMixedRealityPointerHandler
    {
        [SerializeField] private SurfaceMagnetism _sm;
        [SerializeField] private SolverHandler _sh;

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
        }

        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            Destroy(_sm);
            Destroy(_sh);
        }
    }
}
