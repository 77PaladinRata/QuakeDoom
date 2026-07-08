using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private UnityEvent onDeath;
    [SerializeField]
    private UnityEvent onDamageTaken;
    [SerializeField] ////*ÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑÑ
    private UnityEvent<Transform> onHeal; ///* ÑÑÑÑÑÑÑÑ
    private float currentHealth; 
    public float CurrentHealth => currentHealth;///*PPP
    public float MaxHealth => maxHealth; ///*PPPP
    public bool IsDead => currentHealth <= 0f;
    public void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    } ///* Agregando para que se escuche el sonido
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        onHeal ?. Invoke(transform); ////*ÑÑÑÑÑÑÑÑÑÑÑÑ
    } ///* No olvidad Agregar Extremidades
    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            onDamageTaken ?.Invoke();
        }
    }
    public void Die()
    {
        onDeath ?. Invoke();
    }
}
