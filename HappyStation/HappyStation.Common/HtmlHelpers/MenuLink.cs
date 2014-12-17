using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HappyStation.Common.HtmlHelpers
{
    public static class MenuHelpers
    {
        public static MvcHtmlString MenuLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName,
            string classes = "")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var isActive = actionName == currentAction && controllerName == currentController;
            var resultClasses = classes + (isActive ? " active" : string.Empty);

            return
                new MvcHtmlString(string.Format("<li class='{0}' >", resultClasses) +
                                  htmlHelper.ActionLink(linkText,
                                      actionName,
                                      controllerName) + "</li>");
        }

        public static MvcHtmlString MenuImageLink(
            this HtmlHelper htmlHelper,
            string imageSrc,
            string actionName,
            string controllerName,
            string classes = "")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var isActive = actionName == currentAction && controllerName == currentController;
            var resultClasses = classes + (isActive ? " active" : string.Empty);

            return
                new MvcHtmlString(string.Format("<li class='{0}' >", resultClasses) +
                                  string.Format("<a href='{0}/{1}'>{2}</a>",
                                      controllerName,
                                      actionName,
                                      "<img src=\"" + imageSrc + "\" />") +
                                  "</li>");
        }
    }
}
