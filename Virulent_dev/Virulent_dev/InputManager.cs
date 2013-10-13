using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Virulent_dev
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
            return currentState.Buttons.Start == ButtonState.Pressed;
        }

        public bool SKeyPressed()
        {
            return currentKeyState.IsKeyDown(Keys.S);
        }
    }
}
