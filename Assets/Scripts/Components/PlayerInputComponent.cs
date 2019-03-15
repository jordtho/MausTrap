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
    }
}