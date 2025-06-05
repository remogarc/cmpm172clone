using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AnalyticsManager : MonoBehaviour
{
    async void Start()
    {
        await InitializeAnalytics();
        LogStartupEvent();
    }

    async Task InitializeAnalytics()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to initialize Unity Services: " + e.Message);
        }
    }

    void LogStartupEvent()
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "session_time", Time.realtimeSinceStartup },
                { "player_level", 1 }
            };

        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to send Analytics event: " + e.Message);
        }
    }
}
