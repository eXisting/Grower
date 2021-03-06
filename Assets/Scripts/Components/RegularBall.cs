﻿using Base.AbstractClasses;
using Base.Interfaces;
using Enums;
using GameManagers;
using System;
using System.Collections;
using UnityEngine;

namespace Components
{
	public class RegularBall : Ball, IMoveableBall, IDestroyableBall
	{
		public event Action OnMovementStart;
		public event Action OnBallDestroy;

		public int Points;
		public float MoveSpeed;

		public void InitBall()
		{
			BallColor color = (BallColor)GameManager.Instance.Randomizer.Next((int)BallColor.Purple, (int)BallColor.Orange);
			// TODO: Make better generation
			this.Color = ColorGenerator.Get(color);

			// TODO: Add in-game types
			this.Radius = GameManager.Instance.Randomizer.Next(55, 100);
			this.MoveSpeed = GameManager.Instance.Level * 500;
			this.Points = GameManager.Instance.Level * (int)color;
		}

		public void Move(Vector3 _target)
		{
			//Debug.Log("Item is about to move");

			StartCoroutine(Movement(_target));
		}

		public void DestroyBall()
		{
			//Debug.Log("Item is about to be destroyed");

			GameManager.Instance.OnBallsCountChange(-1);
			OnBallDestroy?.Invoke();
			StopCoroutine("Movement");
			Destroy(this.gameObject);
		}

		private void OnMouseDown()
		{
            GameManager.Instance.OnBallKill(Points);
			
			DestroyBall();
		}

		private IEnumerator Movement(Vector3 _target)
		{
			OnMovementStart?.Invoke();

			while (transform.localPosition != _target)
			{
				//var move = transform.DOLocalMove(_target, 15f);
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target, MoveSpeed * Time.deltaTime);

				yield return new WaitForFixedUpdate();
			}
		}
	}
}
