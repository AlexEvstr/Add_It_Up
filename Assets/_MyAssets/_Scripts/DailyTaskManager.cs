using System;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    [SerializeField] private GameObject[] taskObjects;
    private AchieveController achieveController;

    private const string LastResetKey = "LastResetDate";

    private void Start()
    {
        achieveController = GetComponent<AchieveController>();
        CheckResetTasks();
        ShowAvailableTasks();
    }

    private void CheckResetTasks()
    {
        string lastResetStr = PlayerPrefs.GetString(LastResetKey, "");
        DateTime lastResetDate;

        if (string.IsNullOrEmpty(lastResetStr) || !DateTime.TryParse(lastResetStr, out lastResetDate) || DateTime.Now.Date > lastResetDate.Date)
        {
            // Новый день — сбрасываем все задания
            for (int i = 0; i < taskObjects.Length; i++)
            {
                PlayerPrefs.SetInt($"Task_{i}", 0);
                achieveController.ResetAchievements();
            }

            PlayerPrefs.SetString(LastResetKey, DateTime.Now.Date.ToString());
            PlayerPrefs.Save();
        }
    }

    private void ShowAvailableTasks()
    {
        for (int i = 0; i < taskObjects.Length; i++)
        {
            bool completed = PlayerPrefs.GetInt($"Task_{i}", 0) == 1;
            taskObjects[i].SetActive(!completed);
        }
    }

    public void ClaimTaskReward(int taskIndex)
    {
        PlayerPrefs.SetInt($"Task_{taskIndex}", 1);
        PlayerPrefs.Save();
        taskObjects[taskIndex].SetActive(false);
    }

    public void ResetDailyTasksTEST()
    {
        // Новый день — сбрасываем все задания
        for (int i = 0; i < taskObjects.Length; i++)
        {
            PlayerPrefs.SetInt($"Task_{i}", 0);
            achieveController.ResetAchievements();
        }

        PlayerPrefs.SetString(LastResetKey, DateTime.Now.Date.ToString());
        PlayerPrefs.Save();
    }
}
