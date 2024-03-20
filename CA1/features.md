# VIDEO: https://youtu.be/CSqxumFKbZA
# Features

* levels
    * level 1 
        * save and load
        * different area costs
    * level 2 procedurally generated using arrays

* type 1 
    * walks path
    * follows player when detected
    * shoots every 3 seconds at player
    * dies when low health
    * sight detection

* type 3
    * Deliberately walks towards the player using Navmesh
    * Uses or avoids specific predefined areas based on costs
    * Shoots at the player while following the player
    * Looks for ammunitions when its ammunitions run low
    * Looks for health packs when its health runs low (e
    * Can locate (and go to) health packs (or health zones) and ammunitions based on theirs positions
    * If health is low and no health packs are available, but ammunitions are high, this NPC will keep chasing the player until new ammunitions become available

* type 5
    * player lead team
    * teammates follow/stop within 2m

* type 6
    * leader patrols
    * teammates follow
    * stop within 2m
