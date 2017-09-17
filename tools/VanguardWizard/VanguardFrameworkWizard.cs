using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace VanguardWizard
{
    /// <summary>
    /// The Vanguard Framework wizard.
    /// </summary>
    public class VanguardFrameworkWizard: IWizard
    {
        /// <inheritdoc />
        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            try
            {
                AddCommandName(replacementsDictionary);
                AddQueryName(replacementsDictionary);
                AddEventName(replacementsDictionary);
                AddUserFriendlyName(replacementsDictionary);
                AddUserFriendlyCommandName(replacementsDictionary);
                AddUserFriendlyQueryName(replacementsDictionary);
                AddUserFriendlyEventName(replacementsDictionary);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <inheritdoc />
        public void ProjectFinishedGenerating(Project project)
        {
            // Do nothing.
        }

        /// <inheritdoc />
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // Do nothing.
        }

        /// <inheritdoc />
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        /// <inheritdoc />
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            // Do nothing.
        }

        /// <inheritdoc />
        public void RunFinished()
        {
            // Do nothing.
        }
        
        internal void AddCommandName(Dictionary<string, string> replacementsDictionary)
        {
            var value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Command");
            replacementsDictionary.Add("$commandname$", value);
        }

        internal void AddQueryName(Dictionary<string, string> replacementsDictionary)
        {
            var value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Query");
            replacementsDictionary.Add("$queryname$", value);
        }

        internal void AddEventName(Dictionary<string, string> replacementsDictionary)
        {
            var value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Event");
            replacementsDictionary.Add("$eventname$", value);
        }

        internal static string GetStrippedName(string value, string endsWith)
        {
            string endsWithHandler = endsWith + "Handler";
            if (value.EndsWith(endsWith, StringComparison.OrdinalIgnoreCase))
            {
                int length = value.Length - endsWith.Length;
                return value.Substring(0, length);
            }
            else if (value.EndsWith(endsWithHandler, StringComparison.OrdinalIgnoreCase))
            {
                int length = value.Length - endsWithHandler.Length;
                return value.Substring(0, length);
            }

            return value;
        }

        internal static string GetUserFriendlyName(string value)
        {
            // Adds spaces before capitals.
            value = Regex.Replace(value, @"([a-z])([A-Z])", "$1 $2");
            return value.ToLower();
        }

        internal static void AddUserFriendlyName(Dictionary<string, string> replacementsDictionary)
        {
            string value = GetUserFriendlyName(replacementsDictionary["$safeitemname$"]);
            replacementsDictionary.Add("$userfriendlyname$", value);
        }
        private void AddUserFriendlyCommandName(Dictionary<string, string> replacementsDictionary)
        {
            string value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Command");
            value = GetUserFriendlyName(value);
            replacementsDictionary.Add("$userfriendlycommandname$", value);
        }

        private void AddUserFriendlyQueryName(Dictionary<string, string> replacementsDictionary)
        {
            string value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Query");
            value = GetUserFriendlyName(value);
            replacementsDictionary.Add("$userfriendlyqueryname$", value);
        }

        private void AddUserFriendlyEventName(Dictionary<string, string> replacementsDictionary)
        {
            string value = GetStrippedName(replacementsDictionary["$safeitemname$"], "Event");
            value = GetUserFriendlyName(value);
            replacementsDictionary.Add("$userfriendlyeventname$", value);
        }
    }
}
