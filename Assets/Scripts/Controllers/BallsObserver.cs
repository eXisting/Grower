using System.Collections;
using System.Collections.Generic;
using Base.Interfaces;
using Components;
using UnityEngine;

namespace GameManagers
{

	public class BallsObserver : MonoBehaviour
	{
		private Dictionary<int, IMoveableBall> balls = new Dictionary<int, IMoveableBall>();

		private Vector3 TargetPosition;

		public void Add(int _id, IMoveableBall _ball)
		{
			balls[_id] = _ball;
		}

		private void Start()
		{
			GameManager.Instance.SpawnManager.OnBallsAddFinished += StartMoveBalls;
			//GameManager.Instance.SphereObserver.AddListinerToBall(ClearBalls);

			TargetPosition = GameManager.Instance.SphereObserver.BallLocalPosition();
		}
		
		private void Remove(IDestroyableBall _ball)
		{
			RegularBall ball = (RegularBall)_ball;
			balls.Remove(ball.ID);

			if (balls.Count == 0)
			{
				// TODO: Come up with several game modes
				GameManager.Instance.SphereObserver.ToogleObserver();
			}
		}

		private void ClearBalls()
		{
			StopAllCoroutines();

			foreach (var pair in balls)
			{
				RegularBall ball = (RegularBall)pair.Value;
				Destroy(ball.gameObject);
			}

			balls.Clear();

			Debug.ClearDeveloperConsole();
			Debug.Log("Game is over!");
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
				ball.OnBallDestroy += Remove;

				yield return new WaitForSeconds(0.5f);
				ball.Move(TargetPosition);
			}
		}
	}

}
