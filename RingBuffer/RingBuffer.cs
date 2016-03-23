using System;

namespace RingBuffer
{
    public class RingBuffer<T>
    {
        private T[] _buffer;
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
            _buffer = new T[capacity];
            _readPointer = 0;
            _writePointer = 0;
        }

        public void Enqueue(T value)
        {
            _buffer[_writePointer] = value;
            _writePointer = (_writePointer + 1) % _buffer.Length;

            if (_count < _buffer.Length)
            {
                _count++;
            }
            else
            {
                _readPointer = (_readPointer + 1) % _buffer.Length;
            }
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Collection is empty.");

            T value = _buffer[_readPointer];
            _buffer[_readPointer] = default(T);
            _readPointer = (_readPointer + 1) % _buffer.Length;
            _count--;

            return value;
        }

        public void Clear()
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer[i] = default(T);
            }
            _readPointer = 0;
            _writePointer = 0;
            _count = 0;
        }
    }
}
