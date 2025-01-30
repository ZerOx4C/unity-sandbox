using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Demos.ManageTimeline
{
    public class TimelineWrapper : MonoBehaviour
    {
        private bool _stopped;
        public PlayableDirector Director { get; private set; }

        private void Awake()
        {
            Director = GetComponent<PlayableDirector>();
            Director.Stop();
            Director.stopped += _ => _stopped = true;
        }

        public async UniTask PlayAsync(CancellationToken cancellation)
        {
            Director.Play();
            await UniTask.WaitUntil(() => _stopped, cancellationToken: cancellation);
        }
    }
}
