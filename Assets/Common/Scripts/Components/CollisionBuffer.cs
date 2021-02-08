using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct CollisionBuffer : IBufferElementData
{
    public Entity entity;      
}
