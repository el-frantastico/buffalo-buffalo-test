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

## Battle Form Controllers

## Event Driven
No code use Update() as everything is event based. Only systems happening every frame are GUI drawing if the debug overlay is active, and Gizmo drawing for seeing melee attack colliders.
