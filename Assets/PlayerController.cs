using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GeomCatch
{
    

    public class PlayerController : MonoBehaviour, IListener
    {
        [Inject] EventManager eventManager;

        private bool isControllerEnabled;
        private int lastRecordedTouchesCount;

        private void Start()
        {
            isControllerEnabled = true;
            eventManager.AddListener(EventType.playerTap, this);
            eventManager.AddListener(EventType.coinCached, this);
        }

        public void OnEvent(EventType eventType, Component sender, Object param = null)
        {
            if(eventType == EventType.playerTap)
            {
                isControllerEnabled = false;
            }
            else if(eventType == EventType.coinCached)
            {
                isControllerEnabled = true;
            }
        }

        void Update()
        {
            if(!isControllerEnabled) { return; }

            //if (Input.touchCount > lastRecordedTouchesCount)
            //{
            //    eventManager.PostNotification(EventType.coinFallingDown, this);
            //}
            //lastRecordedTouchesCount = Input.touchCount;
            if (Input.anyKeyDown)
            {
                print("keydown");
                eventManager.PostNotification(EventType.playerTap, this);
            }
        }
    } 
}
