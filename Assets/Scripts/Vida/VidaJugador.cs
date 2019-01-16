using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vida
{ 
    public class VidaJugador : MonoBehaviour {

        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int currentHealth;                                   // The current health the player has.

        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.
        public Text uiVida;

        void Awake()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;
            uiVida.text = ("Vida: " + currentHealth);

        }



        public void TakeDamage(int amount)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

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
            damaged = false;

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
            isDead = true;
        }
    }
}
