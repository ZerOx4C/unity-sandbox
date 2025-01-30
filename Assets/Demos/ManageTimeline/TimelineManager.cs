using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Demos.ManageTimeline
{
    public class TimelineManager
    {
        private readonly List<TimelineWrapper> _knownTimelineList = new();

        public void SetPause(bool isPause)
        {
            if (isPause)
            {
                foreach (var timeline in _knownTimelineList)
                {
                    timeline.Director.Pause();
                }
            }
            else
            {
                foreach (var timeline in _knownTimelineList)
                {
                    timeline.Director.Resume();
                }
            }
        }

        public async UniTask<TimelineWrapper> InstantiateAsync(TimelineWrapper prefab, CancellationToken cancellation)
        {
            var instances = await Object.InstantiateAsync(prefab, 1, null, Vector3.zero, Quaternion.identity, cancellation);
            var timeline = instances[0];
            timeline.OnDestroyAsObservable().Subscribe(_ => _knownTimelineList.Remove(timeline)).AddTo(timeline);
            _knownTimelineList.Add(timeline);
            return timeline;
        }
    }
}
