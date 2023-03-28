using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float fireTimer = 10f;
    public int maxJumps = 2;
    private int jumpCount = 0;
    public int maxSlideCount = 2;
    private int slideCount = 0;
    public float jump;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float animationTime = 2f;
    private float animationTimer = 0f;
    public Animator Anim;
    public float jumpCooldown = 0.5f;
    private float lastJumpTime = 0f;
    public GameObject DeadPanel;
    private AudioSource audioSource;
    public GameObject PauseButton;
    public Button attackButton, jumpButton;
    public Text countdownText;
    public float timeToWait = 10f;
    private MusicPlayer Mp;
    private BannerView bannerView;
    private InterstitialAd inter;
    private InterstitialAd interstitial;
    private int rastegelesayi;
    bool reklamSayar = true;
    public AudioSource AttackButtonMusic;
    public GameObject BiziDegerlendirPanel;
    private float rastgeleSayi;
    float rastgelebizidegerlendir;
    private void Start()
    {
        rastgelebizidegerlendir = Random.RandomRange(1,4);
        RequestInterstitial();
        MobileAds.Initialize(initStatus => { });
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        RequestBanner();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3262921798977956/8845651783";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, 90, -70);

        // Called when the ad click caused the user to leave the application.
        //this.bannerView.OnAdLeavingApplication += this.OyunTerkediliyor;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3262921798977956/7569332780";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void gameover()
    {
        if (reklamSayar)
        {
            if (this.inter.IsLoaded())
            {
                this.inter.Show();
                reklamSayar = false;
            }

        }

    }
    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            bannerView.Destroy();

            if (ShouldShowInterstitial())
            {
                this.interstitial.Show();
                reklamSayar = false;
            }
            else
            {
                reklamSayar = true;
            }
            if (rastgelebizidegerlendir == 2f)
            {
                BiziDegerlendirPanel.SetActive(true);
            }
            Anim.SetTrigger("Die");
            PauseButton.SetActive(false);
            DeadPanel.SetActive(true);
            ShowDieButton();
            attackButton.interactable = false;
            jumpButton.interactable = false;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            bannerView.Destroy();
            
            Anim.SetTrigger("Die");
            PauseButton.SetActive(false);
            DeadPanel.SetActive(true);
            ShowDieButton();
            attackButton.interactable = false;
            jumpButton.interactable = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            bannerView.Destroy();

            if (ShouldShowInterstitial())
            {
                this.interstitial.Show();
                reklamSayar = false;
            }
            else
            {
                reklamSayar = true;
            }
            if (rastgelebizidegerlendir == 2f)
            {
                BiziDegerlendirPanel.SetActive(true);
            }
            Anim.SetTrigger("Die");
            PauseButton.SetActive(false);
            DeadPanel.SetActive(true);
            ShowDieButton();
            attackButton.enabled = false;
            jumpButton.enabled = false;

        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            bannerView.Destroy();

            Anim.SetTrigger("Die");
            PauseButton.SetActive(false);
            DeadPanel.SetActive(true);
            ShowDieButton();
            attackButton.interactable = false;
            jumpButton.interactable = false;
        }
    }

    private bool ShouldShowInterstitial()
    {
        if (!reklamSayar || !this.interstitial.IsLoaded())
        {
            return false;
        }

        return true;
    }

    private void ShowDieButton()
    {
        DieFinish();
    }


    public void JumpButton()
    {
        if (CanJump())
        {
            if (!isGrounded && jumpCount == 1)
            {
                Anim.SetTrigger("Slide");
            }
            rb.velocity = Vector2.up * jump;
            jumpCount++;
            lastJumpTime = Time.time;
        }
    }

    private bool CanJump()
    {
        if (Time.time - lastJumpTime < jumpCooldown)
        {
            return false;
        }
        if (jumpCount >= maxJumps && !isGrounded)
        {
            return false;
        }

        return true;
    }
    public void DieFinish()
    {
        Anim.SetTrigger("Die");
        PauseButton.SetActive(false);
        StartCoroutine(Diebutton());
        
    }
    public void AttackAnimationFinished()
    {
        StartCoroutine(EnableButton());
        StartCoroutine(Countdown());
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(bulletPrefab, spawnPosition, spawnRotation);
        fireTimer = 0f;
        StartCoroutine(EnableButton());
        StartCoroutine(Countdown());
        countdownText.text = "";
        attackButton.interactable = false;
    }
    public void AttackButton()
    {
        if (fireTimer >= timeToWait)
        {
            AttackButtonMusic.Play();
            Anim.SetTrigger("Attack");
        }
        else
        {

        }
    }
    IEnumerator Diebutton()
    {
        jumpButton.interactable = false;
        attackButton.interactable = false;
        GameObject musicPlayerObject = GameObject.Find("MusicPlayer");
        MusicPlayer musicPlayer = musicPlayerObject.GetComponent<MusicPlayer>();
        musicPlayer.StopMusic();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(timeToWait);
        attackButton.interactable = true;
    }

    IEnumerator Countdown()
    {
        float timeLeft = timeToWait;
        while (timeLeft > 0f)
        {
            countdownText.text = Mathf.RoundToInt(timeLeft).ToString();
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        countdownText.text = "";
    }
}