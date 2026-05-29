using UnityEngine;

public class MediumTriangle : TangramShape
{
    void OnValidate()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0),
            new Vector3(1, 1),
            new Vector3(2, 0)
        };
        CreateShape(vertices);
    }
}

