using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 1;
    public float good = 10;
    GameObject[] notes = { };

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

    float FindDistance(Gameobject note)
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

        if (notes.GetLength == 0) return;
        float distance = FindDistance(notes[0]);
        if (distance > good) return;
        if (distance > perfect)
        {
            /* GOOD */
            Destroy(notes[0]);
            notes.Skip(1).toArray();
        }
        else
        {
            /* PERFECT */
            Destroy(notes[0]);
            notes.Skip(1).toArray();
        }
    }

    public void AddNewNote(GameObject note)
    {
        notes.Push(note);
    }
}
