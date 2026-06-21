using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    private const string PLAYER_INPUT_BINDINGS = "PLAYER_INPUT_BINDINGS";
    public static GameInput Instance { get; private set; }

    public enum Binding {
        MOVE_UP,
        MOVE_DOWN,
        MOVE_LEFT,
        MOVE_RIGHT,
        INTERACT,
        SECONDARY_INTERACT,
        PAUSE,
    }

    public event EventHandler OnBindingUpdated;
    public event EventHandler OnInteractAction;
    public event EventHandler OnSecondaryInteractAction;
    public event EventHandler OnPausePressed;


    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();


        if (PlayerPrefs.HasKey(PLAYER_INPUT_BINDINGS)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_INPUT_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.SecondaryInteract.performed += SecondaryInteract_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.SecondaryInteract.performed -= SecondaryInteract_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPausePressed?.Invoke(this, EventArgs.Empty);
    }

    private void SecondaryInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSecondaryInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalised() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetKeyboardBinding(Binding binding) {
        switch (binding) {
            case Binding.MOVE_UP:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MOVE_DOWN:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MOVE_LEFT:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MOVE_RIGHT:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.INTERACT:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.SECONDARY_INTERACT:
                return playerInputActions.Player.SecondaryInteract.bindings[0].ToDisplayString();
            case Binding.PAUSE:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }

        throw new NotImplementedException();
    }

    public void RebindBinding(Binding binding) {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding) {
            default:
            case Binding.MOVE_UP:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MOVE_DOWN:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MOVE_LEFT:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MOVE_RIGHT:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.INTERACT:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.SECONDARY_INTERACT:
                inputAction = playerInputActions.Player.SecondaryInteract;
                bindingIndex = 0;
                break;
            case Binding.PAUSE:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                OnBindingUpdated?.Invoke(this, EventArgs.Empty);
                playerInputActions.Player.Enable();

                PlayerPrefs.SetString(PLAYER_INPUT_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();
    }

}
