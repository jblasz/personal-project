# Player control
You control a rocket launchpad in this top-down game where key inputs make the player adjust start position and initial launch force.

# Basic gameplay
During the game, the rocket moves according to current force and gravity relative to position and the goal of the game is to hit target planet without crashing into anything else or leaving the system.

# Sound & effects
There will be sound effects (launch, flight, crash, ambient) and particle effects (launch, flight, crash).

# Gameplay Mechanics
As the game progresses, the solar system rotates making it difficult to predict the flight pattern.

# User Interface
The launch position and initial force will change whenever the user presses arrow keys (during planning phase). At the start of the game, the title rocketeer will appear and the game will end when something is hit, the rocket leaves the system, or player resets the simulation.

# Project Timeline:
- System generation and planetary movement (around sun and surface rotation), randomly pick and mark start and end planet
- Velocity/acceleration per point in space and unit mass
- Apply above to an already-moving object
Hardcode initial rocket positioning and launch vector
- Tune the coefficients into something making sense (neither crashing into planets too easy nor ignoring them)
- Split game into 3 stages (launch screen, preparation phase with immobile system and user input, simulation phase with no user input)
- Let player choose start and target planet (cant think of a way to automatically pick reasonable pairs)
- Add planet texture set to randomly draw from

# Possible extensions:
User input on launch screen (rng seed, SOL preset)
Ambient music, particles
Moons
Rogue asteroids
Draw orbits, rocket path, paths of previous attempts

