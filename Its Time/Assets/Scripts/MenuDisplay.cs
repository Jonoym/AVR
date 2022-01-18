using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{

    public GameObject defaultMenu;

    public GameObject powerUpMenu;

    public GameObject hintMenu;

    void Start() {
        DisplayDefaultMenu();
    }

    public void DisplayDefaultMenu() {
        defaultMenu.SetActive(true);
        powerUpMenu.SetActive(false);
        hintMenu.SetActive(false);
    }

    public void DisplayPowerUpMenu() {
        powerUpMenu.SetActive(true);
        defaultMenu.SetActive(false);
        hintMenu.SetActive(false);
    }

    public void DisplayHintMenu() {
        hintMenu.SetActive(true);
        defaultMenu.SetActive(false);
        powerUpMenu.SetActive(false);
    }
}
