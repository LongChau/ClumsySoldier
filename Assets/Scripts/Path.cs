using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Transform[] nodes;
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    private void OnDrawGizmos()
    {
        if (nodes == null || nodes.Length == 0)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nodes[0].transform.position, .5f);
        for (int i = 1; i < nodes.Length; i++)
        {
            Gizmos.DrawLine(nodes[i].transform.position, nodes[i - 1].transform.position);
        }
    }

    private void Reset()
    {
        nodes = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
    }
}
