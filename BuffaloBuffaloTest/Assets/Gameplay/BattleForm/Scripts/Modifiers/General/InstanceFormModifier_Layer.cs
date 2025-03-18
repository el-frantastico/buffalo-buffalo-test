using UnityEngine;

[CreateAssetMenu(fileName = "Layer", menuName = "Battle Form/Modifier/Physics/Layer")]
public class InstantFormModifier_Layer : ScriptableObject, IInstantFormModifier
{
    [SerializeField]
    private string _layerName;

    [SerializeField]
    private LayerMask _includeLayers;

    [SerializeField]
    private LayerMask _excludeLayers;

    public bool Execute(BattleFormReferences references)
    {
        references.CharacterRoot.layer = LayerMask.NameToLayer(_layerName);
        references.CharacterController.includeLayers = _includeLayers;
        references.CharacterController.excludeLayers = _excludeLayers;
        return true;
    }
}
