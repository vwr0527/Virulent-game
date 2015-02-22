using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Virulent_dev.Input
{
    class InputManager
    {
        GamePadState currentState;
        GamePadState previousState;
        KeyboardState currentKeyState;
        KeyboardState previousKeyState;

        public void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = GamePad.GetState(PlayerIndex.One);

            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState(PlayerIndex.One);
        }

        public bool IsBackPressed()
        {
            return currentState.Buttons.Back == ButtonState.Pressed;
        }

        public bool APressed()
        {
            return previousState.Buttons.A == ButtonState.Released
                && currentState.Buttons.A == ButtonState.Pressed;
        }

        public bool StartPressed()
        {
            bool gamepad = currentState.Buttons.Start == ButtonState.Pressed
            && previousState.Buttons.Start == ButtonState.Released;
            bool keyboard = currentKeyState.IsKeyDown(Keys.Escape)
            && previousKeyState.IsKeyUp(Keys.Escape);
            return gamepad || keyboard;
        }

        public bool SKeyPressed()
        {
            return currentKeyState.IsKeyDown(Keys.S);
        }

        public bool AnyPressed()
        {
            return StartPressed();
        }

        public bool DownPressed()
        {
            return currentKeyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down);
        }
        public bool UpPressed()
        {
            return currentKeyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up);
        }
        public bool EnterPressed()
        {
            return currentKeyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter);
        }

        public bool MoveLeftPressed()
        {
            return currentKeyState.IsKeyDown(Keys.A);
        }
        public bool MoveRightPressed()
        {
            return currentKeyState.IsKeyDown(Keys.D);
        }
        public bool MoveUpPressed()
        {
            return currentKeyState.IsKeyDown(Keys.W);
        }
        public bool MoveDownPressed()
        {
            return currentKeyState.IsKeyDown(Keys.S);
        }
    }
}
