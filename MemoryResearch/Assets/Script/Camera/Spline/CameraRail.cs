using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

public class CameraRail : MonoBehaviour
{
    // ����
    public bool isClose = false;

    // ���[���_�̃��X�g
    public List<GameObject> railPoints = new List<GameObject>();

    //----
    // �A�N�Z�T
    //----

    // ���[���_�̐����擾����
    public int getRailPointNum()
    {
        return railPoints.Count;
    }

    // ���[���_���擾����
    public GameObject getRailPoint(int i)
    {
        Debug.Assert(railPoints.Count > 0);
        return railPoints[i];
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        for (int i = 0, n = railPoints.Count; i < n; ++i)
        {
            if (i == n - 1 && !isClose)
            {
                break;
            }

            GameObject from = railPoints[i];
            GameObject to = railPoints[(i + 1) % n];
            Gizmos.color = Color.green;
            Gizmos.DrawLine(from.transform.position, to.transform.position);
        }
    }

    // ���[���̃J�X�^���G�f�B�^
    [CustomEditor(typeof(CameraRail))]
    public class RailEditor : Editor
    {
        ReorderableList railPointsReorderableList;

        private CameraRail rail
        {
            get
            {
                return target as CameraRail;
            }
        }

        void OnEnable()
        {
            railPointsReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("railPoints"));

            // �R�[���o�b�N�ݒ�
            railPointsReorderableList.onAddCallback += AddRailPoint;
            railPointsReorderableList.onRemoveCallback += RemoveRailPoint;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();

            railPointsReorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        // ���[���_��ǉ�����
        private void AddRailPoint(ReorderableList list)
        {
            // ���[���_�̐����ʒu
            Vector3 position;
            {
                // �ŏ��̃��[���_ �� ���[���̈ʒu
                if (rail.railPoints.Count == 0)
                {
                    position = rail.transform.position;
                }
                // ���Ƀ��[���_������ �� �Ō�̃��[���_�̈ʒu
                else
                {
                    GameObject railPoint_Last = rail.railPoints[rail.railPoints.Count - 1];
                    position = railPoint_Last.transform.position;
                }
            }

            // ���[���_����
            GameObject prefab = (GameObject)Resources.Load("Prefabs/RailPoint");
            GameObject railPoint_New = Instantiate(prefab, position, Quaternion.identity);

            // ���X�g�ɒǉ�
            rail.railPoints.Add(railPoint_New);

            // ���[���̎q�ɂ���
            railPoint_New.transform.parent = rail.transform;
        }

        // ���[���_���폜����
        private void RemoveRailPoint(ReorderableList list)
        {
            GameObject railPoint = rail.railPoints[list.index];
            rail.railPoints.Remove(railPoint);
            DestroyImmediate(railPoint);
        }
    }
}
