using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver
{
    private string _filepath;

    public DataSaver()
    {
        _filepath = Application.persistentDataPath + "/Save.json";
    }

    public void SaveData(List<FilledHistoryWindowData> windowsData, bool onboardingCompleted)
    {
        try
        {
            string json = JsonUtility.ToJson(new FilledHistoryWindowsDataList(windowsData, onboardingCompleted), true);
            File.WriteAllText(_filepath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public List<FilledHistoryWindowData> LoadData(out bool onboardingStatus)
    {
        if (!File.Exists(_filepath))
        {
            Debug.LogWarning("Save file not found!");
            onboardingStatus = false;
            return new List<FilledHistoryWindowData>();
        }

        string json = File.ReadAllText(_filepath);
        onboardingStatus = JsonUtility.FromJson<FilledHistoryWindowsDataList>(json).OnboardingCompleted;
        return JsonUtility.FromJson<FilledHistoryWindowsDataList>(json).WindowsData;
    }
}

[Serializable]
public class FilledHistoryWindowsDataList
{
    public List<FilledHistoryWindowData> WindowsData;
    public bool OnboardingCompleted;

    public FilledHistoryWindowsDataList(List<FilledHistoryWindowData> windowsData, bool onboardingCompleted)
    {
        WindowsData = windowsData;
        OnboardingCompleted = onboardingCompleted;
    }
}