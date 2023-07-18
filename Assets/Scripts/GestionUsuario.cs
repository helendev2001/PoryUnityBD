using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GestionUsuario : MonoBehaviour
{
    public InputField txtUsuario, txtContraseña;
    public string nombreUsuario;
    public int scoreUsuario;
    public int idUsuario;
    public bool sesionIniciada = false;
    public static GestionUsuario singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void conectarUsuario()
    {
        StartCoroutine(Login());
        
    }

    public void registrarUsuario()
    {
        StartCoroutine(Registrar());
    }
    public void actualizarScore(int nScore)
    {
        StartCoroutine(Actualizar(nScore));
    }

    IEnumerator Login()
    {
        WWW conexion = new WWW("http://localhost/Conexion/login.php?uss=" + txtUsuario.text + "&pss=" + txtContraseña.text);
        yield return (conexion);
        if (conexion.text == "200")
        {
            print("Usuario correcto");
            StartCoroutine(Mostrar());
        }
        else if (conexion.text == "401")
            {
                print("Usuario o contrasela incorrectos!!");
            }
        else
        {
            print("ERROR!! en la conexión");
        }

    }

    IEnumerator Mostrar()
    {
        WWW conexion = new WWW("http://localhost/Conexion/mostrar.php?uss=" + txtUsuario.text);
        yield return (conexion);
        if (conexion.text == "401")
        {
            print("Usuario incorrectos!!");
        }
        else
        {
            string[] datos = conexion.text.Split('|');
            print("Datos" + datos);
            if (datos.Length != 3)
            {
                print("Error en la conexión!!");
            }
            else
            {
                nombreUsuario = datos[0];
                scoreUsuario = int.Parse(datos[1]);
                idUsuario = int.Parse(datos[2]);
                sesionIniciada = true;
                SceneManager.LoadScene("Player");
            }
        }

    }

    IEnumerator Registrar()
    {
        WWW conexion = new WWW("http://localhost/Conexion/registrar.php?uss=" + txtUsuario.text + "&pss=" + txtContraseña.text);
        yield return (conexion);

        if (conexion.text == "402")
        {
            print("Usuario ya existe!!");
        }
        else
        {
            if (conexion.text == "201")
            {
                nombreUsuario = txtUsuario.text;
                scoreUsuario = 0;
                sesionIniciada = true;
                print("Usuario o contrasela incorrectos!!");
            }
            else
            {
                Debug.LogError("ERROS!! en la conexion");
            }
        }
    }
    IEnumerator Actualizar(int nScore)
    {
        WWW conexion = new WWW("http://localhost/Conexion/actualizar.php?uss=" + txtUsuario.text + "&nScore=" + nScore.ToString());
        yield return (conexion);

        if (conexion.text == "401")
        {
            print("Usuario no existe!!");
        }
        else if (conexion.text == "202")
        {
            print("Datos actualizados correctamente");
            scoreUsuario = nScore;
        }
        else
        {
            Debug.LogError("ERROS!! en la conexion en la BD");
        }

    }

}