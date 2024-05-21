using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDemo : MonoBehaviour
{
    SoundManager _bgm;
    Animator _anim;
    public GameObject _camaraTree;
    public GameObject _camaraPrincipal;
    public GameObject _bossColliders;
    public GameObject _bossUI;
    [SerializeField] private float _secondsToWait = 1f;

    private void Awake()
    {
        _bgm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _anim = _camaraTree.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _camaraTree.SetActive(true);
            _camaraPrincipal.SetActive(false);
            _bgm.StopBGM();
            SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.presentationBoss);
            StartCoroutine(PauseToWatch());
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator PauseToWatch()
    {
        yield return new WaitForSeconds(_secondsToWait);
        _bgm.ChangeBGM(_bgm.lvl2Tree);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
