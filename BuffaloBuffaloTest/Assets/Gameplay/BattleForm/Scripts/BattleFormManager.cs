using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleFormManager : MonoBehaviour
{
    #region INSPECTOR 
    [SerializeField]
    private BattleFormAsset _resetBattleForm = null;

    [SerializeField]
    private BattleFormAsset[] _battleFormTemplates;

    [SerializeField]
    private BattleFormReferences _formReferences;
    #endregion

    public BattleFormReferences FormReferences => _formReferences;
    public Action<BattleFormAsset, BattleFormAsset> OnBattleFormChanged;

    private int currentFormIndex = -1;
    private List<BattleFormAsset> _battleFormInstances;

    private void Awake()
    {
        _battleFormInstances = new List<BattleFormAsset>(_battleFormTemplates.Length);
        foreach (BattleFormAsset assetTemplate in _battleFormTemplates)
        {
            BattleFormAsset assetInstance = Instantiate(assetTemplate);
            _battleFormInstances.Add(assetInstance);
        }
    }

    public void RegisterFormController(IBattleFormController controller)
    {
        _formReferences.FormController = controller;
    }


    #region BATTLE FORM ACCESS
    public BattleFormAsset GetCurrentBattleForm()
    {
        if (currentFormIndex >= 0 && currentFormIndex < _battleFormInstances.Count)
        {
            return _battleFormInstances[currentFormIndex];
        }

        return null;
    }

    public BattleFormAsset[] GetAllBattleForms()
    {
        return _battleFormInstances.ToArray();
    }
    #endregion

    #region SWITCH BATTLE FORM
    public bool TryActivateNextForm()
    {
        if (_battleFormInstances.Count == 0)
        {
            string message = "GameObject {0} does not have any BattleForms set.";
            Debug.LogWarningFormat(message, gameObject.name);
            return false;
        }

        int nextFormIndex = currentFormIndex >= _battleFormInstances.Count
            ? 0
            : currentFormIndex + 1;
            
        return TryActivateFormAtIndex(nextFormIndex);
    }

    public bool TryActivateFormAtIndex(int formIndex)
    {
        if (formIndex < 0 || formIndex >= _battleFormInstances.Count)
        {
            return false;
        }

        BattleFormAsset oldBattleForm = GetCurrentBattleForm();
        oldBattleForm?.Deactivate(_formReferences);

        bool isResetSuccessful = _resetBattleForm.Activate(_formReferences);
        if (isResetSuccessful)
        {
            BattleFormAsset newBattleForm = _battleFormInstances[formIndex];
            if (newBattleForm.Activate(_formReferences))
            {
                currentFormIndex = formIndex;
                OnBattleFormChanged?.Invoke(oldBattleForm, newBattleForm);
                return true;
            }
            else
            {
                TryActivateFormAtIndex(0);
                return false;
            }

        }
        else
        {
            return false;
        }
    }
    #endregion
}