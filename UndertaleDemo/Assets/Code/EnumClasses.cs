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
    Fight
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