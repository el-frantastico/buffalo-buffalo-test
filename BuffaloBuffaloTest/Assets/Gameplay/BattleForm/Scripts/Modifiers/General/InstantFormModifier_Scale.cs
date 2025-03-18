using UnityEngine;

[CreateAssetMenu(fileName = "ScaleModifier", menuName = "Battle Form/Modifier/Scale")]
public class InstantFormModifier_Scale : ScriptableObject, IInstantFormModifier
{
    [SerializeField]
    private Vector3 _scale = Vector3.one;

    public Vector3 Scale => _scale;
    public bool Execute(BattleFormReferences references)
    {
        Transform transform = references.Transform;
        if (transform == null)
        {
            return false;
        }

        transform.localScale = _scale;
        return true;
    }
}