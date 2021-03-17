using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    public float WaitTime = .1f;
    public float BarScale = 5000;

    [SerializeField]
    API api;
    [SerializeField]
    TextMeshPro title;
    [SerializeField]
    TextMeshPro confirmedNumber;
    [SerializeField]
    TextMeshPro PositiveNumber;
    [SerializeField]
    TextMeshPro DeathNumber;
    [SerializeField]
    List<BarBehaviour> bars = new List<BarBehaviour>();

    void Start()
    {
        api.GetTimeData(OnDataReceived);
    }
    void OnDataReceived(List<TimeData> dataList)
    {
        StartCoroutine(CycleDataRoutine(dataList));
    }
    IEnumerator CycleDataRoutine(List<TimeData> dataList)
    {
        while (true)
        {
            foreach (TimeData data in dataList)
            {
                title.text = data.date.ToString("MMMM dd, yyyy");
                confirmedNumber.text = data.tested.ToString();
                PositiveNumber.text = data.positives.ToString();
                DeathNumber.text = data.deaths.ToString();

                bars[0].SetScale(data.tested / BarScale);
                bars[1].SetScale(data.positives / BarScale);
                bars[2].SetScale(data.deaths / BarScale);


                yield return new WaitForSeconds(WaitTime);
            }
            foreach(BarBehaviour bar in bars)
            {
                bar.Reset();
            }
        }
    }
}
