using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameState> StateHistory = new List<GameState>();
    public enum GameState{
        title,
        overworld,
        combat,
        paused,
        cutscene
    }

    public GameState CurrentGameState;
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(this);

        instance = this;

        StateHistory.Add(CurrentGameState);
    }


    public void SwitchState(GameState newState)
    {
        CurrentGameState = newState;
        StateHistory.Add(CurrentGameState);
    }
    public void SwitchToLastState()
    {
        if (StateHistory.Count > 1)
            SwitchState(StateHistory[StateHistory.Count - 2]);

    }
}
