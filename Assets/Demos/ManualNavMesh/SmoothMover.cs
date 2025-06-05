using UnityEngine;

namespace Demos.ManualNavMesh
{
    public class SmoothMover : MonoBehaviour
    {
        [Range(0, 10)] public float maxSpeed;

        private Vector3 _desiredDirection;
        private float _desiredSpeed;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_desiredSpeed == 0)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                return;
            }

            var directionRatio = Mathf.Max(0, Vector3.Dot(transform.forward, _desiredDirection));
            var deltaSpeed = Mathf.Min(_desiredSpeed, maxSpeed);
            _rigidbody.linearVelocity = directionRatio * deltaSpeed * _desiredDirection;
        }

        public void SetDesiredVelocity(Vector3 velocity)
        {
            _desiredDirection = velocity.normalized;
            _desiredSpeed = velocity.magnitude;
        }
    }
}
