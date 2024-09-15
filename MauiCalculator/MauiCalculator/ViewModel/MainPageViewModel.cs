using System.Data;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiCalculator.ViewModel;

public partial class MainPageViewModel: ObservableObject
{
    private StringBuilder _historyBuilder = new StringBuilder();
    
    [ObservableProperty]
    private string _history = string.Empty;
    
    [ObservableProperty]
    private bool _isHistoryVisible = false;
    
    [ObservableProperty]
    private string _display = "0";
    
    [ObservableProperty]
    private string _result = "0";
    
    [RelayCommand]
    private void Number(string number)
    {
        if ( Display == "0")
        {
            Display = number;
        }
        else
        {
            Display += number;
        }
    }
    
    [RelayCommand]
    private void Operation(string op)
    {
        Display += $"{op}";
    }
    
    [RelayCommand]
    private void Clear()
    {
        Display = "0";
        Result = "0";
    }
    
    [RelayCommand]
    private void Calculate()
    {
        try
        {
            var result = new DataTable().Compute(Display, null);
            Result = result.ToString()!;
            
            _historyBuilder.AppendLine(Display);
            _historyBuilder.AppendLine(Result);
            _historyBuilder.AppendLine();
            
            Display = "0";
        }
        catch
        {
            Result = "Error";
        }
    }
    
    [RelayCommand]
    private void Backspace()
    {
        if (Display.Length > 1)
        {
            Display = Display.Remove(Display.Length - 1);
        }
        else
        {
            Display = "0";
        }
    }

    [RelayCommand]
    private void ShowHistory()
    {
        IsHistoryVisible = !IsHistoryVisible;
        History = _historyBuilder.ToString();
    }
}