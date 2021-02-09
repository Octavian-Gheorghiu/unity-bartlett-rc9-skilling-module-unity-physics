using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity spawnPrefab, spawnObject;   
}
