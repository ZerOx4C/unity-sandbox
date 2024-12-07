using System;
using System.Threading;
using UnityEngine;

namespace DynamicMesh
{
    [ExecuteAlways]
    [RequireComponent(typeof(MeshFilter))]
    public class FanMeshGenerator : MonoBehaviour
    {
        [SerializeField, Range(1, 90)] private float _divide = 10;
        [SerializeField, Range(0, 360)] private float _degree = 90;
        [SerializeField, Range(0, 10)] private float _radius = 2;

        private MeshFilter _meshFilter;
        private Mesh _mesh;

        private void Update()
        {
            Setup();

            var divide = CalcDivide(_degree, _divide);

            var vertices = new Vector3[2 + divide];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[3 * divide];

            var stepAngle = Mathf.Deg2Rad * _degree / divide;
            FillVerticesAndUV(vertices, uv, _radius, stepAngle);
            FillTriangles(triangles);

            uv[1] = new(0, 1);
            uv[^1] = new(1, 1);

            _mesh.Clear();
            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();

            _meshFilter.mesh = _mesh;
        }

        private void Setup()
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }

            if (_mesh == null)
            {
                _mesh = new();
            }
        }

        private int CalcDivide(float degree, float divide)
        {
            return (int)Mathf.Max(4, Mathf.Ceil(degree / divide));
        }

        private void FillVerticesAndUV(in Vector3[] vertices, in Vector2[] uv, float radius, float stepAngle)
        {
            vertices[0] = Vector3.zero;
            uv[0] = new(0.5f, 0);

            var direction = Vector3.zero;
            var count = vertices.Length - 1;

            for (var i = 0; i < count; i++)
            {
                var angle = stepAngle * i;
                direction.x = Mathf.Sin(angle);
                direction.z = Mathf.Cos(angle);

                vertices[i + 1] = direction * radius;
                uv[i + 1] = new(0.5f, 1);
            }
        }

        private void FillTriangles(in int[] triangles)
        {
            var count = triangles.Length / 3;

            for (var i = 0; i < count; i++)
            {
                var index = i * 3;

                triangles[index] = 0;
                triangles[index + 1] = i + 1;
                triangles[index + 2] = i + 2;
            }
        }
    }
}
