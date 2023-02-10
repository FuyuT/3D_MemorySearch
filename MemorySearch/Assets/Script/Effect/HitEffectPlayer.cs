using UnityEngine;

public class HitEffectPlayer : MyUtil.SingletonMonoBehavior<HitEffectPlayer>
{
    /*******************************
    * private
    *******************************/
    EffectPlayer player;

    private void Awake()
    {
        player = new EffectPlayer();
        player.SetEffects(effects);
    }
    /*******************************
    * protected
    *******************************/
    [SerializeField] protected Effekseer.EffekseerEmitter[] effects;
    protected override bool dontDestroyOnLoad { get { return false; } }
    /*******************************
    * public
    *******************************/
    public void PlayEffect(int no = 0)
    {
        player.PlayEffect(no);
    }
    public void PlayEffect(Vector3 pos, int no = 0)
    {
        player.PlayEffect(pos, no);
    }
    public void StopEffect(int no = 0)
    {
        player.StopEffect(no);
    }
}
