using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeomCatch
{
    public enum EventType { playerTap, coinFallingDown, coinCached }

    public interface IListener
    {
        void OnEvent(EventType eventType, Component sender, Object param = null);
    } 
}
