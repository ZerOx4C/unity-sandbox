using System.Collections.Generic;
using UnityEngine;

namespace Demos.Culling
{
    public class FurnitureDeoccluder
    {
        private readonly Dictionary<GameObject, Renderer> _objectRendererMap = new();

        private Camera _camera;
        private RayTarget _rayTarget;
        private RaycastHit[] _raycastHits;

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public void SetRayTarget(RayTarget rayTarget)
        {
            _rayTarget = rayTarget;
        }

        public void AddRenderer(Renderer renderer)
        {
            _objectRendererMap.Add(renderer.gameObject, renderer);
            _raycastHits = new RaycastHit[_objectRendererMap.Count];
        }

        public void Update()
        {
            var furnitureLayer = LayerMask.GetMask("Furniture");
            var objectLevelMap = new Dictionary<GameObject, int>();
            var cameraPosition = _camera.transform.position;
            var pointCount = _rayTarget.Points.Count + 1;

            foreach (var point in _rayTarget.Points)
            {
                var direction = point - cameraPosition;

                var count = Physics.RaycastNonAlloc(cameraPosition, direction, _raycastHits, direction.magnitude, furnitureLayer);
                for (int i = 0; i < count; i++)
                {
                    var hit = _raycastHits[i];
                    var obj = hit.collider.gameObject;
                    var level = objectLevelMap.GetValueOrDefault(obj, 0);
                    objectLevelMap[obj] = level + 1;
                }
            }

            foreach (var (obj, renderer) in _objectRendererMap)
            {
                var level = objectLevelMap.GetValueOrDefault(obj, 0);
                var propertyBlock = new MaterialPropertyBlock();
                propertyBlock.SetFloat("_DitherAlpha", (float)(pointCount - level) / pointCount);
                renderer.SetPropertyBlock(propertyBlock);
            }
        }
    }
}
