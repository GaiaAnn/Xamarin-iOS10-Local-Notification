using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using UserNotifications;
using CoreData;
using System.Threading.Tasks;

namespace test_Newnotification
{
	public class Notification:UNUserNotificationCenterDelegate
	{
		public Notification()
        {
		}

        #region Override Methods
        //Handling Foreground App Notifications YOU NEED TO DELEGATE THIS METHOD
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
		{
			Console.WriteLine("WillPresentNotification");
            completionHandler(UNNotificationPresentationOptions.Alert);
		}

		public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
		{
            // Take action based on Action ID
            switch(response.ActionIdentifier)
			{
				case "reply":
					// Do something
                    Console.WriteLine("Action - reply");
                    var inputresponse = response as UNTextInputNotificationResponse;
                    NSNotificationCenter.DefaultCenter.PostNotificationName("NotificationReply",
                                                this, new NSDictionary("Reply", inputresponse.UserText));
					break;
                case "test":
                    ShowAlert("Hello", "it's Cool", "YES");
                    break;
                    
                default:
                    break;
			}

			// Inform caller it has been handled
			completionHandler();
		}


        #endregion

		public static void Sendlocalnotification()
		{
            //Click Sendnotification button then badgenumber will add
            UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1;
			Console.WriteLine("Sendlocalnotification");

			//If you want to add another picture.Make sure right click your image: Build Action -> BundleResource
            //Warning!!!if you don't add "file:///" it will occur error like invalid attachment file URL......
            //URL detail : https://developer.apple.com/reference/foundation/nsurl#//apple_ref/occ/clm/NSURL/fileURLWithPath:isDirectory:
			var localURL = "file:///" + NSBundle.MainBundle.PathForResource("RUN", "gif");

			NSUrl url = NSUrl.FromString(localURL) ;


			var attachmentID = "gif";
			var options = new UNNotificationAttachmentOptions();
			NSError error;
			var attachment = UNNotificationAttachment.FromIdentifier(attachmentID, url, options,out error);


			var content = new UNMutableNotificationContent();
			content.Attachments = new UNNotificationAttachment[] { attachment };
			content.Title = "Good Morning ~";
			content.Subtitle = "Pull this notification ";
			content.Body = "reply some message-BY Ann";
			content.CategoryIdentifier = "message";

			var trigger1 = UNTimeIntervalNotificationTrigger.CreateTrigger(0.1, false);

			var requestID = "messageRequest";
			var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger1);

			UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
			 {
				 if (err != null)
				 {
					 Console.Write("Notification Error");
				 }
			 });

		}

		public static void CreateAction_Category()
        {
            var Actions = new List<UNNotificationAction>();
            var actionID = string.Empty;
            var title = string.Empty;

            var actionID1 = "reply";
            var title1 = "Reply";

            var actionID2 = "test";
            var title2 = "Test";

            var textInputButtonTitle = "GO";
            var textInputPlaceholder = "Enter comment here...";
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    actionID = actionID1;
                    title = title1; 
                    var action_input = UNTextInputNotificationAction.FromIdentifier(actionID, title, UNNotificationActionOptions.None, textInputButtonTitle, textInputPlaceholder);
                    Actions.Add(action_input);
                }
                else
                {
                    actionID = actionID2;
                    title = title2;
                    var action = UNNotificationAction.FromIdentifier(actionID, title, UNNotificationActionOptions.None);
                    Actions.Add(action);
                }
            }

            // Create category
            var categoryID = "message";
            var actions = Actions.ToArray(); 
            var intentIDs = new string[] { };
            var category = UNNotificationCategory.FromIdentifier(categoryID, actions, intentIDs, UNNotificationCategoryOptions.None);

            // Register category
            var categories = new UNNotificationCategory[] { category };
            UNUserNotificationCenter.Current.SetNotificationCategories(new NSSet<UNNotificationCategory>(categories));
        
        }

        public static Task<int> ShowAlert(string title, string message, params string[] buttons)
        {
            var tcs = new TaskCompletionSource<int>();
            var alert = new UIAlertView
            {
                Title = title,
                Message = message
            };
            foreach (var button in buttons)
                alert.AddButton(button);
            alert.Clicked += (s, e) => tcs.TrySetResult((int)e.ButtonIndex);

            alert.Show();
            return tcs.Task;
        }
	}
}
