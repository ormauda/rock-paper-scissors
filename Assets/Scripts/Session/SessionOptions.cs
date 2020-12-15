using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionOptions : MonoBehaviour
{
    [SerializeField] int initialLife = 3;
    [SerializeField] int initialScore = 0;

    public int GetInitialLife()
    {
        return initialLife;
    }

    public int GetInitialScore()
    {
        return initialScore;
    }
}
