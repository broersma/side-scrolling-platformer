# Coding assignment: Gameplay programmer

### Initial plan
1. Create a basic platformer (2 hours)
1. Add some kind of cool mechanic (2 hours)
1. Tweaking the gamefeel and polishing (4 hours)

### Decisions made

#### General
Having a plan: as the assignment focuses on gamefeel, it seemed like a good idea to reserve 50% of the time on doing polishing. The other hard requirement was creating a **2D side-scrolling platformer**. I figured I could get the bare basics down in about 2 hours. Which would hopefully leave me 2 hours to make it a little more than "just" the first 10 seconds of Mario (or: 1 enemy, 1 powerup, a few platforms). :crossed_fingers:

Using a Tilemap and Tile Palette. I actually didn't use this before, but it seemed easy enough. Setup was actually a bit tricky, but I got it working after actually checking the docs. :-D Hopefully this will help me save some time I can use to create a more interesting level.

#### Third-party code
For the character controller I used Brackeys 2D Character Controller script as a basis. Time is short!

#### Snappy jumps
To make Player's jumps more snappy I tweaked his Gravity Scale upwards to 6. I also had to adjust his Jump Force accordingly so it is just enough to jump on top of a wall that is two tiles high. This way you can choose whether to walk under or over platforms.

#### Physics woes
- I added a Slippery Physics Material so Player's Rigid Body 2D wouldn't get stuck on the sides of walls. However, this caused Player to slide off the edge of platforms, because I initially had a Circle Collider 2D for his feet and a Box Collider 2D for his head. I replaced both with a single rounded Box Collider 2D.
- Because it uses a checking radius of 0.2f units, Brackeys 2D Character Controller ground check triggered immediately after jumping. This caused my jumping animation to be cut short just after starting. I fixed this by letting the Character Controller only do the ground check if its velocity was downward. I did some quick testing (including checking what happened if Player was nearly pinned below a ceiling) and it seemed to work well enough.
- Actually, the previous testing was not good enough. :-D There were still some edge cases. I decided to add two more ground check points (one for each of Player's legs) so you can also jump when standing on the very edge of a platform, and add a timer which decides the minimum time (currently 0.1 seconds) to show the jumping animation. Furthermore I added an event to let the Character-Controller notify PlayerInput when jumping is actually happening, so it can set the animation at the right time.
- There is some weird hitch that sometimes occurs when running from a platform down to a platform that is 1 tile lower. I decided initially to add it to the possible code improvements section below, but I stumbled upon a fix which mentions this being caused by the shared edges of tiles causing unwanted collisions. (The fix was to use a Composite Collider 2D.)

#### Other considerations
I made Player the same size as the level's tiles, because it's simpler that way, and no immediate gameplay need to have it differently.

The Circle Collider for the Bit is twice as big as its sprite. This makes picking it up feel more organic.

The Circle Collider for the Saw is just a little smaller than its sprite. We don't want to be harsh. I added an animation to signal its deadliness.

Sounds (and music) are a big part of game feel, so I had to add some. Sadly, I'm not a sound designer (yet!), but I can press the mutate button in SFXR very well. :-)

### Actual time spent
- 2:00 Project setup, importing assets, create basic testing level, create player avatar with character controller, adding Cinemachine follow camera.
- 4:00 Tweak player movement
- 1:00 Add pickups and a victory condition
- 0:40 Add obstacles 
- 0:20 Add sounds
- 0:30 Create the actual level, add a short story/goal introduction.

### Possible code improvements
- Add renderer sorting layers instead of using Sorting Order within the Default layer.
- Maybe add a low jump and a high jump, depending on length of Jump button press. This would allow more skill-based gameplay and more possibilities for level design.
- I would like to add some kind of animation to the Bits to signal their purpose.
- Add moving obstacles.
- Code-wise the Gameplay and PlayerInput class are a little bit too coupled for my tastes. I would like to refactor these to make the code more self-explaining.
- It seems like there is a small bug where both "Press any key" texts are shown and overlap. This might be caused by a Coroutine I should stop, but I would have to look into it.
- I would have liked to add a Mario-style jump where you are able to jump for a few milliseconds after walking off of a platform. This would prevent Player from sometimes just awkwardly walking to his death.

### Third-party code/tools
- Brackeys 2D Character Controller: https://github.com/Brackeys/2D-Character-Controller
- sfxr: http://www.drpetter.se/project_sfxr.html
