using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using SampleWebsite.Helpers;

namespace SampleWebsite.Attributes  {
    public class UseParsleyValidationAttribute : ActionFilterAttribute {
        public override void OnActionExecuted(ActionExecutedContext context) {
            base.OnActionExecuted(context);
            var controller = context.Controller as Controller;
            if (controller != null) {
                controller.ViewData[ParsleyValidationAttributesGenerator.USE_PARSLEY_KEY] = true;
            }
        }
    }
}
