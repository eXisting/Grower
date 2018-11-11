using UnityEngine;
using System;
using Components;

namespace GameManagers
{

	public class GameManager : MonoBehaviour
	{
		static public GameManager Instance;

		#region Initial stuff

		private GameManager() { }

		// TODO: Come up with pseudo-random
		public System.Random Randomizer { get; private set; }

		[SerializeField]
		private SpawnManager spawnManager;

		[SerializeField]
		private DataDisplayManager dataDisplayManager;

		[SerializeField]
		private BallsObserver ballsObserver;
		
		[Header("Main canvas")]
		[SerializeField]
		private GameObject mainCanvas;

		[Header("Main sphere")]
		[SerializeField]
		private MainSphereObserver mainSphereObserver;

		public int Multiplier { get; private set; }
		public int Level { get; private set; }
		public int InitialHitsCount { get; private set; }

		public GameObject MainCanvas { get { return mainCanvas; } private set { mainCanvas = value; } }
		public MainSphereObserver SphereObserver { get { return mainSphereObserver; } private set { mainSphereObserver = value; } }
		public BallsObserver BallsObserver { get { return ballsObserver; } private set { ballsObserver = value; } }
		public SpawnManager SpawnManager { get { return spawnManager; } private set { spawnManager = value; } }
		public DataDisplayManager DataDisplayManager { get { return dataDisplayManager; } private set { dataDisplayManager = value; } }

		#endregion

		public Action<int> OnBallKill;
		public Action<int> OnLevelChange;
		public Action<int> OnBallsCountChange;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			Screen.orientation = ScreenOrientation.Landscape;
			Level = 0;
			Multiplier = 20;
			InitialHitsCount = 5;
			Randomizer = new System.Random();

			Instance = this;

			DontDestroyOnLoad(this);

			OnLevelChange += IncreaseLevel;
		}

		public void StartGame()
		{
			SpawnManager.StartAddBalls();
		}

		private void IncreaseLevel(int _additional)
		{
			Level += _additional;
		}
	}

}
