using Components;
using GameManagers;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainSphereObserver : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private MainBall main;

    private void Start()
    {
        AddListinerToBall(ToogleObserver);
    }

    public void AddListinerToBall(Action _callBack)
	{
		main.OnMainSphereDeath += _callBack;
	}

    public void RemoveListinerToBall(Action _callBack)
    {
        main.OnMainSphereDeath -= _callBack;
    }

    public Vector3 BallLocalPosition()
	{
		return main.transform.localPosition;
	}

	public void ToogleObserver()
	{
		this.enabled = !this.enabled;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.GameOver)
        {
            GameManager.Instance.RestartGame();
        }

        main.CalculateHitsToDeath();
        ToogleObserver();

        GameManager.Instance.StartGame();
	}
}
