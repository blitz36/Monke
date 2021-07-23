using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void QuitToMenu ()
    {
    SceneManager.LoadScene(0);
		Time.timeScale=1f;
    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
    GameObject[] objs2 = GameObject.FindGameObjectsWithTag("MainCamera");
    Destroy(objs[0]);
    Destroy(objs[0].transform.parent.gameObject);
    Destroy(objs2[0]);


    }

	public void QuitGame ()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

}
