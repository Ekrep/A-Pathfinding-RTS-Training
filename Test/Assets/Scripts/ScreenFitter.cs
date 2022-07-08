using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFitter : MonoBehaviour
{
    public static ScreenFitter Instance;
    public float panSpeed=20f;
    public float panBorderThickness = 10f;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
      
    }
    private void Update()//Belirlenen hiz ve kenar genisligine gore mouse kenara yaklasinca cameranin hareketi 
    {
       
        Vector3 pos = transform.position;
        pos.z = -10f;
        if (Input.mousePosition.y>=Screen.height-panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <=  panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x>=Screen.width-panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <=  panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, 0, Grid.Instance.width);
        pos.y = Mathf.Clamp(pos.y, 0, Grid.Instance.height);
        transform.position = pos;
    }





}
