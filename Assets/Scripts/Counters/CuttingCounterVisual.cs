using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCutPerformed += CuttingCounter_OnCutPerformed;
    }

    private void CuttingCounter_OnCutPerformed(object sender, System.EventArgs e) {
        Cut();
    }

    public void Cut() {
        animator.SetTrigger(CUT);
    }

}
