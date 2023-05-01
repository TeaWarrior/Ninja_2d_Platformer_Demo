using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
   [SerializeField] Transform[] points;
    // Start is called before the first frame update
    private void Awake()
    {
      _lineRenderer =  GetComponent<LineRenderer>();
    }

   public void SetUpLInes(Transform[] _points)
    {
        _lineRenderer.positionCount = _points.Length;
        this.points = _points;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < points.Length; i++)
        {
            _lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
