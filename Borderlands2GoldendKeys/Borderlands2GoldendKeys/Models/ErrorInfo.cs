using System;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys.Models
{
    public class ErrorInfo : HandleErrorInfo
    {
        public bool Is404 { get; set; }
        public string ErrorPath { get; set; }
        public string ImagePath { get; set; }
        public string ImageLink { get; set; }
        public string ImageAuthorName { get; set; }
        public string ImageAuthorLink { get; set; }

        public ErrorInfo() : base(new System.Exception(), "Error", "404")
        {

        }

        public ErrorInfo(System.Exception exception, string controllerName, string actionName)
            : base(exception, controllerName, actionName)
        {

        }
    }
}