# Unity AIPeople
Simple implementation of NPC. These NPCs are created using state machine programming pattern. I have created
3 different variant of AI in this repo. Spline is used to create waypoints for the AIs.

- Villager
- Worker
- Farmer

## Villager
These type of AI can be used to create people in a village or in a city, walk around. In different 
waypoint locations you can set different activities for NPSs to perform. I have added some. Such as: 
- Idle
- Sit
- Eat
- Fight
- Fishing.

more activities can be added all you have to do is create different states and add animations for that action.

## Worker 
These NPCs are limited with activities. There is a `WorkerSupplier` script which will spawn these from their
given home location and will walk to their work location. After that they will just destroy. 

The same can be reversed. They will spawn from their work location and will walk to their home location. 

## Farmer
These are just a variant of the Villager AI. They also have a specific waypoints and each waypoints have different 
activities assign with them. Farmer will go to that location and will perform that task. And they will just keep on 
doing that. 

<img src ="images\Screenshot.png" width="700"/>
