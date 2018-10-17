using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.Jobs;
using GameSystems;
using Unity.Jobs;

public class GameController : MonoBehaviour
{
	#region Game stuff

	[SerializeField]
	private float maxMoveSpeed;

	[SerializeField]
	private int level;

	[SerializeField]
	private int countMultiplier;

	[Header("Main canvas")]
	[SerializeField]
	private GameObject mainCanvas;

	[Header("Main sphere")]
	[SerializeField]
	private GameObject mainSphere;

	[Header("Bot prefab")]
	[SerializeField]
	private GameObject ball;

	#endregion

	TransformAccessArray balls;
	MovementSystem movementSystem;
	JobHandle jobHandle;

	private void Start()
	{
		balls = new TransformAccessArray(level * countMultiplier);

		AddBalls();
	}

	private void AddBalls()
	{
		jobHandle.Complete();

		for (int i = 0; i < level * countMultiplier; i++)
		{
			GameObject obj = GameObject.Instantiate(ball, mainCanvas.transform, false);

			balls.Add(obj.transform);
		}
	}

}
