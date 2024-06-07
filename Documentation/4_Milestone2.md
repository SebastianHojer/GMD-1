### Development Update 2

#### Changelog:

- Added some more objects to the Battleground
- Created the Town scene
- Added some midground objects to the Town
- Added two different vendors to the Town: A weapons vendor and a food vendor
- Added interactions to the player input
- Added interactables that the player can interact with
- Made a Shop UI for the weapons vendor
- Made the Shop UI interactable
- Made the action of buying weapons from the weapons vendor

![Shop UI](https://i.imgur.com/323Zqvr.png)


#### Decisions
  - I decided to handle interactions on the playercontroller
      - I decided to do this since all the interactions in my game will be with colliders the player is located within
      - This also simplifies collision detection a bit
      - I check for interactions before the input is triggered to be able to display a text to the user, that something is interactable.
![Interaction in playercontroller](https://imgur.com/a/pdupTEj)
  - I decided to make an interface for interactables
      - This way it is easy to find all interactables as they all implement the interface
      - This could've been made with e.g. events, but I prefer inheritance as I don't have to handle subscribing methods etc.
      - This way I ensure that all interactables have an Interact method
![Interactable Interface](https://imgur.com/a/pGgEci4)
  - I decided to make an interface for customers in the shop
      - This way I can make the player a customer and make sure that the player interacting with the shop has the needed methods like BuyItem.
      - This could also be handled with events, but I chose this approach, since I will only have one customer in the shop which is the Player.
![Shop Interface](https://imgur.com/a/EmvXNNg)
  - I decided to make an Item class for everything sold in the shop
      - This way I can encapsulate all the logic and information for the different itemtypes
      - This could've been done with a ScriptableObject for each item, or something similar. In retrospect, that would probably be a better approach.
![Item class](https://imgur.com/a/LadLROf)
