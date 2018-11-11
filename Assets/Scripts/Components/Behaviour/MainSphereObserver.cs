using Components;
using GameManagers;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainSphereObserver : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private MainBall main;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.StartGame();
		main.CalculateHitsToDeath();

        gameObject.GetComponent<MainSphereObserver>().enabled = false;
    }
}
