namespace PowerManager
{

    public class PowerManager
    {
        readonly IApplyPowerActions _powerAction;
        readonly ILockComputers _computerLocker;

        public interface IApplyPowerActions
        {
            void Apply();
        }

        public interface ILockComputers
        {
            void LockComputer();
        }

        public PowerManager (IApplyPowerActions powerAction, ILockComputers computerLocker)
        {
            this._computerLocker = computerLocker;
            _powerAction = powerAction;
        }

        public void Run (int idleTime)
        {
            if (idleTime > 0) {
                _powerAction.Apply ();
            }

            _computerLocker.LockComputer ();
        }
    }
}
