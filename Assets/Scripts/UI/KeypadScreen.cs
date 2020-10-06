using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadScreen : UIScreen<KeypadScreen>
{
    public override bool IsOverlay => true;

    public Image image;
    public TextMeshProUGUI inputText;
    public Keypad keypad;

    public string codeBuffer = "";

    public static void Open(Keypad keypad)
    {
        Open();

        Instance.keypad = keypad;
        Instance.image.color = keypad.keypadColor;

        Instance.SetInput("");
    }

    public override void OnOpen()
    {
        base.OnOpen();

        this.inputText.SetText("");

        GameManager.Instance.SetInputEnabled(false);
    }

    public override void OnClose()
    {
        base.OnClose();

        GameManager.Instance.SetInputEnabled(true);
    }

    public void AddInput(string input)
    {
        this.SetInput(this.codeBuffer + input);
    }

    public void BackspaceInput()
    {
        if (this.codeBuffer.Length < 1) return;

        this.SetInput(this.codeBuffer.Remove(this.codeBuffer.Length - 1));
    }

    public void SetInput(string input)
    {
        this.codeBuffer = input;
        
        this.inputText.SetText(this.codeBuffer);

        if (this.keypad.isSolved) this.codeBuffer = this.keypad.correctCode;

        if (this.codeBuffer.Length >= this.keypad.correctCode.Length)
        {
            if (this.codeBuffer == keypad.correctCode)
            {
                if (!this.keypad.isSolved) this.StartCoroutine(this.DelayedClose());

                this.keypad.OnCorrectCodeEvent.Invoke();
                this.inputText.SetText("CORRECT");
            }
            else
            {
                this.inputText.SetText("INCORRECT");
            }

            this.codeBuffer = "";
        }
    }

    IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(1f);

        Close();
    }
}
