using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebsite.Helpers {
    public class ParsleyValidationAttributesGenerator {
        public const string USE_PARSLEY_KEY = "useparsley";
        public static IDictionary<string, object> GetValidationAttributes(IEnumerable<ModelClientValidationRule> clientRules) {
            if (clientRules == null)
            {
                throw new ArgumentNullException(nameof(clientRules));
            }

            IDictionary<string, object> results = null;

            foreach (var rule in clientRules) {
                if (results == null) results = new Dictionary<string, object>(StringComparer.Ordinal);

                var rulename = "";

                switch (rule.ValidationType) {
                    case "required":
                        rulename = "data-parsley-required";
                        results.Add(rulename, "true");
                        break;
                    case "regex":
                        rulename = "data-parsley-pattern";
                        results.Add(rulename, rule.ValidationParameters["pattern"] ?? string.Empty);
                        break;
                    case "range":
                    case "length":
                        if (rule.ValidationParameters.Count == 2)
                        {
                            rulename = $"data-parsley-{rule.ValidationType}";
                            results.Add(rulename, $"[{rule.ValidationParameters["min"] ?? string.Empty},{rule.ValidationParameters["max"] ?? string.Empty}]");
                        }
                        else if (rule.ValidationParameters.Count == 1)
                        {
                            var p = rule.ValidationParameters.First();
                            results.Add($"{rulename}-{p.Key}length", p.Value ?? string.Empty);
                        }
                        break;
                    case "number":
                        rulename = "data-parsley-type";
                        results.Add(rulename, "number");
                        break;
                    case "equalto":
                        rulename = "data-parsley-equalto";
                        results.Add(rulename, rule.ValidationParameters["other"] ?? string.Empty);
                        break;

                }

                // add the custom message
                results.Add($"{rulename}-message", rule.ErrorMessage ?? string.Empty);

            }

            return results;
        }
    }
}
