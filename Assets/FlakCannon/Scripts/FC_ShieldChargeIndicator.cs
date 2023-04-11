using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_ShieldChargeIndicator : MonoBehaviour
{
    public GameObject chargeMarkPrefab;
    public Slider activatedCircle;
    public Color inactivetedColor;
    public Color activatedColor;
    public Color initialMarkColor = new Color(64, 128, 255);
    Slider circle;
    int numMarks = 3;
    List<UI_ChargeMarkAnimation> marks;
    float currentCharge = 0; // 0 ~ 1
    AudioSource indicatorAudio;
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<Slider>();
        marks = new List<UI_ChargeMarkAnimation>();
        indicatorAudio = this.gameObject.AddComponent<AudioSource>();
        indicatorAudio.loop = false;

    }

    // Update is called once per frame
    void Update()
    {
        ValueCorrection();
        CircleUpdates();
        MarkUpdates();
    }
    public void ReleaseShieldFailed()
    {

    }
    public void ReleaseShieldSuccess()
    {

    }
    private void CircleUpdates()
    {
        circle.value = currentCharge;

        if (Mathf.Floor(currentCharge * numMarks) > 0)
        {
            activatedCircle.value = (1f / numMarks) * Mathf.FloorToInt(currentCharge * numMarks);
        }
        else
        {
            activatedCircle.value = 0;
        }
    }
    private void MarkUpdates()
    {
        if (marks.Count < numMarks)
        {
            /// Re-generate marks to match the number, and make a replacement.
            GenerateMarks();
            PlaceMarks();
        }
        else if (marks.Count > numMarks)
        {
            /// Remove marks and make a replacement.
            for (int i = marks.Count; i > numMarks; --i)
            {
                GameObject temp = marks[i].gameObject;
                marks.RemoveAt(i);
                Destroy(temp);
            }
            PlaceMarks();
        }

        int chargedMark = Mathf.FloorToInt(currentCharge * marks.Count);
        for (int i = 1; i <= chargedMark && i <= marks.Count; ++i)
        {
            if (i == marks.Count)
                marks[0].Mark();
            else
                marks[i].Mark();
        }
    }
    private void GenerateMarks()
    {
        GameObject temp = null;
        UI_ChargeMarkAnimation markAnime = null;
        for (int i = marks.Count; i < numMarks; ++i)
        {
            temp = Instantiate(chargeMarkPrefab, this.gameObject.transform);
            temp.name = "ChargingMark_" + i;
            markAnime = temp.GetComponent<UI_ChargeMarkAnimation>();
            if (markAnime)
            {
                markAnime.SetInitialColor(initialMarkColor);
                markAnime.RegisterIndicator(this);
                marks.Add(markAnime);
            }
        }
        OffMarks();
    }
    private void PlaceMarks()
    {
        for (int i = 0; i < marks.Count; ++i)
        {
            marks[i].ChangeAngle((360f / (float)marks.Count) * i);
        }
    }

    public void ValueCorrection()
    {
        if (currentCharge > 1) currentCharge = 1;
        else if (currentCharge < 0) currentCharge = 0;
    }

    public void SetValue(float _value)
    {
        currentCharge = _value;
    }
    public void SetMarkNumber(int _numMark)
    {
        numMarks = _numMark;
    }
    public void ShowMarks()
    {
        foreach (UI_ChargeMarkAnimation mark in marks)
        {
            mark.gameObject.SetActive(true);
        }
    }
    public void OffMarks()
    {
        foreach (UI_ChargeMarkAnimation mark in marks)
        {
            mark.gameObject.SetActive(false);
            mark.Reset();
        }
    }
    public void PlaySound(AudioClip audioClip)
    {
        if (!audioClip)
        {
            Debug.Log("Has a null audio clip called from marks");
            return;
        }
        indicatorAudio.clip = audioClip;
        indicatorAudio.Play();
    }
}
