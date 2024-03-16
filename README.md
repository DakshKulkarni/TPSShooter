
# TPS SHOOTER


Just a rookie game developer trying my hands on developing shooting games with basic working like killing enemies with gun.
A quick look of the setup looks like this👇🏻

## Screenshots

![App Screenshot](https://github.com/DakshKulkarni/FPS-shooter1/assets/115560064/79b445b2-4ca0-42c3-931c-30fcdccabb8f)

(The map asset was a little dark hence used some bright colours for the ammo text and the health bar)


## Player Movement
So the player has basic movement defined like walking, running, jumping and crouching.

## Enemy movement
The movement for the enemy is just simple walking. The use of state machines made it easier for me to define various other states for the enemy and give more variety to it.
Some of the states that enemy can transition to are:

### Patrol state: 
This is the default state for the enemy where it just moves in a confined path(defined by me) until it detects player movement.

### Attack state:
Within a certain radius, the raycast detects the player and starts to shoot towards the player. 
It will keep looking at the player while shooting, and as soon as the player moves out of the sight, it goes into search state.

### Search state:
Here, the enemy looks for the player's last position and just moves around there for a while in search of the player, if not found then it switches back to the patrol state.
#
The use of AI navigation mesh makes it very easy to define enemy movement and behaviour.
## Shooting description
There is not much rocket science used for the shooting mechanincs here. Just used an empty gameobject for a gun barrel for spawning of bullets and gave the player a gun to hold.
Same goes for the enemy.
#
The player will shoot at the enemy with the help of a crosshair 5 times until the enemy is destroyed.
#
For the enemy to kill the player, it will shoot at the player until the player's health is zero. The player health is visible in the game and looks kinda like....
![App Screenshot](https://github.com/DakshKulkarni/FPS-shooter1/assets/115560064/5328331c-62bc-4802-a8df-4cd8e5bd29bf)

### Ammo description
The maximum size in the clip of the player's gun is 30 and the mag size is 120, as soon as the clip is emtied, the player will have to reload the gun from the mag. The number of bullets is also visible in the game view.
![App Screenshot](https://github.com/DakshKulkarni/FPS-shooter1/assets/115560064/706e4ab9-660d-4a99-9df5-76c5552ed0b7)
#
The ammo text updates itself real-time as the bullets are getting fired.
## Animations
### Player Movement
For every movement, the player will perfom an animation based on the user input. If the player is walking, there is a separate walking animation in every direction.
Same goes for crouching and running.
### Player Shooting
When the player wants to shoot, it raises the gun upwards and points at the center of the screen(or the crosshair) and the bullet goes in that direction.
#
There is a player reloading animation for when the clip gets empty, it performs the reloading action while in the backend the math takes place where the clip is getting filled by the mag of the gun.
#
I have also used sounds and particle system muzzle fire when the bullet is getting fired. Also added a kickback or recoil after every bullet fire so that it gives an actual gun firing experience.
#
These are some basic functionalites in this game, will add some more and scaling up of map later on ;)