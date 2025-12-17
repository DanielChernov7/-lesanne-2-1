namespace WpfArraysApp.ViewModels;

public enum SearchField
{
    Brand,
    Model,
    Year
}

public class SearchFieldOption
{
    public SearchFieldOption(string displayName, SearchField field)
    {
        DisplayName = displayName;
        Field = field;
    }

    public string DisplayName { get; }
    public SearchField Field { get; }
}

public class StatusFilterOption
{
    public StatusFilterOption(string displayName, Models.Status? value)
    {
        DisplayName = displayName;
        Value = value;
    }

    public string DisplayName { get; }
    public Models.Status? Value { get; }
}
