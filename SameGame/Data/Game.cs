using System;
using System.Collections.Generic;

namespace SameGame.Data{
    public class Game{
        public event EventHandler GameStateChanged;
        public GameState GameState;
        public Stack<String> undoStack = new Stack<String>();
        public Stack<String> redoStack = new Stack<String>();
        public string GameId{get;set;} 
        public string BoardId{get;set;}
        public string UserId{get;set;}
        public int Rows{get;set;}
        public int Cols{get;set;}
        public int NumColors{get;set;}

        protected virtual void OnGameStateChanged()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler raiseEvent = GameStateChanged;

            // Event will be null if there are no subscribers
            if (raiseEvent != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                // Call to raise the event.
                raiseEvent(this, null);
            }
        }
    
        public Game(int rows, int cols, int numColors){
            this.Rows = rows;
            this.Cols = cols;
            this.NumColors = numColors;
            this.GameState = new GameState(rows,cols,numColors);
            this.GameId = Guid.NewGuid().ToString();
            this.UserId = String.Empty;
        }

        public void InitGame(Board b){
            this.GameState.Score = 0;
            this.undoStack.Clear();
            this.redoStack.Clear();
            this.Rows = b.Rows;
            this.Cols = b.Cols;
            this.NumColors = b.NumColors;
            this.GameState.Rows = b.Rows;
            this.GameState.Cols = b.Cols;
            this.GameState.Data = new Cell[b.Rows,b.Cols];
            for(int i=0;i<Rows;i++){
                for(int j=0;j<Cols;j++){
                    this.GameState.Data[i,j] = b.Data[i,j];
                }
            }
            OnGameStateChanged();
        }

         public void InitGame(string boardId){
             InitGame(DataStore.GameBoard[boardId]);
         }

        public void NewGame(){
            this.GameState.Score = 0;
            this.undoStack.Clear();
            this.redoStack.Clear();
            Board b = new Board(Rows,Cols,NumColors);
            b.NewGame();
            this.BoardId = b.BoardId;
            DataStore.GameBoard[b.BoardId] = b;
            InitGame(b);
        }

        private bool isValid (int row, int col){
            return row >= 0 && row < Rows && col >= 0 && col < Cols;
        }
        private void  getSameColorNeighbors (List<Cell> arr, Cell cell) {
            int row = cell.Row;
            int col = cell.Col;
            //south
            if (this.isValid(row + 1, col) && !GameState.Data[row + 1,col].Deleted && GameState.Data[row + 1,col].Color == cell.Color && !arr.Contains(GameState.Data[row + 1,col])) {
                arr.Add(GameState.Data[row + 1,col]);
                this.getSameColorNeighbors(arr, GameState.Data[row + 1,col]);
            }
            //east
            if (this.isValid(row, col + 1) && !GameState.Data[row,col + 1].Deleted && GameState.Data[row,col + 1].Color == cell.Color && !arr.Contains(GameState.Data[row,col + 1])) {
                arr.Add(GameState.Data[row,col + 1]);
                this.getSameColorNeighbors(arr, GameState.Data[row,col + 1]);
            }

            //north
            if (this.isValid(row - 1, col) && !GameState.Data[row - 1,col].Deleted && GameState.Data[row - 1,col].Color == cell.Color && !arr.Contains(GameState.Data[row - 1,col])) {
                arr.Add(GameState.Data[row - 1,col]);
                this.getSameColorNeighbors(arr, GameState.Data[row - 1,col]);
            }

            //west
            if (this.isValid(row, col - 1) && !GameState.Data[row,col - 1].Deleted && GameState.Data[row,col - 1].Color == cell.Color && !arr.Contains(GameState.Data[row,col - 1])) {
                arr.Add(GameState.Data[row,col - 1]);
                this.getSameColorNeighbors(arr, GameState.Data[row,col - 1]);
            }
        }
        public void Action(Cell cell){
            var arr = new List<Cell>();
            this.getSameColorNeighbors(arr, cell);
            if (arr.Count > 0) {
                if (cell.Selected) {
                    this.redoStack.Clear();
                    this.undoStack.Push(GameState.Serialize());
                    this.GameState.Score = this.GameState.Score + ((arr.Count) * (arr.Count - 1) * 10);
                    arr.ForEach((obj) => GameState.Data[obj.Row,obj.Col].Deleted = true );
                    this.GameState.CurrentSelection = 0;
                }
                else {
                    for(int i=0;i<Rows;i++){
                        for(int j=0;j<Cols;j++){
                            GameState.Data[i,j].Selected=false;
                        }
                    }
                    arr.ForEach((obj) => GameState.Data[obj.Row,obj.Col].Selected = true );
                    this.GameState.CurrentSelection = (arr.Count) * (arr.Count - 1) * 10;
                }
                this.Gravity();
                this.Compact();
                this.GameState.GameOver = this.DetectGameOver();
                // Raise Event await Redraw();
                OnGameStateChanged();
            }
        }
        private void Gravity (){
            for (var j = this.Cols - 1; j >= 0; j--) {
                for (var i = this.Rows - 1; i >= 1; i--) {
                    if (this.GameState.Data[i,j].Deleted) {
                        //find first non empty cell and exchange
                        var r = i - 1;
                        while (this.GameState.Data[r,j].Deleted && r > 0) {
                            r--;
                        }
                        if (!this.GameState.Data[r,j].Deleted) {
                            //exchange
                            this.Swap(i, j, r, j);
                        }
                    }
                }
            }

        }
        private void Compact (){
            for (var j = this.Cols - 1; j >= 1; j--) {
                if (this.ColEmpty(j)) {
                    //find lowest column that is non empty
                    var col = j - 1;
                    while (this.ColEmpty(col) && col > 0) {
                        col--;
                    }
                    for (var i = 0; i < this.Rows; i++) {
                        this.Swap(i, j, i, col);
                    }
                }
            }
        }
        private bool ColEmpty (int col) {
            for (int i = 0; i < Rows; i++) {
                if (!GameState.Data[i,col].Deleted) {
                    return false;
                }
            }
            return true;
        }
        private bool DetectGameOver () {
            for (int i = 0; i < this.Rows; i++) {
                for (int j = 0; j < this.Cols; j++) {
                    if (GameState.Data[i,j].Deleted) {
                        continue;
                    }
                    var color = GameState.Data[i,j].Color;
                    if ((this.isValid(i - 1, j) && !GameState.Data[i - 1,j].Deleted && GameState.Data[i - 1,j].Color == color)
                        || (this.isValid(i, j + 1) && !GameState.Data[i,j + 1].Deleted && GameState.Data[i,j + 1].Color == color)
                        || (this.isValid(i + 1, j) && !GameState.Data[i + 1,j].Deleted && GameState.Data[i + 1,j].Color == color)
                        || (this.isValid(i, j - 1) && !GameState.Data[i,j - 1].Deleted && GameState.Data[i,j - 1].Color == color)) {
                        return false;
                    }
                }
            }
            return true;
        }
        void Swap( int i, int j, int k, int l) {
            int tColor = GameState.Data[i,j].Color;
            bool tSelected = GameState.Data[i,j].Selected;
            bool tDeleted = GameState.Data[i,j].Deleted;

            GameState.Data[i,j].Color = GameState.Data[k,l].Color;
            GameState.Data[i,j].Selected = GameState.Data[k,l].Selected;
            GameState.Data[i,j].Deleted = GameState.Data[k,l].Deleted;
            
            GameState.Data[k,l].Color = tColor;
            GameState.Data[k,l].Selected = tSelected;
            GameState.Data[k,l].Deleted = tDeleted;
            
        }

        private int LiveCellCount(){
            var cellCount = 0;
            for (int i = 0; i < this.Rows; i++) {
                for (int j = 0; j < this.Cols; j++) {
                    if (!GameState.Data[i,j].Deleted) {
                        cellCount++;
                    }
                }
            }
            return cellCount;
        }

        public void Undo() {
            if (this.undoStack.Count > 0) {
                var nowLiveCount = this.LiveCellCount();
                this.redoStack.Push(GameState.Serialize());
                GameState =  GameState.DeSerialize(undoStack.Pop());
                var afterUndoLiveCount = this.LiveCellCount();
                var diff = afterUndoLiveCount - nowLiveCount;
                this.GameState.GameOver = this.DetectGameOver();
                OnGameStateChanged();
            }
        }

        public void Redo(){
            if (this.redoStack.Count > 0) {
                var nowLiveCount = this.LiveCellCount();
                this.undoStack.Push(GameState.Serialize());
                GameState = GameState.DeSerialize(redoStack.Pop());
                var afterUndoLiveCount = this.LiveCellCount();
                var diff = nowLiveCount - afterUndoLiveCount;
                this.GameState.GameOver = this.DetectGameOver();
                OnGameStateChanged();
            }
        }
    }
}