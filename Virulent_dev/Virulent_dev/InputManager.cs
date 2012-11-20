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

        public void Update(GameTime gameTime)
        {
            GamePadState previousState = currentState;
            currentState = GamePad.GetState(PlayerIndex.One);
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
    }
}
