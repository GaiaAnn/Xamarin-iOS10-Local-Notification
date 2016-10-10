// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace test_Newnotification
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btn_Notification { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txt_reply { get; set; }

        [Action ("btn_Notification_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btn_Notification_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btn_Notification != null) {
                btn_Notification.Dispose ();
                btn_Notification = null;
            }

            if (txt_reply != null) {
                txt_reply.Dispose ();
                txt_reply = null;
            }
        }
    }
}