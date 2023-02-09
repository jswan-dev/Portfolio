using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MyArrayDeque<T> {
        // Array base deques perform better because there are less cache misses
        T[] _items = new T[0];
        int _size = 0;
        int _head = 0;
        int _tail = -1;

        private void allocateNewArray(int startingIndex) {
            int newLength = (_size == 0) ? 4 : _size * 2;
            T[] newArray = new T[newLength];

            if (_size > 0) {
                int targetIndex = startingIndex;
                if (_tail < _head) {
                    for (int index = _head; index < _items.Length; index++) {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                    for (int index = 0; index <= _tail; index++) {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }
                else {
                    for (int index = _head; index <= _tail; index++) {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }
                _head = startingIndex;
                _tail = targetIndex - 1;
            }
            else {
                _head = 0;
                _tail = -1;
            }
            _items = newArray;
        }
        public void EnqueueFirst(T value) {
            if (_items.Length == _size) {
                allocateNewArray(1);
            }
            if (_head > 0) {
                _head--;
            }
            else {
                _head = _items.Length - 1;
            }
            _items[_head] = value;
            _size++;
        }
        public void EnqueueLast(T value) {
            if (_items.Length == _size) {
                allocateNewArray(0);
            }
            if (_tail == _items.Length - 1) {
                _tail = 0;
            }
            else {
                _tail++;
            }
            _items[_tail] = value;
            _size++;
        }
        public T DequeueFirst() {
            if (_size == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            T temp = _items[_head];
            if (_head == _items.Length - 1) {
                _head = 0;
            }
            else {
                _head++;
            }
            _size--;
            return temp;
        }
        public T DequeueLast() {
            if (_size == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            T temp = _items[_tail];
            if (_tail == 0) {
                _tail = _items.Length - 1;
            }
            else {
                _tail--;
            }
            _size--;
            return temp;
        }
        public T PeekFirst() {
            if (_size == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            return _items[_head];
        }
        public T PeekLast() {
            if (_size == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            return _items[_tail];
        }
        public int Count {
            get { return _size; }
        }
    }
    class MyDeque<T> {
        // deques are double sided queues
        LinkedList<T> _items = new LinkedList<T>();
        public void EnqueueFirst(T value) {
            _items.AddFirst(value);
        }
        public void EnqueueLast(T value) {
            _items.AddLast(value);
        }
        public T DequeueFirst() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            T temp = _items.First.Value;
            _items.RemoveFirst();
            return temp;
        }
        public T DequeueLast() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            T temp = _items.Last.Value;
            _items.RemoveLast();
            return temp;
        }
        public T PeekFirst() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            return _items.First.Value;
        }
        public T PeekLast() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("deque is empty");
            }
            return _items.Last.Value;
        }
        public int Count {
            get { return _items.Count; }
        }
    }
}
