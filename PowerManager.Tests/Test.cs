using NUnit.Framework;

namespace PowerManager.Tests
{
    [TestFixture]
    public class Given_it_is_during_business_hours
    {
        public class When_the_computer_is_not_idle
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner(0, new Policy{ LockComputerTimeOut = 1 });

            [Test]
            public void Then_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_is_idle_for_less_than_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (1, new Policy{ LockComputerTimeOut= 1});

            [Test]
            public void Then_the_computer_is_not_locked()
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

        public class When_the_lock_time_period_is_set_to_zero
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (6, new Policy{ LockComputerTimeOut = 0 });

            [Test]
            public void Then_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }
    }

}
