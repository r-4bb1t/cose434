using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 1;
    public float good = 10;
    Queue<GameObject> notes = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

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
        if (distance > good) return;
        if (distance > perfect)
        {
            /* GOOD */
            Destroy(note);
            notes.Dequeue();
        }
        else
        {
            /* PERFECT */
            Destroy(note);
            notes.Dequeue();
        }
    }

    public void AddNewNote(GameObject note)
    {
        notes.Enqueue(note);
    }
}
