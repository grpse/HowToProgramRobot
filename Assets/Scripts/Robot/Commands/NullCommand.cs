using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCommand : IRobotCommand {
    public float executeAfter()
    {
        return 0f;
    }

    public bool executeBetween()
    {
        return false;
    }

    public void executeOn(GameObject gameObject)
    {
        
    }

    public bool isExecuting()
    {
        return false;
    }

    public void passParameters(params object[] args)
    {
    }

    public void run()
    {
    }

    public void update()
    {
    }
    
}
