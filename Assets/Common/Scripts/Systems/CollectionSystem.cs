using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class CollectionSystem : SystemBase
{
    protected override void OnUpdate()
    {

        var ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

        Entities
            .WithAll<Player>()
            .ForEach((Entity playerEntity, DynamicBuffer<TriggerBuffer> tBuffer) => {
                for(int i=0; i<tBuffer.Length; i++)
                {
                    var e = tBuffer[i].entity;
                    // Check if any pellet is collected 
                    if (HasComponent<Collectable>(e) && !HasComponent<Kill>(e))
                    {
                        ecb.AddComponent(e, new Kill() { timer = 0 });
                        GameManager.instance.AddPoints(GetComponent<Collectable>(e).points);
                    }
                    // Check if any power pill is connected
                    if (HasComponent<PowerPill>(e) && !HasComponent<Kill>(e))
                    {
                        ecb.AddComponent(playerEntity, GetComponent<PowerPill>(e));
                        ecb.AddComponent(e, new Kill() { timer = 0 });
                    }
                }                            
        }).WithoutBurst().Run();
    }
}
