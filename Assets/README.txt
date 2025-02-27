My first package includes:
player and enemy spaceship system,
with shooting, death, and asteroids!

** THE PACKAGE USES NEW INPUT SYSTEM, TO ACTIVATE IT FOLLOW THE FOLLOWING: **
Get into window->package manager, then find Input System and install it into your project.
After that, you have to activate it in Edit -> Project Settings -> Player ->
Other Settings -> Active Input Handler to New Input System.

To make camera in DEMOSCENE to work, you will have to install Cinemachine in package manager.

now, howtos:
HOWTO CREATE ENEMIES:
To create new enemies, you HAVE TO attach both EnemyHandler, and Enemy Health scripts to it,
additonally add an collision and another 2D collision with istriger on to make an agro zone.
Then, you need to set the Layer and Tag to "Enemy" for it to work properly.
HOWTO ADD NEW PLAYER:
To create player you can use prefab listed in Assets/Prefabs.
Otherwise, you can attach to your new player Ship_Movement and Player Health Handler classes, and
change the MaxHealth attribute of Player Health to your liking.
And set layer to "Player".
HOWTO CREATE A PROJECTILE:
Create an gameobject of your choise with attached cancollide,
Just attach an ProjectileHandler and RidgidBody2D to it.
**MAKE SURE EVERY HOWTOs ABOVE SPRITE IS FACING UP.**

HOWTO ADD DESTRUCTABLE OBJECTS:
Create a sprite and attach Collider and destructable object script, 
set the health to your desired max health.


Thank you for using my first package, make a post if something went wrong, thanks. - Joshua <3