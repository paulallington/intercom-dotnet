using System;
using Intercom.Core;

namespace Intercom.Data
{
    public class SentMessage : Model
    {
        public class From
        {
            public String id { set; get; }
            public String type { private set; get; }

            public From(String id)
            {
                if (String.IsNullOrEmpty(id))
                    throw new ArgumentNullException(nameof(id));

                this.id = id;
                this.type = Message.MessageFromOrToType.ADMIN;
            }
        }

        public class To
        {
            public String id { set; get; }
            public String type { private set; get; }

            public To(String id)
            {
                if (String.IsNullOrEmpty(id))
                    throw new ArgumentNullException(nameof(id));

                this.id = id;
                this.type = Message.MessageFromOrToType.USER;
            }
        }


        public static class MessageType
        {
            public const String IN_APP = "inapp";
            public const String EMAIL = "email";
        }

        public static class MessageTemplate
        {
            public const String PLAIN = "plain";
            public const String PERSONAL = "personal";
        }

        public static class MessageFromOrToType
        {
            public const String USER = "user";
            public const String ADMIN = "admin";
            public const String CONTACT = "contact";
        }

        public virtual string subject { get; set; }

        public virtual string body { get; set; }

        public virtual string message_type { get; set; }

        public virtual string template { get; set; }
        public virtual From from { get; set; }
        public virtual To to { get; set; }

        public SentMessage()
        {
        }
    }
}