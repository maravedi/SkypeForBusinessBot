using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LyncBot.Core
{
    public static class Responses
    {
        public static List<string> WelcomeGreetingsResponse(string name)
        {
            var greetings = new List<string> { "Thanks for contacting the IT ServiceDesk. How may I help you" };
            return AppendNameQuestion(greetings, name);
        }
        public static List<string> GoodMorningGreetingsResponse(string name)
        {
            var greetings = GetGreeting();
            return AppendName(greetings, name);
        }

        public static List<string> PasswordResetResponse(string name)
        {
            var greetings = ResetPassword();
            return AppendName(greetings, name);
        }

        public static List<string> UnlockAccountResponse(string name, string text)
        {
            //var greetings = AccountUnlock(text);
            //return AppendName(greetings, name);
            return new List<string> {
                name
            };
        }

        public static List<string> CallResponse()
        {
            return new List<string>
            {
                "I am little busy right now. Can we talk after an hour?",
                "In a meeting",
                "Busy now. Can we talk later?"
            };
        }

        private static List<string> GetGreeting()
        {
            var afternoon = 12;
            var evening = 16;
            if (DateTime.Now.Hour < afternoon)
                return new List<string> { "Good Morning", "gm", "vgm" };
            else if (DateTime.Now.Hour < evening)
                return new List<string> { "Good Afternoon" };
            else
                return new List<string> { "Good Evening" };
        }

        private static List<string> ResetPassword()
        {
            return new List<string>
            {
                "One moment. Let me try resetting your password",
                "Resetting your password. Please wait",
                "Okay. I'll reset your password"
            };
        }

        private static List<string> AccountUnlock(string text)
        {
            if(Regex.IsMatch(text, "cpr"))
            {
                return new List<string>
                {
                 "I'll unlock your CPR+ account"
                };
            }
            else if (Regex.IsMatch(text, "mckesson"))
            {
                return new List<string>
                {
                 "I'll unlock your McKesson account"
                };
            }
            else
            {
                return new List<string>
                {
                    "I don't recognize that application: " + text
                };
            }

        }

        private static List<string> AppendName(List<string> responses, string name)
        {
            if (!string.IsNullOrEmpty(name))
                responses = responses.Concat(responses.Select(t => t + ", " + name + ".")).ToList();
            else
                responses = responses.Concat(responses.Select(t => t + ".")).ToList();
            return responses;
        }
        private static List<string> AppendNameQuestion(List<string> responses, string name)
        {
            if (!string.IsNullOrEmpty(name))
                responses = responses.Concat(responses.Select(t => t + ", " + name + "?")).ToList();
            else
                responses = responses.Concat(responses.Select(t => t + "?")).ToList();
            return responses;
        }

    }

}
