
using System.Collections.Concurrent;
namespace SameGame.Data{
    public class DataStore{
        public static ObservableConcurrentDictionary<string,Cell[,]> Data=new ObservableConcurrentDictionary<string, Cell[,]>();
    }
}