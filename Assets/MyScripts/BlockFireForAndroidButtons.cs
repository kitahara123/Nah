using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BlockFireForAndroidButtons : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

	public UnityEvent OnClickDown;



	void Start () {

		if (OnClickDown == null)
			OnClickDown = new UnityEvent();
		
	}

	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnClickDown.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnClickDown.Invoke();
	}
}
