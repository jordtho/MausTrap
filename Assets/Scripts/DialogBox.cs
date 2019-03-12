using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Managers;

public class DialogBox : MonoBehaviour {

    public Player m_Player;
    public GameObject m_DialogButton;
    private List<string> m_DialogQueue;
    private Text m_TextComponent;
    private List<Animator> m_Anim; //Dialog button animations
    private IEnumerator m_IDialog; //IEnumerator object used in Coroutine
    private bool m_AlreadyShowingText;
    private bool m_Typing;
    public bool DialogTypingFlag { get { return m_Typing; } } //Intentionally read-only
    private bool m_DialogAcknowledged = true;
    public bool DialogActiveFlag { get { return !m_DialogAcknowledged; } } //Intentionally read-only

    private void Awake() {

        m_DialogQueue = new List<string>();
        m_TextComponent = GetComponentInChildren<Text>();
        m_Anim = new List<Animator>(m_DialogButton.GetComponentsInChildren<Animator>());
    }

    private void Update() { }

    public void ProcessDialogInput() { // Both interrupts and acknowledges current dialog

        if(m_Typing) {

            InterruptTyping();
        }

        if (!m_DialogAcknowledged){

            AcknowledgeDialog();
        }
    }

    public void ProcessDialogInputLong() { // First interrupts dialog, then acknowledges

        if(m_Typing) {

            InterruptTyping();

        } else {

            AcknowledgeDialog();
        }
    }

    public void ProcessDialogInputUnskippable() { // Does not allow acknowledge until complete dialog displays

        if(!m_Typing) {

            AcknowledgeDialog();
        }
    }

    public void UpdateDialog(string text) {

        if(!isActiveAndEnabled) { ShowDialog(); }

        AddToQueue(text);

        if(!m_AlreadyShowingText) {

            m_AlreadyShowingText = true;
            ProcessQueue();
        }
    }

    private void UpdateButtonAnimation() {

            SetButtonAnimation("AlreadyShowingText", m_AlreadyShowingText);
            SetButtonAnimation("DialogAcknowledged", m_DialogAcknowledged);
            SetButtonAnimation("Typing", m_Typing);
    }

    private void SetButtonAnimation(string parameter, bool a) {

        foreach(Animator anim in m_Anim) {

            anim.SetBool(parameter, a);
        }
    }

    private void ProcessQueue() {

        ClearTextComponent();
        SetIDialog(m_DialogQueue[0]);
        StartDialogCoroutine();
        m_Player.m_Acknowledge = false;
        m_DialogAcknowledged = false;
    }

    private void AcknowledgeDialog() {

        RemoveFrontOfQueue();

        if(m_DialogQueue.Count > 0) {

            ProcessQueue();

        } else {

            m_AlreadyShowingText = false;
            UpdateButtonAnimation(); //Hide Typing and Acknowledge buttons
            m_DialogAcknowledged = true;
            HideDialog();
        }
    }

    private void ShowDialog() {

        gameObject.SetActive(true);
    }

    private void HideDialog() {

        gameObject.SetActive(false);
    }

    private void InterruptTyping() {

        StopDialogCoroutine();
        SetTextComponent(m_DialogQueue[0]);
        m_Typing = false;
        UpdateButtonAnimation(); //Acknowledge button
    }

    private void AddToQueue(string text) {

        m_DialogQueue.Add(text);
    }

    private void RemoveFrontOfQueue() {

        if(m_DialogQueue.Count > 0) {

            m_DialogQueue.RemoveAt(0);
        }
    }

    private void SetIDialog(string text) {

        m_IDialog = IUpdateText(text);
    }

    private void StartDialogCoroutine() {

        StartCoroutine(m_IDialog);
    }

    private void StopDialogCoroutine() {

        StopCoroutine(m_IDialog);
    }

    private void ClearTextComponent() {

        m_TextComponent.text = "";
    }

    private void SetTextComponent(string text) {

        m_TextComponent.text = text;
    }

    IEnumerator IUpdateText(string strComplete) {

        m_Typing = true;
        UpdateButtonAnimation(); //Typing button

        foreach(var character in strComplete) {

            m_TextComponent.text += character;
            
            yield return new WaitForSecondsRealtime(1f - GameManager.Instance.m_TextSpeed / 10f);
            //yield return null;

        }

        m_Typing = false;
        UpdateButtonAnimation(); //Acknowledge button
    }
}