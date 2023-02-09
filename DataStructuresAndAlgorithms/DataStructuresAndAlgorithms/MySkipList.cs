using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    internal class MySkipListNode<T> {
        public MySkipListNode(T value, int height) {
            Value = value;
            Next = new MySkipListNode<T>[height];
        }
        public MySkipListNode<T>[] Next { get; private set; }
        public T Value { get; private set; }
    }
    public class MySkipList<T> : ICollection<T> where T:IComparable<T> {
        private readonly Random _rand = new Random();
        private MySkipListNode<T> _head;
        private int _levels = 1;
        private int _count = 0;
        public MySkipList() { }
        public void Add(T item) {
            int level = PickRandomLevel();
            MySkipListNode<T> newNode = new MySkipListNode<T>(item, level + 1);
            MySkipListNode<T> current = _head;

            for (int i = _levels - 1; i >= 0; i--) {
                while (current.Next[i] != null) {
                    if (current.Next[i].Value.CompareTo(item) > 0) {
                        break;
                    }

                    current = current.Next[i];
                }

                if (i <= level) {
                    newNode.Next[i] = current.Next[i];
                    current.Next[i] = newNode;
                }
            }
            _count++;
        }
        private int PickRandomLevel() {
            int rand =_rand.Next();
            int level = 0;
            while ((rand & 1) == 1) {
                if (level == _levels) {
                    _levels++;
                    break;
                }
                rand >>= 1;
                level++;
            }
            return level;
        }
        public bool Contains(T item) {
            MySkipListNode<T> cur = _head;

            for (int i = _levels - 1; i >= 0; i--) {
                while (cur.Next[i] != null) {
                    int cmp = cur.Next[i].Value.CompareTo(item);
                    if (cmp > 0) {
                        break;
                    }
                    if (cmp == 0) {
                        return true;
                    }
                    cur = cur.Next[i];
                }
            }
            return false;
        }
        public bool Remove(T item) {
            MySkipListNode<T> cur = _head;
            bool removed = false;

            for (int level = _levels - 1; level >= 0; level--) {
                while (cur.Next[level] != null) {
                    if (cur.Next[level].Value.CompareTo(item) == 0) {
                        cur.Next[level] = cur.Next[level].Next[level];
                        removed = true;
                        break;
                    }
                    if (cur.Next[level].Value.CompareTo(item) > 0) {
                        break;
                    }

                    cur = cur.Next[level];
                }
            }
            if (removed) {
                _count--;
            }
            return removed;
        }
        public void Clear() {
            _head = new MySkipListNode<T>(default(T), 32 + 1);
            _count = 0;
        }
        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) {
                throw new ArgumentNullException("Array");
            }
            int offset = 0;
            foreach (T item in this) {
                array[arrayIndex + offset++] = item;
            }
        }
        public int Count {
            get {
                return _count;
            }
        }
        public bool IsReadOnly {
            get {
                return false;
            }
        }
        public IEnumerator<T> GetEnumerator() {
            MySkipListNode<T> cur = _head.Next[0];
            while (cur != null) {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
