using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace MathMasters.Services
{
    public static class ImageExtensions
    {
        public static void ResizeImage(this Image targetImage, Sprite sprite, Vector2 maxSize)
        {
            if (targetImage == null || sprite == null) 
                return;

            Vector2 newSpriteSize = SetNewSpriteSize(sprite, maxSize);
            SetSize(targetImage, sprite, newSpriteSize);;
        }

        private static Vector2 SetNewSpriteSize(Sprite sprite, Vector2 maxSize)
        {
            Vector2 newSpriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
            newSpriteSize = ChekByWight(newSpriteSize, maxSize);
            newSpriteSize =  ChekByHight(newSpriteSize, maxSize);

            return newSpriteSize;
        }
        private static Vector2 ChekByWight(Vector2 newSpriteSize, Vector2 maxSize)
        {
            if (newSpriteSize.x > maxSize.x)
            {
                float scaleFactor = maxSize.x / newSpriteSize.x;
                newSpriteSize.x *= scaleFactor;
                newSpriteSize.y *= scaleFactor;
            }

            return newSpriteSize;
        }

        private static Vector2 ChekByHight(Vector2 newSpriteSize, Vector2 maxSize)
        {
            if (newSpriteSize.y > maxSize.y)
            {
                float scaleFactor = maxSize.y / newSpriteSize.y;
                newSpriteSize.x *= scaleFactor;
                newSpriteSize.y *= scaleFactor;
            }

            return newSpriteSize;
        }

        private static void SetSize(Image targetImage, Sprite sprite, Vector2 size)
        {
            targetImage.rectTransform.sizeDelta = size;
            targetImage.sprite = sprite;
            targetImage.SetNativeSize();
        }
    }
}
