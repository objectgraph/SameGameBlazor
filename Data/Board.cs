using System;
namespace SameGame.Data{
    public struct Board{
        public string BoardId{get;set;}
        public int Rows{get;set;}
        public int Cols{get;set;}
        public int NumColors{get;set;}
        public Cell[,] Data{get;set;}

        public Board(int Rows, int Cols, int NumColors){
            this.BoardId = string.Empty;
            this.Rows = Rows;
            this.Cols = Cols;
            this.NumColors = NumColors;
            this.Data = new Cell[Rows,Cols];
        }

        public void NewGame(){
            this.BoardId =  Guid.NewGuid().ToString();
            var random = new Random();
            for(int i=0;i<Rows;i++){
                for(int j=0;j<Cols;j++){
                    var color = random.Next()%NumColors;
                    string cl = null;
                    if(color==0){
                        cl= "red";
                    }
                    else if(color==1){
                        cl= "green";
                    }
                    else if(color==2){
                        cl= "darkorange";
                    }
                    else if(color==3){
                        cl= "brown";
                    }
                    else if(color==4){
                        cl= "purple";
                    }
                    Data[i,j]= new Cell(i,j,cl,false,false);
                }    
            }
        }
    }
}