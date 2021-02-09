using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SpwanSystem : SystemBase
{
    protected override void OnUpdate()
    {
                
        Entities.ForEach((ref Spawner spawner, in Translation trns, in Rotation rot) => {
            if (!EntityManager.Exists(spawner.spawnObject))
            {
                // Final assignmnt maybe spawn more than one spawnPrefab using a for loop
                spawner.spawnObject = EntityManager.Instantiate(spawner.spawnPrefab);
                EntityManager.SetComponentData(spawner.spawnObject, trns);
                EntityManager.SetComponentData(spawner.spawnObject, rot);
            }
        }).WithStructuralChanges().Run();
    }
}
