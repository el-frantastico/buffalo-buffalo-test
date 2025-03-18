# buffalo-buffalo-test
Tech test submission for Buffalo Buffalo by Francisco Martin.

Video demo link here: https://youtu.be/CZCUarl9-zw

This submission makes creates a highly modular fighting state system called "Battle Forms". Each form is defined by instant modifiers and ongoing modifiers. The former are one and done modifications to the player, such as size, health, and physics tangibility. Ongoing modifiers are modifications that need to be instanced and destroyed on switching states. They include abilties such as simple melee and health regen.

No code use Update() as everything is event based. Only systems happening every frame are GUI drawing if the debug overlay is active, and Gizmo drawing for seeing melee attack colliders.
