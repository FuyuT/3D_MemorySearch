using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/*����enum�́A�K��csv�f�[�^�uData/AdjustmentData�@�������ݒ�p�v
  �ɗ񋓂���Ă��郁������id�Ɠ����ԍ��ɂ��Ă�������
  �����Ă��Ȃ��Ɛ������ǂݍ��ނ��Ƃ��ł��܂���*/
public enum MemoryType
{
    None,
    Jump,
    DowbleJump,
    Punch,
    AirDush,
    Dush,
    Tackle,
    Guard,
    UpperDown,
    JetPack,
    BigPunch,
    InvisibleGuard,
    Boost,
    Slam,
    Count,
}

[System.Serializable]
public class Memory
{
    public MemoryType   type;
    public string       explanation;
    public MemoryType[] materialType;

    public Memory()
    {
        type = new MemoryType();
        materialType = new MemoryType[Global.MemoryMaterialMax];
    }
}
