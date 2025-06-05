using System.Linq;
using UnityEngine;

namespace Demos.ManualNavMesh
{
    public class Scene : MonoBehaviour
    {
        public NonPlayerAgent nonPlayerAgent;
        public Transform waypointRoot;

        private Transform[] _waypointArray;

        private void Awake()
        {
            _waypointArray = waypointRoot.GetComponentsInChildren<Transform>()
                .Where(t => t != waypointRoot)
                .ToArray();

            RenewDestination();
        }

        private void Update()
        {
            if (nonPlayerAgent.IsArrived)
            {
                RenewDestination();
            }
        }

        private void RenewDestination()
        {
            nonPlayerAgent.destinationTransform = _waypointArray[Random.Range(0, _waypointArray.Length)];
        }
    }
}
