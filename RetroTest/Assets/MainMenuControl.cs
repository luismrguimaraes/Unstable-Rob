using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public InputActionAsset controls;
    public InputAction navigation;
    public InputAction confirm;
    public int LevelSelected = 1;
    public Button[] buttons;
    public GameObject[] selectors;
    public int buttonSelected = 0;

    bool left=false;
    bool right=false;
    bool down=false;
    bool up=false;
    bool selected=false;

    private void Start()
    {
        controls.FindActionMap("UI").Enable();
        navigation = controls.FindActionMap("UI").FindAction("Navigate");
        confirm = controls.FindActionMap("UI").FindAction("Select");
        InvokeRepeating(nameof(CheckInputs), 0f, 0.10f);

    }

    private void Update()
    {
        Vector2 navigationInput = navigation.ReadValue<Vector2>();
        //Debug.Log(navigationInput);

        left = navigationInput.x < -0.75 || left;
        right = navigationInput.x > 0.75 || right;
        down = navigationInput.y < -0.75 || down;
        up = navigationInput.y > 0.75 || up;
        selected = confirm.WasPressedThisFrame() || selected;

    }



    private void CheckInputs()
    {
        if (down)
        {
            UpdateButts(1);
        }
        else if(up)
        {
            UpdateButts(-1);
        } else if(selected)
        {
            switch(buttonSelected)
            {
                case 0:
                    Play();
                    break;
                case 1:
                    Application.Quit();
                    break;
            }
        } else if(buttonSelected == 0 && right)
        {
            UpdateLevels(1);
        } else if(buttonSelected == 0 && left)
        {
            UpdateLevels(-1);
        }
        down = false;
        up = false;
        left = false;
        right = false;
        selected = false;
    }

    private void UpdateLevels(int dir)
    {
        LevelSelected += dir;
        if (LevelSelected < 1)
            LevelSelected = 3;
        else if (LevelSelected > 3)
            LevelSelected = 1;
        buttons[0].GetComponentInChildren<TMPro.TMP_Text>().text = "Play: Level " + LevelSelected;
    }

    private void UpdateButts(int dir)
    {
        selectors[buttonSelected].SetActive(false);
        buttonSelected += dir;
        if (buttonSelected < 0)
            buttonSelected = buttons.Length - 1;
        else if (buttonSelected > buttons.Length - 1)
            buttonSelected = 0;
        selectors[buttonSelected].SetActive(true);

    }

    public void Play()
    {
        Debug.Log("Loading brother");
        SceneManager.LoadScene("Level"+LevelSelected);
    }
}
