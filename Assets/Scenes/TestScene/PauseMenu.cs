using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void QuitToMenu ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		Time.timeScale=1f;
    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
    Destroy(objs[0]);


    }

	public void QuitGame ()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

}
