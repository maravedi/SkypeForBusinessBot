using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncBot.Core.Dialogs
{
    [LuisModel("3a91f550-4ed6-4628-adad-84f008eb9bfc", "9fad47d8a341461fbf07b0dfdf25c945")]
    [Serializable]
    public class LyncLuisDialog : LuisDialog<object>
    {
        private PresenceService _presenceService;

        public LyncLuisDialog(PresenceService presenceService)
        {
            _presenceService = presenceService;
        }
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            // Dont do anything. Pretend I am busy.
            context.Wait(MessageReceived);
        }

        [LuisIntent("WelcomeGreetings")]
        public async Task WelcomeGreetings(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            var activity = await message;
            string name = GetName(activity.From);
            await context.PostOnlyOnceAsync(Responses.WelcomeGreetingsResponse(name), nameof(WelcomeGreetings));
            context.Wait(MessageReceived);
        }

        [LuisIntent("PasswordReset")]
        public async Task PasswordReset(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            //string name = string.Empty;
            //if (!context.PrivateConversationData.ContainsKey(nameof(WelcomeGreetings)))
            //{
            var activity = await message;
            string name = GetName(activity.From);
            //}
            await context.PostOnlyOnceAsync(Responses.PasswordResetResponse(name), nameof(PasswordReset));
            context.Wait(MessageReceived);
        }

        [LuisIntent("UnlockAccount")]
        public async Task UnlockAccount(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            //string name = string.Empty;
            //if (!context.PrivateConversationData.ContainsKey(nameof(WelcomeGreetings)))
            //{
            var activity = await message;
            string name = activity.From.Id;
            string text = activity.Text;
            //}
            await context.PostOnlyOnceAsync(Responses.UnlockAccountResponse(name, text), nameof(UnlockAccount));
            context.Wait(MessageReceived);
        }

        [LuisIntent("GoodMorningGreetings")]
        public async Task GoodMorningGreetings(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            string name = string.Empty;
            if (!context.PrivateConversationData.ContainsKey(nameof(WelcomeGreetings)))
            {
                var activity = await message;
                name = GetName(activity.From);
            }
            await context.PostOnlyOnceAsync(Responses.GoodMorningGreetingsResponse(name), nameof(GoodMorningGreetings));
            context.Wait(MessageReceived);
        }

        [LuisIntent("Call")]
        public async Task Call(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            await context.PostOnlyOnceAsync(Responses.CallResponse(), nameof(Call));
            _presenceService.SetPresenceBusy();
            context.Wait(MessageReceived);
        }

        private static string GetName(ChannelAccount from)
        {
            string name = string.Empty;
            if (string.IsNullOrEmpty(from.Name))
                return name;

            var res = from.Name.Split(' ');
            foreach (var item in res)
            {
                if (item.Length > 1)
                {
                    name = item;
                    break;
                }
            }
            return name;
        }

    }

}
