using System.Windows;
using System.Windows.Controls;

namespace WpfAppVsNet4;

public class ProcessStageHelper : DependencyObject
{
    //public static double GetProcessCompletion(DependencyObject obj)
    //{
    //    return (double)obj.GetValue(ProcessCompletionProperty);
    //}

    //public static void SetProcessCompletion(DependencyObject obj, double value)
    //{
    //    obj.SetValue(ProcessCompletionProperty, value);
    //}

    // Using a DependencyProperty as the backing store for ProcessCompletion.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProcessCompletionProperty =
        DependencyProperty.RegisterAttached("ProcessCompletion",
            typeof(double), typeof(ProcessStageHelper),
            new PropertyMetadata(0.0, OnProcessCompletionChanged));

    // Using a DependencyProperty as the backing store for ProcessStage.  This enables animation, styling, binding, etc...
    public static readonly DependencyPropertyKey ProcessStagePropertyKey =
        DependencyProperty.RegisterAttachedReadOnly("ProcessStage", typeof(ProcessState),
            typeof(ProcessStageHelper),
            new PropertyMetadata(ProcessState.Stage1));

    public static readonly DependencyProperty ProcessStageProperty = ProcessStagePropertyKey.DependencyProperty;

    

    private static void OnProcessCompletionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        double progress = (double)e.NewValue;
        ProgressBar bar = (d as FrameworkElement).TemplatedParent as ProgressBar;

        if (progress >= 0 && progress < 20)
        {
            bar.SetValue(ProcessStagePropertyKey, ProcessState.Stage1);
        }

        if (progress >= 20 && progress < 40)
        {
            bar.SetValue(ProcessStagePropertyKey, ProcessState.Stage2);
        }
        if (progress >= 40 && progress < 60)
        {
            bar.SetValue(ProcessStagePropertyKey, ProcessState.Stage3);
        }
        if (progress >= 60 && progress < 80)
        {
            bar.SetValue(ProcessStagePropertyKey, ProcessState.Stage4);
        }

        if (progress >= 80 && progress <= 100)
        {
            bar.SetValue(ProcessStagePropertyKey, ProcessState.Stage5);
        }
    }
}

public enum ProcessState
{
    Stage1, Stage2, Stage3, Stage4, Stage5
}