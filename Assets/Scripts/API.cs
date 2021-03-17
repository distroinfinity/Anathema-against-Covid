using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    const string ENDPOINT = "https://api.covid19india.org/csv/latest/case_time_series.csv";
    public void GetTimeData(UnityAction<List<TimeData>> callback)
    {
        StartCoroutine(GetTimeDataRoutine(callback));
    }
    IEnumerator GetTimeDataRoutine(UnityAction<List<TimeData>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(ENDPOINT);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network error");
        }
        else
        {
            callback(ParseData(request.downloadHandler.text));
        }
    }
    List<TimeData> ParseData(string data) {
        //Date,Date_YMD,Daily Confirmed,Total Confirmed,Daily Recovered,Total Recovered,Daily Deceased,Total Deceased

        List<string> lines = data.Split('\n').ToList();
        lines.RemoveAt(0);
        //lines.RemoveAt(lines.Count - 1);

        List<TimeData> dataList = new List<TimeData>();
        foreach(string line in lines)
        {
            List<string> lineData = line.Split(',').ToList();
            TimeData timeData = new TimeData
            {
                date = DateTime.Parse(lineData[1]),
                tested = int.Parse(lineData[3]),
                positives = int.Parse(lineData[5]),
                deaths = int.Parse(lineData[7]),
            };
            dataList.Add(timeData);
        }
        return dataList;
    }
}
