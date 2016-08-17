using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace ObjectEventQueue
{
    [TestFixture]
    public class UnitTests
    {
        const int TestValueA = 21;
        const string TestValueB = "12";

        class TestClassA
        {
            int data;

            public int Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        class TestClassB
        {
            string data;

            public string Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void ListenEvent()
        {
            ObjectEvent<TestClassA> newEvent = new ObjectEvent<TestClassA>("test");
            ObjectEventManager.SubscribeEvent<TestClassA>(newEvent, new ObjectEventDelegate<TestClassA>(_classADelegate), null);
        }

        [Test]
        public void SendEvent()
        {
            ObjectEvent<TestClassA> newEvent = new ObjectEvent<TestClassA>("test");

            TestClassA test = new TestClassA();
            test.Data = TestValueA;

            ObjectEventManager.PublishEvent<TestClassA>(newEvent, test);
        }

        [Test]
        public void PumpEvents()
        {
            ObjectEventManager.PumpEvents();
        }

        [Test]
        public void TestAll()
        {
            ListenEvent();
            SendEvent();
            PumpEvents();
        }

        private void _classADelegate(string eventName, TestClassA eventValue)
        {
            Assert.AreEqual(TestValueA, eventValue.Data);
        }
    }
}
