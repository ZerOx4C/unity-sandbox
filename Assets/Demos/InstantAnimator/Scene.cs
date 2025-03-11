using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Demos.InstantAnimator
{
    public class Scene : MonoBehaviour
    {
        private static readonly int ParamPlay = Animator.StringToHash("Play");

        public Animator animator1;
        public Animator animator2;
        public Button instantiate1Button;
        public Button instantiate2Button;
        public Button play1Button;
        public Button play2Button;
        public Button resetButton;

        private Animator _instance1;
        private Animator _instance2;

        private void Start()
        {
            instantiate1Button.OnClickAsObservable()
                .Where(_ => !_instance1)
                .Subscribe(_ => _instance1 = Instantiate(animator1))
                .AddTo(this);

            instantiate2Button.OnClickAsObservable()
                .Where(_ => !_instance2)
                .Subscribe(_ => _instance2 = Instantiate(animator2))
                .AddTo(this);

            play1Button.OnClickAsObservable()
                .Where(_ => _instance1)
                .Subscribe(_ => _instance1.Play("Animation", 0, 0))
                .AddTo(this);

            play2Button.OnClickAsObservable()
                .Where(_ => _instance2)
                .Subscribe(_ => _instance2.Play("Animation", 0, 0))
                .AddTo(this);

            resetButton.OnClickAsObservable()
                .Where(_ => _instance1)
                .Subscribe(_ => Destroy(_instance1.gameObject))
                .AddTo(this);

            resetButton.OnClickAsObservable()
                .Where(_ => _instance2)
                .Subscribe(_ => Destroy(_instance2.gameObject))
                .AddTo(this);
        }
    }
}
