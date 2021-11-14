using Newtonsoft.Json;

namespace SameGame.Data{
    public struct GameState{
        public int Rows{get;set;}
        public int Cols{get;set;}
        public int NumColors{get;set;}
        public Cell[,] Data;
        public int Score{get;set;}
        public int CurrentSelection{get;set;}
        public bool GameOver{get;set;}
        public string GameId{get;set;}
        public string BoardId{get;set;}

        public GameState(int Rows, int Cols, int NumColors){
            this.Rows = Rows;
            this.Cols = Cols;
            this.NumColors = NumColors;
            this.Data = new Cell[Rows,Cols];
            this.Score = 0;
            this.CurrentSelection = 0;
            this.GameOver = false;
            this.GameId = string.Empty;
            this.BoardId = string.Empty;
        }

        public string Serialize(){
            return JsonConvert.SerializeObject(this);
        }

        public static GameState DeSerialize(string str){
            return JsonConvert.DeserializeObject<GameState>(str);
        }

        public int ColorCount(int color){
            int count = 0;
            for(int i=0;i<Rows;i++){
                for(int j=0;j<Cols;j++){
                    if(Data[i,j].Color==color && !Data[i,j].Deleted){
                        count++;
                    }
                }
            }
            return count;
        }
    }

}