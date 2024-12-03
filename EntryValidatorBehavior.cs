
using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace RecibosNominaCGA.ViewModels;
public class EntryValidatorBehavior : Behavior<Entry>
{
    public static readonly BindableProperty ValidationCommandProperty =
        BindableProperty.Create(nameof(ValidationCommand), typeof(ICommand), typeof(EntryValidatorBehavior));

    public ICommand ValidationCommand
    {
        get => (ICommand)GetValue(ValidationCommandProperty);
        set => SetValue(ValidationCommandProperty, value);
    }

    protected override void OnAttachedTo(Entry entry)
    {
        base.OnAttachedTo(entry);
        entry.TextChanged += OnTextChanged;
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        base.OnDetachingFrom(entry);
        entry.TextChanged -= OnTextChanged;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            // Ejecutar acción si el campo está vacío
            ValidationCommand?.Execute(null);
        }
    }
}