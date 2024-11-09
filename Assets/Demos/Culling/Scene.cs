using System.Collections.Generic;
using UnityEngine;

namespace Culling
{
    public class Scene : MonoBehaviour
    {
        private readonly FurnitureDeoccluder _furnitureDeoccluder = new();

        [SerializeField] private Camera _camera;
        [SerializeField] private RayTarget _rayTarget;
        [SerializeField] private List<Renderer> _furnitures;

        private void Start()
        {
            _furnitureDeoccluder.SetCamera(_camera);
            _furnitureDeoccluder.SetRayTarget(_rayTarget);

            foreach (var furniture in _furnitures)
            {
                _furnitureDeoccluder.AddRenderer(furniture.GetComponent<Renderer>());
            }
        }

        private void Update()
        {
            _furnitureDeoccluder.Update();
        }
    }
}
