using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Spheres.Core;
using Unity.Jobs;
using Unity.Collections;

namespace GameSystems
{

	public class MovementSystem : JobComponentSystem
	{
		[ComputeJobOptimization]
		struct MovementJob : IJobProcessComponentData<Position, MoveSpeed>
		{
			public void Execute(ref Position position, [ReadOnly] ref MoveSpeed movespeed)
			{

			}
		}

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			return base.OnUpdate(inputDeps);
		}
	}

}
