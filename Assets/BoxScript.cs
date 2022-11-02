using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 0.1f;
    public float good = 0.15f;
    public float miss = 0.2f;
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
        // 10-scale
        Vector3 scale = note.transform.localScale;
        return (1.0f - scale.x);
    }

    bool IsPassed(GameObject note)
    {
        return FindDistance(note) > 0;
    }

    public void Trigger()
    {
        /*GameObject nearest = FindClosestNote();
        if (nearest == null) return;
        Debug.Log(nearest);
        Destroy(nearest.gameObject);*/

        if (notes.Count == 0) return;
        GameObject note = notes.Peek();
        float distance = Mathf.Abs(FindDistance(note));
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
