using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Demos.ManageTimeline
{
    public class Scene : MonoBehaviour
    {
        public TimelineWrapper timelinePrefab;

        private void Start()
        {
            RunAsync(CancellationToken.None).Forget();
        }

        private async UniTask RunAsync(CancellationToken cancellation)
        {
            var timelineManager = new TimelineManager();

            var timeline1 = await timelineManager.InstantiateAsync(timelinePrefab, cancellation);
            var timeline2 = await timelineManager.InstantiateAsync(timelinePrefab, cancellation);

            var playTask1 = timeline1.PlayAsync(cancellation);
            await UniTask.WaitForSeconds(1, cancellationToken: cancellation);
            var playTask2 = timeline2.PlayAsync(cancellation);
            await UniTask.WaitForSeconds(1, cancellationToken: cancellation);
            timelineManager.SetPause(true);
            await UniTask.WaitForSeconds(1, cancellationToken: cancellation);
            timelineManager.SetPause(false);
            await playTask1;
            await playTask2;
            Debug.Log("done");
        }
    }
}
