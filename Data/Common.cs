namespace SameGame.Data{
    public class Common{
        public static string GetColor(int color){
             string cl=string.Empty;
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
            return cl;
        }
    }
}