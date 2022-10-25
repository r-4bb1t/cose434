using UnityEngine;


public class Check : MonoBehaviour
{
    //OVRHand hand = GetComponent<OVRHand>();
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