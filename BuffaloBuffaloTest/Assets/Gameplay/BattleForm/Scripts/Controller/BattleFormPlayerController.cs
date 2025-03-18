using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent, RequireComponent(typeof(BattleFormManager))]
public class BattleFormPlayerController : MonoBehaviour, IBattleFormController
{
    private void Awake()
    {
        _battleFormManager = GetComponent<BattleFormManager>();
        _battleFormManager.RegisterFormController(this);
    }

    void Start()
    {
        if (_switchFormActionReference != null)
        {
            _switchFormActionReference.action.performed += OnSwitchFormTriggered;
        }
        else
        {
            string message = string.Format("Switch Form action reference not set.");
            Debug.LogWarning(message);
        }

        if (_primaryAbilityActionReference != null)
        {
            _primaryAbilityActionReference.action.performed += OnPrimaryAbilityTriggered;
        }
        else
        {
            string message = string.Format("Switch Form action reference not set.");
            Debug.LogWarning(message);
        }

        _battleFormManager.TryActivateNextForm();
    }

    #region FORM SWITCHING
    [Header("Form Switch")]
    [SerializeField]
    private InputActionReference _switchFormActionReference = null;

    private BattleFormManager _battleFormManager = null;

    void OnSwitchFormTriggered(InputAction.CallbackContext context)
    {
        int requestedFormIndex = (int)context.ReadValue<float>();
        _battleFormManager.TryActivateFormAtIndex(requestedFormIndex - 1);
    }
    #endregion

    #region ABILITY ACTIVATION
    [Header("Ability Activation")]
    [SerializeField]
    private InputActionReference _primaryAbilityActionReference = null;

    private Action<InputAction.CallbackContext> _abilityAction;

    void OnPrimaryAbilityTriggered(InputAction.CallbackContext context)
    {
        _abilityAction?.Invoke(context);
    }

    public void SubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action)
    {
        _abilityAction += action;
    }

    public void UnsubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action)
    {
        _abilityAction -= action;
    }
    #endregion
}
