using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;


namespace Intercom.Data
{
    public class AdminConversationMessage : Message
	{

        public class From { 
			public String id { set; get; }
			public String type { private set; get; }

			public From(String id)
			{
				if(String.IsNullOrEmpty(id))
					throw new ArgumentNullException (nameof(id));
				
				this.id = id;
                this.type = Message.MessageFromOrToType.ADMIN;
			}
		}

		public class To { 
			public String id { set; get; }
			public String email { set; get; }
			public String user_id { set; get; }
			public String type { set; get; }

            public To(String type = Message.MessageFromOrToType.USER,  String id = null, String email = null, String user_id = null)
			{
				if(String.IsNullOrEmpty(type))
					throw new ArgumentNullException (nameof(type));

				if(String.IsNullOrEmpty(id) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(user_id))
					throw new ArgumentException ("you need to provide either 'id', 'user_id', 'email' to view a user.");

                if(type != Message.MessageFromOrToType.USER && type != Message.MessageFromOrToType.CONTACT)
					throw new ArgumentException ("'type' vale must be either 'contact' or 'user'.");

				this.id = id;
				this.email = email;
				this.user_id = user_id;
				this.type = type;
			}
		}

		public string message_type { get; set; }
		public string subject { get; set; }
		public string template { get; set; }
		public From from { get; set; }
		public To to { get; set; }

        public AdminConversationMessage (
            AdminConversationMessage.From from, 
            AdminConversationMessage.To to,
            String message_type = Message.MessageType.EMAIL,
            String template = Message.MessageTemplate.PLAIN,
			String subject = "", 
			String body = "")
		{
			this.to = to;
			this.from = from;
			this.message_type = message_type;
			this.template = template;
			this.subject = subject;
			this.body = body;
		}
	}

    public class CreateConversation
    {


        public class From : Model
        {
            public String email { set; get; }
            public String user_id { set; get; }

            public From(String type = Message.MessageFromOrToType.USER, String id = null, String email = null, String user_id = null, User user = null)
            {
                //Validate type of message
                if (String.IsNullOrEmpty(type))
                    throw new ArgumentNullException(nameof(type));

                //Validate required fields related to User
                if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(user_id))
                    throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");

                //Validate User required fields
                if (user != null && String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.email) && String.IsNullOrEmpty(user.external_id))
                    throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");

                //Validate required types
                if (type != Message.MessageFromOrToType.USER && type != Message.MessageFromOrToType.CONTACT)
                    throw new ArgumentException("'type' value must be either 'contact' or 'user'.");

                if (user != null)
                {
                    this.id = user.id;
                    this.email = user.email;
                    this.user_id = user.external_id;
                }
                else
                {
                    this.id = id;
                    this.email = email;
                    this.user_id = user_id;
                }

                this.type = type;
            }
        }

        public string body { get; set; }
        public From from { get; set; }

        public CreateConversation(
            CreateConversation.From from,
            String body = "")
        {
            this.from = from;
            this.body = body;
        }
    }

    public class NewConversation
    {
        public string type { get; set; }
        public string id { get; set; }
        public long created_at { get; set; }
        public string body { get; set; }
        public string message_type { get; set; }
        public string conversation_id { get; set; }
    }

    public class ConversationRead
    {
        public bool read { get; set; }

        public ConversationRead()
        {
            read = true;
        }
    }
}
