using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour {
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateBounds();
    }
}