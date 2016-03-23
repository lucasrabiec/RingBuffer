using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RingBuffer.Tests
{
    [TestClass]
    public class RingBufferTests
    {
        [TestMethod]
        public void CorrectAddAndRemoveWhenNotExceeded()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(3);

            buffer.Enqueue(7);
            buffer.Enqueue(8);

            Assert.AreEqual(7, buffer.Dequeue());
            Assert.AreEqual(8, buffer.Dequeue());
        }

        [TestMethod]
        public void CorrectAddAndRemoveWhenExceeded()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(3);

            buffer.Enqueue(7);
            buffer.Enqueue(8);
            buffer.Enqueue(9);
            buffer.Enqueue(10);

            Assert.AreEqual(8, buffer.Dequeue());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveFromEmptyQueue()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(1);

            buffer.Dequeue();
        }

        [TestMethod]
        public void WhenClearedSetIsEmptyFlagToTrue()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(3);

            buffer.Enqueue(7);
            buffer.Enqueue(8);
            buffer.Enqueue(9);
            buffer.Clear();

            Assert.AreEqual(true, buffer.IsEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionWhenGettingLastElementFromEmptyCollection()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(3);

            var value = buffer.LastElement;
        }

        [TestMethod]
        public void ReceiveLastElementWithoutRemovingItFromQueue()
        {
            RingBuffer<int> buffer = new RingBuffer<int>(3);

            buffer.Enqueue(7);
            buffer.Enqueue(8);

            var firstElement = buffer.LastElement;

            Assert.AreEqual(firstElement, buffer.LastElement);
        }
    }
}
