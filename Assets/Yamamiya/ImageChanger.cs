using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image Image;
    [SerializeField] int spritesIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        spritesIndex = (spritesIndex + 1 > sprites.Length -1) ? 0 : spritesIndex + 1;
        Image.sprite = sprites[spritesIndex];
    }
}
