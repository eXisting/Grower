﻿using UnityEngine;
using UnityEngine.Jobs;
using GameSystems;
using Unity.Jobs;
using DG.Tweening;
using Unity.Entities;
using Unity.Collections;
using Spheres.Core;
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

		public int Multiplier { get; }

		public int Level { get; }

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