using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Interfaces
{
	public interface IDestroyableBall
	{
		void DestroyBall();

		event Action OnBallDestroy;
	}
}
