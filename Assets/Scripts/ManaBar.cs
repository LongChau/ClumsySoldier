using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour {

    public GameObject[] gems;
    public Player player;

	//// Use this for initialization
	//void Start () {

	//}
	
	// Update is called once per frame
	void Update () {
        UpdateManaBar();
	}

    void UpdateManaBar()
    {
        float maxIdx = player.ManaScale * gems.Length;

        for (int i = 0; i < gems.Length; i++)
        {
            gems[i].gameObject.SetActive(i <= maxIdx);
        }
    }
}
