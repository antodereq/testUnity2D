using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Gracz otrzyma³ obra¿enia! HP: " + currentHealth);

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Gracz zgin¹³!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
