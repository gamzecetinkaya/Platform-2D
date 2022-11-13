using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yerdeMi : MonoBehaviour
{
    public LayerMask layer;
    public bool yerdeMiyiz; //yerde olup olmad���n� kontrol edicez
    public Rigidbody2D rb;
    public float speed = 8f; //z�plama y�ksekli�i 

   

    // Update is called once per frame
    void Update()
    {
        if (Player.oyunBasladiMi == false)
        {
            return;
        }

        RaycastHit2D carpiyorMu = Physics2D.Raycast(transform.position, Vector2.down, 0.34f, layer); //ba�lang�� pozisyonum(ayak)dan nereye do�ru gitsin
        //onu se�iyoruz. Yani ayaktan vector2.down dedi�imiz i�in a�a�� do�ru yani zemine �arp�cak ve anlayacak
        // 0.15 = foot u se�ip bacak aras� ve zemin aras�ndaki mesafeyi buluyoruz, ��karma i�lemi yap�yoruz.
        // neye �arps�n k�sm� i�in de layer dedik 
        if (carpiyorMu.collider != null) // bir �eye �arp�yorsa null yani bo� de�er vermiyorsa;
        {
            // Zemine �arp�yor
            yerdeMiyiz = true;
        }
        else
        {
            //Havada kal�yor
            yerdeMiyiz = false;
        }

        // Klavyeden tu�a basma i�lemleri Input s�n�f� ile yap�l�r. E�er tu�a bas�lmas� kontrol ediliyorsa GetKeyDown
        // komutu kullan�l�r. Parametresi (KeyCode) dur. E�er KeyCode .  dersek b�t�n her �eye ula��r�z. (tu�lara)
        if (yerdeMiyiz == true && Input.GetKeyDown(KeyCode.Space)) //e�er yerdeyse ve space tu�una bas�ld�ysa;
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

}