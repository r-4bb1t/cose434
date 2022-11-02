using UnityEngine;


public class Check : MonoBehaviour
{
    public GameObject leftFingertip = null;
    public GameObject rightFingertip = null;
    public GameObject[] boxes = null;

    float touchDistance;
    Vector3 leftFingertipForward;
    Vector3 rightFingertipForward;

    void Start()
    {
        leftFingertipForward = leftFingertip.transform.TransformDirection(Vector3.forward);
        rightFingertipForward = rightFingertip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.5f;
    }

    void Update()
    {
        OVRInput.Update();
        checkButton();
    }

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }

    void checkButton()
    {
        float val = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if (val > 0.7f)
        {
            OVRInput.SetControllerVibration(0.1f/*frequency*/, 0.1f/*amplitude*/, OVRInput.Controller.RTouch);
        }

        Debug.DrawRay(leftFingertip.transform.position, leftFingertipForward, Color.red);
        if (Physics.Raycast(leftFingertip.transform.position, leftFingertipForward, out RaycastHit ray1, touchDistance))
        {
            Collider rayCollider = ray1.collider;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider.gameObject.name == box.name)
                    {
                        Debug.Log(rayCollider.gameObject.name);
                        box.GetComponent<BoxScript>().Trigger();
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider.gameObject.name == box.name)
                    {
                        Debug.Log(rayCollider.gameObject.name);
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
        }
        Debug.DrawRay(rightFingertip.transform.position, rightFingertipForward, Color.red);
        if (Physics.Raycast(rightFingertip.transform.position, rightFingertipForward, out RaycastHit ray2, touchDistance))
        {
            Collider rayCollider = ray2.collider;
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider.gameObject.name == box.name)
                    {
                        Debug.Log(rayCollider.gameObject.name);
                        box.GetComponent<BoxScript>().Trigger();
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider.gameObject.name == box.name)
                    {
                        Debug.Log(rayCollider.gameObject.name);
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
        }
    }
}