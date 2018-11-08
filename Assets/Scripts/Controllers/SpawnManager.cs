using Base.AbstractClasses;
using Components;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		private int maxVerticalOffset;

		[SerializeField]
		private float initialBallSize;

		[SerializeField]
		private float initialMoveSpeed;
		
		[Header("Bot prefab")]
		[SerializeField]
		private GameObject ball;
		
		#endregion

		private int amount;

		private ViewBounds viewBounds;
		private Rect mainCanvasRect;

		public event Action<int> OnBallsAddFinished;

		#region Core functions

		private void Awake()
		{
			Initialize();
		}
		
		public void StartAddBalls()
		{
			StartCoroutine(AddBalls());
		}

		IEnumerator AddBalls()
		{
            amount = GameManager.Instance.Level * GameManager.Instance.Multiplier;

            GameManager.Instance.OnLevelChange(GameManager.Instance.Level);
			//TODO: Call 2 functions for clear balls and show remaining in progress view

			for (int ID = 0; ID < amount; ID++)
			{
				Vector3 ballPosition = CalculateBallPosition();

				GameObject obj = Instantiate(this.ball, GameManager.Instance.MainCanvas.transform);
				
				RegularBall r_Ball = obj.AddComponent<RegularBall>();
				r_Ball.InitBall();

				obj.AddComponent<EventTrigger>();
				
				obj.GetComponent<Renderer>().material.color = r_Ball.Color;
				obj.transform.localPosition = ballPosition;
				obj.transform.localScale = new Vector3(r_Ball.Radius, r_Ball.Radius, r_Ball.Radius);

				GameManager.Instance.BallsObserver.Add(ID, r_Ball);

				yield return new WaitForFixedUpdate();
			}
			
			OnBallsAddFinished?.Invoke(amount);
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
				GameManager.Instance.Randomizer.Next(0, 2) < 1 ? xMinRange : xMaxRange,
				GameManager.Instance.Randomizer.Next(0, 2) < 1 ? yMinRange : yMaxRange
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
			mainCanvasRect = GameManager.Instance.MainCanvas.GetComponent<RectTransform>().rect;

			RememberViewBounds();

			GenerateObjectPool();
		}

		private void GenerateObjectPool()
		{
			
		}

		#endregion
	}

}
