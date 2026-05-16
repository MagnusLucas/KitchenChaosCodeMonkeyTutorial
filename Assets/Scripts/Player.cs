using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 0.1f;

    private bool isWalking = false;

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1f;
        }
        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * moveSpeed;

        isWalking = moveDir != Vector3.zero;

        if (moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
            transform.position += moveDir;
        }
    }

    public bool IsWalking() {
        return isWalking;
    }

}
