using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PowerPill : IComponentData
{
    public float pillTimer;

}
