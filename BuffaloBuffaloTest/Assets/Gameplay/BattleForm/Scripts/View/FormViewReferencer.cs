using UnityEngine;

public class FormViewReferencer : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;
    public Transform Transform => _transform;

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    public SkinnedMeshRenderer SkinnedMeshRenderer => _skinnedMeshRenderer;
}
