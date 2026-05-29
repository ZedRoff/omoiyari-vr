using UnityEngine;

public class SmallTriangle1 : TangramShape
{
    void OnValidate()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0),
            new Vector3(1, 0),
            new Vector3(0, 1)
        };
        CreateShape(vertices);
    }
}
