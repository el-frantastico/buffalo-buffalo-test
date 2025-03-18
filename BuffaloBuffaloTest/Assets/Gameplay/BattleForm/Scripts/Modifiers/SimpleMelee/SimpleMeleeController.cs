using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct SimpleMeleeData
{
    [SerializeField]
    private float _damage;
    public float Damage => _damage;

    [Header("Hit Detection")]
    [SerializeField]
    private Vector3 _meleeColliderOffset;
    public Vector3 MeleeColliderOffset => _meleeColliderOffset;

    [SerializeField]
    private float _meleeColliderRadius;
    public float MeleeColliderRadius => _meleeColliderRadius;

    [SerializeField]
    private LayerMask _collisionLayerMask;
    public LayerMask CollisionLayerMask => _collisionLayerMask;


    [Header("Effect")]
    [SerializeField]
    private ParticleSystem _meleeEffect;
    public ParticleSystem MeleeEffect => _meleeEffect;

    [SerializeField]
    private Vector3 _meleeEffectOffset;
    public Vector3 MeleeEffectOffset => _meleeEffectOffset;
}

public class SimpleMeleeController : MonoBehaviour
{
    private SimpleMeleeData _data;

    private Transform _viewTransform;
    private ParticleSystem _meleeEffectInstance;
    private IBattleFormController _formController;
    private HealthComponent _healthComponent;

    public void Initialize(SimpleMeleeData data, BattleFormReferences references)
    {
        _data = data;

        _healthComponent = references.HealthComponent;
        _formController = references.FormController;
        if (_formController != null)
        {
            _formController.SubscribePrimaryAbilityAction(OnAttackInputTriggered);
        }

        _viewTransform = references.Transform;
        if (_viewTransform != null)
        {
            _meleeEffectInstance = Instantiate(_data.MeleeEffect, _viewTransform);
            _meleeEffectInstance.transform.localPosition = _data.MeleeEffectOffset;
        }
    }

    void OnDestroy()
    {
        _formController.UnsubscribePrimaryAbilityAction(OnAttackInputTriggered);
        Destroy(_meleeEffectInstance);
    }

    private void OnAttackInputTriggered(InputAction.CallbackContext context)
    {
        _meleeEffectInstance.Play();

        Vector3 overlapCenter = GetColliderSphereCenter();
        Collider[] overlappedColliders = Physics.OverlapSphere(overlapCenter, _data.MeleeColliderRadius, _data.CollisionLayerMask);

        foreach (Collider collider in overlappedColliders)
        {
            HealthComponent healthComponent = collider.gameObject.GetComponentInChildren<HealthComponent>();
            if (healthComponent != _healthComponent)
            {
                healthComponent?.Damage(_data.Damage);
            }
        }
    }

    private Vector3 GetColliderSphereCenter()
    {
        Vector3 offsetPosition = _viewTransform.TransformVector(_data.MeleeColliderOffset);
        return _viewTransform.position + offsetPosition;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.5f;
        Gizmos.color = gizmoColor;

        Gizmos.DrawSphere(GetColliderSphereCenter(), _data.MeleeColliderRadius);
    }
#endif
}
