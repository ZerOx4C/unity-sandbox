using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demos.Culling
{
    public class RayTarget : MonoBehaviour
    {
        public IReadOnlyList<Vector3> Points => _pointTransforms.Select(t => t.position).ToList();

        private readonly List<Transform> _pointTransforms = new();

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _pointTransforms.AddRange(GetComponentsInChildren<Transform>().Where(t => t != transform));
        }

        private void Update()
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
