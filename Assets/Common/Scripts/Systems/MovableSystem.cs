using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;

        Entities.ForEach((ref Movable mov, ref Translation translation, ref Rotation rot) => {
            translation.Value += mov.speed * mov.direction * dt;
            rot.Value = math.mul(rot.Value, quaternion.RotateY(mov.speed * dt));

        }).Schedule();
    }
}

/*

 float dt = Time.DeltaTime;
 Entities.ForEach((ref Movable mov, ref Translation translation, ref Rotation rot) => {
            translation.Value += mov.speed * mov.direction * dt;
            rot.Value = math.mul(rot.Value, quaternion.RotateY(mov.speed * dt));
        }).Schedule();


float dt = Time.DeltaTime;

        Entities.ForEach((ref PhysicsVelocity physVel, in Movable mov) => {
            var step = mov.direction * mov.speed;
            physVel.Linear = step;


*/