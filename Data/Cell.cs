namespace SameGame.Data{
    public class Cell{
        public int Row{get;set;}
        public int Col{get;set;}
        public string Color{get;set;}
        public bool Selected{get;set;}
        public bool Deleted{get;set;}

        public Cell(int row, int col, string color, bool selected, bool deleted){
            this.Row= row;
            this.Col = col;
            this.Color = color;
            this.Selected = selected;
            this.Deleted = deleted;
        }
    }
       
}