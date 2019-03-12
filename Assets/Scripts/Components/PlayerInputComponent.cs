using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PlayerInputComponent : MonoBehaviour
    {
        Vector2 m_Inputs = new Vector2();

        public delegate void DirectionalInput(Vector2 direction);
        public delegate void ButtonInput();

        public DirectionalInput DirectionalPad;
        public ButtonInput ButtonA;
        public ButtonInput ButtonB;
        public ButtonInput ButtonX;
        public ButtonInput ButtonY;
        public ButtonInput ButtonL;
        public ButtonInput ButtonR;
        public ButtonInput ButtonStart;
        public ButtonInput ButtonSelect;

        void Update()
        {

            var dir = new Vector2()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            DirectionalPad?.Invoke(dir);

            if (Input.GetButtonDown("ButtonA")) { ButtonA(); }
            if (Input.GetButtonDown("ButtonB")) { ButtonB(); }
            if (Input.GetButtonDown("ButtonX")) { ButtonX(); }
            if (Input.GetButtonDown("ButtonY")) { ButtonY(); }
            if (Input.GetButtonDown("ButtonL")) { ButtonL(); }
            if (Input.GetButtonDown("ButtonR")) { ButtonR(); }
            if (Input.GetButtonDown("ButtonStart")) { ButtonStart(); }
            if (Input.GetButtonDown("ButtonSelect")) { ButtonSelect(); }
        }

        private void AcknowledgeFlags()
        {
            //Dialog

            if (GetComponent<Player>().m_DialogBox.DialogActiveFlag)
            {
                GetComponent<Player>().m_DialogBox.ProcessDialogInput();
            }

            //Animation

            if (GetComponent<Player>().m_AnimationHelper.AnimationActiveFlag)
            {
                GetComponent<Player>().m_AnimationHelper.ProcessAnimationInput();
            }

            //Switch Acknowledge Flag to True

            GetComponent<Player>().m_Acknowledge = true;
        }

        public void DirectionalInputMenu()
        {
            MenuManager.Instance.m_Cursor.ValidateInput(m_Inputs);
        }

        public void GetDirectionalInput()
        {

            Vector2 _movement = Vector2.zero;
            m_Inputs = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));

            switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
            {
                case GameState.LOCKED: break;
                case GameState.MENU: MenuManager.Instance.m_Cursor.ValidateInput(m_Inputs); break;
                case GameState.OVERWORLD: if (GetComponent<Player>().m_Acknowledge) { _movement = m_Inputs; } break;
                case GameState.DIALOG: break;
                default: break;
            }

            GetComponent<CharacterComponent>().Move(_movement);
        }

        public void GetAButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonA"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: MenuManager.Instance.m_Cursor.Select(); break;
                    case GameState.OVERWORLD:

                        if (!GetComponent<Player>().m_Acknowledge) { AcknowledgeFlags(); break; }

                        bool collision = true; //Set to true right now until collision can be passed OR may pass a raycast intersection flag
                        if (collision)
                        {
                            Interact();
                        }
                        else
                        {
                            Sprint();
                        }

                        break;
                    case GameState.DIALOG:
                        //GameManager.instance.m_DialogBox.ProcessDialogInput();
                        //GetComponent<Player>().m_AnimationHelper.ProcessAnimationInput();
                        //GetComponent<Player>().m_DialogBox.ProcessDialogInput();
                        AcknowledgeFlags();
                        break;
                    default: break;
                }

            }
        }

        public void GetBButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonB"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: break;
                    case GameState.OVERWORLD: Attack(); break;
                    case GameState.DIALOG:
                        GetComponent<Player>().m_DialogBox.ProcessDialogInput();
                        //GetComponent<Player>().m_AnimationHelper.ProcessAnimationInput();
                        break;
                    default: break;
                }

            }
        }

        public void GetXButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonX"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: break;
                    case GameState.OVERWORLD: OpenMap(); break;
                    case GameState.DIALOG: break;
                    default: break;
                }

            }
        }

        public void GetYButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonY"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: break;
                    case GameState.OVERWORLD: if (!GetComponent<Player>().m_DialogBox.DialogActiveFlag) { UseItem(); } break;
                    case GameState.DIALOG: break;
                    default: break;
                }

            }
        }

        public void GetLButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonL"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: break;
                    case GameState.OVERWORLD: GetComponent<Player>().InvincibilityFrames(); break;
                    case GameState.DIALOG: break;
                    default: break;
                }

            }
        }

        public void GetRButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonR"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU: break;
                    case GameState.OVERWORLD:
                        GetComponent<Player>().m_MoveSpeedMultiplier = 2.0f;
                        break;
                    case GameState.DIALOG: break;
                    default: break;
                }
            }

            if (UnityEngine.Input.GetButtonUp("ButtonR"))
            {
                GetComponent<Player>().m_MoveSpeedMultiplier = 1.0f;
            }
        }

        public void GetStartButtonInput()
        {
            if (UnityEngine.Input.GetButtonDown("ButtonStart"))
            {

                switch (GetComponent<Player>().m_PlayerStateStack[GetComponent<Player>().m_PlayerStateStack.Count - 1])
                {
                    case GameState.LOCKED: break;
                    case GameState.MENU:
                        MenuManager.Instance.CloseCurrentMenu();
                        GetComponent<Player>().m_PlayerStateStack.Remove(GameState.MENU);
                        break;
                    case GameState.OVERWORLD:
                        OpenInventory();
                        GetComponent<Player>().m_PlayerStateStack.Add(GameState.MENU);
                        break;
                    case GameState.DIALOG: break;
                    default: break;
                }

            }
        }

        //These functions should be migrated to Player.cs
        //Not all inputs currently have functions

        public void Interact()
        { //Tied to pressing A Button

            Interactable o_Interactable = GetComponent<Player>().ParseInteraction();
            if (o_Interactable != null) { o_Interactable.OnInteract(GetComponent<Player>()); }
        }

        public void Sprint()
        { //Tied to holding down A Button while standing still
          //Short channel before sprint
          //Begin Sprint (directional input no longer required for character to continue moving)
          //Bonk!
        }

        public void Run()
        { //Tied to holding down R Shoulder Button
          //Change movespeed while button is held
          //1.5x Animation speed
          //Return movespeed when button is released
          //Return Animation speed to normal
        }

        public void Attack()
        { //Tied to pressing B Button

            GetComponent<Player>().m_Weapon.Use(GetComponent<Player>());

            //Play Attack Animation

            if (!GetComponent<Animator>().GetBool("attacking")) { StartCoroutine(IAttack()); }
            //Check for Collisions
            //Raycast?
            //Call Damage function, etc.
        }

        public void UseItem()
        { //Tied to pressing Y Button

            //Use Item
            GetComponent<Player>().m_EquippedItem.UseItem();

            //Animation
        }

        public void OpenMap()
        { //Tied to pressing X Button
          //Open Map
          //Close Map
        }

        public void OpenInventory()
        {

            MenuManager.Instance.OpenMenu(GetComponent<Player>().m_Inventory.m_InventoryMenu);
        }

        IEnumerator IAttack()
        {

            GetComponent<Animator>().SetBool("attacking", true);

            float m_OriginalMovespeed = GetComponent<Player>().m_MoveSpeed;

            GetComponent<Player>().m_MoveSpeed = 0.0f * GetComponent<Player>().m_MoveSpeed;

            //GameManager.instance.m_GameStateStack.Add(GameState.LOCKED);

            yield return new WaitForSeconds(1f / 3f); //Replace with anim.ClipLength if possible: will need to figure out how to access the clip from the Animator

            //GameManager.instance.m_GameStateStack.Remove(GameState.LOCKED);

            GetComponent<Animator>().SetBool("attacking", false);

            GetComponent<Player>().m_MoveSpeed = m_OriginalMovespeed;
        }
    }
}