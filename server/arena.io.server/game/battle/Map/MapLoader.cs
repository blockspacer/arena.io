﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using shared.helpers;

namespace arena.battle
{
    class MapLoader
    {
        private Map map_;
        public MapLoader(string path, Game game)
        {
            var maps = Directory.GetFiles(path);
            var mapName = maps[MathHelper.Range(0, maps.Length - 1)]; 

            var jsonMap = JObject.Parse(File.ReadAllText(mapName));
            var layers = jsonMap["layers"];

            List<PowerUpSpawnPoint> powerUpSpawnPoints = new List<PowerUpSpawnPoint>();
            PowerUpLayer powerUpLayer = new PowerUpLayer(powerUpSpawnPoints);

            ExpLayer expLayer = new ExpLayer();

            List<MobSpawnPoint> mobSpawnPoints = new List<MobSpawnPoint>();
            MobLayer mobLayer = new MobLayer(mobSpawnPoints);

            List<PlayerSpawnPoint> playerSpawnPoints = new List<PlayerSpawnPoint>();
            PlayerSpawnsLayer playerSpawnsLayer = new PlayerSpawnsLayer(playerSpawnPoints);

            NavigationLayer navLayer = new NavigationLayer();

            foreach (var layer in layers)
            {
                var layerName = (string)layer["name"];
                if (layerName == "PowerUps") 
                {
                    Dictionary<proto_game.PowerUpType, int> durations = new Dictionary<proto_game.PowerUpType, int>();
                    //load durations of power ups
                    foreach (JProperty prop in layer["properties"])
                    {
                        if (prop.Name == "RespawnDelay")
                        {
                            powerUpLayer.RespawnDelay = (long)prop.Value.Value<int>() * 1000;
                        }
                        else
                        {
                            durations.Add(
                                    Parsing.ParseEnum<proto_game.PowerUpType>(prop.Name),
                                    prop.Value.Value<int>() * 1000
                                );
                        }
                    }

                    foreach (var obj in layer["objects"])
                    {
                        var spawnPoint = new PowerUpSpawnPoint();
                        var probabilities = new Dictionary<proto_game.PowerUpType, float>();

                        var properties = obj["properties"]; 
                        foreach (JProperty prop in properties)
                        {
                            probabilities.Add(
                                Parsing.ParseEnum<proto_game.PowerUpType>(prop.Name),
                                prop.Value.Value<float>()
                            );
                        }

                        spawnPoint.Probabilities = probabilities;
                        spawnPoint.Area = ParseArea(obj);
                        spawnPoint.Durations = durations;

                        powerUpSpawnPoints.Add(spawnPoint);
                    }
                }
                else if (layerName == "Exp")
                {
                    expLayer.Load(layer);
                }
                else if (layerName == "Map")
                {
                    navLayer.Load(layer);
                }
                else if (layerName == "PlayerSpawns")
                {
                    foreach (var obj in layer["objects"])
                    {
                        var spawnPoint = new PlayerSpawnPoint();
                        spawnPoint.Area = ParseArea(obj);

                        playerSpawnPoints.Add(spawnPoint);
                    }
                }
                else if (layerName == "Mobs")
                {
                    var properties = layer["properties"];
                    mobLayer.RespawnDelay = properties["RespawnDelay"].Value<int>();

                    foreach (var obj in layer["objects"])
                    {
                        var spawnPoint = new MobSpawnPoint();
                        spawnPoint.Area = ParseArea(obj);

                        mobSpawnPoints.Add(spawnPoint);

                        properties = obj["properties"];
                        var probabilities = new Dictionary<proto_game.MobType, float>();
                        foreach (JProperty prop in properties)
                        {
                            if (prop.Name == "MaxCount")
                            {
                                spawnPoint.MaxCount = prop.Value.Value<int>();
                            }
                            else
                            {
                                probabilities.Add(
                                    Parsing.ParseEnum<proto_game.MobType>(prop.Name),
                                    prop.Value.Value<float>()
                                );
                            }
                        }
                        spawnPoint.Probabilities = probabilities;
                    }
                }
            }

            map_ = new Map(game, powerUpLayer, expLayer, navLayer, playerSpawnsLayer, mobLayer);
        }

        private Area ParseArea(JToken node)
        {
            var area = new Area();
            area.minX = node["x"].Value<float>();
            area.minY = node["y"].Value<float>();
            area.maxX = area.minX + node["width"].Value<float>();
            area.maxY = area.minY + node["height"].Value<float>();

            return area;
        }

        public Map Load()
        {
            return map_;
        }
    }
}
