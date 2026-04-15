public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

enum Menu
{
    Options,
    Inventory,
    ItemOptions,
    ItemDescriprion,
    Stats,
    DialogueOptions,
    MercyOptions,
    Attack
}

public enum GameState
{
    Gameplay,
    MainMenu,
    BattleMenu,
    GameplayDialogue,
    BattleDialogue,
    Fight,
    CutScene
}

public enum ActionPanelSize
{
    Square,
    Max,
}

public enum AnimationAxis
{
    Horizontal,
    Vertical,
}