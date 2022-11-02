using UnityEngine;


public class Check : MonoBehaviour
{
    public GameObject[] boxes = { null, null, null, null, null, null, null, null };
    public GameObject[][] notes = { { }, { }, { }, { }, { }, { }, { }, { } };

    void Start()
    {
    }

    void Update()
    {
        checkButton();
    }

    void checkButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        { //왼손
            console.log("left");
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        { //오른손
            console.log("right");
        }
    }
}