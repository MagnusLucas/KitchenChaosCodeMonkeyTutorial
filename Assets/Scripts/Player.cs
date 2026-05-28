using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public KitchenCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 0.1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHookPoint;

    private bool isWalking = false;
    private KitchenCounter selectedCounter;
    private KitchenObject heldKitchenObject;

    public void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one player instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnSecondaryInteractAction += GameInput_OnSecondaryInteractAction;
    }

    private void GameInput_OnSecondaryInteractAction(object sender, EventArgs e) {
        if (selectedCounter is CuttingCounter) {
            (selectedCounter as CuttingCounter).SecondaryInteract(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        selectedCounter?.Interact(this);
    }

    private void Update() {
        HandleMovement();
        DetectInteractibles();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void DetectInteractibles() {
        float reachLength = 1.0f;

        bool reachedSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, reachLength, countersLayerMask);

        if (reachedSomething) {
            if (raycastHit.transform.TryGetComponent<KitchenCounter>(out KitchenCounter kitchenCounter)) {
                if (kitchenCounter != selectedCounter) {
                    SetSelectedCounter(kitchenCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalised();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        Vector3 playerTopPoint = transform.position + playerHeight * Vector3.up;

        bool canMove = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, moveDir, moveDistance);

        if (canMove) {
            transform.position += moveDir * moveDistance;
        } else {
            // I don't normalise the move vectors here, I want the player to go slower when walking into the wall
            var canMoveXDir = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, moveDir.ProjectOntoPlane(Vector3.forward), moveDistance);
            if (canMoveXDir) {
                transform.position += new Vector3(moveDir.x, 0, 0) * moveDistance;
            }
            var canMoveZDir = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, moveDir.ProjectOntoPlane(Vector3.right), moveDistance);
            if (canMoveZDir) {
                transform.position += new Vector3(0, 0, moveDir.z) * moveDistance;
            }
        }
    }

    private void SetSelectedCounter(KitchenCounter newSelectedCounter) {
        selectedCounter = newSelectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectHookPoint() {
        return kitchenObjectHookPoint;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject) {
        heldKitchenObject = newKitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return heldKitchenObject;
    }

    public void ClearKitchenObject() {
        heldKitchenObject = null;
    }

    public bool HasKitchenObject() {
        return (heldKitchenObject != null);
    }
}
