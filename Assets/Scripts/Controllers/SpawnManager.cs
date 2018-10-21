using GameSystems;
using Spheres.Core;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

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

		private Rect mainCanvasRect;

		#endregion

		private ViewBounds viewBounds;

		private EntityManager manager;
		
		private System.Random rand;

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
			int amount = GameManager.Instance.Level * GameManager.Instance.Multiplier;

			NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
			manager.Instantiate(ball, entities);

			for (int i = 0; i < amount; i++)
			{
				manager.SetComponentData(
					entities[i], 
					new LocalPosition { Value = CalculateBallPosition() }
				);

				manager.SetComponentData(
					entities[i],
					new Rotation { Value = new quaternion(0,0,0,0) }
				);

				// TODO: come up with formulas:

				manager.SetComponentData(
					entities[i], 
					new MoveSpeed { speed = initialMoveSpeed * GameManager.Instance.Level }
				);

				//manager.SetComponentData(
				//	entities[i], 
				//	new Size { Value = initialBallSize * GameManager.Instance.Level }
				//);

			}

			entities.Dispose();
		}


		private Vector3 CalculateBallPosition()
		{
			float yMinRange = UnityEngine.Random.Range(
				viewBounds.yBotBound,
				viewBounds.yBotBound - maxVerticalOffset
			);

			float yMaxRange = UnityEngine.Random.Range(
				viewBounds.xRightBound,
				viewBounds.xRightBound + maxVerticalOffset
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

			//balls = new TransformAccessArray(GameManager.Instance.Level * GameManager.Instance.Multiplier);
			mainCanvasRect = mainCanvas.GetComponent<RectTransform>().rect;

			RememberViewBounds();

			manager = World.Active.GetOrCreateManager<EntityManager>();
		}

		#endregion
	}

}
