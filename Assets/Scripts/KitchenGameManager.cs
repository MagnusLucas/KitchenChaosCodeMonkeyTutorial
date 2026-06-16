using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
    
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    enum State {
        WAITING_TO_START,
        COUNTDOWN_TO_START,
        GAME_PLAYING,
        GAME_OVER,
    }

    private const float WAIT_TIME = 1.0f;
    private const float COUNTDOWN_TIME = 3.0f;
    private const float GAMEPLAY_TIME = 3.0f;


    private State state;
    private float stateTimer;

    private void Awake() {
        Instance = this;
        state = State.WAITING_TO_START;
    }

    private void Update() {
        switch(state) {
            case State.WAITING_TO_START:
                stateTimer += Time.deltaTime;
                if (stateTimer > WAIT_TIME) {
                    stateTimer = 0;
                    ChangeState(State.COUNTDOWN_TO_START);
                }
                break;
            case State.COUNTDOWN_TO_START:
                stateTimer += Time.deltaTime;
                if (stateTimer > COUNTDOWN_TIME) {
                    stateTimer = 0;
                    ChangeState(State.GAME_PLAYING);
                }
                break;
            case State.GAME_PLAYING:
                stateTimer += Time.deltaTime;
                if (stateTimer > GAMEPLAY_TIME) {
                    stateTimer = 0;
                    ChangeState(State.GAME_OVER);
                }
                break;
        }
    }

    private void ChangeState(State newState) {
        state = newState;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePlaying() {
        return state == State.GAME_PLAYING;
    }

    public bool IsCountdownToStartActive() {
        return state == State.COUNTDOWN_TO_START;
    }

    public bool IsGameOver() {
        return state == State.GAME_OVER;
    }

    public float GetTimeToStart() {
        return COUNTDOWN_TIME - stateTimer;
    }

    public float GetGameProgress() {
        if (state != State.GAME_PLAYING) return 0;
        return stateTimer / GAMEPLAY_TIME;
    }

}
