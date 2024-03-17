### Development Update 1

#### Changelog:

- Added the first level, a battlefield where the player will fight enemies.
- Added sprites and made prefabs for the player
- Added basic physics to the game
- Added basic movement to the player
- Added borders to the level and handled collision with these
- Added collectable coins and handled collisions with these
- Added a balance that keeps track of the players collected coins
- Added some of the logic for changing weapons
- Added a walking animation and transition to this animation when moving


#### Decisions

- I decided to use the new Input System. I did this for various reasons:
  - Handling various input devices at once is much easier, like keyboard, joystick, xbox controller and so on
  - The new input system has a great API for handling the input. Much cleaner code. No more "Input.GetButton" checks.
  - It's very easy to customize and remap controls
- I decided to make a GameManager object for managing the overall state of the game
    - I chose this because centralizing game state management avoids scattering the state-related logic across the code.
    - This centralization can make debugging more manageable.
    - For now this handles the Coins and the balance.
- I made a CoinManager script that handles the balance.
  - I decided to make this a Singleton as to not instantiate more than one of this object in the game.
  ![Singleton code](https://i.imgur.com/0islhNo.png)
  - I also decided to use delegates and events to "implement" the observer pattern
  ![Observer pattern](https://i.imgur.com/lbWsiva.png)
- The movement is now directly based on the input, before I used acceleration and deceleration
  - I did this because acceleration for the movement caused some weird interactions when using the controller where the player would slide across the screen if the stick was pointing down or up.
