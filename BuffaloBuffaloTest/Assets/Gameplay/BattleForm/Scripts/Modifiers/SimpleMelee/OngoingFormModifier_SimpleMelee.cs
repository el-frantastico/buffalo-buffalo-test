using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SimpleMelee", menuName = "Battle Form/Modifier/Component/Simple Melee")]
public class OngoingFormModifier_SimpleMelee: ScriptableObject, IOngoingFormModifier
{
    [SerializeField]
    private SimpleMeleeData _meleeData;

    private SimpleMeleeController _componentInstance;

    private OngoingFormModifier_SimpleMelee _modifierInstance;

    public IOngoingFormModifier Create(BattleFormReferences references)
    {
        _modifierInstance = Instantiate(this);

        BattleFormManager formManager = references.BattleFormManager;
        _modifierInstance._componentInstance = formManager.gameObject.AddComponent<SimpleMeleeController>();
        _modifierInstance._componentInstance.Initialize(_meleeData, references);

        return _modifierInstance;
    }

    public bool Destroy(BattleFormReferences references)
    {
        if (_componentInstance != null)
        {
            Destroy(_componentInstance);
        }

        Destroy(this);
        return true;
    }
}