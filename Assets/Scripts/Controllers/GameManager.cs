using UnityEngine;
using System;

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
		private GameObject mainSphere;

		public int Multiplier { get; private set; }
		public int Level { get; private set; }

		public GameObject MainCanvas { get => mainCanvas; private set => mainCanvas = value; }
		public GameObject MainSphere { get => mainSphere; private set => mainSphere = value; }
		public BallsObserver BallsObserver { get => ballsObserver; private set => ballsObserver = value; }
		public SpawnManager SpawnManager { get => spawnManager; private set => spawnManager = value; }
		public DataDisplayManager DataDisplayManager { get => dataDisplayManager; private set => dataDisplayManager = value; }

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
			Randomizer = new System.Random();

			Instance = this;

			DontDestroyOnLoad(this);
		}

		public void StartGame()
		{
			SpawnManager.StartAddBalls();
		}
	}

}
