using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 0.1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking = false;

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        float reachLength = 1.0f;

        bool reachedSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, reachLength, countersLayerMask);

        if (reachedSomething) {
            if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter)) {
                clearCounter.Interact();
            }
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

}
