﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using shared.helpers;

namespace arena.battle
{
    class BlockSpawner
    {
        public interface IBlockControl
        {
            void AddBlock(ExpBlock entity);
            void RemoveBlock(ExpBlock entity);
        }

        private IBlockControl control_;
        private int width_, height_;
        private int maxBlocks_;
        private float cellWidth_, cellHeight_;
        private Area spawnArea_;
        private ConcurrentDictionary<int, ConcurrentDictionary<int, int>> buckets_ 
                = new ConcurrentDictionary<int, ConcurrentDictionary<int, int>>();
        private int unitsPerCell_;
        private ExpLayer layer_;

        public BlockSpawner(ExpLayer layer, IBlockControl control)
        {
            layer_ = layer;
            control_ = control;
            width_ = layer.TileWidth;
            height_ = layer.TileHeight;
            maxBlocks_ = layer.MaxBlocks;
            spawnArea_ = layer.Area;  
            unitsPerCell_ = (int)Math.Ceiling((float)maxBlocks_ / (float)(width_ * height_));
            cellWidth_ = spawnArea_.Width / (float)width_;
            cellHeight_ = spawnArea_.Height / (float)height_;
        }

        public void OnExpBlockRemoved(Entity entity)
        {
            var block = entity as ExpBlock;
            if (block != null)
            {
                control_.RemoveBlock(block);

                var pos = entity.Position;
                var tile = GetTileCoord(pos.x - spawnArea_.minX, pos.y - spawnArea_.minY);

                ConcurrentDictionary<int, int> bucket;
                buckets_.TryGetValue(tile.Key, out bucket);

                int count = 0;
                bucket.TryGetValue(tile.Value, out count);
                bucket.TryUpdate(tile.Value, count - 1, count);

                Common.ClassPool<ExpBlock>.Release(block);
            }
        }

        public void Update()
        {
            foreach (var bucket in buckets_) 
            {
                float x = bucket.Key ;
                foreach (var p in bucket.Value) 
                {
                    float y = p.Key;
                    var countToSpawn = unitsPerCell_ - p.Value;
                    for (var ii = 0; ii < countToSpawn; ++ii)
                    {
                        SpawnBlock(spawnArea_.minX + x * cellWidth_, 
                            spawnArea_.minX + (x + 1) * cellWidth_,
                            spawnArea_.minY + y * cellHeight_,
                            spawnArea_.minY + (y + 1) * cellHeight_);
                    }

                    if (countToSpawn > 0)
                        bucket.Value.TryUpdate(p.Key, p.Value + countToSpawn, p.Value);
                }
            }
        }

        private KeyValuePair<int, int> GetTileCoord(float x, float y)
        {
            int tx = (int)Math.Floor(x / cellWidth_);
            tx = tx >= width_ ? width_ : tx;
            int ty = (int)Math.Floor(y / cellHeight_);
            ty = ty >= height_ ? height_ : ty;
            return new KeyValuePair<int, int>(tx, ty); 
        }

        private void SpawnBlock(float minX, float maxX, float minY, float maxY)
        {
            /*
            var block = Common.ClassPool<ExpBlock>.Get();
            block.SetPosition(MathHelper.Range(minX, maxX), MathHelper.Range(minY, maxY));

            var type = layer_.GetBlockTypeByPoint(block.Position);
            var entry = Factories.ExpBlockFactory.Instance.GetEntry(type);
            block.BlockType = type;
            block.Exp = entry.Exp;
            block.HP = entry.Health;
            block.Radius = entry.CollisionRadius;
            block.Coins = entry.Gold; 
            block.Stats.SetValue(proto_game.Stats.MaxHealth, entry.Health); 

            control_.AddBlock(block);

            block.InitPhysics();*/
        }
    }
}
