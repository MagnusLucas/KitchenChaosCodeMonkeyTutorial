using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnKitchenObjectSpawned += ContainerCounter_OnKitchenObjectSpawned;
    }

    private void ContainerCounter_OnKitchenObjectSpawned(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
