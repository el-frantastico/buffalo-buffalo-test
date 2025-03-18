using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent, RequireComponent(typeof(BattleFormManager))]
public class BattleFormDummyController : MonoBehaviour, IBattleFormController
{
    [SerializeField]
    private int _startingFormIndex = 0;

    [SerializeField]
    private float _primaryAbilityActivatePeriod = 5;

    private BattleFormManager _battleFormManager;
    private Action<InputAction.CallbackContext> _abilityAction;

    void Awake()
    {
        _battleFormManager = GetComponent<BattleFormManager>();
        _battleFormManager.RegisterFormController(this);
    }

    void Start()
    {
        _battleFormManager.TryActivateFormAtIndex(_startingFormIndex);
        IEnumerator coroutine = RepeatPrimaryAbilityActivation();
        StartCoroutine(coroutine);
    }

    IEnumerator RepeatPrimaryAbilityActivation()
    {
        while (true)
        {
            yield return new WaitForSeconds(_primaryAbilityActivatePeriod);
            _abilityAction?.Invoke(new InputAction.CallbackContext());
        }
    }

    public void SubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action)
    {
        _abilityAction += action;

    }

    public void UnsubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action)
    {
        _abilityAction -= action;
    }
}
