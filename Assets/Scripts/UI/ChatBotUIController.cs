using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which represents the control system of the
 * chat bot user interface of the game
 */
public class ChatBotUIController : Gazable {

    /* Concepts to ask to the chatbot */
    private string[] concepts = {
        "Consumables",
        "Enemies",
        "Game",
        "Lantern",
        "Movement",
        "Weapon"
    };
    /* Current page of concepts */
    private int currentPage;
    /* First button game object */
    public GameObject firstButtonGameObject;
    /* Second button game object */
    public GameObject secondButtonGameObject;
    /* Third button game object */
    public GameObject thirdButtonGameObject;
    /* Response of the chatbot */
    public Text responseButtonText;
    /* Next page of concepts button */
    public GameObject nextButtonGameObject;
    /* Prev page of concepts button */
    public GameObject prevButtonGameObject;
    /* Chat bot controller reference */
    private ChatBotController chatBotController;

    /*
     * Initialization method
     */
    private void Start() {
        currentPage = 0;
        OnCurrentPageChange();
        chatBotController = GetComponent<ChatBotController>();
        chatBotController.SetAPIKey("");

        firstButtonGameObject.GetComponent<Button>().onClick.AddListener(OnFirstButtonClick);
        secondButtonGameObject.GetComponent<Button>().onClick.AddListener(OnSecondButtonClick);
        thirdButtonGameObject.GetComponent<Button>().onClick.AddListener(OnThirdButtonClick);
        prevButtonGameObject.GetComponent<Button>().onClick.AddListener(OnPrevButtonClick);
        nextButtonGameObject.GetComponent<Button>().onClick.AddListener(OnNextButtonClick);

        prevButtonGameObject.SetActive(false);
    }

    /*
     * Actions to take when next button is clicked
     */
    public void OnNextButtonClick() {
        currentPage += 1;
        OnCurrentPageChange();
        if (!prevButtonGameObject.activeSelf) prevButtonGameObject.SetActive(true);
        if (currentPage == (concepts.Length / 3) - 1) nextButtonGameObject.SetActive(false);
    }

    /*
     * Actions to take when prev button is clicked
     */
    public void OnPrevButtonClick() {
        currentPage -= 1;
        if (!nextButtonGameObject.activeSelf) {
            nextButtonGameObject.SetActive(true);
            //Debug.Log("First condition");
        }
        if (currentPage == 0) {
            prevButtonGameObject.SetActive(false);
            //Debug.Log("Second condition");
        }
    }

    /*
     * Actions to take when first button is clicked
     */
    public void OnFirstButtonClick() {
        string response = chatBotController.SendText(concepts[currentPage * 3]);
        responseButtonText.text = response;
    }

    /*
     * Actions to take when second button is clicked
     */
    public void OnSecondButtonClick() {
        string response = chatBotController.SendText(concepts[currentPage * 3 + 1]);
        responseButtonText.text = response;
    }

    /*
     * Actions to take when third button is clicked
     */
    public void OnThirdButtonClick() {
        string response = chatBotController.SendText(concepts[currentPage * 3 + 2]);
        responseButtonText.text = response;
    }

    /*
     * Actions to take when the current page changes */
    private void OnCurrentPageChange() {
        firstButtonGameObject.GetComponentInChildren<Text>().text = concepts[currentPage * 3];
        if (currentPage * 3 + 1 >= concepts.Length) {
            secondButtonGameObject.SetActive(false);
        } else {
            secondButtonGameObject.SetActive(true);
            secondButtonGameObject.GetComponentInChildren<Text>().text = concepts[currentPage * 3 + 1];
        }

        if (currentPage * 3 + 2 >= concepts.Length) {
            thirdButtonGameObject.SetActive(false);
        } else {
            thirdButtonGameObject.SetActive(true);
            thirdButtonGameObject.GetComponentInChildren<Text>().text = concepts[currentPage * 3 + 2];
        }
    }

    /*
     * Actions to take when the users clicks
     * on chat bot controller
     */
    public override void OnPointerClick() {
        Canvas chatBotUI = GameObject.FindGameObjectWithTag("ChatBotUI").GetComponent<Canvas>();
        chatBotUI.enabled = !chatBotUI.enabled;
    }
}
