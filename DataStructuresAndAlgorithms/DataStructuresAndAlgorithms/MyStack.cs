using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MyStack<T> {
        LinkedList<T> _items = new LinkedList<T>();
        public void Push(T value) {
            _items.AddLast(value);
        }
        public T Pop() {
            if (_items.Count == 0) {
                throw new InvalidOperationException();
            }
            T result = _items.Last.Value;
            _items.RemoveLast();
            return result;
        }
        public T Peek() {
            if (_items.Count == 0) {
                throw new InvalidOperationException();
            }
            return _items.Last.Value;
        }
        public int Count { get { return _items.Count; } }
    }
}
