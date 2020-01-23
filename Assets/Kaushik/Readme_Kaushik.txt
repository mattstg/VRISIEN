README_KAUSHIK

- Created Prefabs
  - BloodParticles : Use or spawn at location for bloodspurt
  - Enemy_Melee_Ragdoll : PLEASE USE FOR AI GOING FORWARD! Handles Animator AI as well as Ragdoll for stun/death. Currently set up with Sword Slash and Stun Gun Trigger (?)
 
- Interactibles (In Progress)
  - Joystick Control : attempt to move sphere handle spherically around pad on grab. Grab releases if you pull too much or push too much.
- Pad : Puzzle system. Can be used as joystick or key system. Transfer rod from one pad to another, for example (could be guarded by enemies maybe)

- State Machine
  - Simple : Rigged with script. Use to test a certain state machine flow for level. Operates on triggers. 




- Other Scripts

  - ControlLevel : Used to rig keyboard input to Simple StateMachine. Add to empty object on scene to test.

- ExtensionFunctBook : Add to this as you can. So far, 
    -GameObject go = transform.CheckRaycast() : Return game object that raycast detects. Null if none in path.
    - rb.Deflect() : Simulated Physics for deflecting bullets. Can be applied to other moving objects, with only this applied at the right place.

RagdollControl : Used to trigger ragdoll on/off based on bool input to the function DoRagdoll(). Currently set up with sword slash and stun gun.

randomGravity : Simulate smoother gravity for fluid grab/throw and fall.

swordDamage : Collider logic to manage velocity of strikes and thus determine damage/kill. Adds Haptic feedback and blood effect, as well as Ragdolls the enemy, if you strike hard enough. 