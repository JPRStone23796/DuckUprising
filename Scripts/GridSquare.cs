using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour {
    public bool bObject;

    public bool ObjectAbove()
    {
        return bObject;
    }

    public void SetAbove(bool Object)
    {
        bObject = Object;
    }
}
