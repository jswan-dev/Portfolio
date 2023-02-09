using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    public class MyNode<T> {
        public MyNode(T value) {
            Value = value;
        }
        public T Value { get; internal set; }
        public MyNode<T> Next { get; internal set; }
    }
    class MyLinkedList<T> : ICollection<T> {
        private MyNode<T> _head;
        // you don't need to keep track of tail, but it makes adding a node simpler
        private MyNode<T> _tail;
        public int Count {
            get; private set;
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public void Add(T value) {
            MyNode<T> node = new MyNode<T>(value);

            if (_head == null) {
                _head = node;
                _tail = node;
            }
            else {
                // point the current _tail to the new node
                _tail.Next = node;
                // then point tail to the new node
                _tail = node;
            }
            Count++;
        }

        public void Clear() {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T item) {
            MyNode<T> current = _head;

            while (current != null) {
                if (current.Value.Equals(item)) {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            MyNode<T> current = _head;
            while (current != null) {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            MyNode<T> current = _head;
            while (current != null) {
                yield return current.Value;
                current = current.Next;
            }
        }

        public bool Remove(T item) {
            MyNode<T> previous = null;
            MyNode<T> current = _head;

            /* SENERIOS
             * - Empty list: do nothing
             * - Single node: previous is null
             * - Many nodes: 
             *      a: node to remove is first node
             *      b: node to remove is middle or last
             */

            while (current != null) {
                if (current.Value.Equals(item)) {
                    // node is in the middle or end
                    if (previous != null) {
                        // case 3b
                        previous.Next = current.Next;
                        if (current.Next == null) {
                            // if it was the last node update _tail
                            _tail = previous;
                        }
                    }
                    else {
                        _head = _head.Next;
                        if (_head == null) {
                            _tail = null;
                        }
                    }
                    Count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
