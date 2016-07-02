﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arena.battle.GameModes
{
    class GameMode
    {
        public Game Game
        { get; set; }

        public virtual void SpawnPlayer(Player player)
        { }
        public virtual proto_game.ExpBlocks GetBlockTypeByPoint(float x, float y)
        { return proto_game.ExpBlocks.Small; }
    }
}
