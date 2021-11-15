namespace SameGame.Data{
    public struct Cell{
        public int Row{get;set;}
        public int Col{get;set;}
        public int Color{get;set;}
        public bool Selected{get;set;}
        public bool Deleted{get;set;}
        public Cell(int Row, int Col, int Color,bool Selected,bool Deleted){
            this.Row = Row;
            this.Col = Col;
            this.Color = Color;
            this.Selected = Selected;
            this.Deleted = Deleted;
        }
    }   
}