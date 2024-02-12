using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ChillRacer
{
    /// <summary>
    /// This class handles the health state of a game object.
    /// 
    /// Implementation Notes: 2D Rigidbodies must be set to never sleep for this to interact with trigger stay damage
    /// </summary>
    public class Health : MonoBehaviour
    {
        public GameObject deathEffectPrefab; // Reference to the PlayerDeathEffect script

        [Header("Team Settings")]
        [Tooltip("The team associated with this damage")]
        public int teamId = 0;

        [Header("Health Settings")]
        [Tooltip("The maximum health value")]
        public int maxHealth = 100; // Maximum health for the player
        [Tooltip("The current in game health value")]
        public int currentHealth = 100;// Current health for the player
        [Tooltip("Invulnerability duration, in seconds, after taking damage")]
        public float invincibilityTime = 3f;
        public bool isAlwaysInvincible = false;

        [Header("Lives settings")]
        [Tooltip("Whether or not to use lives")]
        public bool useLives = false;
        [Tooltip("Current number of lives this health has")]
        public int currentLives; // Current number of lives for the player
        [Tooltip("The maximum number of lives this health has")]
        public int maxLives = 3; // Maximum number of lives for the player

        public bool isDead = false;
        private Vector3 initialPosition; // Initial position of the player
        private Animator animator; // Reference to the Animator component

        private void Start()
        {
            // Get the PlayerDeathEffect script attached to the same GameObject
            PlayerDeathEffect playerDeathEffect = GetComponent<PlayerDeathEffect>();
            if (playerDeathEffect != null)
            {
                deathEffectPrefab = playerDeathEffect.deathEffect; // Assuming 'deathEffect' is the public variable in PlayerDeathEffect
            }

            currentLives = maxLives;
            currentHealth = maxHealth;
            initialPosition = transform.position; // Store the initial position
            animator = GetComponent<Animator>();
        }


        public void TakeDamage(int damage)
        {
            if (isAlwaysInvincible)
                return;

            if (!isDead)
            {
                Debug.Log("Player Took Damage");
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    Debug.Log("Player Died");
                    Die();
                }
            }
        }

        void Die()
        {
            isDead = true;
            Debug.Log("Die method called");

            if (animator != null)
            {
                animator.SetBool("Dead", true);
                Debug.Log("Death animation triggered");
            }
            // Trigger the death effect here from the PlayerDeathEffect script
            if (deathEffectPrefab != null)
            {
                // Instantiate the death effect prefab at the enemy's position
                Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            }
            if (useLives)
            {
                if (currentLives > 0)
                {
                    currentLives--;
                    StartCoroutine(RespawnAfterDelay());
                    return;
                }
                else
                {
                    Debug.Log("Game Over");
                    // Enable the GameOverManager GameObject if it's disabled
                    FindObjectOfType<GameMaster>().gameObject.SetActive(true);
                }
            }
            Respawn();
        }
        IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(2); // Wait for 2 seconds

            Respawn(); // Respawn the player after the delay
        }

        public void Respawn()
        {
            isDead = false;
            currentHealth = maxHealth;

            // Additional logic for resetting other parameters if needed

            // Trigger the respawn animation if an Animator is available
            if (animator != null)
            {
                animator.SetBool("Dead", false); // Assuming the parameter in the Animator controller is named "Dead"
                Debug.Log("Respawn animation triggered");
            }

            // Set the player's position to the initial position only if not already dead
            if (!isDead)
            {
                transform.position = initialPosition; // Reset to initial position
            }
            Debug.Log("Player Respawned");
        }
    }
}