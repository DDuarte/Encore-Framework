using System;
using System.Diagnostics.Contracts;
using System.Threading;
using Trinity.Core.Runtime;

namespace Trinity.Core.Threading.Actors
{
    public sealed class ActorTimer : IDisposableResource
    {
        private readonly Timer _timer;

        public IActor TargetActor { get; private set; }

        public Action Callback { get; private set; }

        public bool IsDisposed { get; private set; }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(_timer != null);
            Contract.Invariant(TargetActor != null);
            Contract.Invariant(Callback != null);
        }

        public ActorTimer(IActor target, Action callback, TimeSpan delay, int period = Timeout.Infinite)
        {
            Contract.Requires(target != null);
            Contract.Requires(callback != null);
            Contract.Requires(period >= Timeout.Infinite);

            TargetActor = target;
            Callback = callback;
            _timer = new Timer(TimerCallback, null, delay, TimeSpan.FromMilliseconds(period));
        }

        ~ActorTimer()
        {
            InternalDispose();
        }

        private void InternalDispose()
        {
            _timer.Dispose();
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            InternalDispose();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public void Change(TimeSpan delay, int period = Timeout.Infinite)
        {
            Contract.Requires(period >= Timeout.Infinite);

            _timer.Change(delay, TimeSpan.FromMilliseconds(period));
        }

        private void TimerCallback(object state)
        {
            TargetActor.PostAsync(Callback);
        }
    }
}
