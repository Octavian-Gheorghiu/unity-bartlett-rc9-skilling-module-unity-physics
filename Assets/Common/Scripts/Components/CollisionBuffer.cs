using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

//[GenerateAuthoringComponent]
public struct CollisionBuffer : IBufferElementData
{
    public Entity entity;      
}
