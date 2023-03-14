using UnityEngine;

public static class BehaviorAnimation
{
    /// <summary>
    /// Triggerの更新を行う
    /// アニメーションが引数animNameのアニメーションになっていなければ、
    /// SetTriggerを行って、falseを返す
    /// すでに変更が終わっている場合は、
    /// ResetTriggerを行って、trueを返す
    /// </summary>
    /// <param name="animator">animator</param>
    /// <param name="animName">アニメーションの名前</param>
    /// <param name="layerNo">レイヤー番号</param>
    /// <returns></returns>
    public static bool UpdateTrigger(ref Animator animator, string animName, int layerNo = 0)
    {
        if (!animator.GetCurrentAnimatorStateInfo(layerNo).IsName(animName))
        {
            animator.SetTrigger(animName);
            return false;
        }
        else
        {
            animator.ResetTrigger(animName);
        }
        return true;
    }

    /// <summary>
    /// 再生しているアニメーションと名前が同じかを返す
    /// 同じならtrue,違えばfalse
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="animName"></param>
    /// <param name="layerNo"></param>
    /// <returns></returns>
    public static bool IsName(ref Animator animator, string animName, int layerNo = 0)
    {
        if (!animator.GetCurrentAnimatorStateInfo(layerNo).IsName(animName))
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// 再生一週目が終了しているかを返す
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="animName"></param>
    /// <param name="layerNo"></param>
    /// <returns></returns>
    public static bool IsPlayEnd(ref Animator animator, string animName, int layerNo = 0)
    {
        //現在再生しているアニメーションが、確認したいアニメーションと違う場合は終了
        if (!IsName(ref animator, animName, layerNo)) return false;
        
        if (animator.GetCurrentAnimatorStateInfo(layerNo).normalizedTime >= 1)
        {
            return true;
        }

        return false;
    }

}