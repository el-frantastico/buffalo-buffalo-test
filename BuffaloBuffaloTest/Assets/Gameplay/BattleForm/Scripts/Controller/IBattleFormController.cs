using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBattleFormController
{

    public void SubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action);
    public void UnsubscribePrimaryAbilityAction(Action<InputAction.CallbackContext> action);
}
