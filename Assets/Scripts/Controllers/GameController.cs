using UnityEngine;
using UnityEngine.Jobs;
using GameSystems;
using Unity.Jobs;
using DG.Tweening;

public class GameController : MonoBehaviour
{
	struct ViewBounds
	{
		public float xLeftBound;
		public float xRightBound;

		public float yTopBound;
		public float yBotBound;
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
			
			balls.Add(obj.transform);
		}
	}


	private Vector3 CalculateBallPosition()
	{
		float xMinRange = UnityEngine.Random.Range(
			viewBounds.xLeftBound, 
			viewBounds.xLeftBound - maxHorizontalOffset
		);

		float xMaxRange = UnityEngine.Random.Range(
			viewBounds.xRightBound, 
			viewBounds.xRightBound + maxHorizontalOffset
		);

		float yMinRange = viewBounds.yBotBound - maxVerticalOffset;
		float yMaxRange = viewBounds.yTopBound + maxVerticalOffset;
		
		Vector3 position = new Vector3(
			rand.Next(0, 2) < 1 ? xMinRange : xMaxRange, 
			rand.Next(0, 2) < 1 ? yMinRange : yMaxRange
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
		viewBounds.xLeftBound = mainCanvasRect.xMin - ballSize;
		viewBounds.xRightBound = mainCanvasRect.xMax + ballSize;

		viewBounds.yBotBound = mainCanvasRect.yMin - ballSize;
		viewBounds.yTopBound = mainCanvasRect.yMax + ballSize;
	}
}
