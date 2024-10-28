using UnityEngine;
using UnityEngine.AI;

namespace Culling
{
    public class Player : MonoBehaviour
    {
        public float speed;

        private Camera _camera;
        private NavMeshAgent _agent;
        private Animator _animator;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            _agent.velocity = speed * GetInputVector();
            _animator.SetFloat("speed", _agent.velocity.magnitude);
        }

        private Vector3 GetInputVector()
        {
            var v = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                v += _camera.transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                v -= _camera.transform.right;
            }

            if (Input.GetKey(KeyCode.S))
            {
                v -= _camera.transform.forward;
            }

            if (Input.GetKey(KeyCode.D))
            {
                v += _camera.transform.right;
            }

            v.y = 0;

            return v.normalized;
        }
    }
}
