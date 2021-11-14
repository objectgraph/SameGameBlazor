using System;
using System.Collections.Generic;
namespace SameGame.Data.BoardGen{
    public class BinaryHeap<T> where T : IComparable{
        List<T> Data;

        public BinaryHeap(){
            Data = new List<T>();
        }

        private static int ParentIndexOf(int i){
             return (int)(((i)-1)/2);
        }

        private static int LeftIndexOf(int k){
            return ((k)*2+1);
        }   
        private static int RightIndexOf(int k){
            return ((k)*2+2);
        }  

        public void Enque(T obj){
            int currI = Count;
            Data.Add(obj); // add new element to bottom of heap
            while(currI > 0) {         // float up to proper position
                int parentI = ParentIndexOf(currI);
                T parentObj = Data[parentI];
                if(parentObj.CompareTo(obj)>=0) return;
                ExchangeObjectAt(currI,parentI);
                currI = parentI;
	        }
        }

        private void ExchangeObjectAt(int i, int j){
            T temp = Data[i];
            Data[i] = Data[j];
            Data[j] = temp;
        }

        public T Dequeue(){
            int lastI = Count-1;
            ExchangeObjectAt(0,lastI);
            var ret = Data[lastI];
            Data.RemoveAt(lastI);
            int currI = 0;
            int leftI = LeftIndexOf(currI);
            while(leftI < lastI) {
                int childI = leftI;
                int rightI = RightIndexOf(currI);
                if(rightI < lastI && Data[rightI].CompareTo(Data[leftI])>=0) {
                    childI = rightI;
                }
                if(Data[currI].CompareTo(Data[childI])>=0) return ret;
                ExchangeObjectAt(childI,currI);
                currI = childI;
                leftI = LeftIndexOf(currI);
	        }

            return ret;
        }

        public int Count{
            get{
                return Data.Count;
            }
        }
    }
}