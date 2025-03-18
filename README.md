# buffalo-buffalo-test
Tech test submission for Buffalo Buffalo by Francisco Martin.

Video demo link here: https://youtu.be/CZCUarl9-zw

## Character States (a.k.a Battle Forms)
This submission makes creates a highly modular fighting state system called "Battle Forms". Each form is ScriptableObject `BattleFormAsset` defined by `IInstantFormModifier` and `IOngoingFormModifier`. Modifiers are meant to be shared among forms, as to allow reduced asset count and universal behaviors accross forms. 

Instant modifiers are stateless overrides of character attributes. This can include size, health, physics layers and more. Currently, all instant modifiers are overrides. Future work would look into allowing modifiers to stack, as to add or multiply the attibutes. As it is, we reset the player to a default state when switching forms, as to keep a base attributes.

Ongoing modifiers are modifications that need to be instanced and destroyed on switching states. They include continuous effects (e.g health regen) and input-binded abilities (e.g. simple melee. Currently, these classes instance a copy of themselves as to not affect other characters that have the same modifier:
```
public IOngoingFormModifier Create(BattleFormReferences references)
{
    _modifierInstance = Instantiate(this);

    BattleFormManager formManager = references.BattleFormManager;
    _modifierInstance._componentInstance = formManager.gameObject.AddComponent<SimpleMeleeController>();
    _modifierInstance._componentInstance.Initialize(_meleeData, references);

    return _modifierInstance;
}
```
While this works, it is an odd behaviour. With more time, I'd split the modifier into a template with data and an instanced object. On `IOngoingFormModifier.Create(...)`, the object would be created and injected the data. 

## Battle Form Controllers
To allow for rapid debugging, I have the debug scene spawned with a prefab variant of the character. These "dummy characters" as I've named them, use the same forms and tech as the player character, with one key exception: the battle form controller. To decouple Battle Form logic from the player control of said inputs, a component must implement `IBattleFormController` as to allow as a gateway between the player andn the Battle Form system. Two have been created for this project:
- `BattleFormPlayerController` : Binds number key inputs to form switching, as well as bining left mouse button the primary ability callback that modifiers subscribe to.
- `BattleFormDebugController ` : Chooses a form on start up and then invokes the primary ability callback every so often to activate any abilties modifiers might have added.
This allows future work to add AI controllers that can choose states depending on certain criteria, without having to change the BattleForm system. 

## Event Driven
No code use Update() as everything is event based. Only systems happening every frame are GUI drawing if the debug overlay is active, and Gizmo drawing for seeing melee attack colliders.
