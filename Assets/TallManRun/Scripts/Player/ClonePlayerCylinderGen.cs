using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePlayerCylinderGen : MonoBehaviour
{
    [Range(.01f, 1), SerializeField] private float radiusInner;
    [Range(3, 16), SerializeField] private int angularSegmentsCount = 3;
    [SerializeField] private float thickness;

    private float _skewAmount;

    private float RadiusOuter => radiusInner + thickness;
    private int VertexCount => angularSegmentsCount * 2;

   
    private Mesh _mesh;

    private void Awake()
    {
        //_filter = GetComponent<MeshFilter>();
    }

    public void GetUpdated(Vector3 direction, float length, string meshName)
    {
        _skewAmount = length;
        GenerateMesh(meshName);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void GenerateMesh(string meshName)
    {
        _mesh = new Mesh();
        _mesh.name = meshName;
        //_filter.sharedMesh = _mesh;

        _mesh.Clear();
        var vCount = VertexCount;

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();

        for (var i = 0; i < angularSegmentsCount; i++)
        {
            var t = i / (float)angularSegmentsCount;
            var angRad = t * MathFx.Tau;
            var dir = MathFx.GetUnitVectorByAngle(angRad);
            vertices.Add(dir * RadiusOuter);
            vertices.Add((Vector3)(dir * radiusInner) + Vector3.forward * _skewAmount);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
        }

        // triangle generation
        var triangleIndices = new List<int>();
        for (var i = 0; i < angularSegmentsCount; i++)
        {
            var indexRoot = i * 2;
            var indexInnerRoot = indexRoot + 1;
            var indexOuterNext = (indexRoot + 2) % vCount;
            var indexInnerNext = (indexRoot + 3) % vCount;

            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexInnerNext);


            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexInnerRoot);
        }

        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(triangleIndices, 0);
        _mesh.SetNormals(normals);

        _mesh.RecalculateNormals();

    }
}
