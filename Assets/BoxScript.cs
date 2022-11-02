using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 1.0f;
    public float good = 1.5f;
    public float miss = 2.0f;
    Queue<GameObject> notes = null;
    public GameObject ring;

    // Start is called before the first frame update
    void Start()
    {
        notes = new Queue<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        while (notes.Count > 0)
        {
            GameObject note = notes.Peek();
            float distance = FindDistance(note);
            if (distance > miss && IsPassed(note))
            {
                /* MISS */
                notes.Dequeue();
                Destroy(note);
            }
            else
            {
                break;
            }
        }
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
        Vector3 scale = note.transform.localScale;
        return Mathf.Abs(10.0f - scale.x);
    }

    bool IsPassed(GameObject note)
    {
        Vector3 ringPosition = ring.transform.position;
        Vector3 diff1 = transform.position - ringPosition;
        float distance1 = diff1.sqrMagnitude;
        Vector3 diff2 = note.transform.position - ringPosition;
        float distance2 = diff2.sqrMagnitude;
        return distance1 > distance2;
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

        Debug.Log(distance);
    }

    public void AddNewNote(GameObject note)
    {
        notes.Enqueue(note);
    }
}
