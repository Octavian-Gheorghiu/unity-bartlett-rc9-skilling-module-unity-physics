using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysUpdateSystem]
public class GameStateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var pelletQuery = GetEntityQuery(ComponentType.ReadOnly<Pellet>());
        Debug.Log(pelletQuery.CalculateEntityCount());
    }
}
