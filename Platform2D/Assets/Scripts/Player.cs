using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    Rigidbody2D rb;


    public Animator anim;
    private int score = 0;
    private int puan;
    
    [SerializeField]
    bool kosuyorMu = false;

   

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject yenidenOynaPanel;

    [SerializeField]
    Text panelScoreText;

    [SerializeField]
    Text highScoreText;



    public static bool oyunBasladiMi = false; // static: oyun baþladý mý durdurana kadar çalýþýr. 

    [SerializeField]
    GameObject baslangicPanel;

    [SerializeField]
    GameObject bitisPanel;

    [SerializeField]
    AudioSource coinSes;

    [SerializeField]
    AudioSource enemySes;

    int highScore;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    // Start is called before the first frame update
    void Start()
    {
         //skoru ekrana yazdýrýyoruz. bizim skorumuz zaten 0 ile baþladýðý için onu
        //çaðýrdýk. to string deme sebebimiz scorun string, skorun int olmasý. o yüzden int i stringe çevirdik.
        if (gameManager.isRestart == true)
        {
            baslangicPanel.SetActive(false); // oyunumuzda yandýðýmýzda yeniden oyna paneli ile baþlangýç paneli
            // denk geliyor ama bunu istemiyoruz. bu yüzden oyna dediðimizde diðer oyna çýkmayacak.
            
        }
        scoreText.text = score.ToString();

       
        highScore = PlayerPrefs.GetInt("highScore");
         highScoreText.text = "Yüksek Skor: " + PlayerPrefs.GetInt("highScore").ToString();
       // highScoreText.text = "Yüksek Skor: " + highScore.ToString();


    }

    
    private void FixedUpdate()

    {
        if (oyunBasladiMi == false)
        {
            return;
        }


        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }

            float horizontal = Input.GetAxis("Horizontal");
        move(horizontal);
        animasyon(horizontal);
        turnMove(horizontal);

       


    }

    void move(float horizontal)
    {
        //Yürüme
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); //velocity hareket kontrol ediyor
    }

    void animasyon(float horizontal)
    {
        //Animasyon
        if (horizontal != 0)
        {
            kosuyorMu = true;
        }
        else
        {
            kosuyorMu = false;
        }

        anim.SetBool("kosuyorMu", kosuyorMu);
    }

    void turnMove(float horizontal)
    {
        //Yön Verme
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(4f, 4f, 1f); //vector x,y,z deðerini alýr
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-4f, 4f, 1f); //local scale yön deðiþtirme(karakterin) yapar. 
        }
    }

     private void OnTriggerEnter2D(Collider2D collision) // altýna deðdiðimiz an puan kazanýyoruz.
    {
        if (collision.CompareTag("cherrie")) // eðer ayarladýðým tag karakterime deðiyorsa;
        {
            coinSes.Play();
            scoreCounter(collision,1);

        }
        else if (collision.CompareTag("enemy"))
        {
            enemySes.Play();
            death();
        }
        else if (collision.CompareTag("superOdul"))
        {
            coinSes.Play();
            scoreCounter(collision,5);
        }
        else if (collision.CompareTag("kupa"))
        {
            string level = SceneManager.GetActiveScene().name;
            

            if (level == "Level 4")
            {
                SceneManager.LoadScene("Level 5");

              
            }

            
        
            else if (level == "Level 5")
            {
                bitisPanel.SetActive(true);           
            }
            
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

               

            }

        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("death"))// triggerda direkt kontrol edebiliyorken collision ile edilmiyo
                                                     //bu yüzden game object compara tag dedik. 
        {
            death();
        }

    }

    void scoreCounter(Collider2D collision, int puan)
    {
        score += puan;         //puaný arttýr 
        Destroy(collision.gameObject); // destroy nesneyi yok eder. altýna ulaþýnca altýn yok olucak
        scoreText.text = score.ToString(); // altýna dokunduysam skoru yazdýr. 
       

    }
    void death()
    {
        // transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        // transform.Rotate(new Vector3(0, 0, 90));
        Destroy(this.gameObject); // bu scripte ait olan game objecti yok et 
        yenidenOynaPanel.SetActive(true); // adam öldüyse bu panel çalýþsýn 
        panelScoreText.text = "Skor: " + score.ToString(); // panel gözükünce skoru yazdýrýyor

        

    }

    public void oyunaBasla()
    {
        oyunBasladiMi = true;
        baslangicPanel.SetActive(false);
    }

   
}
