using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialMenuController : MonoBehaviour {
	public List<UIThing> choices;
	public string message;
	public GameObject recipient;

	private List<Image> choiceImages;
	private List<Text> choiceTexts;
	private Object activeChoice = null;
	private Canvas canvas;
	public Image centerImage;
	public Image choiceImage;
	public Text choiceText;

	private bool menuOpen = false;
	private Dictionary<Object, Object> choiceLookup;
	// Use this for initialization
	void Start () {
		canvas = GetComponentInParent<Canvas> ();
		//gameObject.SendMessage (message);
		choiceLookup = new Dictionary<Object, Object>();
		choiceImages = new List<Image>();
		choiceTexts = new List<Text> ();

		for (int i = 0; i < choices.Count; i++) {
			Image tempBG = (Image)GameObject.Instantiate (choiceImage, this.transform);
			Image temp = (Image)GameObject.Instantiate (choices [i].uiImage, this.transform);

			Text tempText = (Text)GameObject.Instantiate (choiceText, this.transform);
			tempText.text = choices [i].uiName;
			tempBG.rectTransform.localPosition = new Vector3 (Mathf.Cos (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, Mathf.Sin (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, 0f);
			temp.rectTransform.localPosition = new Vector3 (Mathf.Cos (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, Mathf.Sin (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, 0f);
			tempText.rectTransform.localPosition = new Vector3 (Mathf.Cos (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, Mathf.Sin (((float)i / (float)choices.Count) * (Mathf.PI * 2f)) * 50f, 0f);

			//creating pointer over event
			EventTrigger trigger = tempBG.GetComponent<EventTrigger> ();
			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = EventTriggerType.PointerEnter;
			Object choice = choices [i].gameObject;
			entry.callback.AddListener (e => HighlightChoice (choice));
			trigger.triggers.Add (entry);

			tempBG.enabled = false;
			temp.enabled = false;
			tempText.enabled = false;

			choiceImages.Add (tempBG);
			choiceImages.Add (temp);
			choiceTexts.Add (tempText);
			choiceLookup.Add (tempBG, choices [i].gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Input.mousePosition
		if (!menuOpen && Input.GetMouseButtonDown (0)) {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				print ("yes");
				OpenMenu ();
			}
		} else if (menuOpen && Input.GetMouseButtonUp (0)) {
			CloseMenu ();
		} else if (menuOpen) {
			//HighlightChoice ();
		}
	}

	void OpenMenu() {

		if (choices.Count == 0)
			return;

		foreach (Image i in choiceImages) {
			i.enabled = true;
		}

		foreach (Text i in choiceTexts) {
			i.enabled = true;
		}

		centerImage.enabled = false;
		menuOpen = true;

	}

	void HighlightChoice(Object choice) {
		activeChoice = choice;
	}

	void CloseMenu() {
		foreach (Image i in choiceImages) {
			i.enabled = false;
		}

		foreach (Text i in choiceTexts) {
			i.enabled = false;
		}

		if (activeChoice != null)
			SelectChoice (activeChoice);

		activeChoice = null;

		centerImage.enabled = true;
		menuOpen = false;
	}

	void SelectChoice(Object choice) {
		//print ("calling " + message + ": " + choice.ToString());
		recipient.SendMessage (message, choice);
	}

	public bool MenuIsOpen() {
		return menuOpen;
	}
}
