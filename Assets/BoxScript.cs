using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 1.0f;
    public float good = 10.0f;
    public float miss = 100.0f;
    Queue<GameObject> notes = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(notes);
    }

    /*GameObject FindClosestNote()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Note");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }*/

    float FindDistance(GameObject note)
    {
        Vector3 position = transform.position;
        Vector3 diff = note.transform.position - position;
        return diff.sqrMagnitude;
    }

    public void Trigger()
    {
        /*GameObject nearest = FindClosestNote();
        if (nearest == null) return;
        Debug.Log(nearest);
        Destroy(nearest.gameObject);*/

        if (notes.Count == 0) return;
        GameObject note = notes.Peek();
        float distance = FindDistance(note);
        if (distance > miss) return;
        if (distance > good)
        {
            /* MISS */
        }
        else if (distance > perfect)
        {
            /* GOOD */
        }
        else
        {
            /* PERFECT */
        }
        notes.Dequeue();
        Destroy(note);
    }

    public void AddNewNote(GameObject note)
    {
        notes.Enqueue(note);
        Debug.Log("???????");
    }
}
