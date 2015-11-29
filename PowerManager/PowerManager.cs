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
            if (doesIdleExceed(_dependencies.Policy.LockComputerTimeOut)) {
                _dependencies.ComputerLocker.LockComputer ();
            }

            if (doesIdleExceed(_dependencies.Policy.HibernateTimeout)) {
                _dependencies.PowerApplicator.Hibernate ();
            }
        }

        bool doesIdleExceed(int valueToExceed)
        {
            return valueToExceed > 0 && _idleTime > valueToExceed;
        }

    }
}
