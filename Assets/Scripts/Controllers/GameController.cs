using System;
using UnityEngine;
using Unity.Entities;
using UnityEngine.Jobs;
using GameSystems;
using Unity.Jobs;
using Spheres.Core;

public class GameController : MonoBehaviour
{
	struct ViewBounds
	{
		public float LeftClosestHorizontal;
		public float RightClosestHorizontal;

		public float TopClosesVertical;
		public float BottomClosesVertical;
	}

	#region Game stuff

	[SerializeField]
	private float maxMoveSpeed;

	[SerializeField]
	private int maxHorizontalOffset;

	[SerializeField]
	private int maxVerticalOffset;

	[SerializeField]
	private int level;

	[SerializeField]
	private int countMultiplier;

	[Header("Main canvas")]
	[SerializeField]
	private GameObject mainCanvas;

	private Rect mainCanvasRect;

	[Header("Main sphere")]
	[SerializeField]
	private GameObject mainSphere;

	[Header("Bot prefab")]
	[SerializeField]
	private GameObject ball;

	[SerializeField]
	private float ballSize;

	private System.Random rand;

	private ViewBounds viewBounds;

	#endregion

	TransformAccessArray balls;

	MovementSystem movementSystem;

	JobHandle jobHandle;
	
	private void Awake()
	{
		Initialize();
	}

	private void Start()
	{	
		AddBalls();
	}

	private void OnDisable()
	{
		balls.Dispose();
		jobHandle.Complete();
	}

	private void AddBalls()
	{
		jobHandle.Complete();
		
		for (int i = 0; i < level * countMultiplier; i++)
		{
			GameObject obj = GameObject.Instantiate(ball,  mainCanvas.transform);
			obj.transform.localPosition = CalculateBallPosition();

			//Debug.Log("Local position: " + obj.transform.localPosition);

			balls.Add(obj.transform);
		}
	}


	private Vector3 CalculateBallPosition()
	{
		float xMinRange = UnityEngine.Random.Range(
			viewBounds.LeftClosestHorizontal, 
			viewBounds.LeftClosestHorizontal - maxHorizontalOffset
		);

		float xMaxRange = UnityEngine.Random.Range(
			viewBounds.RightClosestHorizontal, 
			viewBounds.RightClosestHorizontal + maxHorizontalOffset
		);

		float yMinRange = UnityEngine.Random.Range(
			viewBounds.BottomClosesVertical, 
			viewBounds.BottomClosesVertical - maxVerticalOffset
		);

		float yMaxRange = UnityEngine.Random.Range(
			viewBounds.TopClosesVertical, 
			viewBounds.TopClosesVertical + maxVerticalOffset
		);
		
		Vector3 position = new Vector3(
			rand.Next(0, 2) < 1 ? xMinRange : xMaxRange, 
			rand.Next(0, 2) < 1 ? xMinRange : xMaxRange
		);

		return position;
	}

	private void Initialize()
	{
		rand = new System.Random();

		balls = new TransformAccessArray(level * countMultiplier);
		mainCanvasRect = mainCanvas.GetComponent<RectTransform>().rect;

		RememberViewBounds();
	}

	private void RememberViewBounds()
	{
		viewBounds.LeftClosestHorizontal = mainCanvasRect.xMin - ballSize;
		viewBounds.RightClosestHorizontal = mainCanvasRect.xMax + ballSize;

		viewBounds.BottomClosesVertical = mainCanvasRect.yMin - ballSize;
		viewBounds.TopClosesVertical = mainCanvasRect.yMax + ballSize;
	}
}
