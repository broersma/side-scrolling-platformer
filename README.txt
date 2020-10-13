# Coding assignment: Gameplay programmer

### Initial plan
1. Create a basic platformer (2 hours)
1. Add some kind of cool mechanic (2 hours)
1. Tweaking the gamefeel and polishing (4 hours)

### Decisions made
- Having a plan: as the assignment focuses on gamefeel, it seemed like a good idea to reserve 50% of the time on doing polishing. The other hard requirement was creating a **2D side-scrolling platformer**. I figured I could get the bare basics down in about 2 hours. Which would hopefully leave me 2 hours to make it a little more than "just" the first 10 seconds of Mario (or: 1 enemy, 1 powerup, a few platforms). :crossed_fingers:
- Using a Tilemap and Tile Palette. I actually didn't use this before, but it seemed easy enough. Setup was actually a bit tricky, but I got it working after actually checking the docs. :-D Hopefully this will help me save some time I can use to create a more interesting level.
- For the character controller I used Brackeys 2D Character Controller script as a basis. Time is short!
- I added a Slippery Physics Material so Player's Rigid Body 2D wouldn't get stuck on the sides of walls. However, this caused Player to slide off the edge of platforms, because I initially had a Circle Collider 2D for his feet and a Box Collider 2D for his head. I replaced both with a single rounded Box Collider 2D.
- Because it uses a checking radius of 0.2f units, Brackeys 2D Character Controller ground check triggered immediately after jumping. This caused my jumping animation to be cut short just after starting. I fixed this by letting the Character Controller only do the ground check if its velocity was downward. I did some quick testing (including checking what happened if Player was nearly pinned below a ceiling) and it seemed to work well enough.
- ...

### Actual time spent
- 1:56 Project setup, importing assets, create basic testing level, create player avatar with character controller, adding Cinemachine follow camera.
- ...

### Possible code improvements
- Add renderer sorting layers instead of using Sorting Order within the Default layer.
- ...

### Third-party code/tools
- Brackeys 2D Character Controller: https://github.com/Brackeys/2D-Character-Controller
- ...
