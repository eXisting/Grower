using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Spheres.Core;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using DG.Tweening;

namespace GameSystems
{
	public class MovementSystem : JobComponentSystem
	{		
		[ComputeJobOptimization]
		struct MovementJob : IJobProcessComponentData<Position, MoveSpeed, Size>
		{
			public float deltaTime;
			public float maxSpeed;
			public Vector3 targetPosition;

			public void Execute(ref Position _position, [ReadOnly] ref MoveSpeed _movespeed, [ReadOnly] ref Size _size)
			{
				float3 pos = _position.Value;
				
				pos += deltaTime * _movespeed.Value;

				_position.Value = pos;
			}
		}

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			MovementJob movementJob = new MovementJob
			{
				deltaTime = Time.deltaTime,
				maxSpeed = 20f,
				targetPosition = Vector3.zero
			};

			JobHandle handler = movementJob.Schedule(this, 64, inputDeps);

			return handler;
		}
	}
}
