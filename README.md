# 3D Platformer Game

This project implements a simple platformer game featuring:

## Core Gameplay Features

### Free-Look Camera
- Utilizes a Cinemachine free-look camera to allow the player to look around.
- The camera’s orientation determines the player's forward direction.  
*Player controls: use the mouse to rotate the view.*

### Movement
- The player moves relative to the camera's rotation, ensuring smooth and intuitive navigation in a 3D environment.  
*Player controls: use W, A, S, D to move.*

### Jumping
- Supports both a single jump when the player is grounded and a double-jump when airborne.  
*Player controls: press the Space key to jump or double-jump.*

### Dash
- A dash mechanic propels the player along the horizontal plane (X and Z directions).
- When no movement input is provided, the dash is executed in the direction the camera is facing; otherwise, it follows the input direction.
- A cooldown period prevents repeated dashes.  
*Player controls: click the right mouse button to dash in the direction of keyboard input or, if no input is provided, dash in the camera's forward direction.*

### Coin Collection & Scoring
- Coins are scattered across the environment.
- When the player collides with a coin, it disappears and the player's score is incremented.
- The UI dynamically updates to reflect the current score.

### Environment
- The game includes a large flat plane featuring platforms, boxes, and invisible walls to create a controlled play area.

## Additional UI Enhancements

### Enhanced Score Display
- A dedicated UI component smoothly displays the player's score updates when coins are collected.

### Pause Menu (Settings Canvas)
- Press the **P** key to toggle the pause menu.
- When activated, the game is paused (Time.timeScale is set to 0), and a settings menu appears with options such as Resume (Play) and Exit.
- The pause menu ensures that the mouse cursor remains visible and interactive, allowing players to click buttons without accidentally locking the cursor.

### Player Speed Control
- A slider in the settings menu allows real-time adjustment of the player's movement speed.
- The slider dynamically updates the player's maximum speed without the need to enter fixed numeric values manually.
- This control provides a more intuitive way to fine-tune gameplay difficulty and responsiveness.

## Controls Summary

- **Mouse Movement:** Rotate the camera to change the player's forward direction.
- **W, A, S, D:** Move the player relative to the camera's orientation.
- **Space:** Jump (or double-jump if airborne).
- **Right Mouse Button:** Dash in the indicated direction.
- **P Key:** Toggle the pause menu/settings canvas.
- **UI Slider (in Pause Menu):** Adjust the player's movement speed in real-time.


https://github.com/user-attachments/assets/b517c8f3-d1e4-4e78-814a-d878c1fe5a70


