using NUnit.Framework;

namespace PowerManager.Tests
{
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

    [TestFixture]
    public class Given_it_is_during_business_hours
    {
        public class When_the_computer_is_not_idle
        {
            [Test]
            public void Then_no_power_action_is_applied ()
            {
                var runner = new PowerManagerRunner ();
                runner.Run (0);
                Assert.IsFalse (runner.PowerActionApplied);
            }
        }

        public class When_the_computer_is_idle_AND_the_idle_time
        {
            [Test]
            public void Then_a_power_action_is_applied()
            {
                var runner = new PowerManagerRunner ();
                runner.Run (1);
                Assert.IsTrue (runner.PowerActionApplied);
            }
        }

        public class When_the_computer_is_idle_for_longer_than_the_lock_timeout
        {
            [Test]
            public void Then_the_computer_is_locked()
            {
                var runner = new PowerManagerRunner ();
                runner.Run (1);
                Assert.IsTrue (runner.ComputerLocked);
            }
        }
    }

}
