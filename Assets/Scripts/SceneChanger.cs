using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName = "";

    // Start is called before the first frame update
    void Start()
    {   
    }


    // Update is called once per frame
    void Update()
    {
    }


    //Reloads the scene whenever player falls from the world
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
