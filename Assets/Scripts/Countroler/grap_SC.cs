using UnityEngine;
using UnityEngine.EventSystems;

public class grap_SC : MonoBehaviour
{
    public Degisken_SC degisken;
    public Transform player;        // Player objesi
    public Transform holdPoint;     // Player’ın el pozisyonu
    public float moveSpeed = 5f;    // Taşınma hızı

       // Seçilen obje
    private bool isMoving = false;

    void Update()
    {
        // Sol tık ile ray at
        if (Input.GetMouseButtonDown(0))
        {
            if (degisken.pickedObj != null) return;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Pickable"))
                {
                    degisken.pickedObj = hit.collider.transform;
                    degisken.pickedObj.SetParent(player);
                    isMoving = true;
                }
            }

        }

        // Objeyi yavaşça holdPoint’e taşı
        if (isMoving && degisken.pickedObj != null)
        {
            degisken.pickedObj.position = Vector3.MoveTowards(
                degisken.pickedObj.position,
                holdPoint.position,
                moveSpeed * Time.deltaTime
            );

            degisken.pickedObj.rotation = Quaternion.Lerp(
                degisken.pickedObj.rotation,
                holdPoint.rotation,
                moveSpeed * Time.deltaTime
            );

            // Hedefe ulaştığında durdur
            if (Vector3.Distance(degisken.pickedObj.position, holdPoint.position) < 0.01f)
            {
                isMoving = false;
            }
        }
    }


}
