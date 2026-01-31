using UnityEngine;
using UnityEngine.UI;


public class Left_SC : MonoBehaviour
{
    public Degisken_SC degisken;
    public GameObject player;   // Ana player referansı
    public Button myButton;     // UI Button referansı
    public float rotationSpeed; // Hız ayarı (Inspector’dan değiştirilebilir)

    private Quaternion targetRotation; // Hedef rotasyon

    void Start()
    {
        //hızı değişkenden çekme
        rotationSpeed = degisken.donus_hiz;

        // Başlangıçta mevcut rotasyonu hedef olarak ayarla
        targetRotation = player.transform.rotation;

        // Butona tıklanınca fonksiyon çalışsın
        myButton.onClick.AddListener(AddYRotationSmooth);
    }

    void Update()
    {
        // Eğer rotasyon aktifse, yavaşça hedefe yaklaş
        if (degisken.rotating)
        {
            player.transform.rotation = Quaternion.Lerp(
                player.transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );

            // Hedefe çok yaklaştığında durdur
            if (Quaternion.Angle(player.transform.rotation, targetRotation) < 0.1f)
            {
                player.transform.rotation = targetRotation;
                degisken.rotating = false;
            }
        }
    }

    void AddYRotationSmooth()
    {
        if (!degisken.rotating && !degisken.rotating_ && !degisken.rotating_MM)
        {
            // Mevcut rotasyonu al
            Vector3 currentRotation = player.transform.eulerAngles;

            // Y değerine +45 ekle
            float newY = currentRotation.y - 90f;

            // Yeni hedef rotasyonu ayarla
            targetRotation = Quaternion.Euler(currentRotation.x, newY, currentRotation.z);

            // Rotasyonu başlat
            degisken.rotating = true;

        }
    }
}
