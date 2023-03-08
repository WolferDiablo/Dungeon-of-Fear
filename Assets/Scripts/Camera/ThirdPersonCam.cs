using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public ContainerStuffs containerStuffs;

    public float rotationSpeed;
    public CinemachineFreeLook camCorder;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;

    private float scrollData;
    private float radiusX;
    private float heightY;

    public PlayerAttack playerAttack;

    private void Start() {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public float GetAxisCustom (string axisName) {
        if (axisName == "Mouse X") {
            if (Input.GetMouseButton(0)) {
                return UnityEngine.Input.GetAxis("Mouse X");
            } else return 0;
        }
        else if (axisName == "Mouse Y") {
            if (Input.GetMouseButton(0)) {
                return UnityEngine.Input.GetAxis("Mouse Y");
            } else return 0;
        }
        return UnityEngine.Input.GetAxis(axisName);
    }

    public void Update() 
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }  
        
        



        // Lock the mouse cursor in middle if left mouse button is held down
        if(Input.GetMouseButton(0)) Cursor.visible = false;
            else Cursor.visible = true;

        scrollData = Input.mouseScrollDelta.y;
        radiusX = camCorder.m_Orbits[0].m_Radius;
        heightY = camCorder.m_Orbits[0].m_Height;
        
        // Scroll wheel zooms in and out the camera
        if(scrollData < 0 && radiusX < 10 && heightY < 13) {
            camCorder.m_Orbits[0].m_Radius ++;
            camCorder.m_Orbits[0].m_Height ++;
            camCorder.m_Orbits[1].m_Radius ++;
            camCorder.m_Orbits[1].m_Height ++;
            camCorder.m_Orbits[2].m_Radius ++;
            camCorder.m_Orbits[2].m_Height ++;
        }

        if (scrollData > 0 && radiusX > 3 && heightY > 5) {
            camCorder.m_Orbits[0].m_Radius --;
            camCorder.m_Orbits[0].m_Height --;
            camCorder.m_Orbits[1].m_Radius --;
            camCorder.m_Orbits[1].m_Height --;
            camCorder.m_Orbits[2].m_Radius --;
            camCorder.m_Orbits[2].m_Height --;
        }
    }
}
