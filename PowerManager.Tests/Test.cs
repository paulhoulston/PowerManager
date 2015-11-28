using NUnit.Framework;

namespace PowerManager.Tests
{
    class PowerManagerRunner : PowerManager.IApplyPowerActions
    {
        public bool PowerActionApplied{ get; private set; }
        readonly PowerManager _powerMgr;

        public PowerManagerRunner ()
        {
            _powerMgr = new PowerManager (this);
        }

        public void Apply ()
        {
            PowerActionApplied = true;
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

        public class When_the_computer_is_idle_a_power_action_is_applied
        {
            [Test]
            public void Then_a_power_action_is_applied()
            {
                var runner = new PowerManagerRunner ();
                runner.Run (1);
                Assert.IsTrue (runner.PowerActionApplied);
            }
        }
    }

    public class PowerManager
    {
        readonly IApplyPowerActions _powerAction;

        public interface IApplyPowerActions
        {
            void Apply();
        }

        public PowerManager (IApplyPowerActions powerAction)
        {
            _powerAction = powerAction;
            
        }

        public void Run (int idleTime)
        {
            if (idleTime > 0) {
                _powerAction.Apply ();
            }
        }
    }
}
