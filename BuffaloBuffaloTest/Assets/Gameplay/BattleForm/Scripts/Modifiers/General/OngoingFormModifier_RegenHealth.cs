using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RegenHealth", menuName = "Battle Form/Modifier/Health/Regen Health")]
public class OngoingFormModifier_RegenHealth : ScriptableObject, IOngoingFormModifier
{
    [SerializeField]
    private float _regenAmount = 5.0f;

    [SerializeField]
    private float _regenPeriod = 5.0f;

    private Coroutine _regenCoroutine;
    private OngoingFormModifier_RegenHealth _modifierInstance;

    public IOngoingFormModifier Create(BattleFormReferences references)
    {
        _modifierInstance = Instantiate(this);

        BattleFormManager formManager = references.BattleFormManager;
        HealthComponent healthComponent = references.HealthComponent;

        IEnumerator regenCoroutine = Regen(healthComponent, _regenAmount, _regenPeriod);
        _modifierInstance._regenCoroutine = formManager.StartCoroutine(regenCoroutine);

        return _modifierInstance;
    }

    public bool Destroy(BattleFormReferences references)
    {
        references.BattleFormManager.StopCoroutine(_regenCoroutine);
        Destroy(this);
        return true;
    }

    private IEnumerator Regen(HealthComponent healthComponent, float regenAmount, float regenPeriod)
    {
        while (true)
        {
            yield return new WaitForSeconds(regenPeriod);
            healthComponent.Heal(regenAmount);
        }
    }
}