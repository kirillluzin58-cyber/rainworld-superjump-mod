namespace SuperJump;

sealed class Options : OptionInterface
{
    public static readonly Options instance = new Options();

    public readonly OptionBool speedBoost = new OptionBool(true, "SuperJump_speedBoost");
    public readonly OptionInt jumpPower = new OptionInt(3, 0, 10, 1, "SuperJump_jumpPower");

    private Options() { }

    public override string ModGetText(string textID)
    {
        switch (textID)
        {
            case "SuperJump_speedBoost": return "Ускорение";
            case "SuperJump_jumpPower": return "Сила прыжка";
        }
        return base.ModGetText(textID);
    }
}
