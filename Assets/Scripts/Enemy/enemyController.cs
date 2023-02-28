using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class enemyController : MonoBehaviour
{
    //rutina dle enemigo
    public int routine;
    //tiemoo entre rutinas
    public float chronometer;
    public Animator anim;
    //rotacion del enemigo
    public Quaternion angle;
    //grado del angulo
    public float grade;
    //objetivo de ataque
    public GameObject target;
    //
    public bool attack;
    public Image enemyLife;
    public float maxLife = 100f;
    public float currentLife = 100f;
    //public characterController muerte;
    public bool Death = false;
    public GameObject enemy;
    public GameObject mission3;
    public TextMeshProUGUI textMisssion2;
    public AudioSource kingSanchoD2;
    public bool d=false;


 



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Cid");

    }

    // Update is called once per frame
    void Update()
    {
        enemyBehaviour();
        enemyLife.fillAmount = (currentLife / maxLife);
        death();

        if(!d){
            kingSanchoD2.Play();
        }
    }

   //comportamiento del enemigo
    public void enemyBehaviour(){
        if(Vector3.Distance(transform.position,target.transform.position)>5){
            anim.SetBool("run",false);
            chronometer+=1*Time.deltaTime;
        if(chronometer>=4){
            routine=Random.Range(0,2);
            chronometer=0;
        }
        switch(routine){
            case 0:
            anim.SetBool("walk",false);
            break;

            case 1:
            grade=Random.Range(0,360);
            angle=Quaternion.Euler(0,grade,0);
            routine++;
            break;

            case 2:
            transform.rotation=Quaternion.RotateTowards(transform.rotation,angle,0.5f);
            transform.Translate(Vector3.forward*1*Time.deltaTime);
            anim.SetBool("walk",true);
            break;
        }

        }else{
            if(Vector3.Distance(transform.position,target.transform.position)>1 && !attack){
                var lookPos=target.transform.position-transform.position;
            lookPos.y=0;
            var rotation=Quaternion.LookRotation(lookPos);
            transform.rotation=Quaternion.RotateTowards(transform.rotation,rotation,2);
            anim.SetBool("walk",false);
            anim.SetBool("run",true);
            transform.Translate(Vector3.forward*2*Time.deltaTime);
            anim.SetBool("attack",false);

            }else{
                anim.SetBool("walk",false);
                anim.SetBool("run",false);
                anim.SetBool("attack",true);
                attack=true;

            }
            
        }
    }

    public void finalAnim(){
        anim.SetBool("attack",false);
        attack=false;
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("armaJugador"))
        {
            print("prueba daño del jugador");
            currentLife -= 20f;
        }
    }

    public void death()
    {  
        if (currentLife <= 0)
        {
            
            d=true;
            mission3.SetActive(true);
            textMisssion2.color=Color.red;
            anim.SetTrigger("death");
            Destroy(enemy,22f);
            
        }
    }

}
