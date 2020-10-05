using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogScreen : UIScreen<DialogScreen>
{
    public override bool ShouldPause => true;
    public override bool IsOverlay => true;

    public TextMeshProUGUI dialogText;
    public float typeSpeed = 1.5f;
    public bool typing = true;

    private IEnumerator typeCoroutine;

    public static void Open(String text)
    {
        Open();
        Instance.SetText(text);
    }

    public static void Open(DialogData data)
    {
        Open(data.Text);
    }

    void Update()
    {
        var totalPages = this.dialogText.textInfo.pageCount;
        var currentPage = this.dialogText.pageToDisplay;

        if (Input.GetMouseButtonDown(0))
        {
            if (this.typing)
            {
                this.StopTyping();
                this.dialogText.maxVisibleCharacters = this.dialogText.textInfo.pageInfo[this.dialogText.pageToDisplay - 1].lastCharacterIndex;
            }
            else if (currentPage == totalPages)
            {
                Close();
            }
            else
            {
                this.dialogText.pageToDisplay++;
                this.StartTyping();
            }
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        this.StartTyping();
    }

    public override void OnEscapePressed()
    {

    }

    public void SetText(String text)
    {
        this.StopTyping();
        this.dialogText.SetText(text);
        this.dialogText.maxVisibleCharacters = 0;
        this.StartTyping();
    }

    public void StartTyping()
    {
        typeCoroutine = this.StartTypewriter();
        this.StartCoroutine(typeCoroutine);
    }

    public void StopTyping()
    {
        this.typing = false;
        this.StopCoroutine(typeCoroutine);
    }

    IEnumerator StartTypewriter()
    {
        this.typing = true;

        var start = this.dialogText.textInfo.pageInfo[this.dialogText.pageToDisplay - 1].firstCharacterIndex;

        var counter = start;

        while (this.typing)
        {
            var end = this.dialogText.textInfo.pageInfo[this.dialogText.pageToDisplay - 1].lastCharacterIndex + 1;
            var visible = counter % (end + 1);

            this.dialogText.maxVisibleCharacters = visible;

            counter++;

            if (visible >= end && end > 0)
            {
                this.typing = false;
                yield break;
            }

            yield return new WaitForSecondsRealtime(0.05f / this.typeSpeed);
        }
    }
}
