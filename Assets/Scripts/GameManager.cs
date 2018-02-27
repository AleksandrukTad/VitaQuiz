using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public List<Question> questions;
	private static List<Question> unansweredQuestion;
	private Question currentQuestion;
	private int currentRound = 0;
	private int maxRound;
	private int rnd;
	private int amountOfQuestions;
	private int points;
	private bool fb = true;

	[SerializeField]
	private GameObject UIquestion;
	[SerializeField]
	private Button UIanswerAtxt;
	[SerializeField]
	private Button UIanswerAimg;
	[SerializeField]
	private Button UIanswerBtxt;
	[SerializeField]
	private Button UIanswerBimg;
	[SerializeField]
	private Button UIanswerCtxt;
	[SerializeField]
	private Button UIanswerCimg;
	[SerializeField]
	private Button UIanswerDtxt;
	[SerializeField]
	private Button UIanswerDimg;
	[SerializeField]
	private Text RoundXOutOfRoundsY;
	[SerializeField]
	private Text Points;
	[SerializeField]
	private GameObject end;
	[SerializeField]
	private GameObject answerA;
	[SerializeField]
	private GameObject answerB;
	[SerializeField]
	private GameObject answerC;
	[SerializeField]
	private GameObject answerD;
	[SerializeField]
	private Text endPoints;

	//
	Text questionTxt;
	Text answerATxt;
	Text answerBTxt;
	Text answerCTxt;
	Text answerDTxt;

	Image questionIMG;
	Image answerAIMG;
	Image answerBIMG;
	Image answerCIMG;
	Image answerDIMG;
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android ) {
			if (Input.GetKey (KeyCode.Escape)) {
				//Destroy (this);
				SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
			}
		}
	}
	void Start()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		maxRound = 20;
		amountOfQuestions = 20;
		questionTxt = UIquestion.GetComponentInChildren<Text> ();
		answerATxt = UIanswerAtxt.GetComponentInChildren<Text> ();
		answerBTxt = UIanswerBtxt.GetComponentInChildren<Text> ();
		answerCTxt = UIanswerCtxt.GetComponentInChildren<Text> ();
		answerDTxt = UIanswerDtxt.GetComponentInChildren<Text> ();

		questionIMG = UIquestion.GetComponentInChildren<Image> ();
		answerAIMG = UIanswerAimg.GetComponentInChildren<Image> ();
		answerBIMG = UIanswerBimg.GetComponentInChildren<Image> ();
		answerCIMG = UIanswerCimg.GetComponentInChildren<Image> ();
		answerDIMG = UIanswerDimg.GetComponentInChildren<Image> ();

		if (unansweredQuestion == null || unansweredQuestion.Count == 0) {
			unansweredQuestion = new List<Question> ();
			//Filling current unansweredQuestions(which represents Question in current game).
			while (amountOfQuestions != 0) {
				rnd = Random.Range (0, questions.Count);
				unansweredQuestion.Add (questions [rnd]);
				//Preventing from doubling the questions
				questions.RemoveAt (rnd);
				amountOfQuestions--;
			}
		}
		questionIMG.enabled = false;
		questionTxt.enabled = false;
		answerATxt.enabled = false;
		answerBTxt.enabled = false;
		answerCTxt.enabled = false;
		answerDTxt.enabled = false;
		answerAIMG.enabled = false;
		answerBIMG.enabled = false;
		answerCIMG.enabled = false;
		answerDIMG.enabled = false;
		end.SetActive (false);
		Debug.Log (unansweredQuestion.Count);

		setRounds ();
		setPoints ();
		StartCoroutine(setCurrentQuestion ());
		setCurrentAnswers ();
	}

	private IEnumerator setCurrentQuestion()
	{	
		currentQuestion = unansweredQuestion.First ();
		if (currentQuestion.question.Contains ("img")) {
			if (!questionIMG.IsActive ()) {
				questionTxt.enabled = false;
				questionIMG.enabled = true;
			}
			Texture2D temp = new Texture2D (0, 0);
			WWW www = new WWW (Application.streamingAssetsPath + "/Questions/"+ currentQuestion.question);

			yield return www; 

			temp = www.texture;
			questionIMG.sprite = Sprite.Create (temp, new Rect (0, 0, temp.width, temp.height), new Vector2 (0, 0));

		} else {
			if (!questionTxt.IsActive ()) {
				questionIMG.enabled = false;
				questionTxt.enabled = true;
			}
			questionTxt.text = currentQuestion.question;
		}
	}
	private void setCurrentAnswers()
	{
		StartCoroutine(setCurrentAnswer (currentQuestion.answerA, answerAIMG, answerATxt));
		StartCoroutine(setCurrentAnswer (currentQuestion.answerB, answerBIMG, answerBTxt));
		StartCoroutine(setCurrentAnswer (currentQuestion.answerC, answerCIMG, answerCTxt));
		StartCoroutine(setCurrentAnswer (currentQuestion.answerD, answerDIMG, answerDTxt));
		unansweredQuestion.Remove (currentQuestion);
	}
	//Function to populate answer form A to D
	private IEnumerator setCurrentAnswer(string investigatedAnswer, Image investigatedAIMG, Text investigatedATxt)
	{
		if (investigatedAnswer.Contains ("img")) {
			if (!investigatedAIMG.IsActive ()) {
				investigatedAIMG.enabled = true;
				investigatedATxt.enabled = false;
			}
			Texture2D temp = new Texture2D (0, 0);
			WWW www = new WWW (Application.streamingAssetsPath + "/Answers/"+ investigatedAnswer);
			yield return www;

			temp = www.texture; 
			investigatedAIMG.sprite = Sprite.Create (temp, new Rect (0, 0, temp.width, temp.height), new Vector2 (0, 0));

		}else {
			if (!investigatedATxt.IsActive ()) {
				investigatedAIMG.enabled = false;
				investigatedATxt.enabled = true;
			}
			investigatedATxt.text = investigatedAnswer;
		}
	}
	private void setRounds()
	{
		RoundXOutOfRoundsY.text = "Pytanie: " + (currentRound + 1) + "/" + maxRound;
	}
	private void setPoints()
	{
		Points.text = "Punkty: " + points;
	}
	private string finalScore(){
		if (points > 10) {
			return endPoints.text = "Gratulacje, twój wynik to: " + points + " z " + maxRound;
		} else if (points <= 10 && points != 0) {
			return endPoints.text = "Wynik to: " + points + " z " + maxRound + ". Następnym razem będzie lepiej!";
		} else if (points == 0) {
			return endPoints.text = "Wynik to: " + points + " z " + maxRound + ". Chyba musisz poćwiczyć.";
		} else {
			Debug.Log (points);
			return "błąd";
		}
	}
	public void UserSelected(string answer){
		Debug.Log ("corr answer: " + currentQuestion.corrAnswer);
		Debug.Log ("your answer: " + answer);
		currentRound++;
		StartCoroutine(FeedBack ());
		StartCoroutine (wait (answer));
	}
	private IEnumerator wait(string answer){
		while (fb) {
			yield return new WaitForSeconds (0.1f);
		}
		summingUpTheRound (answer);
	}
	private void summingUpTheRound(string answer){
		//Correct answer
		if (answer == currentQuestion.corrAnswer) {
			points++;
			//end of the game
			if (currentRound == maxRound) {
				endPoints.text = finalScore ();
				end.SetActive (true);
			}
			//game still going
			else if(currentRound < maxRound) {
				setRounds ();
				StartCoroutine(setCurrentQuestion ());
				setCurrentAnswers ();
			}
		}
		//Wrong answer
		else {
			//end of the game
			if (currentRound == maxRound) {
				endPoints.text = finalScore ();
				end.SetActive (true);
			}
			//game still going
			else if(currentRound < maxRound) {
				setRounds ();
				StartCoroutine(setCurrentQuestion ());
				setCurrentAnswers ();
			}
		}
		setPoints ();
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
			
			Debug.Log ("img not found");
		}
		return tex;
	}
	private IEnumerator FeedBack(){
		fb = true;
		if (currentQuestion.corrAnswer == "A") {
			answerA.SetActive (true);
			answerA.GetComponent<Image> ().color = Color.green;
		}
		if (currentQuestion.corrAnswer == "B") {
			answerB.SetActive (true);
			answerB.GetComponent<Image> ().color = Color.green;
		}
		if (currentQuestion.corrAnswer == "C") {
			answerC.SetActive (true);
			answerC.GetComponent<Image> ().color = Color.green;
		}
		if (currentQuestion.corrAnswer == "D") {
			answerD.SetActive (true);
			answerD.GetComponent<Image> ().color = Color.green;
		}
		yield return new WaitForSeconds (2);
		answerA.SetActive (false);
		answerB.SetActive (false);
		answerC.SetActive (false);
		answerD.SetActive (false);
		fb = false;
	}
}
