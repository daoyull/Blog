namespace AvaloniaBlog.Lib.Attritubes;

[AttributeUsage(AttributeTargets.Field)]
public class IconMark : Attribute
{
    public IconMark(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}