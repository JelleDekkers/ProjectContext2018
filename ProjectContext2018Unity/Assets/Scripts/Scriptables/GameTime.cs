using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTime", menuName = "Scriptables/GameTime", order = 6)]
public class GameTime : ScriptableObjectSingleton<GameTime> {

    public static Action<int> OnYearChanged;
    public static Action OnMaxYearReached;

    [SerializeField] private int startingYear;
    public int StartingYear { get { return startingYear; } }

    [SerializeField] private int maxYear;
    public int MaxYear { get { return maxYear; } }

    [SerializeField] private int currentYear;
    public int CurrentYear { get { return currentYear; } }

    [SerializeField] private float timeInSecondsToReachMaxYear;

    public float Counter { get; private set; }
    public float TimePerYear { get; private set; }

    public void Init() {
        currentYear = startingYear;
        TimePerYear = timeInSecondsToReachMaxYear / (maxYear - startingYear);
        Counter = 0;
    }

    public void Update() {
        if (IsMaxYearReached())
            OnMaxYearReached();

        Counter += Time.deltaTime;
        currentYear = (int)(Counter / TimePerYear) + startingYear;
    }

    public bool IsMaxYearReached() {
        return currentYear >= maxYear;
    }
}
