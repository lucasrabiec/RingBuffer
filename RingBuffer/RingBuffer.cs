using System;

namespace RingBuffer
{
    public class RingBuffer<T>
    {
        private T[] _buffer;
        private int _capacity;
        private int _readPointer;
        private int _writePointer;
        private int _count;

        public T LastElement
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Collection is empty.");
                }

                return _buffer[_readPointer];
            }
        }

        public bool IsEmpty => _count == 0;

        public RingBuffer(int capacity)
        {
            _capacity = capacity;
            _buffer = new T[capacity];
            _readPointer = 0;
            _writePointer = 0;
        }

        public void Enqueue(T value)
        {
            _buffer[_writePointer] = value;
            _writePointer = ++_writePointer % _capacity;
            _count++;

            if (_readPointer == _writePointer)
            {
                _readPointer = ++_readPointer % _capacity;
                _count++;
            }
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Collection is empty.");

            T value = _buffer[_readPointer];
            _readPointer = ++_readPointer % _capacity;
            _count--;

            return value;
        }

        public void Clear()
        {
            for (int i = 0; i < _capacity; i++)
                _buffer[i] = default(T);
            _readPointer = 0;
            _writePointer = 0;
            _count = 0;
        }
    }
}
