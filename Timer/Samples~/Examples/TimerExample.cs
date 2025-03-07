using UnityEngine;
using UnityEngine.UI;
using asim.unity.utils;

public class TimerExample : MonoBehaviour
{
    [Header("Repeater")]
    [SerializeField] Text durationRunning_Text;
    [SerializeField] Text durationTime_Text;
    [SerializeField] Text durationRuns_Text;
    [SerializeField] Button reset_btn;

    [Header("Repeater")]
    [SerializeField] Text repeaterRunning_Text;
    [SerializeField] Text repeaterTime_Text;
    [SerializeField] Text repeaterRuns_Text;
    [SerializeField] Button startstop_btn;

    [Header("Interval")]
    [SerializeField] Text intervalRunning_Text;
    [SerializeField] Text intervalTime_Text;
    [SerializeField] Text intervalRuns_Text;
    [SerializeField] Button shoot_btn;

    // Create new
    Timer durationTimer;
    Timer repeaterTimer;
    Timer intervalTimer;

    void Start()
    {
        durationTimer = Timer.CreateNew(gameObject, "Duration Timer");
        durationTimer.Setup(1f);
        durationTimer.StartTimer();

        repeaterTimer = Timer.CreateNew(gameObject, "Repeater Timer");
        repeaterTimer.SetupRepeater(2f, true, 5);
        repeaterTimer.StartTimer();
        
        intervalTimer = Timer.CreateNew(gameObject, "Interval Timer");
        intervalTimer.SetupInterval(0f, 1f, false, 5);
        intervalTimer.StartTimer();
        

        reset_btn.onClick.AddListener(() =>
        {
            durationTimer.StartTimer();
            repeaterTimer.StartTimer();
            intervalTimer.StartTimer();

        });
        startstop_btn.onClick.AddListener(() =>
        {
            durationTimer.PauseUnpauseTimer();
            repeaterTimer.PauseUnpauseTimer();
            intervalTimer.PauseUnpauseTimer();
        });
        shoot_btn.onClick.AddListener(() =>
        {
            Debug.Log($"durationTimer Can Shoot: {durationTimer.TimerHit}");
            Debug.Log($"repeaterTimer Can Shoot: {repeaterTimer.TimerHit}");
            Debug.Log($"intervalTimer Can Shoot: {intervalTimer.TimerHit}");
        });
    }

    void Update()
    {
        if (durationTimer)
        {
            var running = durationTimer.IsRuning.ToString();
            var time = durationTimer.TotalTime;
            var runs = durationTimer.TotalRuns;
            durationRunning_Text.text = $"Running = {running}";
            durationTime_Text.text = $"Time = {time}";
            durationRuns_Text.text = $"Runs = {runs}";
        }

        if (repeaterTimer)
        {
            var running = repeaterTimer.IsRuning.ToString();
            var time = repeaterTimer.TotalTime;
            var runs = repeaterTimer.TotalRuns;
            repeaterRunning_Text.text = $"Running = {running}";
            repeaterTime_Text.text = $"Time = {time}";
            repeaterRuns_Text.text = $"Runs = {runs}";
        }

        if (intervalTimer)
        {
            var running = intervalTimer.IsRuning.ToString();
            var time = intervalTimer.TotalTime;
            var runs = intervalTimer.TotalRuns;
            intervalRunning_Text.text = $"Running = {running}";
            intervalTime_Text.text = $"Time = {time}";
            intervalRuns_Text.text = $"Runs = {runs}";
        }
    }
}