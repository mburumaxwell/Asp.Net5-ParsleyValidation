using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.WebEncoders;
using System.Collections.Generic;

namespace SampleWebsite.Helpers {
    public class CustomHtmlGenerator : DefaultHtmlGenerator {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHtmlGenerator"/> class.
        /// </summary>
        /// <param name="antiforgery">The <see cref="IAntiforgery"/> instance which is used to generate anti-forgery
        /// tokens.</param>
        /// <param name="optionsAccessor">The accessor for <see cref="MvcOptions"/>.</param>
        /// <param name="metadataProvider">The <see cref="IModelMetadataProvider"/>.</param>
        /// <param name="urlHelper">The <see cref="IUrlHelper"/>.</param>
        /// <param name="htmlEncoder">The <see cref="IHtmlEncoder"/>.</param>
        public CustomHtmlGenerator(IAntiforgery antiforgery, IOptions<MvcViewOptions> optionsAccessor, IModelMetadataProvider metadataProvider, IUrlHelper urlHelper, IHtmlEncoder htmlEncoder) : base(antiforgery, optionsAccessor, metadataProvider, urlHelper, htmlEncoder) { }

        // Only render attributes if client-side validation is enabled, and then only if we've
        // never rendered validation for a field with this name in this form.
        protected override IDictionary<string, object> GetValidationAttributes(ViewContext viewContext, ModelExplorer modelExplorer, string expression) {
            var formContext = viewContext.ClientValidationEnabled ? viewContext.FormContext : null;
            if (formContext == null) {
                return null;
            }

            var fullName = GetFullHtmlFieldName(viewContext, expression);
            if (formContext.RenderedField(fullName)) {
                return null;
            }

            formContext.RenderedField(fullName, true);

            var clientRules = GetClientValidationRules(viewContext, modelExplorer, expression);

            // use inbuilt Unobtrusive validation when the parsley attribute is not used
            if (viewContext.ViewData.ContainsKey(ParsleyValidationAttributesGenerator.USE_PARSLEY_KEY)
                && (bool)viewContext.ViewData[ParsleyValidationAttributesGenerator.USE_PARSLEY_KEY] == true)
                return ParsleyValidationAttributesGenerator.GetValidationAttributes(clientRules);
            else
                return UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules);
        }

        internal static string GetFullHtmlFieldName(ViewContext viewContext, string expression)
        {
            var fullName = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            return fullName;
        }
    }
}
