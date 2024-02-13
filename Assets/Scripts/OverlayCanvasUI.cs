using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line adds the necessary reference for the Slider class

namespace ChillRacer
{
    public class UIManager : MonoBehaviour
    {
        [Header("Pause Settings")]
        [Tooltip("The index of the pause page in the pages list")]
        public int pausePageIndex = 1;
        [Tooltip("Whether or not to allow pausing")]
        public bool allowPause = true;
        [Header("Polish Effects")]
        [Tooltip("The effect to create when navigating between UI")]
        public GameObject navigationEffect;
        [Tooltip("The effect to create when clicking on or pressing a UI element")]
        public GameObject clickEffect;
        [Tooltip("The effect to create when the player is backing out of a Menu page")]
        public GameObject backEffect;
        public Health playerHealth; // Reference to the Health scrip

        // Whether the application is paused
        public static bool isPaused = false;
        public GameObject pauseMenu;

        void Start()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<Health>();
            }
            else
            {
                Debug.LogError("Player object not found or inactive!");
            }
        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        /// <summary>
        /// Description:
        /// Creates a back effect if one is set
        /// Input:
        /// none
        /// Return:
        /// void (no return)
        /// </summary>
        public void CreateBackEffect()
        {
            if (backEffect)
            {
                Instantiate(backEffect, transform.position, Quaternion.identity, null);
            }
        }

        /// <summary>
        /// Description:
        /// Creates a click effect if one is set
        /// Input:
        /// none
        /// Return:
        /// void (no return)
        /// </summary>
        public void CreateClickEffect()
        {
            if (clickEffect)
            {
                Instantiate(clickEffect, transform.position, Quaternion.identity, null);
            }
        }

        /// <summary>
        /// Description:
        /// Creates a navigation effect if one is set
        /// Input:
        /// none
        /// Return:
        /// void (no return)
        /// </summary>
        public void CreateNavigationEffect()
        {
            if (navigationEffect)
            {
                Instantiate(navigationEffect, transform.position, Quaternion.identity, null);
            }
        }
    }
}