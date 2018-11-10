using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.AbstractClasses;
using Base.Interfaces;
using Components;
using UnityEngine;

namespace GameManagers
{

	public class BallsObserver : MonoBehaviour
	{
		private Dictionary<int, IMoveableBall> balls = new Dictionary<int, IMoveableBall>();

		private Vector3 TargetPosition;

		private void Start()
		{
			GameManager.Instance.SpawnManager.OnBallsAddFinished += StartMoveBalls;
			TargetPosition = GameManager.Instance.MainSphere.transform.localPosition;
		}

		public void Add(int _id, IMoveableBall _ball)
		{
			balls[_id] = _ball;
		}

		public void Remove(int _id)
		{
			balls.Remove(_id);
		}

        public void DestroyAllBalls()
        {
            balls.Clear();
        }

		private void StartMoveBalls(int _amount)
		{
			if (balls.Count == _amount)
				StartCoroutine(QueueableMove(_amount));
			else
				Debug.LogErrorFormat("Cannot start game! There is balls missmatch. Need: {0}, Present: {1}", _amount, balls.Count);
		}

		private IEnumerator QueueableMove(int _amount)
		{
			for (int ID = 0; ID < _amount; ID++)
			{
				RegularBall ball = (RegularBall)balls[ID];

				yield return new WaitForSeconds(0.5f);
				ball.Move(TargetPosition);
			}
		}		
	}

}
