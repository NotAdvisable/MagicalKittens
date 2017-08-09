# Magical Kittens
aka: Magical Kittens: The fluffle of Darkness  


This game project was made with the Unity Engine for the Game Development 2 course at H-Da.  

You and up to 3 of your friends play as kittens and shoot down evil bunnies.

Key features:

 * 2.5D Shmup (specifically a Run and gun based on classic arcade games like Contra and Metal Slug) 
 * LAN Multiplayer for up to 4 Players
 * Custom Lobby where you can choose from 4 different cats and enter a display name
 * 6 different spells set up using the Ultimate VFX library
 * 5 different enemies and 4 different AI types (Guard, Patrol, Kamikaze, Mage)
 * Custom AI Editor incl. field of view visualisation
 * Screen shake and camera use through cinemachine
 * Adorable kittens and bunnies
 * A couple of easter eggs i included when i didn't really want to work on the serious stuff
 
 ## Controls
 
 <kbd>W</kbd><kbd>A</kbd><kbd>S</kbd><kbd>D</kbd> move <kbd>Space</kbd> jump  
 <kbd>Left Mouse</kbd> shoot in facing direction  
 
 additionally:  
 <kbd>ESC</kbd> open the menu ingame  
 <kbd>TAB</kbd> toggle screen names

 ## Spells
 
 There are 5 different spells you can learn by picking up the fitting spellbook.  
 *Note: You can only have one spell at a time*  
 
| Spell           	| Strength 	  	| Speed   	|
|---	       		|---	       	|---	    |
|  *Normal*       	|  50	       	|  30      	| 
| *Leaf*          	|  110		    |  60 	    | 
| *Flame*        	|  150   	    |  40 	    | 
| *Water*		    |  150	    	|  40     	| 
| ***Ultima***	    |  300	        |  50		| 


 ## Enemies

### Behaviours
**Patrol:** These enemies patrol between multiple points and start chasing any player that enters it's field of view.  
A patrol Enemy keeps chasing the player until they're out of side or another player is closer.

**Kamikaze:** Kamikaze enemies have a bigger field of view. *(Most likely because of the adrenaline)*  
When they find you, they'll chase you relentlessly. Once they get to you they'll blow themselves up and inflict massive damage.

**Guard:** They guard a single position and will only chase you for up to 3 seconds until they return to their post. These enemies are bulkier,
have the most health, and field of view to compensate for their limited movements.

**Mage:** The only type of bunny capable of ranged attacks. They shoot any of the first 4 spells at you. Since they don't have to guard anything,
they'll wait at the position they lost you and scan the area.

**King:** The bunny king himself. He is 6 times bigger than his subjects and has unique ground attacks.  

*the rest you must find out yourself*

### Stats

| Colour         	| Behaviour 	| Health  	| FIeld of view  |  Search Radius 	| Speed (walk/run)	| Damage per hit	|
|---	            |---	   		|---	    |---	         |---	            |---				| ---				|
|  *Black*			|  Patrol	    |  150 	    |  110           |   30	            | 6/12				|	75				|
|  *Purple*		    |  Kamikaze	 	|  200 	    |  160 	         |   35 	        | 16				|   300				|
|  *White*          |  Guard  	  	|  300 	    |  200 	         |   35             | 6/12				| 	100				|
|  *Blue*	        |  Mage	    	|  200    	|  150           |   30             | 6/12 				|   50-150			|
|  **King** 	    |  ?        	|  3000     |   ?  	         |   ?	            | ?					|	?				|

 ## How to setup
 
 - clone the repository  
 - open it as a Unity project  
 - build the game  
 - play the build
 - enjoy  
 
 ## Sources
 ### Used Assets
 
* **Cinemachine** - *Unity Technologies*
* **Post Processing Stack** - *Unity Technologies*
* **Cute Cats** - *DarkPurple Lion*
* **Forest Bunny** - *MeshTint*
* **Fantasy Environment** - *BRAiNBOX*
* **Ultimate VFX (v2.7)** - *Mirza Beig*
* **Universal Sound FX** - *Imphenzia*
* **Toon Loot and Props Pack** - *MeshTint*
 
 ### Background Music
 
 * Bring on the Super Ability - Kirby's Return to Dream Land
 * Fight On! - Final Fantasy VII
 * Victory Fanfare - Final Fantasy VII
 
 ### Additional
 
 Special Thanks to Daniel Schellhaas for modelling the King's Crown
