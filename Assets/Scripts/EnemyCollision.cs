using UnityEngine;

namespace ChillRacer
{
    public class FastEnemyAttack : MonoBehaviour
    {
        public int damage = 10; // Adjust the damage output for this specific enemy
        public GameObject deathEffectPrefab; // Assign the EnemyDeathEffect_Fast prefab in the Inspector

        public int Damage { get => damage; set => damage = value; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the colliding object is the player
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                // Deal damage to the player using the Health script
                playerHealth.TakeDamage(Damage);

                // Destroy the enemy GameObject
                Destroy(gameObject);

                // Trigger the EnemyDeathEffect_Fast
                TriggerDeathEffect();
            }
        }

        private void TriggerDeathEffect()
        {
            if (deathEffectPrefab != null)
            {
                // Instantiate the death effect prefab at the enemy's position
                Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            }
        }
    }
}
