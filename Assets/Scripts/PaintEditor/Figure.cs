using System;
using UnityEngine;

[Serializable]
public  class Figure 
{
    public virtual void CreateGameObject(Vector3 position)
    {

    }

    public virtual void Draw(Vector3 position)
    {

    }

    public virtual bool EndDraw()
    {
        return false;
    }

    public virtual void DestroyObject()
    {

    }

}
