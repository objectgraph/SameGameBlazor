
using System.Collections.Concurrent;
namespace SameGame.Data{
    public class DataStore{
        public static ObservableConcurrentDictionary<string,GameState> GlobalStore=new ObservableConcurrentDictionary<string, GameState>();
        public static ObservableConcurrentDictionary<string,Board> GameBoard=new ObservableConcurrentDictionary<string, Board>();

    }
}