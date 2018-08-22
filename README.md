# SnakeGame
Simple implementation of the famous Nokia Snake game in VB .NET

## Instructions

In a 30x30 blocks window you start as a 5 blocks long snake and must use the arrow keys to navigate in the window and eat the white blocks that will randomly appear in the window. Everytime you eat a white block, the snake's length length increases by one.

<p align="center">
  <img src="https://github.com/dario-marvin/SnakeGame/blob/master/snake0.png">
</p>

If you collide with a wall you will reappear on the opposite side, however you must avoid running into your own body, or you will lose all the body you have cut yourself from. Try to eat the highest number of white block without reducing yourself!

<p align="center">
  <img src="https://github.com/dario-marvin/SnakeGame/blob/master/snake1.png">
</p>

## Cheats

Since this is test mode, the following cheat buttons are available:
- `x`: slow down the snake
- `y`: speed up the snake
- `c`: augment snake's length by one
- `v`: diminish the snake's length by one
- `space`: remove all snake's blocks and return to length 1
- `r`: pause the game

## Execute

The code was written to be compiled with Visual Studio, but if you wanna play the game without comiling the code, download the Snake.exe executable and double click on it.

## Possible TODO in the future

- Change the position of the white block after a certain time uneaten
- Augment snake's speed at specific length (should the speed reduce if we lose the tail?)
- Proper start screen 
- More maps, like untouchable walls and labyrinths
