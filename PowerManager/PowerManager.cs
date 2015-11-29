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

        public interface ILockComputers
        {
            void LockComputer();
        }

        public interface IApplyPowerActions : IHibernateComputers
        {
        }

        public interface IHibernateComputers
        {
            void Hibernate();
        }

        public class Dependencies
        {
            public ILockComputers ComputerLocker { get; set; }
            public IApplyPowerActions PowerApplicator { get; set; }
            public Policy Policy { get; set; }
        }

        public PowerManager (int idleTime, Dependencies dependencies)
        {
            _idleTime = idleTime;
            _dependencies = dependencies;
        }

        public void Run ()
        {
            if (lockTimeoutExceeded ()) {
                _dependencies.ComputerLocker.LockComputer ();
            }
        }

        bool lockTimeoutExceeded ()
        {
            return _dependencies.Policy.LockComputerTimeOut > 0 && _idleTime > _dependencies.Policy.LockComputerTimeOut;
        }

    }
}
