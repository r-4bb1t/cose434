using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchscreenController : MonoBehaviour
{
    public GameObject fingertip;
    public GameObject game;
    public GameObject ui;
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
                isTouching = true;
                game.GetComponent<GameObject>().SetActive(true);
                ui.GetComponent<GameObject>().SetActive(false);
            }
            else
            {
                isTouching = false;
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
