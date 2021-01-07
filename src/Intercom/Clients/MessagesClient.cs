using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Factories;
using Newtonsoft.Json;

namespace Intercom.Clients
{
    public class MessageClient : Client
    {
        private const String NOTES_RESOURCE = "messages";

        public MessageClient(RestClientFactory restClientFactory)
            : base(NOTES_RESOURCE, restClientFactory)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use SentMessagesClient(RestClientFactory restClientFactory)")]
        public MessageClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, NOTES_RESOURCE, authentication)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use SentMessagesClient(RestClientFactory restClientFactory)")]
        public MessageClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, NOTES_RESOURCE, authentication)
        {
        }

        public SentMessage Create(SentMessage note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            ClientResponse<SentMessage> result = null;

            String b = JsonConvert.SerializeObject(note,
                Formatting.None, 
                new JsonSerializerSettings
                { 
                    NullValueHandling = NullValueHandling.Ignore
                });

            result = Post<SentMessage>(b);
            return result.Result;
        }
    }
}