using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoScreen : MonoBehaviour
{
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        count++;
        if (count > 522)
            SceneManager.LoadScene(1);
    }
}
