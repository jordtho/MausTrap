using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Managers;

class Enemy : Character {

    public int m_CollisionDamage = 1;
    public float m_CollisionKnockbackForce = 20;

    public CircleCollider2D m_AggroCollider;
    public List<Character> m_CharacterTargetList; 
    public float m_AggroRange = 10.0f;
    [Range(0,180)] public float m_FieldofView;

    void Awake() {

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();

        if(!GetComponentInChildren<CircleCollider2D>()) {

            GameObject _NewAggroObject = Instantiate(GameManager.Instance.m_AggroRangeObject);
            _NewAggroObject.transform.SetParent(transform);
            _NewAggroObject.transform.position = transform.position;
            _NewAggroObject.transform.name = "AggroRangeObject";
        }

        m_AggroCollider = GetComponentInChildren<CircleCollider2D>();
        m_AggroCollider.radius = m_AggroRange;
    }

    void Update() {

        if(m_CharacterTargetList.Count > 0) {
            MoveCharacter(new Vector2(m_CharacterTargetList[0].transform.position.x - transform.position.x, m_CharacterTargetList[0].transform.position.y - transform.position.y));
        }
    }

    public void AcquireTarget(Character target) {

        if(m_HP > 0) {

            Character _NewTarget = target;

            RaycastHit2D hit = Physics2D.Linecast(transform.position, _NewTarget.transform.position, LayerMask.NameToLayer("Player"));

            if(hit.collider != null) {

                if(hit.collider.GetInstanceID() == _NewTarget.GetInstanceID()) {

                    if(GameManager.Instance.m_DebugMode) { Debug.DrawLine(transform.position, _NewTarget.transform.position, Color.green); }

                } else {

                    if(GameManager.Instance.m_DebugMode) { Debug.DrawLine(transform.position, _NewTarget.transform.position, Color.red); }
                    DisengageTarget(target);
                    return;
                }
            }

            if(!m_CharacterTargetList.Contains(_NewTarget)) {
                m_CharacterTargetList.Add(_NewTarget);
            }
        }
    }

    public void DisengageTarget(Character target) {

        if(m_CharacterTargetList.Contains(target)) { m_CharacterTargetList.Remove(target); }
        if(m_CharacterTargetList.Count == 0) { MoveCharacter(Vector2.zero); }
    }
}