using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Demos.AnimatorConflict
{
    public class Scene : MonoBehaviour
    {
        public PlayableDirector timelineA;
        public PlayableDirector timelineB;
        public GameObject animationTarget;

        private void Start()
        {
            Bind<AnimationTrack>(timelineA, "Target", animationTarget);
            Bind<AnimationTrack>(timelineB, "Target", animationTarget);

            UniTask.Void(async token =>
            {
                timelineA.Play();
                await UniTask.WaitForSeconds(1.5f, cancellationToken: token);
                timelineB.Play();
            }, destroyCancellationToken);
        }

        private static void Bind<TTrack>(PlayableDirector director, string key, Object value)
        {
            var output = director.playableAsset.outputs
                .First(o => o.streamName == key && o.sourceObject is TTrack);

            director.SetGenericBinding(output.sourceObject, value);
        }
    }
}
