using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public GameObject[] boxes;
    public GameObject rightFingertip;
    float touchDistance;
    Vector3 RightFingertipForward;

    void Start()
    {
        RightFingertipForward = rightFingertip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(rightFingertip.transform.position, RightFingertipForward, out RaycastHit ray, touchDistance))
        {
            Collider rayCollider = ray.collider;
        }
        else
        {
        }
    }
}
