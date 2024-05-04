markdown

### Development Update 3

#### Changelog:

- Implemented enemy spawning system
- Added various enemies to the Battleground
- Developed enemy AI for basic movement and attacking behavior
- Introduced weapon swapping mechanic for the player
- Enhanced Shop UI
- Integrated a second shop into the Town scene offering different items
- Created a portal for seamless teleportation between the Town and the Battleground

#### Decisions:

- Implemented a simple follow-and-attack AI for enemies
  - Enemies will attempt to close the distance with the player and engage in melee or ranged attacks if within striking distance
- Introduced a secondary shop to diversify available items and to add some depth to combat
  - By incorporating multiple shops, players have access to a wider range of equipment and consumables, enriching the game economy
- Implemented a teleportation portal to start a round, where the player can fend off enemies
  - The addition of a portal between the Town and the Battleground streamlines travel. Some UI still needs to be added to make it more apparent to the player that they are starting a round.
