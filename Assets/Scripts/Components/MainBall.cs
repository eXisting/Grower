using Base.AbstractClasses;
using Base.Interfaces;
using GameManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Components
{
	public class MainBall : Ball, IDestroyableBall
	{
		public event Action<IDestroyableBall> OnBallDestroy;
		public event Action OnMainSphereDeath;

		private int HitsToDeath;
		
		public void CalculateHitsToDeath()
		{
			HitsToDeath = GameManager.Instance.Level * GameManager.Instance.InitialHitsCount;
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (HitsToDeath == 1)
			{
				OnBallDestroy?.Invoke(this);
				OnMainSphereDeath?.Invoke();
			}
            else
            {
			    RegularBall ball = collision.gameObject.GetComponent<RegularBall>();
			    if (ball != null)
				    ball.DestroyBall();
			    else
				    Destroy(collision.gameObject);
            }

			--HitsToDeath;
		}

		public void DestroyBall()
		{
			throw new NotImplementedException();
		}
	}
}
