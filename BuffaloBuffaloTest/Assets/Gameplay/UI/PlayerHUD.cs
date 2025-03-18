using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("External")]
    [SerializeField]
    private BattleFormManager _battleFormManager;

    private HealthComponent _healthComponent;

    [Header("User Interface")]
    [SerializeField]
    private Text _formText;

    [SerializeField]
    private Slider _healthBar;

    private void Start()
    {
        _battleFormManager.OnBattleFormChanged += OnBattleFormChanged;
        _healthComponent = _battleFormManager.FormReferences.HealthComponent;
        _healthComponent.OnCurrentHealthChanged += OnCurrentHealthChanged;
        _healthComponent.OnCurrentMaxHealthChanged += OnCurrentMaxHealthChanged;
    }

    private void OnCurrentHealthChanged(float oldHealth, float newHealth)
    {
        _healthBar.value = newHealth / _healthComponent.CurrentMaxHealth;
    }

    private void OnCurrentMaxHealthChanged(float oldMaxHealth, float newMaxHealth)
    {
        _healthBar.value = _healthComponent.CurrentHealth / newMaxHealth;
    }

    private void OnBattleFormChanged(BattleFormAsset oldForm, BattleFormAsset newForm)
    {
        _formText.text = newForm.FormName;
    }
}
