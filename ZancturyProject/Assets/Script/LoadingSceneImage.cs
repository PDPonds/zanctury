using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneImage : MonoBehaviour
{
    [SerializeField] Image loadingImage;
    [SerializeField] int ranCount;
    [SerializeField] List<Sprite> LoadingSprite = new List<Sprite>();

    private void Start()
    {
        ranCount = Random.Range(0, LoadingSprite.Count);
        loadingImage.sprite = LoadingSprite[ranCount];
    }
}
