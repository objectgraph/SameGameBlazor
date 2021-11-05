using System.Collections.Generic;
using System.Linq;
namespace SameGame.Data.BoardGen{
    public class Graph<T>{
        Dictionary<T,List<T>> data;
        public Graph(){
            data = new Dictionary<T, List<T>>();
        }

        public void AddVertex(T c){
            data[c] = new List<T>(); 
        }

        public void AddEdgeBetween(T a,T b){
            if(!data.Keys.Contains(a)){
                data[a]=new List<T>();
            }
            if(!data.Keys.Contains(b)){
                data[b]=new List<T>();
            }
            data[a].Add(b);
            data[b].Add(a);
        }

        public int DegreeOf(T t){
            return data[t].Count;
        }

        public List<T> NeighboarsOf(T v){
            return data[v];
        }
    }
}