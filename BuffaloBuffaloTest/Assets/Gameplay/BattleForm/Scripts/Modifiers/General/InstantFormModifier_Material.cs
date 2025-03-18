using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialModifier", menuName = "Battle Form/Modifier/Material")]
public class InstantFormModifier_Material : ScriptableObject, IInstantFormModifier
{
    [SerializeField]
    private List<Material> _materials;

    public bool Execute(BattleFormReferences references)
    {
        SkinnedMeshRenderer skinnedMeshRenderer = references.SkinnedMeshRenderer;
        if (skinnedMeshRenderer == null)
        {
            return false;
        }

        if (skinnedMeshRenderer.materials.Length != _materials.Count)
        {
            return false;
        }

        skinnedMeshRenderer.SetMaterials(_materials);

        return true;
    }
}
