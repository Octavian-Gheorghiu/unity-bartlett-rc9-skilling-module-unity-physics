using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var dt = Time.DeltaTime;

        Entities
            .WithAll<Player>()
            .ForEach((ref Movable mov) => {
            mov.direction = new float3(x, 0, y);
        }).Schedule();

        var ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

        Entities
            .WithAll<Player>()
            .ForEach((Entity e, ref Health hp, ref PowerPill pill, ref Damage dmg) =>
            {
                dmg.value = 100;
                pill.pillTimer -= dt;
                hp.invincibleTimer = pill.pillTimer;
                if (pill.pillTimer <= 0)
                {
                    ecb.RemoveComponent<PowerPill>(e);
                    dmg.value = 0;
                }                    
            }).Run();
    }
}
