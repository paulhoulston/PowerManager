using NUnit.Framework;
using System;

namespace PowerManager.Tests
{
    [TestFixture ()]
    public class Given_I_want_to_manage_the_power_on_my_computer
    {
        public class When_the_computer_idle_time_is_zero
        {
            [Test ()]
            public void Then_no_power_action_is_applied ()
            {
                var powerMgr = new PowerManager ();
                Assert.IsFalse (powerMgr.Run(0));
            }
        }
    }

    public class PowerManager
    {
        public bool Run (int idleTime)
        {
            return false;
        }
    }
}
