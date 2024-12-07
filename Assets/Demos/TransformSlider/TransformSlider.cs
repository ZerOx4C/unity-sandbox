using UnityEngine;

namespace TransformSlider
{
    [ExecuteAlways]
    public class TransformSlider : MonoBehaviour
    {
        public float TargetDistance
        {
            get => _targetDistance;
            set
            {
                Debug.Log("distance");
                _targetDistance = value;
            }
        }

        public float TargetHeight
        {
            get => _targetHeight;
            set
            {
                Debug.Log("height");
                _targetHeight = value;
            }
        }

        [SerializeField] private Transform _target;
        [SerializeField, Range(0, 5)] private float _targetDistance;
        [SerializeField, Range(0, 5)] private float _targetHeight;
        [SerializeField, Range(0, 1)] private float _distancePhase;
        [SerializeField, Range(0, 1)] private float _heightPhase;

        public void Update()
        {
            if (_target == null) return;

            _target.position = Vector3.forward * _targetDistance * _distancePhase + Vector3.up * _targetHeight * _heightPhase;
        }
    }

}
