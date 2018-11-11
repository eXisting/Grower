using Base.AbstractClasses;
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
		public event Action<IDestroyableBall> OnBallDestroy;

		public int ID;
		public int Points;
		public float MoveSpeed;

		public void InitBall()
		{
			BallColor color = (BallColor)GameManager.Instance.Randomizer.Next((int)BallColor.Purple, (int)BallColor.Orange);
			// TODO: Make better generation
			this.Color = ColorGenerator.Get(color);

			// TODO: Add in-game types
			this.Radius = GameManager.Instance.Randomizer.Next(80, 140);
			this.MoveSpeed = GameManager.Instance.Level * 100;
			this.Points = (GameManager.Instance.Level + 1) * (int)color;
		}

		public void Move(Vector3 _target)
		{
			StartCoroutine(Movement(_target));
		}

		public void DestroyBall()
		{
			GameManager.Instance.OnBallsCountChange(-1);
			OnBallDestroy?.Invoke(this);

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
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target, MoveSpeed * Time.deltaTime);

				yield return new WaitForFixedUpdate();
			}
		}
	}
}
