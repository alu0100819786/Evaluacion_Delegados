using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    GameObject[] Cilindro;
    GameObject[] Cubo;
    GameObject[] Sphere;
    GameObject Foco;
    GameObject Character1;
    public Button Boton;
    public Button DisparoA;
    public Button DisparoB;
    public Button Reset;
    public static GameController controlador;
    float x = 1;
    float y = 1;
    float z = 1;
    float j = 3;
    float k = 3;
    float l = 3;
    float c = 1;
    int score = 0;

    private Color[] colors = new Color[] { Color.green, Color.red, Color.blue };

  
    void Awake()
    {
      if (controlador == null)
        {
            controlador = this;
            DontDestroyOnLoad(this);
        }  
      else if(controlador != this)
        {
            Destroy(gameObject);
        }
        Boton = GetComponent<Button>();
        DisparoA = GetComponent<Button>();
        DisparoB = GetComponent<Button>();
        Reset = GetComponent<Button>();
    }

    private void Start()
    {
        Foco = GameObject.FindGameObjectWithTag("Foco");
        Boton = GameObject.Find("Tienda").GetComponent<Button>();
        Boton.onClick.AddListener(TaskOnClick);

        DisparoA = GameObject.Find("DisparoA").GetComponent<Button>();
        DisparoA.onClick.AddListener(TaskOnCill);
        DisparoA.interactable = false;

        DisparoB = GameObject.Find("DisparoB").GetComponent<Button>();
        DisparoB.onClick.AddListener(TaskOnCube);
        DisparoB.interactable = false;

        Reset = GameObject.Find("Reset").GetComponent<Button>();
        Reset.onClick.AddListener(TaskOnReset);

        Character1 = GameObject.FindGameObjectWithTag("Esfera");
        Character1.GetComponent<Renderer>().material.color = colors[2];
    }
    private void Update()
    {
        LightOrNot(Foco);
    }

    void OnEnable()
    {
        EventoCubo.OnChangeColor += ChangeColor;
        EventoCilindro.OnIncreasePower += IncreasePower;
        EventoFoco.OnLightOrNot += LightOrNot;
    }

    void OnDisable()
    {
        EventoCubo.OnChangeColor -= ChangeColor;
        EventoCilindro.OnIncreasePower -= IncreasePower;
        EventoFoco.OnLightOrNot += LightOrNot;
    }
    
    void ChangeColor(GameObject go)
    {
        /*Cubo = GameObject.FindGameObjectsWithTag("Cubo");
        Sphere = GameObject.FindGameObjectsWithTag("Sphere");
        x += 0.5f;
        y += 0.5f;
        z += 0.5f;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cubo").Length; i++)
        {
            //Cubo[i].transform.localScale = new Vector3(x, y, z);
            var number = UnityEngine.Random.Range(0, 3);
            Cubo[i].GetComponent<Renderer>().material.color = colors[number];
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        {
            Sphere[i].GetComponent<Renderer>().material.color = colors[2];

            if (score <= 10)
            {
                c += 0.1f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);    
            }
            if (score > 10 && score <= 20)
            {
                c += 0.3f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }
            if (score > 20 && score <= 30)
            {
                c += 0.7f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }
            if (score > 30)
            {
                c += 1;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }

        }
        //score -=1;
        score += 10;*/
        DisparoB.interactable = true;
        DisparoA.interactable = false;
        
    }


    void IncreasePower(GameObject go)
    {
        /*Cilindro = GameObject.FindGameObjectsWithTag("Cilindro");
        
        if(score <= 10)
        {
            j -= 0.1f;
            k -= 0.1f;
            l -= 0.1f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 1;
        }
        if (score >10 && score <= 20)
        {
            j -= 0.25f;
            k -= 0.25f;
            l -= 0.25f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 5;
        }
        if (score > 20 && score <= 30)
        {
            j -= 0.5f;
            k -= 0.5f;
            l -= 0.5f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 10;
        }
        if (score > 30)
        {
            j -= 1f;
            k -= 1f;
            l -= 1f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 20;
        }*/
        DisparoA.interactable = true;
        DisparoB.interactable = false;
    }

    void LightOrNot(GameObject go)
    {

        if (Input.GetKey(KeyCode.L))
        {
            Foco.SetActive(true);
            Debug.Log("Encendemos");
        }
        if (Input.GetKey(KeyCode.N))
        {
            Foco.SetActive(false);
            Debug.Log("Apagamos");
        }


    }

    void TaskOnClick()
    {
        score += 20;
    }

    void TaskOnCill()
    {
        Cilindro = GameObject.FindGameObjectsWithTag("Cilindro");

        if (score <= 10)
        {
            j -= 0.1f;
            k -= 0.1f;
            l -= 0.1f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 1;
        }
        if (score > 10 && score <= 20)
        {
            j -= 0.25f;
            k -= 0.25f;
            l -= 0.25f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 5;
        }
        if (score > 20 && score <= 30)
        {
            j -= 0.5f;
            k -= 0.5f;
            l -= 0.5f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 10;
        }
        if (score > 30)
        {
            j -= 1f;
            k -= 1f;
            l -= 1f;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
            {
                Cilindro[i].transform.localScale = new Vector3(j, k, l);
            }
            score -= 20;
        }
    }

    void TaskOnCube()
    {


        Cubo = GameObject.FindGameObjectsWithTag("Cubo");
        Sphere = GameObject.FindGameObjectsWithTag("Sphere");
        x += 0.5f;
        y += 0.5f;
        z += 0.5f;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cubo").Length; i++)
        {
            //Cubo[i].transform.localScale = new Vector3(x, y, z);
            var number = UnityEngine.Random.Range(0, 3);
            Cubo[i].GetComponent<Renderer>().material.color = colors[number];
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        {
            Sphere[i].GetComponent<Renderer>().material.color = colors[2];

            if (score <= 10)
            {
                c += 0.1f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }
            if (score > 10 && score <= 20)
            {
                c += 0.3f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }
            if (score > 20 && score <= 30)
            {
                c += 0.7f;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }
            if (score > 30)
            {
                c += 1;
                Sphere[i].transform.position = new Vector3(c, 0.5f, c);
            }

        }
        //score -=1;
        score += 10;
    }

    void TaskOnReset()
    {
        Cubo = GameObject.FindGameObjectsWithTag("Cubo");
        Sphere = GameObject.FindGameObjectsWithTag("Sphere");
        Cilindro = GameObject.FindGameObjectsWithTag("Cilindro");
        DisparoA.interactable = false;
        DisparoB.interactable = false;
        
        score = 0;
        x = 1;
        y = 1;
        z = 1;
        j = 3;
        k = 3;
        l = 3;
        c = 1;
        
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Sphere").Length; i++)
        {
            Sphere[i].transform.position = new Vector3(c, 0.5f, c);
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cilindro").Length; i++)
        {
            Cilindro[i].transform.localScale = new Vector3(j, k, l);
        }
    }
        private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 10, 10), "Score = " + score);
        GUILayout.EndArea();
    }


}
