public class Chest : Interactable {

    public bool m_IsOpen = false;

    public override void OnInteract(Player player) {

        base.OnInteract(player);

        OpenChest();
    }

    public void OpenChest() {

        if(m_IsOpen) { return; }

        UseAlternateSprite();
        AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_OpenChestSoundEffect, 0.1f);

        if(m_ContainsItem) {

            AddItem();

        } else {

            m_InteractingPlayer.m_DialogBox.UpdateDialog("It's empty.");
        }

        m_IsOpen = !m_ContainsItem;
    }
}