public enum Condition {
    NONE,
    POISON,
    SLEEP,
    CONFUSE,
    STUN,
    BURN,
    FEEBLE,
    MAX_CONDITION
}

public enum InputDirection {
    UP,
    DOWN,
    LEFT,
    RIGHT,
    MAX_INPUTS
}

public enum GameState {
    LOCKED,
    MENU,
    OVERWORLD,
    DIALOG,
    MAX_GAMESTATE
}

public enum AnimationType {
    NONE,
    RUNNING,
    ATTACK,
    USE_ITEM,
    MAX_ANIMATIONTYPE
}