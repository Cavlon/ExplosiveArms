using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTimer : MonoBehaviour
{

    [SerializeField] protected Image fuse;
    [SerializeField] protected Image spark;
    [SerializeField] protected Text timerText;
    [SerializeField] protected int timeLimit;
    [SerializeField] protected Explosion explosion;

    [HideInInspector] public bool startTimer;

    public bool stopTimer;
    private bool explode; 
    private Vector3 sparkPos;
    private Vector3 initialSparkPos;
    private float time;
    private float posTime;
    private Player_Shooting playerShooting;
    private gunScript gun;

    void Start()
    {
        spark.enabled = false;
        stopTimer = true;
        initialSparkPos = spark.rectTransform.anchoredPosition;     
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Shooting>();
    }

    void Update()
    {

        if (startTimer)
        {
            StartTimer();
        }

        if (!playerShooting.hasGun)
        {
            stopTimer = true;
        }

        if (!stopTimer)
        {
            Timer();
        } else
        {
            spark.enabled = false;            
        } 

        if (explode && playerShooting.hasGun)
        {
            Explode();
        }      
    }

    private void Timer()
    {
        time -= Time.deltaTime;

        posTime = time / timeLimit;

        float seconds = Mathf.CeilToInt(time % 60);

        timerText.text = seconds.ToString();      


        fuse.fillAmount = posTime;
        sparkPos.y = (posTime * 650) + 135;
        spark.rectTransform.anchoredPosition = sparkPos;

        if (time <= 0)
        {
            stopTimer = true;
            explode = true;
        }
    }

    private void Explode()
    {
        playerShooting.hasGun = false;
        gun = playerShooting.gun;
        Instantiate(explosion, gun.transform.position, Quaternion.identity);
        Destroy(gun.gameObject);
        explode = false;
    }

    private void StartTimer()
    {
        time = timeLimit;
        sparkPos = initialSparkPos;
        fuse.fillAmount = 1;
        spark.enabled = true;
        stopTimer = false;
        startTimer = false;
    }
}
