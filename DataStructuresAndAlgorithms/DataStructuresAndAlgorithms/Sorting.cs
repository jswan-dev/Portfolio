using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    public static class Sorting<T> where T : IComparable<T> {
        public static void Swap(T[] items, int left, int right) {
            if (left != right) {
                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }
        public static void BubbleSort(T[] items) {
            /*
             *  Best Case = O(n)
             *  Average Case = O(n^2)
             *  Worst Case = O(n^2)
             *  
             *  Spase = O(1) 
             */
            bool swapped;
            do {
                swapped = false;
                for (int i = 1; i < items.Length; i++) {
                    if (items[i - 1].CompareTo(items[i]) > 0) {
                        Swap(items, i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped != false);
        }

        public static void InsertionSort(T[] items) {
            /*
             *  Best Case = O(n)
             *  Average Case = O(n^2)
             *  Worst Case = O(n^2)
             *  
             *  Space = O(1)
             */
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length) {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0) {

                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }
                sortedRangeEndIndex++;
            }
        }

        private static int FindInsertionIndex(T[] items, T valueToInsert) {
            for (int index = 0; index < items.Length; index++) {
                if (items[index].CompareTo(valueToInsert) > 0) {
                    return index;
                }
            }
            throw new InvalidOperationException("The insertion index was not found");
        }

        private static void Insert(T[] itemArray, int indexInsertingAt, int indexInsertingFrom) {
            T temp = itemArray[indexInsertingAt];
            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];

            for (int current = indexInsertingFrom; current > indexInsertingAt; current--) {
                itemArray[current] = itemArray[current - 1];
            }
            itemArray[indexInsertingAt + 1] = temp;
        }

        public static void SelectionSort(T[] items) {
            /*
             * Best Case = O(n)
             * Average Case = O(n^2)
             * Worst Case = O(n^2)
             * 
             * Space = O(1)
             */
            int sortedRangeEnd = 0;
            while (sortedRangeEnd < items.Length) {
                int nextIndex = FindIndexOfSmallestValueFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);

                sortedRangeEnd++;
            }
        }

        private static int FindIndexOfSmallestValueFromIndex(T[] items, int sortedRangeEnd) {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++) {
                if (currentSmallest.CompareTo(items[i]) > 0) {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }
            return currentSmallestIndex;
        }

        private static void MergeSort(T[] items) {
            /*
             *  Best Case = O(n log n)
             *  Average Case = O(n log n)
             *  Worst Case = O(n log n)
             *  
             *  Space = O(n)
             */

            if (items.Length <= 1) {
                return;
            }
            
            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;

            T[] left = new T[leftSize];
            T[] right = new T[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);

            MergeSort(left);
            MergeSort(right);
            Merge(items, left, right);
        }

        private static void Merge(T[] items, T[] left, T[] right) {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0) {
                if (leftIndex >= left.Length) {
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length) {
                    items[targetIndex] = left[leftIndex];
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0) {
                    items[targetIndex] = left[leftIndex++];
                }
                else {
                    items[targetIndex] = right[rightIndex++];
                }

                targetIndex++;
                remaining--;
            }
        }

        public static void QuickSort(T[] items) {
            /*
             *  Best Case = O(n log n)
             *  Average Case = O(n log n)
             *  Worst Case = O(n^2)
             *  
             *  Space = O(1)
             */
            quickSort(items, 0, items.Length - 1);
        }

        private static void quickSort(T[] items, int left, int right) {
        Random _pivotRng = new Random();
            if (left < right) {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(items, left, right, pivotIndex);

                quickSort(items, left, newPivot - 1);
                quickSort(items, newPivot + 1, right);
            }
        }

        private static int partition(T[] items, int left, int right, int pivotIndex) {
            T pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++) {
                if (items[i].CompareTo(pivotValue) < 0) {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
            }
            Swap(items, storeIndex, right);
            return storeIndex;
        }
    }
}
