using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vida
{ 
    public class VidaJugador : MonoBehaviour {

        public Animator anim;
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int currentHealth;                                   // The current health the player has.
        public int startingLifes = 5;
        public int currentLifes;
        bool isDead;                                                // Whether the player is dead.
        public Text uiVida;
        float timer;
        float timeToRespawn = 15f;

        void Awake()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;
            currentLifes = startingLifes;
            uiVida.text = ("Vida: " + currentHealth);

        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (isDead && timer >= timeToRespawn)
            {
                Respawn();
            }
        }

        public void TakeDamage(int amount)
        {
            // Set the damaged flag so the screen will flash.

            // Reduce the current health by the damage amount.
            currentHealth -= amount;
            uiVida.text = ("Vida: " + currentHealth);
            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        public void TakeHeal(int amount)
        {
            currentHealth += amount;
            uiVida.text = ("Vida: " + currentHealth);

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        void Death()
        {
            // Set the death flag so this function won't be called again.
            uiVida.text = ("Vida: " + "0");
            isDead = true;
            currentLifes = currentLifes - 1;
            anim.SetTrigger("FadeOut");
        }

        void Respawn()
        {
            if (isDead && currentLifes >= 0)
            {
                currentHealth = 100;
                Debug.Log("Moriste" + currentLifes);
                anim.SetTrigger("FadeIn");
                uiVida.text = ("Vida: " + currentHealth);
                isDead = false;
            }
        }
    }
}
