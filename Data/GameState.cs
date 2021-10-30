namespace SameGame.Data{
    public struct GameState{
        public int Rows{get;set;}
        public int Cols{get;set;}
        public int NumColors{get;set;}
        public Cell[,] Data;
        public int Score{get;set;}

        public GameState(int Rows, int Cols, int NumColors){
            this.Rows = Rows;
            this.Cols = Cols;
            this.NumColors = NumColors;
            this.Data = new Cell[Rows,Cols];
            this.Score = 0;
        }
    }

}