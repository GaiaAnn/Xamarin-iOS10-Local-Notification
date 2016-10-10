using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using UserNotifications;

namespace test_Newnotification
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{

		}
		public static UNUserNotificationCenter center = UNUserNotificationCenter.Current;
        public static ViewController modify_VC;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            NSNotificationCenter.DefaultCenter
                                .AddObserver((NSString)"NotificationReply", NotificationReply);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

        partial void btn_Notification_TouchUpInside(UIButton sender)
        {
            center.Delegate = new Notification();
            Notification.CreateAction_Category();
            Notification.Sendlocalnotification();
        }


        /// <summary>
        /// Set reply content in textbox
        /// </summary>
        /// <param name="notification">Notification.</param>
        public void NotificationReply(NSNotification notification)
		{
            var replycontent = notification.UserInfo
                .ValueForKey((NSString)"Reply");
            
             Console.WriteLine("Press Reply");
             Console.WriteLine(replycontent);

            txt_reply.Text = replycontent.ToString();

		}


	}
}
