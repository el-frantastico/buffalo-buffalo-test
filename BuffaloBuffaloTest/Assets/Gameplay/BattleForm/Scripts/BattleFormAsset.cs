using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleForm", menuName = "Battle Form/Form")]
public class BattleFormAsset : ScriptableObject
{
    [SerializeField]
    private string _formName;
    public string FormName => _formName;

    [SerializeField]
    private List<Object> _instantFormModifiers = new List<Object>();

    [SerializeField]
    private List<Object> _ongoingFormModifiers = new List<Object>();

    private List<IOngoingFormModifier> _ongoingFormModifierInstances;

    public bool Activate(BattleFormReferences battleFormReferences)
    {
        bool areInstantModifiersSuccessful = true;
        foreach (IInstantFormModifier instantModifier in _instantFormModifiers)
        {
            areInstantModifiersSuccessful &= instantModifier.Execute(battleFormReferences);
        }

        _ongoingFormModifierInstances = new List<IOngoingFormModifier>(_ongoingFormModifiers.Count);

        bool areOngoingModifiersSuccessful = true;
        foreach (IOngoingFormModifier ongoingModifier in _ongoingFormModifiers)
        {
            IOngoingFormModifier ongoingModifierInstance = ongoingModifier.Create(battleFormReferences);
            _ongoingFormModifierInstances.Add(ongoingModifierInstance);
            areOngoingModifiersSuccessful &= ongoingModifierInstance != null;
        }

        return areInstantModifiersSuccessful && areOngoingModifiersSuccessful;
    }

    public bool Deactivate(BattleFormReferences battleFormReferences)
    {
        bool areOngoingModifiersSuccessful = true;
        foreach (IOngoingFormModifier ongoingModifierInstance in _ongoingFormModifierInstances)
        {
            if (ongoingModifierInstance != null)
            {
                areOngoingModifiersSuccessful &= ongoingModifierInstance.Destroy(battleFormReferences);
            }
        }

        _ongoingFormModifierInstances.Clear();
        return areOngoingModifiersSuccessful;
    }

    private void OnValidate()
    {
        for (int i = _instantFormModifiers.Count - 1; i >= 0; --i)
        {
            if (_instantFormModifiers[i] is not IInstantFormModifier)
            {
                string message = "Removing object [{0}] as it does not implement IInstantFormModifier";
                Debug.LogErrorFormat(message, _instantFormModifiers[i].name);
                _instantFormModifiers.RemoveAt(i);
            }
        }

        for (int i = _ongoingFormModifiers.Count - 1; i >= 0; --i)
        {
            if (_ongoingFormModifiers[i] != null && _ongoingFormModifiers[i] is not IOngoingFormModifier)
            {
                string message = "Removing object [{0}] as it does not implement IOngoingFormModifier";
                Debug.LogErrorFormat(message, _ongoingFormModifiers[i].name);
                _ongoingFormModifiers.RemoveAt(i);
            }
        }
    }
}