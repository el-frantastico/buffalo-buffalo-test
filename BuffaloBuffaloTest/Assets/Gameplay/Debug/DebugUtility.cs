using UnityEngine;

public static class DebugUtility
{
    public static string GetCharacterDebugString(BattleFormManager formManager)
    {
        string name = formManager.FormReferences.CharacterRoot.name;
        float currentHealth = formManager.FormReferences.HealthComponent.CurrentHealth;
        float maxHealth = formManager.FormReferences.HealthComponent.CurrentMaxHealth;
        BattleFormAsset form = formManager.GetCurrentBattleForm();

        string formName = form == null ? "None" : form.FormName;

        return string.Format(
            "Character: {0} | Health: {1}/{2} | Form: {3}",
            name.PadRight(25),
            currentHealth.ToString().PadLeft(3),
            maxHealth.ToString().PadRight(3),
            formName);
    }

    public static string[] GetCharacterDebugStrings(BattleFormManager[] formManagers)
    {
        string[] debugStrings = new string[formManagers.Length];

        for (int i = 0; i < formManagers.Length; ++i)
        {
            debugStrings[i] = GetCharacterDebugString(formManagers[i]);
        }

        return debugStrings;
    }
}