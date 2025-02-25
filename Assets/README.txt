My first package includes:
player and enemy spaceship system,
with shooting, death, and asteroids!

HOWTO CREATE ENEMIES:
To create new enemies, you HAVE TO attach both EnemyHandler, and Enemy Health scripts to it,
additonally add an collision and another 2D collision with istriger on to make an agro zone.
Then, you need to set the Layer and Tag to "Enemy" for it to work properly.
HOWTO ADD NEW PLAYER:
To create player you can use prefab listed in Assets/Prefabs.
Otherwise, you can attach to your new player Ship_Movement and Player Health Handler classes, and
change the MaxHealth attribute of Player Health to your liking.
And set layer to "Player".
HOWTO ADD DESTRUCTABLE OBJECTS:
Create a sprite and attach Collider and destructable object script, 
set the health to your desired max health.

Thank you for using my first package, make a post if something went wrong, thanks. - Joshua <3