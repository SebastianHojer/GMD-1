### Development Update 3

#### Changelog:

- Implemented enemy spawning system
- Added a round system and a round manager
- Added various enemies to the Battleground
- Developed enemy AI for basic movement and attacking behavior
- Introduced weapon swapping mechanic for the player
- Enhanced Shop UI
- Created a portal for seamless teleportation between the Town and the Battleground

#### Decisions:
- The enemy spawning system is based on 2 gameobjects that act as spawnpoints. These are given to the RoundManager as gameobjects.
- The RoundManager is responsible for controlling the round, spawning enemies and keeping track of time and enemies remaining
  - The display of the time is split into another class,
  - When the scene is loaded, a RoundCoroutine is started. This Coroutine is stopped when the round is over.
  - Roundmanager uses events to fire OnRoundStart and OnRoundOver, which the timedisplay class uses to manage displaying the time, and keeping track of the time.
  - After each round, the enemies get progressively harder. Their damagemodifiers are increased and the spawnrate is increased aswell.
  - When the round is over, return to the Town. \
![RoundManager](https://i.imgur.com/CKXN1iK.png)
- Implemented a simple follow-and-attack AI for enemies.
  - Enemies will attempt to close the distance with the player and engage in melee or ranged attacks if within striking distance. \
![Enemy AI](https://i.imgur.com/MG2eRVf.png)
- Implemented a teleportation portal to start a round, where the player can fend off enemies.
  - The addition of a portal between the Town and the Battleground streamlines travel. Some UI still needs to be added to make it more apparent to the player that they are starting a round. \
![Portal](https://i.imgur.com/GiqE6zp.png)
