# RPG-Modell-Alpha
IN PROGRESS: A Unity RPG (3D) for PC with click to move implementation. Repo used mainly for backup and version control.

VIDEO DEMO: https://www.youtube.com/watch?v=RLTGbBoFpj0 (video not in full resolution)

Log May 31, 2018: I have updated the assets (mostly the models of houses and other environmental set pieces) and placed them in a folder so that if I can get a collaborator for the art and level design tasks, it is easy for them to locate and place the models.

As of this push, all the models in the current scene are NOT in their final location/orientation/size and have simply been placed for testing purposes.

Log June 7, 2018: Implemented the new Raycasting system and Player Pathfinding systems. Next Step: Fix planes and structures to work with navmesh (consider tilting plane to 60 degrees, or maybe implement a lift).

Log June 9, 2018: Removed BridgegameObject and added a slope on the terrain (using the smooth height tool), baked new NavMesh which now allows player to go up and down the house. NOTE: Should use the paint trees tool to create the forest.

Loge June 13, 2018: Enemies now launch projectiles at the player when the player walks into the enemy attack radius. However, this implementation needs updating because
the projectiles hit the enemy before hitting the player.

Log June 14, 2018: instead of the IDamageable interface, I have now implemented two spinoffs: IDamageablePlayer and IDamageableEnemy. Also, I have created two new projectile spinoffs, PlayerProjectile and EnemyProjectile. These now ensure that player fired projectiles do not harm the player and same goes for the enemies.

The projectile fired by player: PlayerProjectile. Player implements IDamageablePlayer
The projectile fired by enemy: EnemyProjectile. Enemy implements IDamageableEnemy

Log June 15, 2018: Added some objects to the village and forest areas, made the bridge navigation compatible. USE THE NAVIGATION STATIC OPTION in the future.

Log june 16, 2018: Started work on stat sheet for player and enemy attributes. Link here -> https://docs.google.com/spreadsheets/d/1xpPusDjEatUJLLt8BOa7suODnX1R7NMYPWJ6uducxGc/edit?usp=sharing

Log June 21, 2018: Added some grass and water to the terrain for testing. Will add full coverage (including trees) once city design is fully mapped out. (TODO?) Create an asset demo scene. SECTION 2 COMPLETE

Log June 22, 2018: Build Known Errors: Grass rendering needs to be improved.
Animations and sounds to be added. Navmesh behaves erratically close to the castle.

Log July 14, 2018: Added namespaces RPG.CameraUI and RPG.Character

Log July 16, 2018: Instead of having different weapons for the player, an array of weapons has been created. Instantiating different weapons works, but I need to figure out a way to make the old weapon disappear.

Log July 25, 2018: Animations added to weapons, need to make two fields in Player.cs weapon properties (add to Weapon.cs and use getters).

Log July 27, 2018: New terrain created, trees added, detail added, need to improve quality of "Marketplace" and "Pond near village".

Log August 6, 2018: Added special abilities, refactored CameraRaycasting and Removed Cursor Affordances. Energy regeneration added.


/**************** LIST OF FEATURES TO ADD

1) Moveable Camera (using WASD buttons and R to reset)

2) Dynamic weather conditions

3) Animations, sounds and particle effects for special abilities.

4) IMPORTANT: NEED TO MAP OUT LEVEL TO ADD DIALOGUE (Recorded/text?)

5) Reset and draw level story layout.



***********/

/****************BUGS TO FIX



**************/


/**************** NOTES
Maybe add freeze and poison abilities?

********/