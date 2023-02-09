using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MySet<T> : IEnumerable<T> where T:IComparable<T> {
        // most Sets use trees underneath for performance
        private readonly List<T> _items = new List<T>();
        public MySet() {

        }
        public MySet(IEnumerable<T> items) {
            AddRange(items);
        }
        public void Add(T item) {
            if (Contains(item)) {
                throw new InvalidOperationException("Item already exists in set");
            }
            _items.Add(item);
        }
        public void AddRange(IEnumerable<T> items) {
            foreach (T item in items) {
                Add(item);
            }
        }
        public bool Remove(T item) {
            return _items.Remove(item);
        }
        public bool Contains(T item) {
            return _items.Contains(item);
        }
        public int Count {
            get {
                return _items.Count();
            }
        }
        public MySet<T> Union(MySet<T> other) {
            MySet<T> result = new MySet<T>(_items);
            // this is the way the book did it
            // I would ignore duplicates in Add() rather than throw an error and get an easy win here
            foreach (T item in other._items) {
                if (!Contains(item)) {
                    result.Add(item);
                }
            }
            return result;
        }
        public MySet<T> Intersection(MySet<T> other) {
            MySet<T> result = new MySet<T>();
            foreach (T item in _items) {
                if (other._items.Contains(item)) {
                    result.Add(item);
                }
            }
            return result;
        }
        public MySet<T> Difference(MySet<T> other) {
            MySet<T> result = new MySet<T>(_items);
            foreach (T item in other._items) {
                result.Remove(item);
            }
            return result;
        }
        public MySet<T> SymetricDifference(MySet<T> other) {
            MySet<T> union = Union(other);
            MySet<T> intersection = Intersection(other);

            return union.Difference(intersection);
        }
        public IEnumerator<T> GetEnumerator() {
            return _items.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _items.GetEnumerator();
        }
    }
}
