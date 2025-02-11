using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
    public static void ResizeImage(this Image targetImage, Sprite sprite, Vector2 maxSize)
    {
        if (targetImage == null || sprite == null) return;

        RectTransform rectTransform = targetImage.rectTransform;

        Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
        float aspectRatio = spriteSize.x / spriteSize.y;

        float width = spriteSize.x;
        float height = spriteSize.y;

        if (width > maxSize.x)
        {
            float scaleFactor = maxSize.x / width;
            width *= scaleFactor;
            height *= scaleFactor;
        }
        if (height > maxSize.y)
        {
            float scaleFactor = maxSize.y / height;
            width *= scaleFactor;
            height *= scaleFactor;
        }

        rectTransform.sizeDelta = new Vector2(width, height);
        targetImage.sprite = sprite;
        targetImage.SetNativeSize();
    }
}
