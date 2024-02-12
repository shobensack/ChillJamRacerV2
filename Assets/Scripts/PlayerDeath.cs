using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillRacer
{
    public class PlayerDeathEffect : MonoBehaviour
    {
        public GameObject deathEffect; // The effect to play when the player dies
        public Animator playerAnimator; // Reference to the player's Animator component
        public AudioClip deathAudioClip; // Audio clip for death effect

        private Health playerHealth;
        private CharacterController characterController;
        // Flag to control movement
        private bool canMove = true;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerHealth = GetComponent<Health>();
            if (playerHealth == null)
            {
                Debug.LogError("Health script not found on the player GameObject.");
            }
        }

        private void Update()
        {
            if (playerHealth != null && playerHealth.isDead)
            {
                if (canMove)
                {
                    // The player's health is zero or less, trigger the death effect
                    PlayDeathEffect();
                }
            }
        }

        public void PlayDeathEffect()
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            if (playerHealth != null)
            {
                playerHealth.isAlwaysInvincible = true; // Set the player as invincible

                Debug.Log("Playing Death Effect - Before animation");
                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("Dead", true); // Trigger the "Dead" animation state
                }
                Debug.Log("Playing Death Effect - After animation");

                if (deathAudioClip != null)
                {
                    AudioSource.PlayClipAtPoint(deathAudioClip, transform.position); // Play death audio at the player's position
                }
                canMove = false; // Disable movement
                StartCoroutine(RespawnAfterDelay());
            }
        }

        IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(2); // Wait for 2 seconds
            canMove = true; // Enable movement after respawn

            if (playerHealth != null)
            {
                playerHealth.isAlwaysInvincible = false; // Set the player back to normal vulnerability

                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("Dead", false); // Set the "Dead" animation state back to false
                }

                playerHealth.Respawn(); // Respawn the player
            }
        }
    }
}