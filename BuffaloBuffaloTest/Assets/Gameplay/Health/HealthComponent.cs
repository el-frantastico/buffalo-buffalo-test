using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _startingHealth = 100;

    [SerializeField]
    private float _startingMaxHealth = 100;
    
    public float CurrentHealth { get; private set; }
    public float CurrentMaxHealth { get; private set; }

    public Action<float, float> OnCurrentHealthChanged;
    public Action<float, float> OnCurrentMaxHealthChanged;

    private void Awake()
    {
        CurrentHealth = _startingHealth;
        CurrentMaxHealth = _startingMaxHealth;
    }

    public void Damage(float damage)
    {
        if (damage > 0)
        {
            float oldHealth = CurrentHealth;
            float newHealth = CurrentHealth - damage;
            CurrentHealth = Mathf.Max(newHealth, 0);
            OnCurrentHealthChanged?.Invoke(oldHealth, CurrentHealth);
        }
    }

    public void Heal(float heal)
    {
        if (heal > 0)
        {
            float oldHealth = CurrentHealth;
            float newHealth = CurrentHealth + heal;
            CurrentHealth = Mathf.Min(newHealth, CurrentMaxHealth);
            OnCurrentHealthChanged?.Invoke(oldHealth, CurrentHealth);
        }
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        if (CurrentMaxHealth == newMaxHealth)
        {
            return;
        }

        float oldMaxHealth = CurrentMaxHealth;
        CurrentMaxHealth = newMaxHealth;
        OnCurrentMaxHealthChanged?.Invoke(oldMaxHealth, CurrentMaxHealth);

        if (CurrentHealth > CurrentMaxHealth)
        {
            CurrentHealth = CurrentMaxHealth;
        }
    }
    #region INSPECTOR DEBUG
#if UNITY_EDITOR
    [ContextMenu("Reset Health")]
    private void ResetHealth()
    {
        Heal(CurrentMaxHealth);
    }

#endif
    #endregion
}
