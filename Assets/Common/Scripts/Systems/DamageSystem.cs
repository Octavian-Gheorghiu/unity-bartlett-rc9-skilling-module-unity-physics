using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class DamageSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        // Handle Enemy collsision with Player
        Entities.ForEach((DynamicBuffer<CollisionBuffer> col, ref Health hp) =>
        {
            for(int i=0; i<col.Length; i++)
            {
                if (hp.invincibleTimer <= 0 && HasComponent<Damage>(col[i].entity))
                {
                    hp.value -= GetComponent<Damage>(col[i].entity).value;
                    hp.invincibleTimer = 1;
                }
            }
        }).Schedule();

        // Handle Player health
        Entities
            .WithNone<Kill>()
            .ForEach((Entity e, ref Health hp) =>
        {
            hp.invincibleTimer -= dt;
            if (hp.value <= 0)
                EntityManager.AddComponentData(e, new Kill() { timer = hp.killTimer});
        }).WithStructuralChanges().Run();

        // Destroy Player
        var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        var ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.ForEach((Entity e, int entityInQueryIndex, ref Kill kill) =>
        {
            kill.timer -= dt;
            if (kill.timer <= 0)
                ecb.DestroyEntity(entityInQueryIndex, e);
        }).Schedule();

        ecbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
