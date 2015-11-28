using NUnit.Framework;

namespace PowerManager.Tests
{
    class PowerManagerRunner : PowerManager.IApplyPowerActions, PowerManager.ILockComputers
    {
        public bool PowerActionApplied{ get; private set; }
        public bool ComputerLocked { get; private set; }

        readonly PowerManager _powerMgr;

        public PowerManagerRunner (int idleTime, Policy policy)
        {
            _powerMgr = new PowerManager (idleTime, new PowerManager.Dependencies {
                ComputerLocker = this, 
                PowerAction = this,
                Policy = policy
            });
            _powerMgr.Run ();
        }
            
        public void Apply ()
        {
            PowerActionApplied = true;
        }

        public void LockComputer ()
        {
            ComputerLocked = true;
        }
    }

    [TestFixture]
    public class Given_it_is_during_business_hours
    {
        public class When_the_computer_is_not_idle
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner(0, new Policy());

            [Test]
            public void Then_no_power_action_is_applied ()
            {
                Assert.IsFalse (_runner.PowerActionApplied);
            }

            [Test]
            public void And_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_is_idle_for_less_than_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (1, new Policy{ LockComputerTimeOut= 1});

            [Test]
            public void Then_a_power_action_is_applied()
            {
                Assert.IsTrue (_runner.PowerActionApplied);
            }

            [Test]
            public void And_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_is_idle_for_longer_than_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (6, new Policy{ LockComputerTimeOut = 5 });

            [Test]
            public void Then_the_computer_is_locked()
            {
                Assert.IsTrue (_runner.ComputerLocked);
            }
        }
    }

}
