using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private InputActionReference _debugInputActionReference;

    [SerializeField]
    private Font _debugTextFont;

    [Header("Player Character")]
    [SerializeField]
    private BattleFormManager _playerFormManager;

    [Header("Other Characters")]
    [SerializeField, Tooltip("WARNING: Finding all other characters is non-performant. Use only in scenes with few characters.")]
    private bool _searchAllCharactersInScene = true;

    private string _playerDebugString = "";
    private string[] _otherDebugStrings = { };

    private GUIStyle _boldStyle;

    private void Awake()
    {
        _debugInputActionReference.action.performed += OnDebugToggle;

        _boldStyle = new GUIStyle();
        _boldStyle.fontStyle = FontStyle.Bold;
    }

    private void OnEnable()
    {
        _playerDebugString = DebugUtility.GetCharacterDebugString(_playerFormManager);
        BindCharacterDebugActions(_playerFormManager);

        if (_searchAllCharactersInScene)
        {
            BattleFormManager[] otherFormManagers = FindObjectsByType<BattleFormManager>(FindObjectsSortMode.None);
            UpdateDebugStrings(_playerFormManager, otherFormManagers);

            foreach (BattleFormManager formManager in otherFormManagers)
            {
                if (_playerFormManager != formManager)
                {
                    BindCharacterDebugActions(formManager);
                }
            }
        }
    }

    private void OnDisable()
    {
        UnbindCharacterDebugActions(_playerFormManager);
        if (_searchAllCharactersInScene)
        {
            BattleFormManager[] otherFormManagers = FindObjectsByType<BattleFormManager>(FindObjectsSortMode.None);

            foreach (BattleFormManager formManager in otherFormManagers)
            {
                UnbindCharacterDebugActions(formManager);
            }
        }
    }

    void OnGUI()
    {
        if (isActiveAndEnabled)
        {
            GUI.skin.font = _debugTextFont;

            GUILayout.BeginArea(new Rect(Screen.width - 800, 0, 800, Screen.height));
            GUILayout.Label(_playerDebugString + "\n" + string.Join("\n", _otherDebugStrings));
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(0, 0, 500, Screen.height));
            GUILayout.Label("Available Forms - Press Keys 1-5 to Switch");

            int index = 1;
            foreach (BattleFormAsset formAsset in _playerFormManager.GetAllBattleForms())
            {
                string availableFormString = index + ": " + formAsset.FormName;
                if (formAsset == _playerFormManager.GetCurrentBattleForm())
                {
                    GUILayout.Label(availableFormString, _boldStyle);
                }
                else
                {
                    GUILayout.Label(availableFormString);
                }

                index++;
            }
            GUILayout.EndArea();
        }
    }

    private void UpdateDebugStrings(BattleFormManager playerFormManager, BattleFormManager[] otherFormManagers = null)
    {
        _playerDebugString = DebugUtility.GetCharacterDebugString(playerFormManager);

        if (otherFormManagers != null)
        {
            _otherDebugStrings = new string[otherFormManagers.Length];

            int index = 0;
            foreach (BattleFormManager otherFormManager in otherFormManagers)
            {
                if (playerFormManager != otherFormManager)
                {
                    _otherDebugStrings[index] = DebugUtility.GetCharacterDebugString(otherFormManager);
                    index++;
                }
            }
        }
    }

    private void BindCharacterDebugActions(BattleFormManager formManager)
    {
        formManager.FormReferences.HealthComponent.OnCurrentHealthChanged += OnHealthChangedAction;
        formManager.FormReferences.HealthComponent.OnCurrentMaxHealthChanged += OnHealthChangedAction;

        formManager.OnBattleFormChanged += OnFormChangedAction;
    }

    private void UnbindCharacterDebugActions(BattleFormManager formManager)
    {
        formManager.FormReferences.HealthComponent.OnCurrentHealthChanged -= OnHealthChangedAction;
        formManager.FormReferences.HealthComponent.OnCurrentMaxHealthChanged -= OnHealthChangedAction;

        formManager.OnBattleFormChanged -= OnFormChangedAction;
    }

    private void OnDebugToggle(InputAction.CallbackContext context)
    {
        enabled = !enabled;
    }

    private void OnHealthChangedAction(float oldHealth, float newHealth)
    {
        BattleFormManager[] otherFormManagers = null;
        if (_searchAllCharactersInScene)
        {
            otherFormManagers = FindObjectsByType<BattleFormManager>(FindObjectsSortMode.None);
        }

        UpdateDebugStrings(_playerFormManager, otherFormManagers);
    }

    private void OnFormChangedAction(BattleFormAsset oldForm, BattleFormAsset newForm)
    {
        BattleFormManager[] otherFormManagers = null;
        if (_searchAllCharactersInScene)
        {
            otherFormManagers = FindObjectsByType<BattleFormManager>(FindObjectsSortMode.None);
        }

        UpdateDebugStrings(_playerFormManager, otherFormManagers);
    }

#endif
}
