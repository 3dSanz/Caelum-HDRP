using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaBoss : MonoBehaviour
{
    SoundManager _bgm;
    Boss _boss;
    public GameObject _camaraPrincipal;
    public GameObject _bossColliders;
    public GameObject _bossUI;
    public float _secondsToWait;
    public bool _initiatingCombat = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _bgm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _boss = GameObject.Find("BOSS 1").GetComponent<Boss>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _camaraPrincipal.SetActive(false);
            _bgm.StopBGM();
            _bossColliders.SetActive(true);
            SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.presentationBoss);
            _boss._anim.SetTrigger("presentation");
            StartCoroutine(PauseToWatch());
            _bossUI.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator PauseToWatch()
    {
        yield return new WaitForSeconds(_secondsToWait);
        _bgm.ChangeBGM(_bgm.lvl1Boss);
        _camaraPrincipal.SetActive(true);
        _initiatingCombat = true;
    }
}
