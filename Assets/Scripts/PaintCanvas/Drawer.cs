using UnityEngine;
public class Drawer {
    public Line line{ get; set; }
    public void Draw(Vector2 point){
        if(line ==null) return;
       
        if (line.HasPoints && IsDuplicate(point)) return;

        line.AddPoint(point);
    }

    public void StopDraw(){
        line.LineIsReady();
        line = null;
    }

    private bool IsDuplicate(Vector2 position){
        return position == line.GetPositionLast();
    }
}