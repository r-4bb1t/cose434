using UnityEngine;
using TMPro;
using OVR;

public class Check : MonoBehaviour
{
    public GameObject leftFingertip = null;
    public GameObject rightFingertip = null;
    public GameObject[] boxes = null;

    float touchDistance;
    Vector3 leftFingertipForward;
    Vector3 rightFingertipForward;

    private bool isRightKeyDown = false;
    private bool isLeftKeyDown = false;

    private GameObject rightHover = null;
    private GameObject leftHover = null;
    private GameObject rightClick = null;
    private GameObject leftClick = null;

    public int score = 0;
    public GameObject UI;

    void Start()
    {
        leftFingertipForward = new Vector3(0, 0, 1);
        rightFingertipForward = new Vector3(0, 0, 1);
        touchDistance = 1.0f;
    }

    void Update()
    {
        UI.transform.Find("Score").GetComponent<TextMeshPro>().text = score.ToString();

        /*if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if (!isRightKeyDown) isRightKeyDown = true;
            else isRightKeyDown = false;
        }
        if (!(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)))
        {
            if (isRightKeyDown) isRightKeyDown = false;
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (!isLeftKeyDown) isLeftKeyDown = true;
            else isLeftKeyDown = false;
        }
        if (!(OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)))
        {
            if (isLeftKeyDown) isLeftKeyDown = false;
        }*/

        checkButton();
    }

    void checkButton()
    {
        Debug.DrawRay(leftFingertip.transform.position, leftFingertipForward, Color.red);
        if (Physics.Raycast(leftFingertip.transform.position, leftFingertipForward, out RaycastHit ray1, touchDistance))
        {
            Collider rayCollider1 = ray1.collider;

            foreach (GameObject box in boxes)
            {
                if (rayCollider1.gameObject.name.Equals(box.name))
                {
                    leftHover = box;
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider1.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().Trigger();
                        leftClick = box;
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider1.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
            else leftClick = null;
        }
        else
        {
            leftHover = null;
        }


        Debug.DrawRay(rightFingertip.transform.position, rightFingertipForward, Color.red);
        if (Physics.Raycast(rightFingertip.transform.position, rightFingertipForward, out RaycastHit ray2, touchDistance))
        {
            Collider rayCollider2 = ray2.collider;

            foreach (GameObject box in boxes)
            {
                if (rayCollider2.gameObject.name.Equals(box.name))
                {
                    rightHover = box;
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider2.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().Trigger();
                        rightClick = box;
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider2.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
            else rightClick = null;
        }
        else
        {
            rightHover = null;
        }

        foreach (GameObject box in boxes)
        {
            if ((leftClick != null && leftClick.name.Equals(box.name)) || (rightClick != null && rightClick.name.Equals(box.name))) box.GetComponent<Renderer>().material.color = new Color(255 / 255f, 0 / 255f, 0 / 255f, 200 / 255f);
            else if ((leftHover != null && leftHover.name.Equals(box.name)) || (rightHover != null && rightHover.name.Equals(box.name))) box.GetComponent<Renderer>().material.color = new Color(255 / 255f, 255 / 255f, 150 / 255f, 200 / 255f);
            else box.GetComponent<Renderer>().material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 64 / 255f);
        }
    }
}