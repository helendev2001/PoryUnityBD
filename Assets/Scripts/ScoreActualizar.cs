using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreActualizar : MonoBehaviour
{
    public InputField txtNScore;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Text>().text = "Hola, " + GestionUsuario.singleton.nombreUsuario + ", Bienvenido. ";

        
    }

    public void ChangeScores ()
    {
        GestionUsuario.singleton.actualizarScore(int.Parse(txtNScore.text));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
