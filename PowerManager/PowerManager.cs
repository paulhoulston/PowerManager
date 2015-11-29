namespace PowerManager
{
    public class Policy
    {
        public int LockComputerTimeOut{ get; set; }
    }

    public class PowerManager
    {
        readonly Dependencies _dependencies;
        readonly int _idleTime;

        public interface IApplyPowerActions
        {
            void Apply();
        }

        public interface ILockComputers
        {
            void LockComputer();
        }

        public class Dependencies
        {
            public IApplyPowerActions PowerAction{get;set;}
            public ILockComputers ComputerLocker{get;set;}
            public Policy Policy{get;set;}
        }

        public PowerManager (int idleTime, Dependencies dependencies)
        {
            _idleTime = idleTime;
            _dependencies = dependencies;
        }

        public void Run ()
        {
            if (_dependencies.Policy.LockComputerTimeOut > 0 && _idleTime > _dependencies.Policy.LockComputerTimeOut) {
                _dependencies.ComputerLocker.LockComputer ();
            }

            if (_idleTime > 0) {
                _dependencies.PowerAction.Apply ();
            }
        }
    }
}
