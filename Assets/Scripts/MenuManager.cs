using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	void Start()
	{
		Time.timeScale = 1;
		Screen.orientation = ScreenOrientation.Portrait;
	}
	public void switchScene(string scene)
	{
		SceneManager.LoadScene (scene, LoadSceneMode.Single);
	}
}
