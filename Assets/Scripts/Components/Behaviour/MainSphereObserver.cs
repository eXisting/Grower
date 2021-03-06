﻿using GameManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainSphereObserver : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.StartGame();

        gameObject.GetComponent<MainSphereObserver>().enabled = false;
    }
}
