using UnityEngine;

public class MM_Start_sc : MonoBehaviour
{
    public Degisken_SC degisken;
    [Header("Button")]
    public GameObject Button_NewGame;
    public GameObject Button_Countinue;
    public GameObject Button_Exit;
    [Header("Turn")]
    public GameObject player;
    public float rotationSpeed;
    private Quaternion targetRotation; // Hedef rotasyon

    private void Start()
    {
        //hızı değişkenden çekme
        rotationSpeed = degisken.donus_hiz;

        // Başlangıçta mevcut rotasyonu hedef olarak ayarla
        targetRotation = player.transform.rotation;
    }
    void Update()
    {
        //esc tuşu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            degisken.in_game = false;
            degisken.out_game = false;
            AddYRotationSmooth_();
        }

        // Butona Tıklama
        if (Input.GetMouseButtonDown(0)) // Sol tık
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Dışarıdan atanan objeye tıklandı mı?
                if (hit.transform == Button_NewGame.transform)
                {
                    PlayerPrefs.DeleteAll();
                    AddYRotationSmooth();
                    degisken.out_game = true;
                }
                if (hit.transform == Button_Countinue.transform)
                {
                    AddYRotationSmooth();
                    degisken.out_game = true;
                }
                if (hit.transform == Button_Exit.transform)
                {
                    Application.Quit();
                    degisken.out_game = true;
                }
            }
        }
        //hareket
        if (degisken.rotating_MM)
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
                degisken.rotating_MM = false;
                if(degisken.out_game)
                degisken.in_game = true;
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
            float newY = currentRotation.y + 90f;

            // Yeni hedef rotasyonu ayarla
            targetRotation = Quaternion.Euler(currentRotation.x, newY, currentRotation.z);

            // Rotasyonu başlat
            degisken.rotating_MM = true;

        }
    }

    void AddYRotationSmooth_()
    {
        if (!degisken.rotating && !degisken.rotating_ && !degisken.rotating_MM)
        {
            // Mevcut rotasyonu al
            Vector3 currentRotation = player.transform.eulerAngles;

            

            // Yeni hedef rotasyonu ayarla
            targetRotation = Quaternion.Euler(currentRotation.x, -90, currentRotation.z);

            // Rotasyonu başlat
            degisken.rotating_MM = true;

        }
    }
}
