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



    public static bool oyunBasladiMi = false; // static: oyun ba�lad� m� durdurana kadar �al���r. 

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
         //skoru ekrana yazd�r�yoruz. bizim skorumuz zaten 0 ile ba�lad��� i�in onu
        //�a��rd�k. to string deme sebebimiz scorun string, skorun int olmas�. o y�zden int i stringe �evirdik.
        if (gameManager.isRestart == true)
        {
            baslangicPanel.SetActive(false); // oyunumuzda yand���m�zda yeniden oyna paneli ile ba�lang�� paneli
            // denk geliyor ama bunu istemiyoruz. bu y�zden oyna dedi�imizde di�er oyna ��kmayacak.
            
        }
        scoreText.text = score.ToString();

       
        highScore = PlayerPrefs.GetInt("highScore");
         highScoreText.text = "Y�ksek Skor: " + PlayerPrefs.GetInt("highScore").ToString();
       // highScoreText.text = "Y�ksek Skor: " + highScore.ToString();


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
        //Y�r�me
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
        //Y�n Verme
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(4f, 4f, 1f); //vector x,y,z de�erini al�r
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-4f, 4f, 1f); //local scale y�n de�i�tirme(karakterin) yapar. 
        }
    }

     private void OnTriggerEnter2D(Collider2D collision) // alt�na de�di�imiz an puan kazan�yoruz.
    {
        if (collision.CompareTag("cherrie")) // e�er ayarlad���m tag karakterime de�iyorsa;
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
                                                     //bu y�zden game object compara tag dedik. 
        {
            death();
        }

    }

    void scoreCounter(Collider2D collision, int puan)
    {
        score += puan;         //puan� artt�r 
        Destroy(collision.gameObject); // destroy nesneyi yok eder. alt�na ula��nca alt�n yok olucak
        scoreText.text = score.ToString(); // alt�na dokunduysam skoru yazd�r. 
       

    }
    void death()
    {
        // transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        // transform.Rotate(new Vector3(0, 0, 90));
        Destroy(this.gameObject); // bu scripte ait olan game objecti yok et 
        yenidenOynaPanel.SetActive(true); // adam �ld�yse bu panel �al��s�n 
        panelScoreText.text = "Skor: " + score.ToString(); // panel g�z�k�nce skoru yazd�r�yor

        

    }

    public void oyunaBasla()
    {
        oyunBasladiMi = true;
        baslangicPanel.SetActive(false);
    }

   
}
