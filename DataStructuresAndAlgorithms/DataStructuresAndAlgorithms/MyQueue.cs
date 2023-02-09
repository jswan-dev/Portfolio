using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MyQueue<T> {
        LinkedList<T> _items = new LinkedList<T>();
        public void Enqueue(T value) {
            _items.AddFirst(value);
        }
        public T Dequeue() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("Queue is empty");
            }
            T last = _items.Last.Value;
            _items.RemoveLast();
            return last;
        }
        public T Peek() {
            if (_items.Count == 0) {
                throw new InvalidOperationException("Queue is empty");
            }
            return _items.Last.Value;
        }
        public int Count {
            get { return _items.Count; }
        }
    }
}
