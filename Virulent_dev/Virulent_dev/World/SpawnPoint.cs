using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Virulent_dev.Graphics;

namespace Virulent_dev.World
{
    class SpawnPoint
    {
        public Vector2 pos = new Vector2();
        public Vector2 vel = new Vector2();
        public float rot;
        public float rotvel;
        public State state;
        public SpriteElement sprite;
        public bool pending;

        public SpawnPoint(Vector2 _pos, Vector2 _vel,
            float _rot, float _rotvel, State _state, SpriteElement _sprite)
        {
            pos = _pos;
            vel = _vel;
            rot = _rot;
            rotvel = _rotvel;
            state = _state;
            sprite = _sprite;
            pending = false;
        }
    }
}
