using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameObject[] scrolls;
    public Player player;

	//// Use this for initialization
	//void Start () {

	//}
	
	// Update is called once per frame
	void Update () {
        UpdateHealthBar();
	}

    void UpdateHealthBar()
    {
        float maxIdx = player.HPScale * scrolls.Length;

        for (int i = 0; i < scrolls.Length; i++)
        {
            scrolls[i].gameObject.SetActive(i < maxIdx);
        }

    }
}
