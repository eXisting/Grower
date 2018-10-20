using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Spheres.Core;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using DG.Tweening;
using Unity.Transforms;

namespace GameSystems
{
	public class MovementSystem : JobComponentSystem
	{		
		[ComputeJobOptimization]
		struct MovementJob : IJobProcessComponentData<LocalPosition, MoveSpeed, Rotation>
		{
			public float deltaTime;
			public float maxSpeed;
			public Vector3 targetPosition;

			public void Execute(
				ref LocalPosition _position, 
				[ReadOnly] ref MoveSpeed _movespeed, 
				[ReadOnly] ref Rotation _rotation
			)
			{
				//Debug.Log(_position.Value);

				float3 pos = _position.Value;

				pos.x += deltaTime * 1;

				_position.Value = pos;
			}
		}

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			MovementJob movementJob = new MovementJob
			{
				deltaTime = Time.deltaTime,
				maxSpeed = GameManagers.GameManager.Instance.Level * 5,
				targetPosition = Vector3.zero
			};

			JobHandle handler = movementJob.Schedule(this, 64, inputDeps);

			return handler;
		}
	}
}
