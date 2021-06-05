using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexturizer : MonoBehaviour
{
    private Object[] textures;
    private Texture defaultTex;

    public float randomTextureChance = .3f;
    public float randomMax = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (TextureShouldBeRandom()) {
            textures = Resources.LoadAll("Box Textures", typeof(Texture2D));

            Texture2D texture = (Texture2D)textures[Random.Range(0, textures.Length)];
            gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        }
    }

    private bool TextureShouldBeRandom()
    {
        return Random.Range(0f, randomMax) < (randomMax * randomTextureChance);
    }

}
