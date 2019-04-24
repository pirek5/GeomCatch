using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeomCatch
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<EventType, List<IListener>> listeners = new Dictionary<EventType, List<IListener>>();

        public void AddListener(EventType eventType, IListener listener)
        {
            List<IListener> listenerList = null;

            if(listeners.TryGetValue(eventType, out listenerList))
            {
                listenerList.Add(listener);
            }
            else
            {
                listenerList = new List<IListener>();
                listenerList.Add(listener);
                listeners.Add(eventType, listenerList);
            }
        }

        public void PostNotification(EventType eventType, Component sender, Object param = null)
        {
            if (listeners.ContainsKey(eventType))
            {
                foreach(var listener in listeners[eventType])
                {
                    listener.OnEvent(eventType, sender, param);
                }
            }
        }

    } 
}
