using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;

    public Transform playerBody;

    float xRotation = 0f;

    private bool rotationEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        // Retire le curseur du joueur de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationEnabled)
        {
            // Permet la rotation de la caméra et du joueur en fonction de la souris à partir de la vidéo de Brackey

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            //Debug.Log(mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void ChangeCursorLockMode(bool locked)
    {
        rotationEnabled = locked;

        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
