using System.IO;
using UnityEngine;

public class PngSaver : MonoBehaviour
{
    [SerializeField] private ParticleRenderer _particleRenderer;

    public void SaveToPng(string fileName)
    {
        byte[] bytes = _particleRenderer.Texture.EncodeToPNG();

        var dirPath = Application.dataPath + "/../SaveImages/";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(dirPath + fileName + ".png", bytes);
    }
}
