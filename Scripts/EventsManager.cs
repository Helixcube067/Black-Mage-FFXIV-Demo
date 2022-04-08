using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static UnityEvent OnTargeted;
    void Awake()
    {
        if (OnTargeted == null)
            OnTargeted = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
