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
 
    public float counter;
    public float year;
    public float deltaTimePerYear;

    public void Init() {
        currentYear = startingYear;
        deltaTimePerYear = (maxYear - startingYear) / timeInSecondsToReachMaxYear;

        year = startingYear;
    }

    public void Update() {
        if (IsMaxYearReached())
            OnMaxYearReached();

        counter += deltaTimePerYear / Time.deltaTime;
        year += counter;
    }

    public bool IsMaxYearReached() {
        return currentYear >= maxYear;
    }
}
