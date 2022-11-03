using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnToMain : MonoBehaviour
{
    public GameObject result;
    public GameObject ui;

    float touchDistance;
    Vector3 FingertipForward;

    void Start()
    {
        Invoke("Return", 10f);
    }

    void Return()
    {
        result.SetActive(false);
        ui.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
