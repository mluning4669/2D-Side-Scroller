2D Sidescroller Roadmap

TODO:
	rework pig collision detection - DONE
	Add Sound effects
		- walking
		- jumping - DONE
		- attacks
		- hits - DONE
		- pig knockout - DONE
		- diamond pickup
		- music?
	Animate big diamond sprite in UI
	Replace text with numeric sprites
	Add Health to king
	Make King killable
	Make a kill screen with option to restart
	
	

DONE:
	Track how many diamonds the king has collected - DONE
	Replace BigDiamond in UI with big diamond sprite - DONE
	Create pool for diamonds - DONE
	Expand size of second room - DONE
	Make pigs drop diamond upon death - DONE
	Find way to disable physics for objects before you destroy them - DONE
	Find a way to jump up through a platform and land on top of it - DONE (for boxes)
	Make pigs knocked back on billing blow - DONE
	Make camera transitions in rooms wider than 20 block units - DONE
	Move camera to room king is moving to - DONE
	Find a way to transition between doors with the camera. Make it look nice - DONE
	Make basic door functionality - DONE
	Fix issue with spamming jump while in the air - DONE
	Fix issue with going back through the door when releasing the up key - DONE
	Fire off door open animation and king leave animation then door close animation - DONE
	Fire off door open animation and king arrival animation then door close animation - DONE
	fix issue with pig getting stuck in invisible wall - DONE
	fix king's infinite jumping - DONE
	use raycasting to test if king is on the ground or airborne - DONE
	make king's jump force proportional to the time the jump button is pressed - DONE
	fix issue with king sticking to walls when jumping - DONE
	add health to pig - DONE
	add damage to king's attack - DONE
	make pigs killable - DONE
	fix bug where pig continues to walk into a wall when he is knocked into a wall - DONE
	make pigs able to damage other pigs after being knocked back - DONE
	make knocked back pigs able to knock other pigs - DONE
	refactor pigs damaging other pigs - DONE
	add "Damage" and "Heal" methods to EnemyHealth - DONE

OTHER:
	Fix issue with king arriving slightly above ground when arriving through an entrance - NOT AN ISSUE BECAUSE IT IS ACTUALLY PART OF THE ARRIVAL ANIMATION
	Rework door code and maybe control them from a single script instead of the spaghettified mess you've made for yourself