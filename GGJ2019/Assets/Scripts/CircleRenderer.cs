using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour {

    private int _numSegments = 20;

    void Start ( ) {
    }

    private void Update()
    {
    }

    public void Render (float radius) {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        Color c1 = new Color(1.0f, 0f, 0f, 1);
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(_numSegments + 1);
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float) (2.0 * Mathf.PI) / _numSegments;
        float theta = 0f;

        for (int i = 0 ; i < _numSegments + 1 ; i++) {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}