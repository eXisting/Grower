using UnityEngine;
using System;

namespace GameManagers
{

	public class GameManager : MonoBehaviour
	{
		static public GameManager Instance;

		#region Initial stuff

		private GameManager() { }

		[SerializeField]
		private SpawnManager spawnManager;

		[SerializeField]
		private DataDisplayManager dataDisplayManager;

		public SpawnManager SpawnManager
		{
			get => spawnManager;
			set
			{
				if (spawnManager == null)
					spawnManager = value;
			}
		}

		public DataDisplayManager DataDisplayManager
		{
			get => dataDisplayManager;
			set
			{
				if (spawnManager == null)
					dataDisplayManager = value;
			}
		}

		public int Multiplier { get; private set; }

		public int Level { get; private set; }

		#endregion

		public Action<int> OnBallKill;
		public Action<int> OnLevelChange;
		public Action<int> OnBallsCountChange;

		private void Awake()
		{
			Initialize();
		}

		private void Update()
		{
			if (Input.GetKeyDown("space"))
				StartGame();
		}

		private void Initialize()
		{
			Screen.orientation = ScreenOrientation.Landscape;
			Level = 1;
			Multiplier = 5;

			Instance = this;

			DontDestroyOnLoad(this);
		}

		public void StartGame()
		{
			spawnManager.StartAddBalls();

			// TODO: Make balls move
		}
	}

}
