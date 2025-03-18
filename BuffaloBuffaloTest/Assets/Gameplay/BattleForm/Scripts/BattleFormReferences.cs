using System;
using UnityEngine;

[Serializable]
public struct BattleFormReferences
{
    [SerializeField]
    private BattleFormManager _battleFormManager;
    public BattleFormManager BattleFormManager => _battleFormManager;

    [Header("Character")]
    [SerializeField]
    private GameObject _characterRoot;
    public GameObject CharacterRoot => _characterRoot;

    [SerializeField]
    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    [SerializeField]
    private HealthComponent _healthComponent;
    public HealthComponent HealthComponent => _healthComponent;

    // TECH-DEBT: Create interface inspector drawer.
    public IBattleFormController FormController;

    [Header("View")]
    [SerializeField]
    private Transform _transform;
    public Transform Transform => _transform;

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    public SkinnedMeshRenderer SkinnedMeshRenderer => _skinnedMeshRenderer;
}
