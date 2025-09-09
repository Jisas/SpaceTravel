using UnityEngine.InputSystem;

public static class InputsManager
{
    private static readonly MyInputActions input;

    static InputsManager()
    {
        input = new MyInputActions();
        input.Player.Enable();
    }

    public static MyInputActions.PlayerActions Player
    {
        get { return input.Player; }
    }

    public static MyInputActions.UIActions UI
    {
        get { return input.UI; }
    }

    public static void EnablePlayerMap(bool enable)
    {
        if (enable) input.Player.Enable();
        else input.Player.Disable();
    }

    public static void EnableUIMap(bool enable)
    {
        if (enable) input.UI.Enable();
        else input.UI.Disable();
    }

    public static bool GetIsCurrentDiviceMouse(PlayerInput playerInput)
    {
        return playerInput.currentControlScheme == "Keyboard&Mouse";
    }
}
