using NUnit.Framework;

namespace PowerManager.Tests
{
    class PowerManagerRunner : PowerManager.ILockComputers, PowerManager.IApplyPowerActions
    {
        public bool ComputerHibernated { get; private set; }
        public bool ComputerLocked { get; private set; }

        readonly PowerManager _powerMgr;

        public PowerManagerRunner (int idleTime, Policy policy)
        {
            _powerMgr = new PowerManager (idleTime, new PowerManager.Dependencies {
                ComputerLocker = this,
                PowerApplicator = this,
                Policy = policy
            });
            _powerMgr.Run ();
        }

        public void LockComputer ()
        {
            ComputerLocked = true;
        }

        public void Hibernate ()
        {
            ComputerHibernated = true;
        }
    }

}
