using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yerdeMi : MonoBehaviour
{
    public LayerMask layer;
    public bool yerdeMiyiz; //yerde olup olmadýðýný kontrol edicez
    public Rigidbody2D rb;
    public float speed = 8f; //zýplama yüksekliði 

   

    // Update is called once per frame
    void Update()
    {
        if (Player.oyunBasladiMi == false)
        {
            return;
        }

        RaycastHit2D carpiyorMu = Physics2D.Raycast(transform.position, Vector2.down, 0.34f, layer); //baþlangýç pozisyonum(ayak)dan nereye doðru gitsin
        //onu seçiyoruz. Yani ayaktan vector2.down dediðimiz için aþaðý doðru yani zemine çarpýcak ve anlayacak
        // 0.15 = foot u seçip bacak arasý ve zemin arasýndaki mesafeyi buluyoruz, çýkarma iþlemi yapýyoruz.
        // neye çarpsýn kýsmý için de layer dedik 
        if (carpiyorMu.collider != null) // bir þeye çarpýyorsa null yani boþ deðer vermiyorsa;
        {
            // Zemine çarpýyor
            yerdeMiyiz = true;
        }
        else
        {
            //Havada kalýyor
            yerdeMiyiz = false;
        }

        // Klavyeden tuþa basma iþlemleri Input sýnýfý ile yapýlýr. Eðer tuþa basýlmasý kontrol ediliyorsa GetKeyDown
        // komutu kullanýlýr. Parametresi (KeyCode) dur. Eðer KeyCode .  dersek bütün her þeye ulaþýrýz. (tuþlara)
        if (yerdeMiyiz == true && Input.GetKeyDown(KeyCode.Space)) //eðer yerdeyse ve space tuþuna basýldýysa;
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

}