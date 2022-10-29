using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchscreenController : MonoBehaviour
{
    public GameObject fingertip;
    public GameObject game;
    public GameObject ui;
    float touchDistance;
    Vector3 FingertipForward;
    public string[] musics;

    void Start()
    {
        FingertipForward = fingertip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(fingertip.transform.position, FingertipForward, out RaycastHit ray, touchDistance))
        {
            Collider rayCollider = ray.collider;
            foreach (string music in musics)
            {
                ui.gameObject.transform.Find(music).Find("Light").GetComponent<Light>().enabled = false;
                if (rayCollider.gameObject.name.Equals(music))
                {
                    rayCollider.gameObject.transform.Find("Light").GetComponent<Light>().enabled = true;
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        ui.SetActive(false);
                        game.SetActive(true);
                    }
                }
            }
        }
        else
        {
            foreach (string music in musics)
                ui.gameObject.transform.Find(music).Find("Light").GetComponent<Light>().enabled = false;
        }
    }
}
