using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Demos.ManualNavMesh
{
    public class PlayerAgent : MonoBehaviour
    {
        private static readonly Vector3 ToXZ = new(1, 0, 1);

        private Camera _camera;
        private NavMeshAgent _navMeshAgent;
        private SmoothMover _smoothMover;
        private SmoothRotator _smoothRotator;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _camera = Camera.main;
            _smoothMover = GetComponent<SmoothMover>();
            _smoothRotator = GetComponent<SmoothRotator>();

            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            var y = Vector3.Scale(_camera.transform.forward, ToXZ).normalized;
            var x = Vector3.Scale(_camera.transform.right, ToXZ).normalized;
            var v = (Keyboard.current.wKey.isPressed ? y : Vector3.zero) +
                    (Keyboard.current.aKey.isPressed ? -x : Vector3.zero) +
                    (Keyboard.current.sKey.isPressed ? -y : Vector3.zero) +
                    (Keyboard.current.dKey.isPressed ? x : Vector3.zero);
            var ratio = Keyboard.current.shiftKey.isPressed ? 1 : 0.5f;

            _smoothMover.SetDesiredVelocity(_smoothMover.maxSpeed * ratio * v.normalized);
            _smoothRotator.SetDesiredForward(v);

            _navMeshAgent.nextPosition = transform.position;
            transform.position = _navMeshAgent.nextPosition;
        }
    }
}
