using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace X_Utils.Events
{
    public class IntEventDataHolder : EventDataHolder<int>
    {
        [System.Serializable]
        public class DataEvent : UnityEvent<int>
        {
        
        }
        [SerializeField]
        public DataEvent dataEvent;
    
        public void Invoke()
        {
            dataEvent.Invoke(data);
        }
    
        
        public void SetData(string rawData)
        {
            int.TryParse(rawData, out this.data);
        }
    }
}