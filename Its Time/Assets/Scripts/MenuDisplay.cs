using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{

    public GameObject defaultMenu;

    public GameObject powerUpMenu;

    public GameObject hintMenu;

    public GameObject solutionButton;

    public GameObject showButton;

    public GameObject returnButton;

    public GameObject restartButton;

    public GameObject menuButton;

    public GameObject powerUpButton;

    public GameObject hintSelectButton;

    void Start() {
        DisplayDefaultMenu();
    }

    public void DisplayDefaultMenu() {
        defaultMenu.SetActive(true);
        powerUpMenu.SetActive(false);
        hintMenu.SetActive(false);
        menuButton.GetComponent<MenuButton>().SetReleasedFalse();
        restartButton.GetComponent<RestartButton>().SetReleasedFalse();
        powerUpButton.GetComponent<PowerUpButton>().SetReleasedFalse();
        hintSelectButton.GetComponent<HintSelectButton>().SetReleasedFalse();
    }

    public void DisplayPowerUpMenu() {
        powerUpMenu.SetActive(true);
        defaultMenu.SetActive(false);
        hintMenu.SetActive(false);
        returnButton.GetComponent<ReturnButton>().SetReleasedFalse();
    }

    public void DisplayHintMenu() {
        hintMenu.SetActive(true);
        defaultMenu.SetActive(false);
        powerUpMenu.SetActive(false);
        showButton.GetComponent<ShowItemsButton>().SetReleasedFalse();
        solutionButton.GetComponent<SolutionButton>().SetReleasedFalse();
    }
}
