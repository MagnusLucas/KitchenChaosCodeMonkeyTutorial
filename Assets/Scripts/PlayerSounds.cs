using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    [SerializeField] private AudioClip[] footstepSounds;

    [SerializeField] private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;


    private void Update() {
        footstepTimer += Time.deltaTime;
        if (footstepTimer > footstepTimerMax) {
            footstepTimer = 0f;

            if (player.IsWalking()) {
                AudioClip randomClip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                AudioSource.PlayClipAtPoint(randomClip, player.transform.position);
            }

        }
    }

}
