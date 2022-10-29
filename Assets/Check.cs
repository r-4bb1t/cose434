using UnityEngine;


public class Check : MonoBehaviour
{
    public GameObject leftFingertip;
    public GameObject rightFingertip;
    public GameObject[] box = null;
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
        checkButton();
    }

    void checkButton()
    {
        if (Physics.Raycast(leftFingertip.transform.position, leftFingertipForward, out RaycastHit ray1, touchDistance))
        {
            Collider rayCollider = ray1.collider;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
            }
        }
        if (Physics.Raycast(rightFingertip.transform.position, rightFingertipForward, out RaycastHit ray2, touchDistance))
        {
            Collider rayCollider = ray2.collider;
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
            }
        }
    }
}