namespace BESTTieBreaker.Views
{
    using System.Windows.Controls;

    /// <summary>
    /// Interface for objects that emit SelectionChanged events
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Fires when a selection changes
        /// </summary>
        event SelectionChangedEventHandler SelectionChanged;
    }
}

