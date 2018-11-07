using Base.AbstractClasses;
using Base.Interfaces;
using Enums;
using GameManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Components
{
	public class RegularBall : Ball, IDestroyableBall, IMoveableBall
	{
		public event Action OnMoveBegan;
		public event Action OnMoveEnded;
		public event Action OnBallDestroy;

		public float MoveSpeed;

		public void Destroy()
		{
			Debug.Log("Item is about to be destroyed");
		}

		public void Move(Vector3 _target)
		{
			Debug.Log("Item is about to move");

			StartCoroutine(BallMovement(_target));
		}

		private IEnumerator BallMovement(Vector3 _target)
		{
			while (transform.localPosition != _target)
			{
				Debug.Log("Item is moving");

				transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target, MoveSpeed * Time.deltaTime);

				yield return new WaitForFixedUpdate();
			}
		}

		public void InitBall()
		{
			// TODO: Add in-game types
			this.Radius = GameManager.Instance.Randomizer.Next(55, 100);
			this.MoveSpeed = GameManager.Instance.Level * 100;

			// TODO: Make better generation
			this.Color = ColorGenerator.Get((BallColor)GameManager.Instance.Randomizer.Next((int)BallColor.Purple, (int)BallColor.Orange));
		}
	}
}
