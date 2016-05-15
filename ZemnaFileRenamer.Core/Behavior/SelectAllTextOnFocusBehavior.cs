using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ZemnaFileRenamer.Behavior
{
    public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObject_GotKeyboardFocus;
            AssociatedObject.GotMouseCapture += AssociatedObject_GotMouseCapture;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= AssociatedObject_GotKeyboardFocus;
            AssociatedObject.GotMouseCapture -= AssociatedObject_GotMouseCapture;
        }

        void AssociatedObject_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AssociatedObject.SelectAll();
        }

        void AssociatedObject_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }
    }
}
