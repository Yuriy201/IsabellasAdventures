using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControl : MonoBehaviour
{
    bool left, right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveLeft()
    {
        left = true;
    }

    public void MoveRight()
    {
        right = true;
    }

    public void MoveStop()
    {
        left = false;
        right = false;
    }
}
