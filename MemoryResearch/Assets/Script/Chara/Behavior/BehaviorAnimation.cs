using UnityEngine;

public static class BehaviorAnimation
{
    /// <summary>
    /// アニメーションが引数animNameのアニメーションになっているか確認する
    /// なっていなければ、もう一度SetTriggerで再生しなおす
    /// </summary>
    /// <param name="animator">animator</param>
    /// <param name="animName">アニメーションの名前</param>
    /// <param name="layerNo">レイヤー番号</param>
    /// <returns></returns>
    public static bool CheckChangedAnimation(ref Animator animator, string animName,int layerNo = 0)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            animator.SetTrigger(animName);
            return false;
        }
        return true;
    }
}