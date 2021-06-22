using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    public void StartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale=1f;
    }
	
	public void QuitGame ()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

}
