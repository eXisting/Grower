using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Interfaces
{
	interface IMoveableBall
	{
		void Move();

		event Action OnMoveBegan;
		event Action OnMoveEnded;
	}
}
