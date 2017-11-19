namespace Keyboard.View.Behaviour
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// Represend key event behaviour. Suitable for InputDidings in order to path pressed key
    /// </summary>
    public class KeyUpWithArgsBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty KeyUpCommandProperty =
            DependencyProperty.Register("KeyUpCommand", typeof(ICommand), typeof(KeyUpWithArgsBehavior), new UIPropertyMetadata(null));

        public ICommand KeyUpCommand
        {
            get { return (ICommand)GetValue(KeyUpCommandProperty); }
            set { SetValue(KeyUpCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUpCommand != null)
            {
                KeyUpCommand.Execute(e.Key);
            }
        }
    }
}