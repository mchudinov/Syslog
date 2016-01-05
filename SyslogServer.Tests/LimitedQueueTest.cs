using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SyslogServer.Tests
{
    [TestClass]
    public class LimitedQueueTest
    {
        [TestMethod]
        public void TestEnqueue()
        {
            var queue = new LimitedQueue<int>(2);
            Assert.AreEqual(2, queue.Limit);
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);
            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Count);
            queue.Enqueue(3);
            Assert.AreEqual(2, queue.Count);
        }
    }
}
