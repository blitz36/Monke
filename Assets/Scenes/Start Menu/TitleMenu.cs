using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject Cinematic;
    private bool isCinematic = false;

  void Awake() {
    if (isCinematic == false) {
      isCinematic = true;
      Cinematic.SetActive(true);
    }
  }

  void Update() {
    if (isCinematic = true) {
      if (Input.GetKeyDown("space"))
      {
        Cinematic.SetActive(false);
      }
    }
  }

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
