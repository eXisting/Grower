using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagers
{

	public class DataDisplayManager : MonoBehaviour
	{
		#region Serialized fields

		[SerializeField]
		private Text level;

		[SerializeField]
		private Text score;

		[SerializeField]
		private Text ballsPresentCount;

		#endregion

		private void Awake()
		{
			ClearTextFields();
			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		public void UpdateLevelText(int _value)
		{
			level.text = ValueToDisplayText(level, _value);
		}

		public void UpdateScoreText(int _value)
		{
			score.text = ValueToDisplayText(score, _value);
		}

		public void UpdateBallsCountText(int _value)
		{
			ballsPresentCount.text = ValueToDisplayText(ballsPresentCount, _value);
		}

		private void Subscribe()
		{
			GameManager.Instance.OnBallKill += UpdateScoreText;
			GameManager.Instance.OnLevelChange += UpdateLevelText;
			GameManager.Instance.OnBallsCountChange += UpdateBallsCountText;
            GameManager.Instance.SphereObserver.AddListinerToBall(ClearTextFields);
        }

        private void Unsubscribe()
		{
			GameManager.Instance.OnBallKill -= UpdateScoreText;
			GameManager.Instance.OnLevelChange -= UpdateLevelText;
			GameManager.Instance.OnBallsCountChange -= UpdateBallsCountText;
            GameManager.Instance.SphereObserver.RemoveListinerToBall(ClearTextFields);

        }

		private string ValueToDisplayText(Text _text, int _value)
		{
			return (Convert.ToInt32(_text.text) + _value).ToString();
		}

		private void ClearTextFields()
		{
			level.text = score.text = ballsPresentCount.text = "0";
		}
	}

}
