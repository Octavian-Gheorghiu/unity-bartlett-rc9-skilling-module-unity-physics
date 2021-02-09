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
        var playerQuery = GetEntityQuery(ComponentType.ReadOnly<Player>());

        //Debug.Log(pelletQuery.CalculateEntityCount());
        if (pelletQuery.CalculateEntityCount() <= 0)
            GameManager.instance.Win();
        if (playerQuery.CalculateEntityCount() <= 0)
            GameManager.instance.Lose();
    }
}
