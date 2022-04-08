using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Renderer enemyRender;

    void Start()
    {
        enemyRender = GetComponent<Renderer>();
        EventsManager.OnTargeted.AddListener(ChangeTarget);
    }

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        PlayerStats.target = this;
        if(EventsManager.OnTargeted != null)
            EventsManager.OnTargeted.Invoke();
    }

    void ChangeTarget() {
        if (PlayerStats.target == this)
            enemyRender.material.color = Color.red;
        else
            enemyRender.material.color = Color.white;
    }
}
