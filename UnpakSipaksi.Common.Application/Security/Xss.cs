﻿using FluentValidation.Validators;
using System.Text.RegularExpressions;
using System.Web;

namespace UnpakSipaksi.Common.Application.Security
{
    public class Xss
    {
        private static readonly string[] BlackListTags =
        {
            "script", "iframe", "object", "embed", "form", "input", "button", "select", "textarea",
            "style", "link", "meta", "base", "noscript", "svg", "canvas", "audio", "video", "applet",
            "math", "frameset", "frame", "noframes", "head", "body", "html", "title", "marquee",
            "bgsound", "blink", "iframe", "frame", "style"
        };

        private static readonly string[] BlackListAttributes =
        {
            "onload", "onclick", "onerror", "onfocus", "onblur", "onchange", "onmouseover", "onmouseout",
            "onkeydown", "onkeyup", "onkeypress", "oninput", "onsubmit", "onabort", "onresize", "onselect",
            "onactivate", "ondeactivate", "onbeforeactivate", "onbeforedeactivate", "onafteractivate",
            "onafterdeactivate", "href", "src", "background", "style", "data", "formaction", "action"
        };

        private static readonly string[] BlackListCssProperties =
        {
            "expression", "behaviour", "url", "filter", "progid", "import", "font-face", "keyframes"
        };

        private static readonly string[] BlackListProtocols =
        {
            "javascript", "data", "vbscript", "file", "ftp", "mailto"
        };

        private static readonly string[] BlackListCommands =
        {
            "echo", "ls", "cat", "rm", "mv", "cp", "chmod", "chown", "wget", "curl", "bash", "sh", "eval"
        };

        private static readonly string[] BlackListPhpTags =
        {
            "<\\?php.*?\\?>", // Matches PHP code (<?php ... ?>)
            "<\\?=.*?\\?>"     // Matches short PHP tags (<? ... ?>)
        };

        private static readonly string[] BlackListMySqlQueries =
        {
            "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "CREATE", "ALTER", "TRUNCATE", "OR", "AND", "UNION", "DESC", "--", "#", "/*", "*/"
        };

        private static readonly string[] BlackListAspTags =
        {
            "<%.*?%>" // Matches ASP code (<% ... %>)
        };
        public enum SanitizerType
        {
            CLEAR,
            EMPTY,
            BlackListTags,
            BlackListAttributes,
            BlackListCssProperties,
            BlackListProtocols,
            BlackListCommands,
            BlackListPhpTags,
            BlackListMySqlQueries,
            BlackListAspTags,
            BlackListJavascriptTags,
            BlackListLink,
        }

        public static string Sanitize(string input)
        {
            // Remove blacklisted HTML tags
            foreach (var tag in BlackListTags)
            {
                var tagRegex = new Regex($"<\\/?\\s*{tag}\\s*[^>]*>", RegexOptions.IgnoreCase);
                input = tagRegex.Replace(input, string.Empty);
            }

            // Remove blacklisted attributes from tags
            foreach (var attr in BlackListAttributes)
            {
                var attrRegex = new Regex($"{attr}\\s*=\\s*['\"].*?['\"]", RegexOptions.IgnoreCase);
                input = attrRegex.Replace(input, string.Empty);
            }

            // Remove blacklisted CSS properties
            var styleRegex = new Regex(@"<style.?>.?</style>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            input = styleRegex.Replace(input, match =>
            {
                var styleContent = match.Value;
                foreach (var cssProp in BlackListCssProperties)
                {
                    var cssPropRegex = new Regex($@"\b{cssProp}\b\s*[:;]", RegexOptions.IgnoreCase);
                    styleContent = cssPropRegex.Replace(styleContent, string.Empty);
                }
                return styleContent;
            });

            // Remove blacklisted protocols (e.g., javascript, data, etc.)
            foreach (var protocol in BlackListProtocols)
            {
                var protocolRegex = new Regex($@"\b({protocol})\b:", RegexOptions.IgnoreCase);
                input = protocolRegex.Replace(input, string.Empty);
            }

            // Remove PHP code
            foreach (var phpTag in BlackListPhpTags)
            {
                var phpTagRegex = new Regex(phpTag, RegexOptions.IgnoreCase);
                input = phpTagRegex.Replace(input, string.Empty);
            }

            // Remove SQL queries (SELECT, INSERT, etc.)
            foreach (var query in BlackListMySqlQueries)
            {
                var sqlRegex = new Regex($@"\b{query}\b", RegexOptions.IgnoreCase);
                input = sqlRegex.Replace(input, string.Empty);
            }

            // Remove shell/bash commands (echo, ls, rm, etc.)
            foreach (var command in BlackListCommands)
            {
                var commandRegex = new Regex($@"\b{command}\b", RegexOptions.IgnoreCase);
                input = commandRegex.Replace(input, string.Empty);
            }

            // Remove ASP code
            foreach (var aspTag in BlackListAspTags)
            {
                var aspTagRegex = new Regex(aspTag, RegexOptions.IgnoreCase);
                input = aspTagRegex.Replace(input, string.Empty);
            }

            // Remove dangerous links (javascript, data, etc.)
            var jsLinkRegex = new Regex(@"href\s*=\s*['""]javascript:[^'""]*['""]", RegexOptions.IgnoreCase);
            input = jsLinkRegex.Replace(input, string.Empty);

            // Remove plain http and https links
            var plainLinkRegex = new Regex(@"(http|https):\/\/[^\s<>]+", RegexOptions.IgnoreCase);
            input = plainLinkRegex.Replace(input, string.Empty);

            // Encode any remaining HTML
            return HttpUtility.HtmlEncode(input);
        }

        public static SanitizerType Check(string input)
        {
            /*if (string.IsNullOrEmpty(input))
                return SanitizerType.EMPTY;*/

            // Remove blacklisted HTML tags
            foreach (var tag in BlackListTags)
            {
                if (Regex.IsMatch(input, $"<\\/?\\s*{tag}\\s*[^>]*>", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListTags;
            }

            // Remove blacklisted attributes from tags
            foreach (var attr in BlackListAttributes)
            {
                if (Regex.IsMatch(input, $"{attr}\\s*=\\s*['\"].*?['\"]", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListAttributes;
            }

            // Remove blacklisted CSS properties
            foreach (var cssProp in BlackListCssProperties)
            {
                if (Regex.IsMatch(input, $@"\b{cssProp}\b\s*[:;]", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListCssProperties;
            }

            // Remove blacklisted protocols (e.g., javascript, data, etc.)
            foreach (var protocol in BlackListProtocols)
            {
                if (Regex.IsMatch(input, $@"\b({protocol})\b:", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListProtocols;
            }

            // Remove PHP code
            foreach (var phpTag in BlackListPhpTags)
            {
                if (Regex.IsMatch(input, phpTag, RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListPhpTags;
            }

            // Remove SQL queries (SELECT, INSERT, etc.)
            foreach (var query in BlackListMySqlQueries)
            {
                if (Regex.IsMatch(input, $@"\b{query}\b", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListMySqlQueries;
            }

            // Remove shell/bash commands (echo, ls, rm, etc.)
            foreach (var command in BlackListCommands)
            {
                if (Regex.IsMatch(input, $@"\b{command}\b", RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListCommands;
            }

            // Remove ASP code
            foreach (var aspTag in BlackListAspTags)
            {
                if (Regex.IsMatch(input, aspTag, RegexOptions.IgnoreCase))
                    return SanitizerType.BlackListAspTags;
            }

            // Remove dangerous links (javascript, data, etc.)
            var jsLinkRegex = new Regex(@"href\s*=\s*['""]javascript:[^'""]*['""]", RegexOptions.IgnoreCase);
            if (jsLinkRegex.IsMatch(input))
            {
                return SanitizerType.BlackListJavascriptTags;
            }

            // Remove plain http and https links
            var plainLinkRegex = new Regex(@"(http|https):\/\/[^\s<>]+", RegexOptions.IgnoreCase);
            if (plainLinkRegex.IsMatch(input))
            {
                return SanitizerType.BlackListLink;
            }

            // Encode any remaining HTML
            return SanitizerType.CLEAR;
        }
    }
}
