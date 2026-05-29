using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TangramShape : MonoBehaviour
{
    protected void CreateShape(Vector3[] vertices)
    {
        Mesh mesh = new Mesh();

        int[] triangles = new int[(vertices.Length - 2) * 3];
        for (int i = 0; i < vertices.Length - 2; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;

        // --- AJOUTE CES LIGNES POUR CORRIGER LES COLLIDERS ---
        if (TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
        {
            meshCollider.sharedMesh = null; // Reset le vieux mesh
            meshCollider.sharedMesh = mesh; // Assigne le nouveau mesh parfaitement aligné
        }
    }
}