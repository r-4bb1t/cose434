using UnityEngine;


public class Check : MonoBehaviour
{
    public GameObject[] box = null;

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
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        { //오른손
        }
    }
}