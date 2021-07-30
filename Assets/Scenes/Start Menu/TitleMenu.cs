using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject player;
    public void StartGame ()
    {
      Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		  Time.timeScale=1f;
     }

	public void QuitGame ()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
