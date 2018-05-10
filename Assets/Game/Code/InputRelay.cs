//-----------------------------------------------------------------------
// <copyright file="InputRelay.cs" company="Google">
//
// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnPointerClickEvent : UnityEvent<PointerEventData> { }
public class OnPointerEnterEvent : UnityEvent<PointerEventData> { }
public class OnPointerDownEvent : UnityEvent<PointerEventData> { }
public class OnPointerDragEvent : UnityEvent<PointerEventData> { }
public class OnPointerUpEvent : UnityEvent<PointerEventData> { }
public class OnPointerExitEvent : UnityEvent<PointerEventData> { }


public class InputRelay : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {


    public OnPointerClickEvent onPointerClick = new OnPointerClickEvent();
    public OnPointerEnterEvent onPointerEnter = new OnPointerEnterEvent();
    public OnPointerDownEvent onPointerDown = new OnPointerDownEvent();
    public OnPointerDragEvent onPointerDrag = new OnPointerDragEvent();
    public OnPointerUpEvent onPointerUp = new OnPointerUpEvent();
    public OnPointerExitEvent onPointerExit = new OnPointerExitEvent();


    void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        onPointerClick.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        onPointerEnter.Invoke(eventData);
    }
        
    public void OnPointerDown(PointerEventData eventData) {
        onPointerDown.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        onPointerDrag.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData) {
        onPointerUp.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData) {
        onPointerExit.Invoke(eventData);
    }


}
