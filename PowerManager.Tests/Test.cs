using NUnit.Framework;

namespace PowerManager.Tests
{
    

    [TestFixture]
    public class Given_it_is_during_business_hours
    {
        public class When_the_computer_is_not_idle : PowerManager.IApplyPowerActions
        {
            bool _powerActionApplied;
            readonly PowerManager _powerMgr;

            public When_the_computer_is_not_idle ()
            {
                _powerMgr = new PowerManager (this);
            }

            public void Apply ()
            {
                _powerActionApplied = true;
            }

            [Test]
            public void Then_no_power_action_is_applied ()
            {
                _powerMgr.Run (0);
                Assert.IsFalse (_powerActionApplied);
            }
        }

        public class When_the_computer_is_idle_a_power_action_is_applied
        {
            [Test]
            public void Then_a_power_action_is_applied()
            {
                bool powerActionApplied = false;
                Assert.IsTrue (powerActionApplied);
            }
        }
    }

    public class PowerManager
    {
        private readonly IApplyPowerActions _powerAction;

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
