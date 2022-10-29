using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Trigger()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        if (notes.Length > 0)
        {
            Debug.Log(notes[0].gameObject);
            Destroy(notes[0].gameObject);
        }
    }
}
