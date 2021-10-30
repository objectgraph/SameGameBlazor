
using System.Collections.Concurrent;
namespace SameGame.Data{
    public class DataStore{
        public static ObservableConcurrentDictionary<string,Cell[,]> Data=new ObservableConcurrentDictionary<string, Cell[,]>();
        public static ObservableConcurrentDictionary<string,GameState> GlobalStore=new ObservableConcurrentDictionary<string, GameState>();

    }
}