﻿using Joyride.Platforms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Tests.Android.Native.SampleApp.ApiDemo.ModalDialogs
{
    public class LoremIpsumModalDialog : ApiDemoModalDialog
    {
        [FindsBy(How = How.Id, Using = "android:id/alertTitle")]
        private IWebElement AlertTitle;

        [FindsBy(How = How.XPath, Using = "//android.widget.Button[@text='OK']")] 
        private IWebElement Ok;

        [FindsBy(How = How.XPath, Using = "//android.widget.Button[@text='Cancel']")] 
        private IWebElement Cancel;

        public override bool IsOnScreen(int timoutSecs)
        {
            var foundTitle = IsPresent("Alert Title", timoutSecs);
            return foundTitle && AlertTitle.Text.StartsWith("Lorem ipsum dolor sit aie consectetur");
        }

        public LoremIpsumModalDialog()
        {
            TransitionMap.Add("Ok", NoTransition);
            TransitionMap.Add("Cancel", NoTransition);

        }
        public override string Name
        {
            get { return "Lorem Ipsum"; }
        }

        public override string Body
        {
            get { return null; }
        }

        public override string Title
        {
            get { return AlertTitle.Text; }
        }

        public override Screen Accept()
        {
            Ok.Click();
            return TransitionFromResponse("Ok");
        }

        public override Screen Dismiss()
        {
            Cancel.Click();
            return TransitionFromResponse("Ok");
        }

        public override Screen RespondWith(string response)
        {
            switch (response)
            {
                case "Ok":
                    return Accept();
                case "Cancel":
                    return Dismiss();
                default:
                    throw new UndefinedResponseException("Undefined response for modal dialog with: " + response);
            }
        }
    }
}
