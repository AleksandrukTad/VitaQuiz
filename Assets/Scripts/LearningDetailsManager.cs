using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LearningDetailsManager : MonoBehaviour {

	public List<Learning> learningList;
	[SerializeField]
	private Text name;
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text description;

	private string n;

	private Learning l;
	// Use this for initialization

	void Start()
	{
		Time.timeScale = 1; 
		Screen.orientation = ScreenOrientation.Portrait;
		StartCoroutine (start());
	}
	IEnumerator start()
	{
		n = GameObject.Find ("LearningMenu").GetComponentInChildren<Text> ().text;
		l = learningList.Find (learning => learning.name == n);
		name.text = l.name;
		Texture2D temp = new Texture2D (0, 0);
		WWW www = new WWW (Application.streamingAssetsPath + "/Learn/"+ l.image);
		yield return www;
		temp = www.texture;
		Debug.Log (www.url);
		image.sprite = Sprite.Create (temp, new Rect (0, 0, 598, 492), new Vector2 (0, 0));
		description.text = l.description;
	}
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				DestroyObject (GameObject.Find ("LearningMenu"));
				SceneManager.LoadScene ("Learning", LoadSceneMode.Single);
			}
		}
	}
	private Texture2D loadPNG(string path)
	{
		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists (path)) {
			fileData = File.ReadAllBytes (path);
			tex = new Texture2D (2, 2);
			tex.LoadImage (fileData); //..this will auto-resize the texture dimensions.
		} else {
			Debug.Log (path);
			Debug.Log ("img not found");
		}
		return tex;
	}
}
