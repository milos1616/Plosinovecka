﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayers : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, /*playerTransform.position.y*/ this.transform.position.y + Time.deltaTime*speed);
    }
}
