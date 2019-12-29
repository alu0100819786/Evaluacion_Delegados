# Evaluación Delegados.

 -Autor: Gabriel Melián Hernández.

Descripción de la Práctica: 

Modificar la escena desarrollada para la práctica para que se ajuste a los siguientes requisitos:


-Debe incluir varias instancias de 2 tipos de objetos A, B  (pueden ser primitivas 3d). Los objetos de tipo B son físicos. Los objetos de tipo A son estáticos.
-Debe incluir una UI que permita comprar energía que supondrá la intensidad con la que se realizarán los disparos. Se deben gestionar dos teclas de disparo:
-Disparo A: Se disminuye el poder y tamaño de los objetos de tipo A con lo que se esté en colisión, proporcionalmente a la energía.
-Disparo B: Se cambia el color de los objetos con lo que se esá en colisión y aumenta el dinero disponible para la compra de energía .
-Cuando el jugador colisiona con objetos de tipo B se deben mover los objetos de tipo C de forma proporcional a su poder.

--------------------------------------------

Para llevar a cabo la realización de esta practica vamos a usar una esfera como personaje controlable que se moverá con movimiento físico y rigidBody como de costumbre:

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * 1f, ForceMode.Impulse);
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * 1f, ForceMode.Impulse);
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * 1f, ForceMode.Impulse);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 1f, ForceMode.Impulse);
            
        }
        
Por otra parte tendremos en la escena, 4 cilindros y 4 cubos que serán las instancias de los dos tipos de obejtos que trataremos en esta práctica, además también habrá un foco que tendremos que controlar cuando se apague y encienda y una esfera que se moverá dependiendo de la interacción de un botón. Primero de nada creamos los objetos y les añadimos a cada uno el script correspondiente para la activación de su evento:

Para los cilindros:

        public class EventoCilindro : MonoBehaviour
        {
        public delegate void _OnIncreasePower(GameObject go);
        public static event _OnIncreasePower OnIncreasePower;

        private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.tag == "Esfera")
        {
            if (OnIncreasePower != null)
            {
                OnIncreasePower(gameObject);
            }
        }
        }
        }
    
Para los Cubos:

        public class EventoCubo : MonoBehaviour
        {
        public delegate void _OnChangeColor(GameObject go);
        public static event _OnChangeColor OnChangeColor;

        private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.tag == "Esfera")
        {
            if(OnChangeColor != null)
            {
                OnChangeColor(gameObject);
            }
        }
        }
        }
        
Para el foco:

    public class EventoFoco : MonoBehaviour
    {
 
    public delegate void _OnLightOrNot(GameObject go);
    public static event _OnLightOrNot OnLightOrNot;

    private void LightOrNot()
    {
            if (OnLightOrNot != null)
            {
                OnLightOrNot(gameObject);
            }
    }
    }
    
  A diferencia de la práctica anterior, en esta llevaremos la activación de los eventos por medio de botones, que estarán al igual que en la práctica anterior recogidos dentro del GameController como objeto vacío. Ahora cuando se produzcan las colisiones se activará el botón y al pulsarlo se llevará a cabo el evento en sí.
  Tendremos 4 botones diferentes, uno para la tienda donde podremos comprar poder, otro para el DisparoA para llevar a cabo el evento correspondiente a la colision con los cilindros, otro para el DisparoB que llevará a cabo el evento de colision con los cubos y por último el botón Reset que llevará la escena a su estado original.
  
  Estos botones los activaremos en el Awake:
  
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

Y en el Start pondremos a cada uno de ellos el evento que les corresponde:

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
    
Cuando se colisione con un cilindro o un Cubo activaremos los botones correspondientes:

        void ChangeColor(GameObject go)
    {
        DisparoB.interactable = true;
        DisparoA.interactable = false;
        
    }
        void IncreasePower(GameObject go)
    {
        DisparoA.interactable = true;
        DisparoB.interactable = false;
    }
    
Y luego una Vez activos los botones, resolveremos su evento al pulsarlos.

  En el caso de la Tienda, incrementaremos el poder del jugador:
  
        void TaskOnClick()
    {
        score += 20;
    }
    
  En el caso de los cilindros dependiendo del score, iremos decreciendo el tamaño de los mismos y perdiendo poder en cada uso:
  
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
    
  En el caso de los Cubos, variamos su color aleatoriamente y incrementamos su tamaño dependiendo del score que tengamos, movemos la esfera en diagonal y incrementamos nuestro poder:
  
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
    
  Por último en el botón del Reset devolvemos toda la escena a su estado original:
  
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
