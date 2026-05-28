using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private enum Mode {
        NORMAL,
        INVERTED,
        CAMERA_FORWARD,
        CAMERA_FORWARD_INVERTED,
    }

    [SerializeField] Mode mode;

    private void LateUpdate() {
        switch (mode) {
            case Mode.NORMAL:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.INVERTED:
                Vector3 inverseDirection = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + inverseDirection);
                break;
            case Mode.CAMERA_FORWARD:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CAMERA_FORWARD_INVERTED:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
