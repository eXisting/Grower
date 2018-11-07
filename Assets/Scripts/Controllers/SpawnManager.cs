using Base.AbstractClasses;
using Components;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers
{

	public class SpawnManager : MonoBehaviour
	{
		struct ViewBounds
		{
			public float xLeftBound;
			public float xRightBound;

			public float yTopBound;
			public float yBotBound;
		}

		#region Serialized fields

		[SerializeField]
		private int maxHorizontalOffset;

		[SerializeField]
		private float initialBallSize;

		[SerializeField]
		private float initialMoveSpeed;

		[SerializeField]
		private int maxVerticalOffset;

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

		private ViewBounds viewBounds;
		private Rect mainCanvasRect;

		private System.Random rand;

		private List<Ball> balls = new List<Ball>();

		private int amount;

		#region Core functions

		private void Awake()
		{
			Initialize();
		}
		
		public void StartAddBalls()
		{
			AddBalls();
		}
		
		private void AddBalls()
		{
			amount = GameManager.Instance.Level * GameManager.Instance.Multiplier;

			for (int i = 0; i < amount; i++)
			{
				Vector3 ballPosition = CalculateBallPosition();

				GameObject obj = Instantiate(this.ball, mainCanvas.transform);
				obj.transform.localPosition = ballPosition;
			}
		}


		private Vector3 CalculateBallPosition()
		{
			float yMinRange = UnityEngine.Random.Range(
				0,
				viewBounds.yBotBound - maxVerticalOffset
			);

			float yMaxRange = UnityEngine.Random.Range(
				0,
				viewBounds.yTopBound + maxVerticalOffset
			);

			float xMinRange = viewBounds.xLeftBound - maxHorizontalOffset;
			float xMaxRange = viewBounds.xRightBound + maxHorizontalOffset;

			Vector3 position = new Vector3(
				rand.Next(0, 2) < 1 ? xMinRange : xMaxRange,
				rand.Next(0, 2) < 1 ? yMinRange : yMaxRange
			);

			return position;
		}

		private void RememberViewBounds()
		{
			viewBounds.xLeftBound = mainCanvasRect.xMin - initialBallSize;
			viewBounds.xRightBound = mainCanvasRect.xMax + initialBallSize;

			viewBounds.yBotBound = mainCanvasRect.yMin - initialBallSize;
			viewBounds.yTopBound = mainCanvasRect.yMax + initialBallSize;
		}

		private void Initialize()
		{
			Screen.orientation = ScreenOrientation.Landscape;
			
			rand = new System.Random();
			
			mainCanvasRect = mainCanvas.GetComponent<RectTransform>().rect;

			RememberViewBounds();

			GenerateObjectPool();
		}

		private void GenerateObjectPool()
		{
			
		}

		#endregion
	}

}
