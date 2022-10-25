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
    private int DIROFFSET = 90;
    private float DURATION = 1.5f;

    IEnumerator MakeLongNote(int dir, float delay, bool doub, int i)
    {
        while (true)
        {
            if (i == 1) yield return new WaitForSeconds(delay / 2f);
            GameObject ln = null;
            if (i == 0)
            {
                if (doub) ln = Instantiate(longNoteDouble);
                else ln = Instantiate(longNote);
            }
            else
            {
                if (doub) ln = Instantiate(dotDouble);
                else ln = Instantiate(dot);
            }
            ln.transform.SetParent(UI.transform);
            ln.transform.localPosition = Vector3.zero;

            ln.GetComponent<NoteParent>().dir = (dir + DIROFFSET) * 45;
            Destroy(ln, DURATION);

            if (i == 0) yield return new WaitForSeconds(delay);
            else yield return new WaitForSeconds(delay / 2f);
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
                dn.transform.SetParent(UI.transform);
                dn.transform.localPosition = Vector3.zero;

                dn.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(dn, DURATION);
            }
            if (note.noteType == 2)
            {
                GameObject sn = null;
                if (!doub) sn = Instantiate(startNote);
                else sn = Instantiate(startNoteDouble);
                sn.transform.SetParent(UI.transform);
                sn.transform.localPosition = Vector3.zero;

                sn.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(sn, DURATION);

                isLongNote[i] = true;
                isDoub[i] = doub;
                makeLongNote[i] = MakeLongNote(i, 60f / (float)data.bpm, doub, 0);
                makeDot[i] = MakeLongNote(i, 60f / (float)data.bpm, doub, 1);
                StartCoroutine(makeLongNote[i]);
                StartCoroutine(makeDot[i]);
            }
            /*if (note.noteType == 3)
            {
                GameObject sn = Instantiate(longNote);
                sn.transform.SetParent(UI.transform);
                sn.transform.localPosition = Vector3.zero;

                sn.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(sn, 2f);
            }*/
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
                en.transform.SetParent(UI.transform);
                en.transform.localPosition = Vector3.zero;

                en.GetComponent<NoteParent>().dir = (i + DIROFFSET) * 45;
                Destroy(en, DURATION);

                isDoub[i] = false;
            }
        }
        n++;
    }

    void SetUI()
    {
        UI.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = data.title;
        UI.transform.Find("Composer").GetComponent<TextMeshProUGUI>().text = data.composer;
        UI.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "LV " + data.level;
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
        UI.transform.Find("FPS").GetComponent<TextMeshProUGUI>().text = "FPS " + (1.0f / deltaTime).ToString();
    }
}
