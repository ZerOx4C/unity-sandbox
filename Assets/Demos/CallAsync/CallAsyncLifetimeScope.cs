using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CallAsync
{
    public class CallAsyncLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Module>();
        }
    }

    public class Module : IAsyncStartable
    {
        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            var task = Heavy1();

            Debug.Log("StartAsync: mid");

            await task;
        }

        public async UniTask Heavy1()
        {
            Debug.Log("Heavy1: begin");

            await HeavyChild(300);
            await HeavyChild(200);
            await HeavyChild(100);

            Debug.Log("Heavy1: end");
        }

        public async UniTask Heavy2()
        {
            Debug.Log("Heavy2: begin");

            var task1 = HeavyChild(300);
            var task2 = HeavyChild(200);
            var task3 = HeavyChild(100);

            await task1;
            await task2;
            await task3;

            Debug.Log("Heavy2: end");
        }

        public async UniTask HeavyChild(int ms)
        {
            await UniTask.Delay(ms);
            Debug.Log(ms);
        }
    }
}
