namespace PowerManager
{
    public class Policy
    {
        public int LockComputerTimeOut{ get; set; }
    }

    public class PowerManager
    {
        readonly IApplyPowerActions _powerAction;
        readonly ILockComputers _computerLocker;
        readonly Policy _policy;

        public interface IApplyPowerActions
        {
            void Apply();
        }

        public interface ILockComputers
        {
            void LockComputer();
        }

        public PowerManager (IApplyPowerActions powerAction, ILockComputers computerLocker, Policy policy)
        {
            _policy = policy;
            _computerLocker = computerLocker;
            _powerAction = powerAction;
        }

        public void Run (int idleTime)
        {
            if (idleTime > _policy.LockComputerTimeOut) {
                _computerLocker.LockComputer ();
            }

            if (idleTime > 0) {
                _powerAction.Apply ();
            }
        }
    }
}
