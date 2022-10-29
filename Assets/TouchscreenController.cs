using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchscreenController : MonoBehaviour
{
    public GameObject fingertip;
    public GameObject game;
    bool isPointing;
    bool isTouching;
    float touchDistance;
    Vector3 FingertipForward;
    void Start()
    {
        FingertipForward = fingertip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.005f;
        isTouching = false;
        isPointing = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPointing();

        if (Physics.Raycast(fingertip.transform.position, FingertipForward, out RaycastHit ray, touchDistance))
        {
            Collider rayCollider = ray.collider;
            if (rayCollider.gameObject.name.Equals("Emocloche") && isPointing)
            {
                Instantiate(game, new Vector3(-10.83f, 2.06f, -2.297f), new Quaternion(), gameObject.transform);
            }
        }
    }

    void CheckPointing()
    {
        if (!OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger))
        {
            isPointing = true;
        }
        else
        {
            isPointing = false;
        }
    }
}
