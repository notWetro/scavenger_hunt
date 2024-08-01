using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    public TMP_Text hintText;
    public Image hintImage;

    public void DisplayHint(int hintType, string hintData)
    {
        switch (hintType)
        {
            case 0:
                // Text hint
                hintText.text = hintData;
                hintText.gameObject.SetActive(true);
                hintImage.gameObject.SetActive(false);
                break;

            case 1:
                // Image hint
                const string base64Prefix = "data:image/png;base64,";
                string base64Data = hintData.StartsWith(base64Prefix) ? hintData.Substring(base64Prefix.Length) : hintData;

                byte[] imageBytes = Convert.FromBase64String(base64Data);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(imageBytes);

                // Create new Sprite Drink (without Vodka)
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                hintImage.sprite = sprite;
                hintImage.gameObject.SetActive(true);
                hintText.gameObject.SetActive(false);
                break;

            default:
                Debug.LogError("Unknown hint type: " + hintType);
                break;
        }
    }
}
