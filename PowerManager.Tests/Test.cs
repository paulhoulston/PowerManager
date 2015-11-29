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

            [Test]
            public void And_the_computer_is_not_hibernated()
            {
                Assert.IsFalse (_runner.ComputerHibernated);
            }
        }

        public class When_the_computer_idle_time_is_equal_to_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (1, new Policy {
                LockComputerTimeOut= 1,
                HibernateTimeout=0
            });

            [Test]
            public void Then_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_is_idle_time_is_less_than_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (2, new Policy {
                LockComputerTimeOut= 3,
                HibernateTimeout=0
            });

            [Test]
            public void Then_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_idle_time_is_less_than_the_hibernate_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (1, new Policy {
                LockComputerTimeOut= 0,
                HibernateTimeout=2
            });

            [Test]
            public void Then_the_computer_is_not_hibernated()
            {
                Assert.IsFalse(_runner.ComputerHibernated);
            }
        }

        public class When_the_computer_idle_time_is_equal_to_the_hibernate_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (1, new Policy {
                LockComputerTimeOut= 0,
                HibernateTimeout=1
            });

            [Test]
            public void Then_the_computer_is_not_hibernated()
            {
                Assert.IsFalse(_runner.ComputerHibernated);
            }
        }

        public class When_the_computer_idle_time_is_greater_than_the_lock_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (3, new Policy {
                LockComputerTimeOut = 2,
                HibernateTimeout = 0
            });

            [Test]
            public void Then_the_computer_is_locked()
            {
                Assert.IsTrue (_runner.ComputerLocked);
            }
        }

        public class When_the_computer_idle_time_is_greater_than_the_hibernate_timeout
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (3, new Policy {
                LockComputerTimeOut = 0,
                HibernateTimeout = 2
            });

            [Test]
            public void Then_the_computer_is_hibernated()
            {
                Assert.IsTrue (_runner.ComputerHibernated);
            }
        }

        public class When_the_policy_timeouts_are_set_to_zero_and_the_computer_idle_time_is_greater_than_zero
        {
            readonly PowerManagerRunner _runner = new PowerManagerRunner (6, new Policy{ 
                LockComputerTimeOut = 0,
                HibernateTimeout=0
            });

            [Test]
            public void Then_the_computer_is_not_locked()
            {
                Assert.IsFalse (_runner.ComputerLocked);
            }

            [Test]
            public void And_the_computer_is_not_hibernated()
            {
                Assert.IsFalse(_runner.ComputerHibernated);
            }
        }
    }
}
