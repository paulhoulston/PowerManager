using NUnit.Framework;

namespace PowerManager.Tests
{
    #region PowerManagerRunner

    class PowerManagerRunner : PowerManager.IApplyPowerActions, PowerManager.ILockComputers
    {
        public bool PowerActionApplied{ get; private set; }
        public bool ComputerLocked { get; private set; }

        readonly PowerManager _powerMgr;

        public PowerManagerRunner ()
        {
            _powerMgr = new PowerManager (this, this);
        }

        public void Apply ()
        {
            PowerActionApplied = true;
        }

        public void LockComputer ()
        {
            ComputerLocked = true;
        }
            
        public void Run(int idleTime)
        {
            _powerMgr.Run (idleTime);
        }
    }
    #endregion

    [TestFixture]
    public class Given_it_is_during_business_hours
    {
        public class When_the_computer_is_not_idle
        {
            readonly PowerManagerRunner _runner;

            public When_the_computer_is_not_idle ()
            {
                _runner = new PowerManagerRunner();
                _runner.Run (0);
            }

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
            readonly PowerManagerRunner _runner = new PowerManagerRunner ();

            public When_the_computer_is_idle_for_less_than_the_lock_timeout ()
            {
                _runner.Run (1);
            }

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
            [Test]
            public void Then_the_computer_is_locked()
            {
                var runner = new PowerManagerRunner ();
                runner.Run (2);
                Assert.IsTrue (runner.ComputerLocked);
            }
        }
    }

}
