using System.Windows.Input;

namespace UniversalModal.WPF.ModalContentPresenter
{
    /// <summary>
    /// Defines common commands for use with the ModalContentPresenter.
    /// </summary>
    internal static class ModalContentCommands
    {
        private static RoutedUICommand __ShowModalContent;
        private static RoutedUICommand __HideModalContent;

        /// <summary>
        /// Gets the value that represents the show modal content command.
        /// </summary>
        public static RoutedUICommand ShowModalContent => 
            __ShowModalContent ??= new RoutedUICommand("Show Modal Content", "ShowModalContent", typeof(ModalContentCommands));

        /// <summary>
        /// Gets the value that represents the hide modal content command.
        /// </summary>
        public static RoutedUICommand HideModalContent => 
            __HideModalContent ??= new RoutedUICommand("Hide Modal Content", "HideModalContent", typeof(ModalContentCommands));
    }
}
