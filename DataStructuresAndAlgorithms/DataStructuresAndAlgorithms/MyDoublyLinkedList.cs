using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MyDoublyLinkedNode<T> {
        public MyDoublyLinkedNode(T value) {
            Value = value;
        }
        public T Value { get; internal set; }
        public MyDoublyLinkedNode<T> Next { get; internal set; }
        public MyDoublyLinkedNode<T> Previous { get; internal set; }
}

    class MyDoublyLinkedList<T> : ICollection<T> {
        MyDoublyLinkedNode<T> _head;
        MyDoublyLinkedNode<T> _tail;

        public MyDoublyLinkedNode<T> Head { get { return _head; } }
        public MyDoublyLinkedNode<T> Tail { get { return _tail; } }
        public void AddFirst(T value) {
            MyDoublyLinkedNode<T> node = new MyDoublyLinkedNode<T>(value);
            MyDoublyLinkedNode<T> temp = _head;

            _head = node;
            _head.Next = temp;

            if (Count == 0) {
                _tail = _head;
            }
            else {
                temp.Previous = _head;
            }
            Count++;
        }
        public void AddLast(T value) {
            MyDoublyLinkedNode<T> node = new MyDoublyLinkedNode<T>(value);

            if (Count == 0) {
                _head = node;
            }
            else {
                _tail.Next = node;
                node.Previous = _tail;
            }
            _tail = node;
            Count++;
        }
        public void RemoveFirst() {
            if (Count != 0) {
                _head = _head.Next;
                Count--;

                if (Count == 0) {
                    _tail = null;
                }
                else {
                    _head.Previous = null;
                }
            }
        }
        public void RemoveLast() {
            if (Count != 0) {
                if (Count == 1) {
                    _head = null;
                    _tail = null;
                }
                else {
                    _tail.Previous.Next = null;
                    _tail = _tail.Previous;
                }
                Count--;
            }
        }
        public int Count {
            get; private set;
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public void Add(T item) {
            AddLast(item);
        }

        public void Clear() {
            _tail = null;
            _head = null;
            Count = 0;
        }

        public bool Contains(T item) {
            MyDoublyLinkedNode<T> current = _head;
            while (current != null) {
                if (current.Value.Equals(item)) {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator() {
            MyDoublyLinkedNode<T> current = _head;
            while (current != null) {
                yield return current.Value;
                current = current.Next;
            } 
        }

        public bool Remove(T item) {
            MyDoublyLinkedNode<T> previous = null;
            MyDoublyLinkedNode<T> current = _head;

            while (current != null) {
                if (current.Value.Equals(item)) {
                    if (previous != null) {
                        previous.Next = current.Next;
                        if (current.Next == null) {
                            _tail = previous;
                        }
                        else {
                            current.Next.Previous = previous;
                        }
                        Count--;
                    }
                    else {
                        RemoveFirst();
                    }
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
