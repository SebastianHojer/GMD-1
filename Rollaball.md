# Roll-a-ball

Today I created my first game ever. I did this by following the roll-a-ball tutorial on Unity.
- I started off by creating a project on Unity. I used the Basic URP(Universal Rendering Pipeline) template for the project.
- Then I created a plane to use for the ground of the game. I created a Player object, in this case it was just a Sphere.
- I then adjusted the light and set it to white instead of yellow, and created 2 materials to be used for the background and for the player.
- Then I added a rigidbody to the sphere(player) to make it solid. 
- To make the player able to move I added the Input System package and added a Player Input component to the Player.
- I then added a PlayerController script to the player, to be able to control what happens to the player.
- I added an OnMove function to be able to control the movement of the player, and a FixedUpdate function, where I applied force to the rigidbody of the player, so the player moves.
- I then wanted to make the camera follow the player, and I did this by adding a CameraController script that could calculate the position of the camera, by taking the position of the player and applying an offset.
- After that was done, I created a small play-area with some walls to hold the ball and the other objects in.
- Now I created some collectibles, to add a goal to the game and make it a little fun to play. I made some PickUp prefabs and added some code to make them rotate.
- I made an OnTriggerEvent function to make the collectibles disappear on collision. To make sure that I only removed collectibles, I added a "Pickup" tag to them all, and made a conditional statement for this tag before removing the collectible.
- This also included adding a Rigidbody to the Pickup and setting the "Is Trigger" boolean.
- After this was done, I added a "score" text to the top right of the screen to keep track of collectibles collected. I also added a win-text for when all the collectibles had been collected. To make it a little more fun, I also added a timer that keeps track of the time and outputs it when the game is won.
- To finish, I decided to make an obstacle course level, to make the game a little more challenging. An image of this level can be seen below.

![level](https://i.imgur.com/9jtmAbf.png)
