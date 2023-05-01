using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrenght;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrenght;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
    }
}
