using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class Program {
        static void Main(string[] args) {
            
            MyDoublyLinkedList<int> test = new MyDoublyLinkedList<int>();
            test.Add(3);
            test.Add(5);
            test.Add(7);
            test.RemoveFirst();
            Console.WriteLine(test.First().ToString());
            Console.ReadLine();
        }
        private static void InsertionSort() {
            int[] stuff = { 4, 5, 1, 2, 10 };

            for (int x = 1; x < stuff.Length; x++) {
                int y = x;

                while ((y > 0) && (stuff[y] < stuff[y - 1])) {
                    int tmp = stuff[y - 1];
                    stuff[y - 1] = stuff[y];
                    stuff[y] = tmp;

                    y--;
                }
            }
            foreach (int num in stuff) {
                Console.Write("{0}, ", num);
            }
            Console.WriteLine();
        }
        private static void ReverseInsertionSort() {
            int[] stuff = { 4, 5, 1, 2, 10 };

            for (int x = 1; x < stuff.Length; x++) {
                int y = x;

                // All you need to do is change the this less than to greater than
                while ((y > 0) && (stuff[y] > stuff[y - 1])) {
                    int tmp = stuff[y - 1];
                    stuff[y - 1] = stuff[y];
                    stuff[y] = tmp;

                    y--;
                }
            }
            foreach (int num in stuff) {
                Console.Write("{0}, ", num);
            }
            Console.WriteLine();
        }
    }
}
