using UnityEngine;

[CreateAssetMenu(fileName = "OverrideMaxHealth", menuName = "Battle Form/Modifier/Health/Override Max Health")]
public class InstantFormModifier_MaxHealth : ScriptableObject, IInstantFormModifier
{
    [SerializeField]
    private float _maxHealth = 100.0f;
    public bool Execute(BattleFormReferences references)
    {
        references.HealthComponent.SetMaxHealth(_maxHealth);
        return true;
    }
}
