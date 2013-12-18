using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Virulent_dev.Graphics;
using System.Diagnostics;

namespace Virulent_dev.World
{
    class SpawnManager
    {
        private int numPendingSpawns;
        private int currentSpawnIndex;
        private List<SpawnPoint> spawns = new List<SpawnPoint>();
        private Dictionary<string, State> states = new Dictionary<string, State>();

        public void AddLoadState(State state)
        {
            states.Add(state.GetUniqueName(), state);
        }

        public void LoadContent(ContentManager content)
        {
            foreach (State state in states.Values)
            {
                state.LoadContent(content);
            }
        }

        public int CreateSpawn(string stateName, Vector2 pos, Vector2 vel, float rot, float rotv)
        {
            if (!states.ContainsKey(stateName))
            {
                Debug.WriteLine("No such state found.");
                return -1;
            }
            State state = states[stateName];
            spawns.Add(new SpawnPoint(pos, vel, rot, rotv, state, state.GetSprite()));
            return spawns.Count-1;
        }

        public SpriteElement GetSpriteAt(int index)
        {
            return spawns[index].sprite;
        }

        public void RespawnAt(int index)
        {
            spawns[index].pending = true;
        }

        public int CurrentSpawnIndex()
        {
            return currentSpawnIndex;
        }

        public void SpawnAt(int index, Vector2 pos, Vector2 vel, float rot, float rotvel)
        {
            SpawnPoint sp = spawns[index];
            ++numPendingSpawns;
            sp.pending = true;
            sp.pos = pos;
            sp.vel = vel;
            sp.rot = rot;
            sp.rotvel = rotvel;
        }

        public void SpawnAt(int index, string stateName, Vector2 pos, Vector2 vel, float rot, float rotvel)
        {
            SpawnPoint sp = spawns[index];
            ++numPendingSpawns;
            sp.pending = true;
            sp.state = states[stateName];
            sp.sprite = sp.state.GetSprite();
            sp.pos = pos;
            sp.vel = vel;
            sp.rot = rot;
            sp.rotvel = rotvel;
        }

        public SpawnPoint GetNextSpawn()
        {
            SpawnPoint sp = spawns[currentSpawnIndex];
            int iterated = 0;
            while (sp.pending != true)
            {
                currentSpawnIndex = (currentSpawnIndex + 1) % spawns.Count;
                sp = spawns[currentSpawnIndex];
                ++iterated;
                if (iterated > spawns.Count)
                {
                    Debug.WriteLine("No pending spawns found, even though I thought there would be.");
                    numPendingSpawns = 0;
                    return null;
                }
            }
            sp.pending = false;
            --numPendingSpawns;
            return sp;
        }

        public int GetNumPending()
        {
            return numPendingSpawns;
        }

        public void RespawnAll()
        {
            foreach (SpawnPoint spawn in spawns)
            {
                spawn.pending = true;
            }
            numPendingSpawns = spawns.Count;
        }
    }
}
