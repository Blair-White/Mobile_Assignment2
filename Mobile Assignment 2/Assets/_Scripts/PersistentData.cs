using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private int PlayerScore;
    private int PlayerLives;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerScore = 0;
        PlayerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
