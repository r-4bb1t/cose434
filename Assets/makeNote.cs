using System.Dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Note
{
    public int noteType;
}

[System.Serializable]
public class Beat
{
    public Note[] notes;
}

[System.Serializable]
public class Bar
{
    public Beat[] beats;
}

[System.Serializable]
public class Chart
{
    public string title;
    public int level;
    public string composer;
    public string cover;
    public int bpm;
    public int barNumber;
    public Bar[] bars;
}

[RequireComponent(typeof(AudioSource))]
public class makeNote : MonoBehaviour
{
    public GameObject defaultNote;
    public GameObject longNote;
    public GameObject startNote;
    public GameObject endNote;
    public GameObject dot;
    public GameObject defaultNoteDouble;
    public GameObject longNoteDouble;
    public GameObject startNoteDouble;
    public GameObject endNoteDouble;
    public GameObject dotDouble;
    public GameObject UI;
    public GameObject game;
    NoteParent script;
    Chart data;
    int n;
    float deltaTime = 0.0f;
    private AudioSource audioSource;
    private IEnumerator[] makeLongNote = { null, null, null, null, null, null, null, null };
    private IEnumerator[] makeDot = { null, null, null, null, null, null, null, null };
    private float num = 12;
    private bool[] isLongNote = { false, false, false, false, false, false, false, false };
    private bool[] isDoub = { false, false, false, false, false, false, false, false };
    private float OFFSET = 1.0f;
    private int DIROFFSET = 135;
    private float DURATION = 2f;
    public GameObject[] boxes;

    IEnumerator MakeLongNote(int dir, float delay, bool doub)
    {
        while (true)
        {
            GameObject ln = null;
            if (doub) ln = Instantiate(longNoteDouble);
            else ln = Instantiate(longNote);

            ln.transform.SetParent(game.transform);
            ln.transform.localPosition = Vector3.zero;

            ln.GetComponent<NoteParent>().dir = (dir + DIROFFSET) * 45;
            Destroy(ln, DURATION);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator MakeDot(int dir, float delay, bool doub)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay / 2);
            GameObject d = null;
            if (doub) d = Instantiate(dotDouble);
            else d = Instantiate(dot);

            d.transform.SetParent(game.transform);
            d.transform.localPosition = Vector3.zero;

            d.GetComponent<NoteParent>().dir = (dir + DIROFFSET) * 45;
            Destroy(d, DURATION);

            boxes[dir].GetComponent<BoxScript>().AddNewNote(d);

            yield return new WaitForSeconds(delay / 2);
        }
    }

    void MakeNote()
    {
        int noteCnt = 0;
        for (int i = 0; i < 8; i++) if (data.bars[n / 48].beats[n % 48].notes[i].noteType == 1 || data.bars[n / 48].beats[n % 48].notes[i].noteType == 2) noteCnt++;
        bool doub = (noteCnt >= 2);
        for (int i = 0; i < 8; i++)
        {
            Note note = data.bars[n / 48].beats[n % 48].notes[i];
            if (note.noteType == 1)
            {
                GameObject dn = null;
                if (!doub) dn = Instantiate(defaultNote);
                else dn = Instantiate(defaultNoteDouble);
                dn.transform.SetParent(game.transform);
                dn.transform.localPosition = Vector3.zero;

                dn.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(dn, DURATION);

                boxes[i].GetComponent<BoxScript>().AddNewNote(dn);
            }
            if (note.noteType == 2)
            {
                GameObject sn = null;
                if (!doub) sn = Instantiate(startNote);
                else sn = Instantiate(startNoteDouble);
                sn.transform.SetParent(game.transform);
                sn.transform.localPosition = Vector3.zero;

                sn.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(sn, DURATION);

                boxes[i].GetComponent<BoxScript>().AddNewNote(sn);

                isLongNote[i] = true;
                isDoub[i] = doub;
                makeLongNote[i] = MakeLongNote(i, 60f / (float)data.bpm, doub);
                makeDot[i] = MakeDot(i, 60f / (float)data.bpm / 6, doub);
                //StartCoroutine(makeLongNote[i]);
                StartCoroutine(makeDot[i]);
            }
            /* if (note.noteType == 3)
            {
                GameObject ln = null;
                if (doub) ln = Instantiate(longNoteDouble);
                else ln = Instantiate(longNote);
                ln.transform.SetParent(UI.transform);
                ln.transform.localPosition = Vector3.zero;

                ln.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(ln, DURATION);
            } */
            if (note.noteType == 4)
            {
                if (isLongNote[i])
                {
                    StopCoroutine(makeLongNote[i]);
                    StopCoroutine(makeDot[i]);
                    isLongNote[i] = false;
                }

                GameObject en = null;
                if (!isDoub[i]) en = Instantiate(endNote);
                else en = Instantiate(endNoteDouble);
                en.transform.SetParent(game.transform);
                en.transform.localPosition = Vector3.zero;

                en.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(en, DURATION);

                boxes[i].GetComponent<BoxScript>().AddNewNote(en);

                isDoub[i] = false;
            }
        }
        n++;
    }

    void SetUI()
    {
        UI.transform.Find("Title").GetComponent<TextMeshPro>().text = data.title;
        UI.transform.Find("Composer").GetComponent<TextMeshPro>().text = data.composer;
        UI.transform.Find("Level").GetComponent<TextMeshPro>().text = "LV " + data.level;
    }

    void setBgm()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        AudioClip audioAsset = (AudioClip)Resources.Load("Audio/emocloche");
        audioSource.clip = (AudioClip)audioAsset;
        audioSource.Play();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        var loadedJson = Resources.Load("JSON/emocloche") as TextAsset;
        data = JsonUtility.FromJson<Chart>(loadedJson.ToString());
        SetUI();
        Invoke("setBgm", 5f);
        InvokeRepeating("MakeNote", 5f + OFFSET, 60f / (float)data.bpm / num);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        UI.transform.Find("FPS").GetComponent<TextMeshPro>().text = "FPS " + (1.0f / deltaTime).ToString();
    }
}
