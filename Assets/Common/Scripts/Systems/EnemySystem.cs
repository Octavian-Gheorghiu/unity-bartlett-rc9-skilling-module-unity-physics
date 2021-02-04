using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class EnemySystem : SystemBase
{
    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random(123);

    protected override void OnUpdate()
    {
        var rayCaster = new MovementRayCast() { pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld};
        rng.NextInt();
        var rngTemp = rng;

        Entities
            .WithAll<Enemy>()
            .ForEach((ref Movable mov, ref Enemy enemy, in Translation trns) => {
           if(math.distance(trns.Value, enemy.previousCell) > 0.9)
            {
                enemy.previousCell = math.round(trns.Value);

                // Perfrom raycast to see what travel direction the enemy can use
                var valiDir = new NativeList<float3>(Allocator.Temp);
                // Check if enemy can travel right
                if (!rayCaster.CheckRay(trns.Value, new float3(0, 0, -1), mov.direction))
                    valiDir.Add(new float3(0, 0, -1));
                // Check if enemy can travel left
                if (!rayCaster.CheckRay(trns.Value, new float3(0, 0, 1), mov.direction))
                    valiDir.Add(new float3(0, 0, 1));
                // Check if enemy can travel down
                if (!rayCaster.CheckRay(trns.Value, new float3(-1, 0, 0), mov.direction))
                    valiDir.Add(new float3(-1, 0, 0));
                // Check if enemy can travel up
                if (!rayCaster.CheckRay(trns.Value, new float3(1, 0, 0), mov.direction))
                    valiDir.Add(new float3(1, 0, 0));

                // From the list of possible direction randomly choose one
                mov.direction = valiDir[rngTemp.NextInt(valiDir.Length)];

                valiDir.Dispose();
            }
        }).Schedule();
    }

    private struct MovementRayCast
    {
        [ReadOnly] public PhysicsWorld pw;

        public bool CheckRay(float3 pos, float3 direction, float3 currentDirection)
        {
            if (direction.Equals(-currentDirection))
                return true;

            var ray = new RaycastInput()
            {
                Start = pos,
                End = pos + (direction * 0.9f),
                Filter = new CollisionFilter()
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 11,
                    CollidesWith = 1u << 12
                }
            };

            return pw.CastRay(ray);
        }
    }
}


