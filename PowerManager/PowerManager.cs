namespace PowerManager
{
    public class Policy
    {
        public int LockComputerTimeOut{ get; set; }
        public int HibernateTimeout{ get; set; }
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
            // Locking the computer is not a power action so perform lock whether or not we apply a power action
            if (lockTimeoutExceeded ()) {
                _dependencies.ComputerLocker.LockComputer ();
            }

            if (hibernatePeriodExceeded ()) {
                _dependencies.PowerApplicator.Hibernate ();
            }
        }

        bool lockTimeoutExceeded ()
        {
            return _dependencies.Policy.LockComputerTimeOut > 0 && _idleTime > _dependencies.Policy.LockComputerTimeOut;
        }

        bool hibernatePeriodExceeded ()
        {
            return _dependencies.Policy.HibernateTimeout > 0 && _idleTime > _dependencies.Policy.HibernateTimeout;
        }

    }
}
