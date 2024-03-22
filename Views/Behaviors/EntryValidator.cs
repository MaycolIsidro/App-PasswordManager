namespace PasswordManager.Views.Behaviors;
public class EntryValidator : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        base.OnAttachedTo(entry);
    }
    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
    }
    void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        bool isValid = false;
        if (args.NewTextValue.Length > 0) isValid = true;
        Entry entry = (Entry)sender;
        Frame frame = (Frame)entry.Parent.Parent;
        frame.BorderColor = isValid ? Color.FromArgb("#E9EFFF") : Color.FromArgb("#FF3737");
    }
}
