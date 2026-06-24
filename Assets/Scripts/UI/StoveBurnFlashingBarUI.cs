using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour {

    private const float FLASH_THRESHOLD = 0.6f;
    private const string FLASH_BOOL = "Flash";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProgressUpdated += StoveCounter_OnProgressUpdated;
        stoveCounter.OnKitchenObjectRemoved += (_, _) => animator.SetBool(FLASH_BOOL, false);
        stoveCounter.OnFullyFried += (_, _) => animator.SetBool(FLASH_BOOL, false);
    }

    private void StoveCounter_OnProgressUpdated(object sender, ProgressEventArgs e) {
        if (stoveCounter.IsBurningObject() && e.progress > FLASH_THRESHOLD) {
            animator.SetBool(FLASH_BOOL, true);
        }
    }


}
