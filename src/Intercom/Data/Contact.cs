using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Intercom.Converters.AttributeConverters;

namespace Intercom.Data
{
    public class Contact
    {
        public string type { get; set; }
        public string id { get; set; }
        public string workspace_id { get; set; }
        public string external_id { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public int? owner_id { get; set; }
        public SocialProfiles social_profiles { get; set; }
        public bool has_hard_bounced { get; set; }
        public bool marked_email_as_spam { get; set; }
        public bool unsubscribed_from_emails { get; set; }
        public long created_at { get; set; }
        public long? updated_at { get; set; }
        public long? signed_up_at { get; set; }
        public long? last_seen_at { get; set; }
        public long? last_replied_at { get; set; }
        public long? last_contacted_at { get; set; }
        public long? last_email_opened_at { get; set; }
        public long? last_email_clicked_at { get; set; }
        public object language_override { get; set; }
        public string browser { get; set; }
        public string browser_version { get; set; }
        public string browser_language { get; set; }
        public string os { get; set; }
        public LocationData location { get; set; }
        public object android_app_name { get; set; }
        public object android_app_version { get; set; }
        public object android_device { get; set; }
        public object android_os_version { get; set; }
        public object android_sdk_version { get; set; }
        public object android_last_seen_at { get; set; }
        public object ios_app_name { get; set; }
        public object ios_app_version { get; set; }
        public object ios_device { get; set; }
        public object ios_os_version { get; set; }
        public object ios_sdk_version { get; set; }
        public object ios_last_seen_at { get; set; }
        public Dictionary<string, object> custom_attributes { get; set; }
        public Tags tags { get; set; }
        public Notes notes { get; set; }
        public Companies companies { get; set; }

        public Contact()
        {
        }
    }

    public class ContactUpdate
    {
        public string external_id { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public long? signed_up_at { get; set; }
        public long? last_seen_at { get; set; }
        public Dictionary<string, object> custom_attributes { get; set; }

        public ContactUpdate()
        {
        }
    }
}