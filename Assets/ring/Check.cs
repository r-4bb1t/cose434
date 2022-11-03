using UnityEngine;


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

    void Start()
    {
        leftFingertipForward = new Vector3(0, 0, 1);
        rightFingertipForward = new Vector3(0, 0, 1);
        touchDistance = 0.5f;
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
            if(!isRightKeyDown) isRightKeyDown = true;
            else isRightKeyDown = false;
        }
        if (!(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))) {
            if(isRightKeyDown) isRightKeyDown = false;
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            if(!isLeftKeyDown) isLeftKeyDown = true;
            else isLeftKeyDown = false;
        }
        if (!(OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))) {
            if(isLeftKeyDown) isLeftKeyDown = false;
        }

        checkButton();
    }

    void FixedUpdate()
    {
    }

    void checkButton()
    {
        Debug.DrawRay(leftFingertip.transform.position, leftFingertipForward, Color.red);
        if (Physics.Raycast(leftFingertip.transform.position, leftFingertipForward, out RaycastHit ray1, touchDistance))
        {
            Collider rayCollider1 = ray1.collider;

            /*foreach (GameObject box in boxes)
            {
                box.transform.Find("Light").GetComponent<Light>().enabled = false;
                if (rayCollider1.gameObject.name.Equals(box.name))
                {
                    Debug.Log("left " + rayCollider1.gameObject.name + " " + box.name);
                    box.transform.Find("Light").GetComponent<Light>().enabled = true;
                }
            }*/
            
            foreach (GameObject box in boxes)
            {
                if (rayCollider1.gameObject.name.Equals(box.name))
                {
                    leftHover = box;
                }
            }

            if (isLeftKeyDown)
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider1.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().Trigger();
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider1.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
        }
        else {
            //foreach (GameObject box in boxes) box.transform.Find("Light").GetComponent<Light>().enabled = false;
            leftHover = null;
            foreach (GameObject box in boxes) box.GetComponent<Renderer>().material.color = new Color(255/ 255f, 255/ 255f, 255/ 255f, 64 / 255f);
        }


        Debug.DrawRay(rightFingertip.transform.position, rightFingertipForward, Color.red);
        if (Physics.Raycast(rightFingertip.transform.position, rightFingertipForward, out RaycastHit ray2, touchDistance))
        {
            Collider rayCollider2 = ray2.collider;

            /*foreach (GameObject box in boxes)
            {
                box.transform.Find("Light").GetComponent<Light>().enabled = false;
                if (rayCollider2.gameObject.name.Equals(box.name))
                {
                    Debug.Log("right " + rayCollider2.gameObject.name + " " + box.name);
                    box.transform.Find("Light").GetComponent<Light>().enabled = true;
                }
            }*/

            foreach (GameObject box in boxes)
            {
                if (rayCollider2.gameObject.name.Equals(box.name))
                {
                    rightHover = box;
                }
            }

            if (isRightKeyDown)
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider2.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().Trigger();
                    }
                }
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                foreach (GameObject box in boxes)
                {
                    if (rayCollider2.gameObject.name.Equals(box.name))
                    {
                        box.GetComponent<BoxScript>().LongTrigger();
                    }
                }
            }
        }
        else {
            //foreach (GameObject box in boxes) box.transform.Find("Light").GetComponent<Light>().enabled = false;
            rightHover = null;
        }

        foreach (GameObject box in boxes) {
            if((leftHover != null && leftHover.name.Equals(box.name)) || (rightHover != null && rightHover.name.Equals(box.name))) box.GetComponent<Renderer>().material.color = new Color(255/ 255f, 255/ 255f, 150/ 255f, 200 / 255f);
            else box.GetComponent<Renderer>().material.color = new Color(255/ 255f, 255/ 255f, 255/ 255f, 64 / 255f);
        }
    }
}