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
            string controllerName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var isActive = actionName == currentAction && controllerName == currentController;

            return
                new MvcHtmlString(string.Format("<li {0} >", isActive ? "class='active'" : string.Empty) +
                                  htmlHelper.ActionLink(linkText, actionName, controllerName) + "</li>");
        }
    }
}
