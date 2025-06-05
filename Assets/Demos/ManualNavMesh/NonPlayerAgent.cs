using UnityEngine;
using UnityEngine.AI;

namespace Demos.ManualNavMesh
{
    public class NonPlayerAgent : MonoBehaviour
    {
        public Transform destinationTransform;

        private NavMeshAgent _navMeshAgent;
        private SmoothMover _smoothMover;
        private SmoothRotator _smoothRotator;

        public bool IsArrived { get; private set; }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _smoothMover = GetComponent<SmoothMover>();
            _smoothRotator = GetComponent<SmoothRotator>();

            _navMeshAgent.speed = _smoothMover.maxSpeed;
            _navMeshAgent.acceleration = 1000;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            if (!destinationTransform)
            {
                return;
            }

            if ((destinationTransform.position - transform.position).sqrMagnitude < 0.1f)
            {
                IsArrived = true;
                return;
            }

            IsArrived = false;

            if (0.1f < (destinationTransform.position - _navMeshAgent.destination).sqrMagnitude)
            {
                _navMeshAgent.destination = destinationTransform.position;
            }

            if (!Mathf.Approximately(_navMeshAgent.speed, _smoothMover.maxSpeed))
            {
                _navMeshAgent.speed = _smoothMover.maxSpeed;
            }

            var diffPosition = _navMeshAgent.nextPosition - transform.position;
            _smoothMover.SetDesiredVelocity(_smoothMover.maxSpeed * diffPosition.normalized);
            _smoothRotator.SetDesiredForward(diffPosition);

            _navMeshAgent.nextPosition = transform.position;
            transform.position = _navMeshAgent.nextPosition;
        }
    }
}
